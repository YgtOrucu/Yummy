using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Yummy.WebUI.Dtos.MessageDto;
using Yummy.WebUI.Dtos.OpenAIDto;

namespace Yummy.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

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
                mimeMessage.From.Add(new MailboxAddress("Admin", "orucuyigit@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress("User", ReceiverEmail));
                mimeMessage.Subject = "Yummy Ekibi Mesaj Yanıtınız";
                var bodybuilder = new BodyBuilder
                {
                    TextBody = FinalResponse
                };
                mimeMessage.Body = bodybuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("orucuyigit@gmail.com", mailKey);
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

    }
}
