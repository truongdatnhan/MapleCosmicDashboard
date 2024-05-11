using FluentValidation;

namespace Web.Models.Profile
{
    public class ProfileVM
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
        public int Gender { get; set; }
    }

    public class ProfileVMValidator : AbstractValidator<ProfileVM>
    {
        public ProfileVMValidator()
        {
            RuleFor(request => request.Email).EmailAddress();
            RuleFor(request => request.Birthday).NotNull().NotEmpty();
            RuleFor(request => request.Gender).NotNull().InclusiveBetween(0, 1);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ProfileVM>.CreateWithOptions((ProfileVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
