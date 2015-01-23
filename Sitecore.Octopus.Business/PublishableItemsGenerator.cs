using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Octopus.Business.Domain;
using Sitecore.Update.Interfaces;

namespace Sitecore.Octopus.Business
{
    public class PublishableItemsGenerator
    {
        public List<ItemToPublish> FindItemsToPublishFromCommands(IEnumerable<ICommand> commands)
        {
            var itemsToPublish = commands.Where(x => x.Parent != string.Empty).Select(command => new ItemToPublish() { Id = new Guid(command.Parent), Path = command.EntityPath }).ToList();
            return itemsToPublish;
        }
    }
}
