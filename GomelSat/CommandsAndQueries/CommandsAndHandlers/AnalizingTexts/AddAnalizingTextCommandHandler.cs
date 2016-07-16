using Common.CommandQueryTools;
using DataBase.Contexts;
using DataBase.Models.AnalizingTextModels;

namespace CommandsAndQueries.CommandsAndHandlers.AnalizingTexts
{
    public class AddAnalizingTextCommandHandler : ICommandHandler<AddAnalizingTextCommand, long>
    {
        private readonly DataBaseContext context;

        public AddAnalizingTextCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public long Handle(AddAnalizingTextCommand command)
        {
            var modelToAdd = new AnalizingTextDataBaseModel
            {
                Id = 0,
                HeaderText = command.HeaderText,
                ContentText = command.ContentText
            };

            context.AnalizingTextModels.Add(modelToAdd);
            context.SaveChanges();

            return modelToAdd.Id;
        }
    }
}
