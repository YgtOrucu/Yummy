using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebAPI.Dtos.ForProfileInTheAdminPageDto;
using Yummy.WebAPI.Dtos.LoginDto;
using Yummy.WebAPI.Dtos.ProfileDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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
                    return Ok(new
                    {
                        Message = "Giriş Başarılı",
                        Username = user.UserName,
                        NameSurname = user.Name + " " + user.Surname,
                        Email = user.Email
                    });
                }
            }
            return BadRequest("Kullanıcı adı veya şifre hatalı");
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
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, updateProfileDto.Password);
                if (!passwordResult.Succeeded) return BadRequest(passwordResult.Errors);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok("Profil başarıyla güncellendi.");

            return BadRequest(result.Errors);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Oturum başarıyla kapatıldı." });
        }
    }
}
