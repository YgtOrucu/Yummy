using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Yummy.WebUI.Dtos.MessageDto;
using Yummy.WebUI.Dtos.OpenAIDto;

namespace Yummy.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public string UserEmail => User.FindFirstValue(ClaimTypes.Email);

        public MessageController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> MessageList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Messages");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultMessageDto>>();
                    return View(values);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }
        public async Task<IActionResult> MessageDelete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responsemessage = await client.DeleteAsync("Messages?id=" + id);

                if (responsemessage.IsSuccessStatusCode)
                    return RedirectToAction("MessageList");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AnswerMessageWithOpenAI(int id)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Messages/GetMessageById?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<GetMessageByIdDto>();
                if (TempData["AIResponse"] != null)
                {
                    ViewBag.answerAI = TempData["AIResponse"].ToString();
                }
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendFinalMail(string FinalResponse, string ReceiverEmail, int MessageId)
        {
            try
            {
                string mailKey = _configuration["MailKitKey"];
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("Admin", $"{UserEmail}"));
                mimeMessage.To.Add(new MailboxAddress("User", ReceiverEmail));
                mimeMessage.Subject = "Yummy Ekibi Mesaj Yanıtınız";
                var bodybuilder = new BodyBuilder
                {
                    TextBody = FinalResponse
                };
                mimeMessage.Body = bodybuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync($"{UserEmail}", mailKey);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);

                bool status = await UpdateReadReceipt(MessageId);

                if (status)
                {
                    return RedirectToAction("MessageList");
                }
                else
                {
                    ModelState.AddModelError("", "Mail gönderildi ancak mesaj durumu güncellenirken bir hata oluştu.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private async Task<bool> UpdateReadReceipt(int messageId)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Messages/GetMessageById?id=" + messageId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateMessageDto>();
                value.IsRead = true;

                var updateResponse = await client.PutAsJsonAsync("Messages", value);
                return updateResponse.IsSuccessStatusCode;
            }
            return false;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateAIDraft(int MessageId, string CustomerMessage, string CustomerName, string CustomerSubject)
        {
            var Apıkey = "";
            var client = _httpClientFactory.CreateClient("OpenAIClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Apıkey);

            var RequestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content ="Sen Yummy Restaurant'ın profesyonel ve nazik Müşteri İlişkileri Uzmanısın. Görevin, müşterilerden gelen mesajlara markanın samimi, kaliteli ve çözüm odaklı vizyonunu yansıtacak şekilde yanıt taslakları oluşturmaktır. Yanıtların her zaman 'Sayın [Müşteri Adı],' ile başlamalı ve 'Yummy Restaurant Ekibi' olarak sonlanmalıdır. Dilin davetkar ve iştah açıcı olmalı"
                    },
                    new
                    {
                        role = "user",
                        content = $"Müşteri: {CustomerName}, Konu: {CustomerSubject}, Mesaj: {CustomerMessage}. Bu mesaja uygun bir mail yanıtı yazar mısın?"
                    }
                },
                temperature = 0.4
            };

            var json = JsonConvert.SerializeObject(RequestBody);
            var StringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("chat/completions", StringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responsevalue = await responseMessage.Content.ReadFromJsonAsync<OpenAIResponse>();
                var value = responsevalue.Choice[0].Message.Content;
                TempData["AIResponse"] = value;
                return RedirectToAction("AnswerMessageWithOpenAI", new { id = MessageId });
            }
            else
            {
                ViewBag.AnswerAI = $"Mesaj oluşturuluklen bir hata ile karşılaşıldı : {responseMessage.StatusCode}";
            }
            return RedirectToAction("AnswerMessageWithOpenAI", new { id = MessageId });
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            try
            {
                var apiKey = "";

                var translatedText = await TranslateMessageAsync(createMessageDto.MessageContent, apiKey);
                if (string.IsNullOrEmpty(translatedText)) throw new Exception("Çeviri boş döndü");

                var moderationResult = await ModerateMessageAsync(translatedText, apiKey);
                createMessageDto.Status = GetModerationStatus(moderationResult);
                createMessageDto.MessageDate = DateTime.Now;
                var client1 = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client1.PostAsJsonAsync("Messages", createMessageDto);

                if (responseMessage.IsSuccessStatusCode) return RedirectToAction("YummyHomePage", "YummyHomePage");

                ViewBag.Error = "Mesaj kaydedilemedi";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        public async Task<string> TranslateMessageAsync(string message, string apiKey)
        {
            var client = _httpClientFactory.CreateClient("OpenAIClient");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = $"Sen profesyonel bir çevirmensin. Sadece kullanıcıdan gelen mesajı İngilizce diline çevir, giriş veya çıkış cümleleri kurma.Uzun metinleri çevirirken paragraf yapısını bozma ve akıcılığı koru."
                    },
                    new
                    {
                        role = "user",
                        content = message
                    }
                },
                temperature = 0.2
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("chat/completions", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var requestvalue = await responseMessage.Content.ReadFromJsonAsync<OpenAIResponse>();
                var value = requestvalue.Choice[0].Message.Content;     
                return value;
            }
            return new string("HATA");
        }

        public async Task<ModerationResult> ModerateMessageAsync(string message, string apiKey)
        {
            var client = _httpClientFactory.CreateClient("OpenAIClient");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var request = new { model = "omni-moderation-latest", input = message };
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("moderations", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var moderationResponse = System.Text.Json.JsonSerializer.Deserialize<ModerationResponse>(
                responseString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return moderationResponse?.Results?.FirstOrDefault();
        }

        public string GetModerationStatus(ModerationResult result, double warningThreshold = 0.4, double toxicThreshold = 0.7)
        {
            if (result == null) return "Moderation sonucu alınamadı";

            var toxicLabels = result.Category_Scores
                .Where(x => x.Value >= toxicThreshold)
                .Select(x => x.Key)
                .ToList();

            var warningLabels = result.Category_Scores
                .Where(x => x.Value >= warningThreshold && x.Value < toxicThreshold)
                .Select(x => x.Key)
                .ToList();

            if (toxicLabels.Any() && warningLabels.Any())
            {
                return $"Toxic mesaj. Kategoriler: {string.Join(", ", toxicLabels)}\n⚠️ Riskli Kategoriler: {string.Join(", ", warningLabels)}";
            }
            else if (toxicLabels.Any())
            {
                return $"Toxic mesaj. Kategoriler: {string.Join(", ", toxicLabels)}";
            }
            else if (warningLabels.Any())
            {
                return $"Riskli mesaj. Kategoriler: {string.Join(", ", warningLabels)}";
            }
            else
            {
                return "Mesaj temiz";
            }
        }

    }
}
