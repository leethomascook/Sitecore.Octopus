using System.Collections.Generic;
using NDesk.Options;

namespace Sitecore.Octopus.Business
{
    public class ArgumentProcessor
    {
        public ArgumentProcessor(IEnumerable<string> args)
        {
            new OptionSet
            {
                {"s|source=", v => SerializationFolder = v},
                {"o|out|output=", v => PackageDestinationFolder = v},
                {"c|commit=", v => CurrentCommitId = v},
                {"b|build=", v => CurrentBuildId = v},
                {"e|extract=", v => ExtractPath = v},
            }.Parse(args);
        }

        public string PackageDestinationFolder { get; set; }
        public string SerializationFolder { get; set; }
        public string CurrentCommitId { get; set; }
        public string CurrentBuildId { get; set; }
        public string ExtractPath { get; set; }
    }
}