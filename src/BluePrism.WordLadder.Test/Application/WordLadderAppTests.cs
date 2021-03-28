using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluePrism.WordLadder.Application;
using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace BluePrism.WordLadder.Test.Application
{
    public class WordLadderAppTests
    {
        private readonly WordLadderApp _sut;
        private readonly IInputValidator _inputValidator;
        private readonly IWordDictionaryService _wordDictionaryService;
        private readonly IWordLadderSolver _wordLadderSolver;
        private readonly IOpenFileHelper _openFileHelper;
        private readonly IFileWrapper _fileWrapper;

        public WordLadderAppTests()
        {
            _inputValidator = Substitute.For<IInputValidator>();
            _wordDictionaryService = Substitute.For<IWordDictionaryService>();
            _wordLadderSolver = Substitute.For<IWordLadderSolver>();
            _openFileHelper = Substitute.For<IOpenFileHelper>();
            _fileWrapper = Substitute.For<IFileWrapper>();

            _sut = new WordLadderApp(_inputValidator, _wordDictionaryService, _wordLadderSolver, _openFileHelper,
                _fileWrapper);
        }

        [Fact]
        public void Execute_ValidatesInput_InitialisesDictionarires_SolvesWordLadder_WritesAnswerFile_OpensAnswerFile()
        {
            var exceptionCaught = false;

            var startWord = "sure";
            var endWord = "HIRE";
            var wordDictionaryFilePath = ".\\words.txt";
            var wordLadderResultFilePath = "answers.txt";
            var args = new Options(startWord, endWord, wordDictionaryFilePath, wordLadderResultFilePath);

            IDictionary<string, bool> dictionary = new Dictionary<string, bool>()
            {
                {"SAME", false},
                {"TIME", false},
                {"TAME", false},
                {"SOME", false},
                {"MUST", false},
                {"HIRE", false},
                {"SIRE", false},
                {"SITE", false}
            };
            IDictionary<string, ICollection<string>> preProcessedDictionary =
                new Dictionary<string, ICollection<string>>()
                {
                    {"*AME", new[] {"SAME", "TAME"}},
                    {"S*ME", new[] {"SAME", "SOME"}},
                    {"SA*E", new[] {"SAME"}},
                    {"SAM*", new[] {"SAME"}},
                    {"T*ME", new[] {"TAME", "TIME"}},
                    {"TA*E", new[] {"TAME"}},
                    {"TAM*", new[] {"TAME"}},
                    {"TI*E", new[] {"TAME"}},
                    {"TIM*", new[] {"TIME"}},
                    {"*OME", new[] {"SOME"}},
                    {"SO*E", new[] {"SOME"}},
                    {"SOM*", new[] {"SOME"}},
                    {"*IRE", new[] {"SIRE", "HIRE"}},
                    {"S*RE", new[] {"SIRE"}},
                    {"SI*E", new[] {"SIRE", "SITE"}},
                    {"SIR*", new[] {"SIRE"}},
                    {"*ITE", new[] {"SITE"}},
                    {"S*TE", new[] {"SITE"}},
                    {"SIT*", new[] {"SITE"}}
                };
            var wordLadderSolved = new List<string>() {"SURE", "SIRE", "HIRE"};

            _inputValidator.Validate(Arg.Is<Options>(args), Arg.Invoke(args));
            _wordDictionaryService.Initialise(Arg.Is<Options>(args));
            _wordDictionaryService.GetWordDictionary().Returns(dictionary);
            _wordDictionaryService.GetPreprocessedWordsDictionary().Returns(preProcessedDictionary);
            _wordLadderSolver.SolveLadder(Arg.Is(startWord), Arg.Is(endWord), Arg.Is(dictionary),
                    Arg.Is(preProcessedDictionary))
                .Returns(wordLadderSolved);

            // ACT
            _sut.Execute(args, s => { exceptionCaught = true; });

            // ASSERT
            Assert.False(exceptionCaught);
            _inputValidator.Received(1).Validate(Arg.Is<Options>(args), Arg.Invoke(args));

            _wordDictionaryService.Received(1).Initialise(Arg.Is<Options>(args));

            _wordLadderSolver.Received(1).SolveLadder(Arg.Is(startWord),
                Arg.Is(endWord),
                Arg.Is(dictionary),
                Arg.Is(preProcessedDictionary));

            _openFileHelper.Received(1).OpenFile(Arg.Is<string>(a => a.Contains(wordLadderResultFilePath)));

            _fileWrapper.Received(1).Write(Arg.Any<IList<string>>(), Arg.Is(wordLadderResultFilePath));
        }
    }
}