namespace Sitecore.Octopus.Business
{
    public class ArgumentProcessor
    {
        public ArgumentProcessor(string[] args)
        {
            SerilizationFolder = args[0];
            PackageDestinationFolder = args[1];
            CurrentCommitId = args[2];
            CurrentBuildId = args[3];
        }

        public ArgumentProcessor()
        {
            
        }

        public string PackageDestinationFolder { get; set; }
        public string SerilizationFolder { get; set; }
        public string CurrentCommitId { get; set; }
        public string CurrentBuildId { get; set; }
    }
}