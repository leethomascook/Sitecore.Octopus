using System.Collections.Generic;
using Sitecore.Update.Interfaces;

namespace Sitecore.Octopus.Business.Contracts
{
    public interface ISitecoreSerilizationDiffGenerator
    {
        List<ICommand> GetDiffCommands(string sourcePath, string targetPath);
    }
}