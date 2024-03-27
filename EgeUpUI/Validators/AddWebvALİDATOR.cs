using EgeUpUI.Dtos.AddWebsiteDto;
using FluentValidation;

namespace EgeUpUI.Validators
{
    public class AddWebvALİDATOR:AbstractValidator<AddWebDto>
    {
        public AddWebvALİDATOR()
        {
            RuleFor(x=>x.WebsiteName).NotEmpty().WithMessage("Lütfen Bu alın Doldurun");
            RuleFor(x=>x.WebsiteName).MaximumLength(30).WithMessage("Lütfen Bu alın Doldurun");
        }
    }
}
