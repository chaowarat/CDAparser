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
    public class SDHS : ISDHS
    {
        //static BbCasemanagerDataContext casemanager = new BbCasemanagerDataContext();

        public XmlElement getSDHSProfile()
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
    }
}
