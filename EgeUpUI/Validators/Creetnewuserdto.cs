using EgeUpUI.Dtos.RegisterDto;
using FluentValidation;

namespace EgeUpUI.Validators
{
    public class Creetnewuserdto:AbstractValidator<CreateNewUSerDto>
    {
        public Creetnewuserdto()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş geçilemez");
           
            RuleFor(x => x.ReferanceCode).NotEmpty().WithMessage("Referans kodu boş geçilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre  boş geçilemez");
            RuleFor(x => x.Mail).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta adresi girin");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır");
        }
    }
}
