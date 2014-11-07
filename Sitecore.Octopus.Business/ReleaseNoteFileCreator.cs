using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using RazorEngine;
using Sitecore.Octopus.Business.Domain;

namespace Sitecore.Octopus.Business
{
    public class ReleaseNoteFileCreator
    {
        private string RELEASE_NOTES_FILEPATH = "ReleaseNotes.txt";

        public string CreateFile(List<Commit> commits, List<Issue> issues)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Sitecore.Octopus.Business.Templates.ReleaseNotes.cshtml";
            string template = "";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                template = reader.ReadToEnd();
            }

            var model = new ReleaseNotesModel() {Commits = commits, Issues = issues};
            var body = Razor.Parse(template, model);
            File.WriteAllText(RELEASE_NOTES_FILEPATH, body);
            return RELEASE_NOTES_FILEPATH;
        }
    }

    public class ReleaseNotesModel
    {
        public List<Commit> Commits { get; set; }
        public List<Issue> Issues { get; set; }
    }
}