using DataaccsessLayer.Table;
using EgeUpUI.Dtos.AddWebsiteDto;
using FluentValidation;

namespace EgeUpUI.Validators
{
    public class WebsiteUrlValidator:AbstractValidator<AddWebDto>
    {

        public WebsiteUrlValidator()
        {
            RuleFor(x => x.WebsiteName).NotNull().WithMessage("Website Adı Boş Bıraklımaz");
        }
    }
}
