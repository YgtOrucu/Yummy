namespace Yummy.WebAPI.Dtos.DashboardDto
{
    public class ResultStatisticalDataDto
    {
        public int TotalReservation { get; set; }
        public int NewReservationCount { get; set; }
        public int TotalCustomer { get; set; }
        public int TotalProduct { get; set; }
        public int ChefCount { get; set; }
        public int CategoryCount { get; set; }
        public int EventCount { get; set; }
        public int TestimonialCount { get; set; }
        public int GalleryCount { get; set; }
        public int AllMessageCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public int IsReadTrueCount { get; set; }
    }
}
