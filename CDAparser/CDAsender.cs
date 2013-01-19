using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml;

namespace CDAparser
{
    public class CDAsender
    {
        static string dateTimeFormat = "yyyyMMdd";
        static EducationDataContext education = new EducationDataContext();
        static string educationEvaluationXml = HttpContext.Current.Server.MapPath(@"XML/01_CDA_Education_Evaluation.xml");
        static string educationPlanningXml = HttpContext.Current.Server.MapPath(@"XML/02_CDA_Education_Planning.xml");
        static string educationServiceProvisionXml = HttpContext.Current.Server.MapPath(@"XML/03_CDA_Education_ServiceProvision.xml");
        static string educationDevelopmentXml = HttpContext.Current.Server.MapPath(@"XML/04_CDA_Education_Development.xml");

        public static XmlDocument getEducationEvaluationXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initEducationXML(educationEvaluationXml, cid, planNo));
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationPlanningXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initEducationXML(educationPlanningXml, cid, planNo));
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationServiceProvisionXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initEducationXML(educationServiceProvisionXml, cid, planNo));
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationDevelopmentXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initEducationXML(educationDevelopmentXml, cid, planNo));
            // create component

            return parser.getDocument();
        }

        public static XmlDocument initEducationXML(string path, string cid, string planNo)
        {
            CDAparser parser = new CDAparser(path);
            string time = DateTime.Now.ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            Person person = (from a in education.Persons select a).First();
            var gender = (from a in education.HL7_Gender_Standards where a.GenderCode.Equals(person.Gender) select a).First();

            parser.setCDAeffectiveTime(time);
            parser.setCDArecordTargetPatientRoleIdExtension(cid);
            parser.setCDArecordTargetPatientRolePatientNameGiven(person.FirstName);
            parser.setCDArecordTargetPatientRolePatientNameFamily(person.LastName);
            parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCode(person.Gender);
            parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName(gender.GenderEnglishDescription);
            parser.setCDArecordTargetPatientRolePatientBirthTime(((DateTime)person.DOB).ToString(dateTimeFormat, CultureInfo.InvariantCulture));
            parser.setCDAauthorTimeValue(time);
            var staff = (from a in education.Evaluations where a.CID.Equals(cid) select a).First();
            parser.setCDAauthorAssignedAuthorIdExtension(staff.StaffID);
            var staffName = (from a in education.Employees where a.Staff_id.Equals(staff.StaffID) select a).First();
            parser.setCDAauthorAssignedAuthorAssignedPersonNameGiven(staffName.Firstname);
            parser.setCDAauthorAssignedAuthorAssignedPersonNameFamily(staffName.Lastname);
            //parser.setCDAauthorAssignedAuthorHostIDExtension(); รหัส ศุนย์การศึกษา

            //parser.setCDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdExtension();
            //parser.setCDAcustodianAssignedCustodianRepresentedCustodianOrganizationName();

            parser.setCDAlegalAuthenticatorTimeValue(time);
            //parser.setCDAlegalAuthenticatorAssignedEntityIdExtension();
            //parser.setCDAlegalAuthenticatorAssignedEntityIdRoot();
            //parser.setCDAlegalAuthenticatorAssignedEntityAssignedPersonNameGiven();
            //parser.setCDAlegalAuthenticatorAssignedEntityAssignedPersonNameFamily();
            //parser.setCDAlegalAuthenticatorAssignedEntityHostIDExtension();

            parser.setCDAauthenticatorTimeValue(time);
            //parser.setCDAauthenticatorAssignedEntityIdExtension();
            //parser.setCDAauthenticatorAssignedEntityIdRoot();
            //parser.setCDAauthenticatorAssignedEntityAssignedPersonNameGiven();
            //parser.setCDAauthenticatorAssignedEntityAssignedPersonNameFamily();
            //parser.setCDAauthenticatorAssignedEntityHostIDExtension();

            return parser.getDocument();
        }

    }
}