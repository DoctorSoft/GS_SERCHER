using System.Collections.Generic;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataBase.Models.WordModels;

namespace CommandsAndQueries.CommandsAndHandlers.Words
{
    public class RemoveWordListCommandHandler : IVoidCommandHandler<RemoveWordListCommand>
    {
        private readonly DataBaseContext context;

        public RemoveWordListCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public VoidCommandResponse Handle(RemoveWordListCommand command)
        {
            var wordsToDelete = command.WordList.Select(word => context.WordModels.FirstOrDefault(model => model.Word.ToUpper() == word.ToUpper())).ToList();
            
            context.WordModels.RemoveRange(wordsToDelete);
            context.SaveChanges();

            return new VoidCommandResponse();
        }
    }
}
