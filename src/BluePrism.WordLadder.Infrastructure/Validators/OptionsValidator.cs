using BluePrism.WordLadder.Domain.Models;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using FluentValidation;

namespace BluePrism.WordLadder.Infrastructure.Validators
{
    /// <summary>
    /// After command line parsing, the program arguments are validated within this class rules. It is a fluent Validations abstraction.
    /// </summary>
    public class OptionsValidator : AbstractValidator<Options>
    {
        public OptionsValidator(IFileWrapper fileWrapper)
        {
            RuleFor(options => options.StartWord).Length(Constants.LimitedWordLength);
            RuleFor(options => options.EndWord).Length(Constants.LimitedWordLength);
            RuleFor(options => options.WordDictionaryFilePath).SetValidator(new FileExistsValidator(fileWrapper));
            RuleFor(options => options.WordLadderResultFilePath).SetValidator(new IsPathValidator(fileWrapper));
        }
    }
}