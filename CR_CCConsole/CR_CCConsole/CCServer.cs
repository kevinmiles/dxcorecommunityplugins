using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace CR_CCConsole
{
    public class CCServer
    {
        private readonly Uri _url;
        private readonly string _serverName;

        public CCServer(string serverName)
        {
            _serverName = serverName;
            var builder = new UriBuilder("http",serverName);
            builder.Path = "CCNET/XmlStatusReport.aspx";
            _url = builder.Uri;
        }

        public IEnumerable<CCProject> GetProjects()
        {
            XElement xmlStatus = null;
            using (var client = new WebClient())
            {
                var xml = client.DownloadString(_url);
                xmlStatus = XElement.Parse(xml);
            }
            if (xmlStatus == null)
                throw new InvalidOperationException("Unable to retrieve project information from CruiseControl.Net server");
            return GetProjects(xmlStatus).OrderBy(p => p.Name);
        }

        internal IEnumerable<CCProject> GetProjects(XElement xmlStatus)
        {
            return from project in xmlStatus.Descendants("Project")
                   select new CCProject 
                   { 
                       Name = project.Attribute("name").Value,
                       LastBuildStatus = project.Attribute("lastBuildStatus").Value == "Success" ? CCBuildStatus.Success : (project.Attribute("lastBuildStatus").Value == "Failure" || project.Attribute("lastBuildStatus").Value == "Exception" ? CCBuildStatus.Failure : CCBuildStatus.Unknown),
                       LastBuildTime = DateTime.Parse(project.Attribute("lastBuildTime").Value),
                       NextBuildTime = DateTime.Parse(project.Attribute("nextBuildTime").Value),
                       Activity = project.Attribute("activity").Value == "Building" ? CCActivity.Building : CCActivity.Sleeping
                   };
        }
    }

    public class CCProject
    {
        public string Name { get; set; }
        public CCBuildStatus LastBuildStatus { get; set; }
        public DateTime LastBuildTime { get; set; }
        public DateTime NextBuildTime { get; set; }
        public CCActivity Activity { get; set; }
    }

    public enum CCBuildStatus
    {
        Unknown = 0,
        Success = 1,
        Failure = 2,
        Exception = 2
    }

    public enum CCActivity
    {
        Sleeping,
        Building
    }
}
