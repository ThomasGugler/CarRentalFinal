namespace CarRental.Models
{
    using FluentValidation;

    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            this.RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName darf nicht leer sein!");
        }
    }
}
