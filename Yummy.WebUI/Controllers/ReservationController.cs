using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ReservationDto;

namespace Yummy.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ReservationList()
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient("YummyClient"))
                {
                    var responseMessage = await client.GetAsync("Reservations");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultReservationDto>>();
                        return View(values);
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ReservationCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ReservationCreate(CreateReservationDto createReservationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(createReservationDto);

                using (var client = _httpClientFactory.CreateClient("YummyClient"))
                {
                    var responseMessage = await client.PostAsJsonAsync("Reservations", createReservationDto);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ReservationList");
                    }
                    return View(createReservationDto);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> ReservationDelete(int id)
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient("YummyClient"))
                {
                    var ResponseMessage = await client.DeleteAsync("Reservations?id=" + id);
                    if (ResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ReservationList");
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReservationUpdate(int id)
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient("YummyClient"))
                {
                    var responseMessage = await client.GetAsync("Reservations/GetReservationById?id=" + id);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var values = await responseMessage.Content.ReadFromJsonAsync<UpdateReservationDto>();
                        return View(values);
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReservationUpdate(UpdateReservationDto updateReservationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updateReservationDto);
                }
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Reservations", updateReservationDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ReservationList");
                }
                return View(updateReservationDto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            try
            {
                createReservationDto.Status = "Onay Bekliyor";
                if (!ModelState.IsValid)
                    return View(createReservationDto);

                using (var client = _httpClientFactory.CreateClient("YummyClient"))
                {
                    var responseMessage = await client.PostAsJsonAsync("Reservations", createReservationDto);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("YummyHomePage", "YummyHomePage");
                    }
                    return View(createReservationDto);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
