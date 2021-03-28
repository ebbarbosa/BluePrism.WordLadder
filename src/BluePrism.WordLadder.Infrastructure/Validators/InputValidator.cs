using System;
using System.IO;
using BluePrism.WordLadder.Domain.Models;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using FluentValidation.Results;

namespace BluePrism.WordLadder.Infrastructure.Validators
{

    /// <summary>
    /// Implementation that validates the program input arguments and publicizes a continuation action and an error handler action to the caller.
    /// </summary>
    public class InputValidator : IInputValidator
    {
        private readonly IFileWrapper _fileWrapper;
        private ValidationResult _result;

        public InputValidator(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        /// <summary>
        /// This method validates the input args for the program and if they are valid, delegates the execution the the caller.
        /// </summary>
        /// <param name="inputArgs">The arguments being validated.</param>
        /// <param name="executeProgram">Action to be executed after validations pass.</param>
        /// <returns></returns>
        public IInputValidator Validate(Options inputArgs, Action<Options> executeProgram)
        {
            try
            {
                var validator = new OptionsValidator(_fileWrapper);
                var results = validator.Validate(inputArgs);

                if (!results.IsValid)
                {
                    _result = results;
                    return this;
                }

                var argsResultValidated = TransformOptions(inputArgs);

                executeProgram(argsResultValidated);
            }
            catch (Exception ex)
            {
                _result = new ValidationResult(new[] { new ValidationFailure(Constants.AppName, ex.Message) });
            }

            return this;
        }

        private static Options TransformOptions(Options inputArgs)
        {
            var startWord = inputArgs.StartWord.ToUpper();
            var endWord = inputArgs.EndWord.ToUpper();
            var wordDictionaryFilePathValidated = Path.GetFullPath(inputArgs.WordDictionaryFilePath);
            var wordLadderResultFilePathValidated = Path.GetFullPath(inputArgs.WordLadderResultFilePath);
            var argsResultValidated = new Options(startWord, endWord, wordDictionaryFilePathValidated,
                wordLadderResultFilePathValidated);
            return argsResultValidated;
        }

        /// <summary>
        /// Handles validation errors by sending them to the <paramref name="catchAction"/> delegate.
        /// </summary>
        /// <param name="catchAction">Action to handle the errors captured by the validator.</param>
        public void HandleErrors(Action<string> catchAction)
        {
            if (_result == null) return;
            foreach (var failure in _result.Errors)
            {
                catchAction(failure.ErrorMessage);
            }
        }
    }
}
