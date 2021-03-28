using FluentValidation;

namespace BluePrism.WordLadder.Infrastructure.Validators
{
    /// <summary>
    /// Validates if the a file path has .txt extension and is a valid path within the File System.
    /// </summary>
    public class IsPathValidator : AbstractValidator<string>
    {
        public IsPathValidator(IFileWrapper fileWrapper)
        {
            RuleFor(p => p).Must(fileWrapper.HasTxtExtension).WithMessage(fn =>
                $"The provided file path {fn} is not valid. Please provide a valid file path with a .txt extension for the answer file.");
            RuleFor(p => p).Must(fileWrapper.IsValidPath).WithMessage(fn =>
                $"The provided file path {fn} is not valid. Please provide a valid file path for the answer file.");
        }
    }
}