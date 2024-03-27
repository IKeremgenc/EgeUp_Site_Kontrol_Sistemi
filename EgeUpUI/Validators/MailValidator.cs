using DataaccsessLayer.Table;
using EgeUpUI.Dtos.AddWebsiteDto;
using FluentValidation;

namespace EgeUpUI.Validators
{
    public class MailValidator : AbstractValidator<Mail>
    {
        public MailValidator()
        {
           
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Lüfen Bir Mail Giriniz");

        }
    }
}
