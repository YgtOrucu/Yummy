using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.DashboardDto;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly YummyContext _yummyContext;
        private readonly IMapper _mapper;

        public DashboardsController(YummyContext yummyContext, IMapper mapper)
        {
            _yummyContext = yummyContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult StatisticalData()
        {
            var values = new ResultStatisticalDataDto
            {
                TotalReservation = _yummyContext.Reservations.Count(),
                NewReservationCount = _yummyContext.Reservations.Where(x => x.Status == "Onay Bekliyor").Count(),
                TotalCustomer = _yummyContext.Reservations.Sum(x => x.PeopleCount),
                TotalProduct = _yummyContext.Products.Count(),
                ChefCount = _yummyContext.Chefs.Count(),
                CategoryCount = _yummyContext.Categories.Count(),
                EventCount = _yummyContext.YummyEvents.Count(),
                TestimonialCount = _yummyContext.Testimonials.Count(),
                GalleryCount = _yummyContext.Galleries.Count(),
                AllMessageCount = _yummyContext.Messages.Count(),
                IsReadTrueCount = _yummyContext.Messages.Where(x => x.IsRead).Count(),
                UnreadMessageCount = _yummyContext.Messages.Where(x => !x.IsRead).Count()
            };
            return Ok(values);
        }

        [HttpGet("GetDashboardStatistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            var today = DateTime.Today;
            var fourMonthsAgo = today.AddMonths(-3);

            // ADIM 1: Grafik verilerini (Aylık bazda gruplanmış) çek
            var chartDataRaw = await _yummyContext.Reservations
                .Where(r => r.ReservationDate >= fourMonthsAgo)
                .GroupBy(r => new { r.ReservationDate.Year, r.ReservationDate.Month })
                .Select(g => new MonthlyChartData // Bu sınıfı DTO içine eklemelisin
                {
                    Year = g.Key.Year,
                    MonthInt = g.Key.Month,
                    Approved = g.Count(x => x.Status == "Onaylandı"),
                    Pending = g.Count(x => x.Status == "Onay Bekliyor"),
                    Canceled = g.Count(x => x.Status == "İptal Edildi")
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.MonthInt)
                .ToListAsync();

            var statistics = new ResultMainChartDto
            {
                ChartData = chartDataRaw.Select(x => new MonthlyChartData
                {
                    Month = new DateTime(x.Year, x.MonthInt, 1).ToString("MMM yyyy"),
                    Approved = x.Approved,
                    Pending = x.Pending,
                    Canceled = x.Canceled
                }).ToList(),

                TotalApprovedCount = await _yummyContext.Reservations.CountAsync(x => x.Status == "Onaylandı"),
                TotalPendingCount = await _yummyContext.Reservations.CountAsync(x => x.Status == "Onay Bekliyor"),
                TotalCanceledCount = await _yummyContext.Reservations.CountAsync(x => x.Status == "İptal Edildi"),
                TotalCustomerCount = await _yummyContext.Reservations.Select(x => x.Email).Distinct().CountAsync(),
                TotalGuestCount = await _yummyContext.Reservations.SumAsync(x => x.PeopleCount),
                CompletedReservationCount = await _yummyContext.Reservations.CountAsync(x => x.ReservationDate < DateTime.Now && x.Status == "Onaylandı"),
                AverageGuestPerReservation = await _yummyContext.Reservations.AnyAsync() ? await _yummyContext.Reservations.AverageAsync(x => x.PeopleCount) : 0,
                NewCustomerCount = 75
            };

            return Ok(statistics);
        }


        [HttpGet("ResultGetMessage")]
        public async Task<IActionResult> ResultGetMessage()
        {
            var values = await _yummyContext.Messages.Take(4).OrderByDescending(x=>x.MessageDate).ToListAsync();
            return Ok(_mapper.Map<List<ResultGetMessage>>(values));
        }

        [HttpGet("ResultGetChef")]
        public IActionResult ResultGetChef()
        {
            var x = _yummyContext.Chefs.Include(x => x.ChefTasks).ToList();
            return Ok(_mapper.Map<List<ResultGetChef>>(x));
        }
    }
}
