namespace Yummy.WebUI.Dtos.ProfileDto
{
    public class UpdateProfileDto
    {
        public string CurrentUsername { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
