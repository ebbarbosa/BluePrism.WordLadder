using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
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
        
        public WordLadderAppTests()
        {
            _inputValidator = Substitute.For<IInputValidator>();
            _wordDictionaryService = Substitute.For<IWordDictionaryService>();
            _wordLadderSolver = Substitute.For<IWordLadderSolver>();
            
            _sut = new WordLadderApp(_inputValidator, _wordDictionaryService, _wordLadderSolver);
        }

        [Fact]
        public void GetResult_ValidatesInput_InitialisesDictionarires_SolvesWordLadder_WritesAnswerFile_OpensAnswerFile()
        {
            var exceptionCaught = false;

            var startWord = "sure";
            var endWord = "HIRE";
            var wordDictionaryFilePath = "./words.txt";
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
            var result = _sut.GetResult(args, s => { exceptionCaught = true; });

            // ASSERT
            Assert.False(exceptionCaught);
            _inputValidator.Received(1).Validate(Arg.Is<Options>(args), Arg.Invoke(args));

            _wordDictionaryService.Received(1).Initialise(Arg.Is<Options>(args));

            _wordLadderSolver.Received(1).SolveLadder(Arg.Is(startWord),
                Arg.Is(endWord),
                Arg.Is(dictionary),
                Arg.Is(preProcessedDictionary));

            Assert.Equal(wordLadderSolved, result);
        }
    }
}