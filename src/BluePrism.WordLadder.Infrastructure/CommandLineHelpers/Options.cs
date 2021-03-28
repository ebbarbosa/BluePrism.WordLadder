using CommandLine;

namespace BluePrism.WordLadder.Infrastructure.CommandLineHelpers
{
    /// <summary>
    /// Class where the program arguments are parsed via CommandLineParser.
    /// </summary>
    public class Options
    {
        public Options(string startWord, string endWord, string wordDictionaryFilePath, string wordLadderResultFilePath)
        {
            StartWord = startWord;
            EndWord = endWord;
            WordDictionaryFilePath = wordDictionaryFilePath;
            WordLadderResultFilePath = wordLadderResultFilePath;
        }

        [Value(0, MetaName = "start word", Required = true, HelpText = "Start word for the word ladder.")]
        public string StartWord { get; }

        [Value(1, MetaName = "end word", Required = true, HelpText = "End word for the word ladder.")]
        public string EndWord { get; }

        [Value(2, MetaName = "word dictionary file", Required = true,
            HelpText =
                "Word Dictionary file name. Please provide a path to an existing .txt file where every line is a single word.")]
        public string WordDictionaryFilePath { get; }

        [Value(3, MetaName = "word ladder output file", Required = true,
            HelpText =
                "Word Ladder output file name. Please provide a path to write the new .txt file containing the word ladder result.")]
        public string WordLadderResultFilePath { get; }
    }
}