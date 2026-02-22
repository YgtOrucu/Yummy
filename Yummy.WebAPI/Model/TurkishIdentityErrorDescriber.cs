using Microsoft.AspNetCore.Identity;

namespace Yummy.WebAPI.Model
{
    public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
         => new IdentityError
         {
             Code = nameof(PasswordTooShort),
             Description = $"Şifre en az {length} karakter olmalıdır."
         };

        public override IdentityError PasswordRequiresUpper()
            => new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "Şifre en az bir büyük harf içermelidir."
            };

        public override IdentityError PasswordRequiresLower()
            => new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "Şifre en az bir küçük harf içermelidir."
            };

        public override IdentityError PasswordRequiresDigit()
            => new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "Şifre en az bir rakam içermelidir."
            };

        public override IdentityError DuplicateUserName(string userName)
        => new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = $"'{userName}' kullanıcı adı zaten kullanılıyor."
        };

        public override IdentityError DuplicateEmail(string email)
        => new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = $"'{email}' e-posta adresi zaten kayıtlı."
        };

        public override IdentityError InvalidUserName(string userName)
        => new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description =
                $"'{userName}' kullanıcı adı geçersiz." +
                "Türkçe karakter veya özel karakter kullanılamaz."
        };
    }
}
