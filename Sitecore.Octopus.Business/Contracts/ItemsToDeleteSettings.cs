using System.Collections.Generic;

namespace Sitecore.Octopus.Business.Contracts
{
    public interface IItemsToDeleteSettings
    {
        List<string> PathsToRemove { get; }
    }
}
