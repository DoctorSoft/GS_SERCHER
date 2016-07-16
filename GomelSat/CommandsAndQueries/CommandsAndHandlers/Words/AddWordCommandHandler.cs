using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataBase.Models.WordModels;

namespace CommandsAndQueries.CommandsAndHandlers.Words
{
    public class AddWordCommandHandler : IVoidCommandHandler<AddWordCommand>
    {
        private readonly DataBaseContext context;

        public AddWordCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public VoidCommandResponse Handle(AddWordCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Word))
            {
                return new VoidCommandResponse();
            }

            var thereIsWord = context
                .WordModels
                .Any(model => model.Word.ToUpper() == command.Word.ToUpper());

            if (thereIsWord)
            {
                return new VoidCommandResponse();
            }

            var wordModel = new WordDataBaseModel
            {
                Id = 0,
                Word = command.Word
            };

            context.WordModels.Add(wordModel);
            context.SaveChanges();

            return new VoidCommandResponse();
        }
    }
}
