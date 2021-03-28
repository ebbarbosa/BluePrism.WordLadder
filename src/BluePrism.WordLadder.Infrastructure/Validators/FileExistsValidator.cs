using FluentValidation;

namespace BluePrism.WordLadder.Infrastructure.Validators
{
    /// <summary>
    /// Validator to check if a file exists using the IFileWrapper.
    /// </summary>
    public class FileExistsValidator : AbstractValidator<string>
    {
        public FileExistsValidator(IFileWrapper fileWrapper)
        {
            RuleFor(p => p).Must(fileWrapper.FileExists).WithMessage(fn =>
                $"Unable to find the specified file {fn}. Please provide an existing word dictionary file.");
        }
    }
}