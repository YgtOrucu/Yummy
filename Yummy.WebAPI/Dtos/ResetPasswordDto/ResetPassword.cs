namespace Yummy.WebAPI.Dtos.ResetPasswordDto
{
    public class ResetPassword
    {
        public string Email { get; set; }
        public int Code { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
