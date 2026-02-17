namespace Yummy.WebAPI.Dtos.EmployeeTaskDto
{
    public class ResultEmployeeTaskDto
    {
        public int EmployeeTaskId { get; set; }
        public string TaskName { get; set; }
        public int TaskStatus { get; set; } 
        public DateTime AssignDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; } 
        public List<string> ChefImageUrls { get; set; }
    }
}
