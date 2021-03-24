using CommandLine;

namespace BluePrism.WordLadder.Infrastructure.CommandLineHelpers
{
    public class Options
    {

        [Value(0, Required = true, HelpText = "Input word to begin the word ladder.")]
        public string BeginWord { get; set; }

        [Value(1, Required = true, HelpText = "Target word for the word ladder.")]
        public string TargetWord { get; set; }

        [Value(2, Required = true, HelpText = "Word Dictionary file name. Please provide a path to an existing .txt file where every line is a single word.")]
        public string WordDictionaryFilePath { get; set; }

        [Value(3, Required = true, HelpText = "Word Ladder output file name. Please provide a path to write the new .txt file containing the word ladder result.")]
        public string WordLadderResultFilePath { get; set; }
    }
}