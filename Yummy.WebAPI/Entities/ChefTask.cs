namespace Yummy.WebAPI.Entities
{
    public class ChefTask
    {
        public int ChefId { get; set; }
        public Chef Chef { get; set; }

        public int EmployeeTaskId { get; set; }
        public EmployeeTask EmployeeTask { get; set; }
    }
}
