using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace CDAparser
{
    public class ServiceEducation : IServiceEducation
    {
        /* string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string xmlFile = Path.Combine(appPath, @"XML/01_CDA_Education_Evaluation.xml");
            CDAparser parser = new CDAparser(xmlFile);
        */
        //static CasemanagerDataContext casemanager = new CasemanagerDataContext();

        public XmlElement getEducationEvaluation(string cid, string planNo)
        {
            return CDAsender.getEducationEvaluationXML(cid, planNo).DocumentElement;
        }

    }
}
