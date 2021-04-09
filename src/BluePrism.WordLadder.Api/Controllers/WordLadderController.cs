using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BluePrism.WordLadder.Application;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BluePrism.WordLadder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordLadderController : ControllerBase
    {
        IWordLadderApp _wordLadderApp;
        IConfiguration _configuration;

        public WordLadderController(IWordLadderApp wordLadderApp, IConfiguration configuration)
        {
            _wordLadderApp = wordLadderApp;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<string> Get(string startWord, string endWord)
        {
            var result = _wordLadderApp.GetResult(new Options(startWord,
                endWord,
                _configuration["WordDictionary"],
                "./null.txt"),
                err => throw new ApplicationException(err));

            return result;
        }
    }
}
