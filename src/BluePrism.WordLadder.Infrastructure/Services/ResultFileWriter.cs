using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BluePrism.WordLadder.Infrastructure
{
    public class ResultFileWriter : IOutputService
    {
        IFileWrapper _fileWrapper;
        IOpenFileHelper _openFileHelper;

        public ResultFileWriter(IFileWrapper fileWrapper, IOpenFileHelper openFileHelper)
        {
            _fileWrapper = fileWrapper;
            _openFileHelper = openFileHelper;
        }

        public void Execute(IList<string> result, Options options)
        {
            WriteResultToTxtFile(result, options.WordLadderResultFilePath);
        }

        private void WriteResultToTxtFile(IList<string> wordLadder, string filePath)
        {
            if (!wordLadder.Any())
            {
                Console.WriteLine(
                    ":( - Unfortunately, the word ladder returned no results. You may try again with different values.");
                return;
            }

            _fileWrapper.Write(wordLadder, filePath);
            OpenFile(filePath);
        }

        private void OpenFile(string wordLadderResultFilePath)
        {
            _openFileHelper.OpenFile(wordLadderResultFilePath);
        }
    }

}