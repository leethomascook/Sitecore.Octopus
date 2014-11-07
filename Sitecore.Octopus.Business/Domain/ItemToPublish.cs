using System;

namespace Sitecore.Octopus.Business.Domain
{
    [Serializable]
    public class ItemToPublish
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
    }
}
