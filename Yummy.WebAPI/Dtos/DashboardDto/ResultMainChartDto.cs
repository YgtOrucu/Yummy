namespace Yummy.WebAPI.Dtos.DashboardDto
{
    public class ResultMainChartDto
    {
        public List<MonthlyChartData> ChartData { get; set; }
        public int TotalApprovedCount { get; set; }
        public int TotalPendingCount { get; set; }
        public int TotalCanceledCount { get; set; }
        public int TotalCustomerCount { get; set; }
        public int CompletedReservationCount { get; set; }
        public int TotalGuestCount { get; set; }
        public double AverageGuestPerReservation { get; set; }
        public int NewCustomerCount { get; set; }
    }

    public class MonthlyChartData
    {
        public int Year { get; set; }
        public int MonthInt { get; set; } 
        public string Month { get; set; } 
        public int Approved { get; set; }
        public int Pending { get; set; }
        public int Canceled { get; set; }
    }
}
