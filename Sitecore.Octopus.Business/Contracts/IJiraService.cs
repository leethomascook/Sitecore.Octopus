using System;
using System.Collections.Generic;
using Issue = Sitecore.Octopus.Business.Domain.Issue;

namespace Sitecore.Octopus.Business.Contracts
{
    public interface IBugTrackingService
    {
        List<Issue> GetIssuesResolvedSinceDate(DateTime dateTime);
    }
}
