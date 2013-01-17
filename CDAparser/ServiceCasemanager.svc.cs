using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;

namespace CDAparser
{
    public class ServiceCasemanager : IServiceCasemanager
    {
        static BbCasemanagerDataContext casemanager = new BbCasemanagerDataContext();

        public string DoWork()
        { 
            return "Hello";
        }

        public XmlElement getCasemanagerPlan()
        {
            
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string xmlFile = Path.Combine(appPath, @"XML/17_CDA_CaseManager_Planning.xml");
            CDAparser parser = new CDAparser(xmlFile);

            // Ex.
            //Person data = (from a in casemanager.Persons select a).First();
            //parser.setCDAcode(data.CID.Trim());
            //parser.setCDAcustodianTypeCode(data.CID.Trim());

            return parser.getDocument().DocumentElement;
        }

    }
}
