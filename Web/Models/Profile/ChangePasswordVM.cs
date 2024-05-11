using FluentValidation;

namespace Web.Models.Profile
{
    public class ChangePasswordVM
    {
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? NewPassword2 { get; set; }
    }

    public class ChangePasswordVMValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordVMValidator()
        {
            RuleFor(request => request.Password).NotNull().NotEmpty().Length(6,30);
            RuleFor(request => request.NewPassword).NotNull().NotEmpty().Length(6, 30);
            RuleFor(request => request.NewPassword2).Equal(x => x.NewPassword);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ChangePasswordVM>.CreateWithOptions((ChangePasswordVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
