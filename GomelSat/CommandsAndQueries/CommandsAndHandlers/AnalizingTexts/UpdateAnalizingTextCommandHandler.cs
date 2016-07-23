using System.Data.Entity.Migrations;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.CommandsAndHandlers.AnalizingTexts
{
    public class UpdateAnalizingTextCommandHandler : IVoidCommandHandler<UpdateAnalizingTextCommand>
    {
        private readonly DataBaseContext context;

        public UpdateAnalizingTextCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public VoidCommandResponse Handle(UpdateAnalizingTextCommand command)
        {
            SaveSourceLink(command);
            SaveImageLink(command);

            context.AnalizingTextModels.AddOrUpdate(command.AnalizingText);

            context.SaveChanges();

            return new VoidCommandResponse();
        }

        private void SaveSourceLink(UpdateAnalizingTextCommand command)
        {
            if (command.AnalizingText.SourceLink == null)
            {
                return;
            }

            var savedSourceLink = context
                .SourceLinks
                .FirstOrDefault(model => model.Link.ToUpper() == command.AnalizingText.SourceLink.Link.ToUpper());

            if (savedSourceLink != null)
            {
                command.AnalizingText.SourceLinkId = savedSourceLink.Id;
                command.AnalizingText.SourceLink = savedSourceLink;
            }
            else
            {
                var newSourceLink = command.AnalizingText.SourceLink;
                context.SourceLinks.Add(newSourceLink);

                context.SaveChanges();

                command.AnalizingText.SourceLink = newSourceLink;
                command.AnalizingText.SourceLinkId = newSourceLink.Id;
            }
        }

        private void SaveImageLink(UpdateAnalizingTextCommand command)
        {
            if (command.AnalizingText.ImageLink == null)
            {
                return;
            }

            var savedImageLink = context
                .ImageLinks
                .FirstOrDefault(model => model.Link.ToUpper() == command.AnalizingText.ImageLink.Link.ToUpper());

            if (savedImageLink != null)
            {
                command.AnalizingText.ImageLinkId = savedImageLink.Id;
                command.AnalizingText.ImageLink = savedImageLink;
            }
            else
            {
                var newImageLink = command.AnalizingText.ImageLink;
                context.ImageLinks.Add(newImageLink);

                context.SaveChanges();

                command.AnalizingText.ImageLink = newImageLink;
                command.AnalizingText.ImageLinkId = newImageLink.Id;
            }
        }
    }
}
