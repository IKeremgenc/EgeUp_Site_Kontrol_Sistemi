using EgeUpUI.Dtos.AddWebsiteDto;
using EgeUpUI.Dtos.UsereditDto;
using FluentValidation;

namespace EgeUpUI.Validators
{
    public class UserEditValidator : AbstractValidator<Useredit>
    {
        public UserEditValidator()
        {
         
            RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen Bu alanı Doldurun")
                .MinimumLength(5).WithMessage("Lütfen Bu alanı Doldurun");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Lütfen Bu alanı Doldurun")
                .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor");
        }
    }
}
