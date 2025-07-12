using OrbitaChallengerBackEnd.Models;
using FluentValidation;

namespace OrbitaChallengerBackEnd.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail is required..");
            RuleFor(x => x.RA).NotEmpty().WithMessage("RA is required.");
            RuleFor(x => x.CPF)
                  .NotEmpty()
                  .WithMessage("CPF is required.")
                  .Custom((cpf, context) =>
                  {
                      // Remove qualquer caractere não numérico do CPF
                      var cleanedCpf = new string(cpf.Where(char.IsDigit).ToArray());

                      // Substitui o CPF original pelo CPF limpo
                      context.InstanceToValidate.CPF = cleanedCpf;

                      // Valida o comprimento do CPF (depois de limpar os caracteres não numéricos)
                      if (cleanedCpf.Length != 11)
                      {
                          context.AddFailure("The CPF must contain 11 digits.");
                      }

                      // Verifica se o CPF tem apenas números
                      if (!cleanedCpf.All(char.IsDigit))
                      {
                          context.AddFailure("The CPF must contain only numbers.");
                      }
                  });
        }
    }
}
