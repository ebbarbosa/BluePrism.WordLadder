using BluePrism.WordLadder.Application;
using BluePrism.WordLadder.Domain;
using BluePrism.WordLadder.Domain.Business;
using BluePrism.WordLadder.Infrastructure;
using BluePrism.WordLadder.Infrastructure.CommandLineHelpers;
using BluePrism.WordLadder.Infrastructure.FileHelpers;
using BluePrism.WordLadder.Infrastructure.Services;
using BluePrism.WordLadder.Infrastructure.Validators;
using Ninject.Modules;

namespace BluePrism.WordLadder
{

    /// <summary>
    /// This class works is an IoC wrapper - DI implementation of ninject.
    /// All dependencies are here so the other layers can de free from implementations and use the interfaces only.
    /// </summary>
    public class Factory : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileWrapper>().To<FileWrapper>();
            Bind<IOpenFileHelper>().To<OpenFileHelper>();
            Bind<IInputValidator>().To<InputValidator>();
            Bind<ICommandLineWrapper>().To<CommandLineWrapper>();
            Bind<IDictionaryPreprocessService>().To<DictionaryPreprocessService>();
            Bind<IGetSimilarWordsFromProcessedListService>().To<GetSimilarWordsFromProcessedListService>();
            Bind<IWordDictionaryService>().To<WordDictionaryService>();
            Bind<IWordLadderSolver>().To<WordLadderSolver>();
            Bind<IWordLadderApp>().To<WordLadderApp>();
        }
    }
}