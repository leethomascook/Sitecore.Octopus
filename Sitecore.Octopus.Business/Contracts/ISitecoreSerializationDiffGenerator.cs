using System.Collections.Generic;
using Sitecore.Update.Interfaces;

namespace Sitecore.Octopus.Business.Contracts
{
    public interface ISitecoreSerializationDiffGenerator
    {
        List<ICommand> GetDiffCommands(string sourcePath, string targetPath);
    }
}