using CommandLine;

namespace BluePrism.WordLadder.Infrastructure.CommandLineHelpers
{
    public class Options
    {
        public Options(string sourceWord, string targetWord, string wordDictionaryFilePath, string wordLadderResultFilePath)
        {
            SourceWord = sourceWord;
            TargetWord = targetWord;
            WordDictionaryFilePath = wordDictionaryFilePath;
            WordLadderResultFilePath = wordLadderResultFilePath;
        }

        [Value(0, MetaName = "starting word", Required = true, HelpText = "Input word to begin the word ladder.")]
        public string SourceWord { get; }

        [Value(1, MetaName = "target word", Required = true, HelpText = "Target word for the word ladder.")]
        public string TargetWord { get; }

        [Value(2, MetaName = "word dictionary file", Required = true, HelpText = "Word Dictionary file name. Please provide a path to an existing .txt file where every line is a single word.")]
        public string WordDictionaryFilePath { get; }

        [Value(3, MetaName = "word ladder output file", Required = true, HelpText = "Word Ladder output file name. Please provide a path to write the new .txt file containing the word ladder result.")]
        public string WordLadderResultFilePath { get; }
    }
}