namespace Yummy.WebAPI.Dtos.ProfileDto
{
    public class UpdateProfileDto
    {
        public string CurrentUsername { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
