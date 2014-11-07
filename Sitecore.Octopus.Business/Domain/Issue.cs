using System;

namespace Sitecore.Octopus.Business.Domain
{
    public class Issue
    {
        public string Description { get; set; }
        public DateTime DateResolved { get; set; }
        public string IssueNumber { get; set; }
        public string Reporter { get; set; }
    }
}