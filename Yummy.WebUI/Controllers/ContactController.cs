using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ContactDto;

namespace Yummy.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ContactList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Contacts");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultContactDto>>();
                    return View(values);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ContactCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactCreate(CreateContactDto createContactDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(createContactDto);

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responsemessage = await client.PostAsJsonAsync("Contacts", createContactDto);

                if (responsemessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ContactList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(createContactDto);
        }

        public async Task<IActionResult> ContactDelete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responsemessage = await client.DeleteAsync("Contacts?id=" + id);

                if (responsemessage.IsSuccessStatusCode)
                    return RedirectToAction("ContactList");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ContactUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Contacts/GetContactById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<UpdateContactDto>();
                    return View(values);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> ContactUpdate(UpdateContactDto updateContactDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(updateContactDto);

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Contacts", updateContactDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ContactList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(updateContactDto);
        }
    }
}
