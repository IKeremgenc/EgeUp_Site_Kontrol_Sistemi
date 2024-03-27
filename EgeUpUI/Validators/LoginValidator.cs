using DataaccsessLayer.Table;
using EgeUpUI.Dtos.LoginDto;
using FluentValidation;

namespace EgeUpUI.Validators
{
    public class LoginValidator:AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Ad Boş geçilmez");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre Boş geçilemez");



        }
    }
}
