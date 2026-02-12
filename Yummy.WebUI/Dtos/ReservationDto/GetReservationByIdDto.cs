namespace Yummy.WebUI.Dtos.ReservationDto
{
    public class GetReservationByIdDto
    {
        public int ReservationId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime ReservationDate { get; set; }
        public string? ReservationTime { get; set; }
        public int PeopleCount { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
    }
}
