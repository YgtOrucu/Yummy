using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebAPI.Dtos.ForProfileInTheAdminPageDto;
using Yummy.WebAPI.Dtos.LoginDto;
using Yummy.WebAPI.Dtos.ProfileDto;
using Yummy.WebAPI.Dtos.ResetPasswordDto;
using Yummy.WebAPI.Dtos.VerifyCodeDto;
using Yummy.WebAPI.Entities;
using Yummy.WebAPI.Services;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public AuthsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _mailService = mailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<AppUser>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                if (registerDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(registerDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = Path.Combine(resource, "wwwroot/images/LoginUserImage", imageName);
                    using (var stream = new FileStream(saveLocation, FileMode.Create))
                    {
                        await registerDto.ImageFile.CopyToAsync(stream);
                    }
                    user.AvatarUrl = "/images/LoginUserImage/" + imageName;
                }
                await _userManager.UpdateAsync(user);
                return Ok("Kullanıcı Kaydı başarılı bir şekilde oluşturuldu");
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (result.Succeeded)
                {

                    if (!user.EmailConfirmed)
                    {
                        var random = new Random();
                        var confirmcode = random.Next(100000, 999999);
                        user.ConfirmCode = confirmcode;
                        await _userManager.UpdateAsync(user);

                        string subject = "Yummy - Giriş Doğrulama Kodu";
                        string message = $"Merhaba {user.Name}, giriş yapmak için onay kodunuz: {confirmcode}";
                        await _mailService.SendEmailAsync(user.Email, subject, message);

                        return Ok(new
                        {
                            RequiresConfirmation = true,
                            Message = "Doğrulama kodu mail adresinize gönderildi.",
                            Email = user.Email
                        });

                    }
                    else
                    {
                        return Ok(new
                        {
                            RequiresConfirmation = false,
                            Username = user.UserName,
                            Email = user.Email,
                            NameSurname = user.Name + " " + user.Surname
                        });
                    }
                }
            }
            return BadRequest("Kullanıcı adı veya şifre hatalı");
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode(VerifyCode verifycode)
        {
            var user = await _userManager.FindByEmailAsync(verifycode.Email);

            if (user != null && user.ConfirmCode == verifycode.Code)
            {
                user.ConfirmCode = 0;
                user.EmailConfirmed = true;
                user.TwoFactorEnabled = true;
                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Status = true,
                    Username = user.UserName,
                    Email = user.Email,
                    NameSurname = user.Name + " " + user.Surname,
                });
            }

            return BadRequest("Girdiğiniz kod hatalı veya süresi dolmuş.");
        }

        [HttpGet("GetProfileNavbarSection")]
        public async Task<IActionResult> GetProfileNavbarSection(string username)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest("Kullanıcı adı boş olamaz.");

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            return Ok(_mapper.Map<ProfileDto>(user));
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(string username)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest("Kullanıcı adı boş olamaz.");

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            return Ok(_mapper.Map<UpdateProfileDto>(user));
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDto updateProfileDto)
        {
            var user = await _userManager.FindByNameAsync(updateProfileDto.CurrentUsername);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            _mapper.Map(updateProfileDto, user);

            if (updateProfileDto.ImageFile != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(updateProfileDto.ImageFile.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(resource, "wwwroot/images/LoginUserImage", imageName);

                if (!string.IsNullOrEmpty(user.AvatarUrl))
                {
                    var oldImagePath = Path.Combine(
                        resource,
                        "wwwroot",
                        user.AvatarUrl.TrimStart('/')
                    );

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await updateProfileDto.ImageFile.CopyToAsync(stream);
                }
                user.AvatarUrl = "/images/LoginUserImage/" + imageName;
            }

            if (!string.IsNullOrEmpty(updateProfileDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, updateProfileDto.Password);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                return Ok("Profil başarıyla güncellendi.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Bu e-posta adresi ile kayıtlı bir kullanıcı bulunamadı.");
            }

            var random = new Random();
            int resetCode = random.Next(100000, 999999);
            user.ConfirmCode = resetCode;
            await _userManager.UpdateAsync(user);

            string subject = "Yummy - Şifre Sıfırlama Kodu";
            string message = $"Merhaba {user.Name}, şifrenizi sıfırlamak için onay kodunuz: {resetCode}";

            await _mailService.SendEmailAsync(user.Email, subject, message);

            return Ok(new { Message = "Sıfırlama kodu başarıyla gönderildi." });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetDto)
        {
            var user = await _userManager.FindByEmailAsync(resetDto.Email);
            if (user == null || user.ConfirmCode != resetDto.Code)
            {
                return BadRequest("Doğrulama kodu hatalı veya geçersiz.");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetDto.NewPassword);

            if (result.Succeeded)
            {
                user.ConfirmCode = 0;
                await _userManager.UpdateAsync(user);
                return Ok(new { Message = "Şifre başarıyla güncellendi." });
            }

            return BadRequest("Şifre güncellenirken bir hata oluştu.");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Oturum başarıyla kapatıldı." });
        }
    }
}
