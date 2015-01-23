namespace Sitecore.Octopus.Business
{
    public class ArgumentProcessor
    {
        public ArgumentProcessor(string[] args)
        {
            SerializationFolder = args[0];
            PackageDestinationFolder = args[1];
            CurrentCommitId = args[2];
            CurrentBuildId = args[3];
        }

        public ArgumentProcessor()
        {

        }

        public string PackageDestinationFolder { get; set; }
        public string SerializationFolder { get; set; }
        public string CurrentCommitId { get; set; }
        public string CurrentBuildId { get; set; }
    }
}