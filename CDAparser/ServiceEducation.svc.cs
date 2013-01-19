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
        //static CasemanagerDataContext casemanager = new CasemanagerDataContext();
        static EducationDataContext education = new EducationDataContext();

        public XmlElement getEducationEvaluation()
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string xmlFile = Path.Combine(appPath, @"XML/01_CDA_Education_Evaluation.xml");
            CDAparser parser = new CDAparser(xmlFile);

            string timeNow = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            parser.setCDAeffectiveTime(timeNow);
            //parser.setCDArecordTargetPatientRoleIdExtension(); บัตรประชาชน CID
            //parser.getCDArecordTargetPatientRolePatientNameGiven();
            //parser.getCDArecordTargetPatientRolePatientNameFamily();
            //parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCode();
            //parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName();
            //parser.setCDArecordTargetPatientRolePatientBirthTime();

            parser.setCDAauthorTimeValue(timeNow);

            return parser.getDocument().DocumentElement;
        }

        public void setEducationEvaluation(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            CDAparser parser = new CDAparser(doc);
            

        }
    }
}
