using System.Collections.Generic;

namespace Sitecore.Octopus.Business
{
    public class ArgumentProcessor
    {
        public ArgumentProcessor(IList<string> args)
        {
            SerializationFolder = args[0];
            PackageDestinationFolder = args[1];
            CurrentCommitId = args[2];
            CurrentBuildId = args[3];

            if (args.Count > 4)
            {
                OutputPath = args[4];
            }
        }

        public string PackageDestinationFolder { get; set; }
        public string SerializationFolder { get; set; }
        public string CurrentCommitId { get; set; }
        public string CurrentBuildId { get; set; }
        public string OutputPath { get; set; }
    }
}