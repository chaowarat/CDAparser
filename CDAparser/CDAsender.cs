using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace CDAparser
{
    public class CDAsender
    {
        static string dateTimeFormat = "yyyyMMdd";
        static EducationDataContext education = new EducationDataContext();
        static string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
        static string educationEvaluationXml = Path.Combine(appPath, @"XML/01_CDA_Education_Evaluation.xml");
        static string educationPlanningXml = Path.Combine(appPath, @"XML/02_CDA_Education_Planning.xml");
        static string educationServiceProvisionXml = Path.Combine(appPath, @"XML/03_CDA_Education_ServiceProvision.xml");
        static string educationDevelopmentXml = Path.Combine(appPath, @"XML/04_CDA_Education_Development.xml");
        static string educationProfiletXml = Path.Combine(appPath, @"XML/05_CDA_Education_Profile.xml");
        static string educationMilkXml = Path.Combine(appPath, @"XML/06_CDA_Education_Milk.xml");

        static string CasemanagerProfileXml = Path.Combine(appPath, @"XML/10_CDA_CaseManager_Profile.xml");
        static string CasemanagerQuestionareXml = Path.Combine(appPath, @"XML/16_CDA_CaseManager_Questionare.xml");
        static string CasemanagerPlanningXml = Path.Combine(appPath, @"XML/17_CDA_CaseManager_Planning.xml");

        #region Education
        public static XmlDocument initXML(string path, string cid, string planNo, bool hasRecordTarget)
        {
            CDAparser parser = new CDAparser(path);
            string time = DateTime.Now.ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            Person person = (from a in education.Persons where a.CID.Equals(cid) select a).First();
            var gender = (from a in education.HL7_Gender_Standards where a.GenderCode.Equals(person.Gender) select a).First();

            parser.setCDAeffectiveTime(time);

            if (hasRecordTarget)
            {
                parser.setCDArecordTargetPatientRoleIdExtension(cid);
                parser.setCDArecordTargetPatientRolePatientNameGiven(person.FirstName);
                parser.setCDArecordTargetPatientRolePatientNameFamily(person.LastName);
                parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCode(person.Gender);
                parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName(gender.GenderEnglishDescription);
                parser.setCDArecordTargetPatientRolePatientBirthTime(((DateTime)person.DOB).ToString(dateTimeFormat, CultureInfo.InvariantCulture));
            }
            parser.setCDAauthorTimeValue(time);
            var staff = (from a in education.Evaluations where a.CID.Equals(cid) select a).First();
            try
            {
                parser.setCDAauthorAssignedAuthorIdExtension(staff.StaffID);
                var staffName = (from a in education.Employees where a.Staff_id.Equals(staff.StaffID) select a).First();

                parser.setCDAauthorAssignedAuthorAssignedPersonNameGiven(staffName.Firstname);
                parser.setCDAauthorAssignedAuthorAssignedPersonNameFamily(staffName.Lastname);
                //parser.setCDAauthorAssignedAuthorHostIDExtension(); รหัส ศุนย์การศึกษา
            }
            catch{}
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

        public static XmlDocument getEducationEvaluationXML(string cid, string planNo)
        {
            XmlDocument doc = initXML(educationEvaluationXml, cid, planNo, true);
            CDAparser parser = new CDAparser(doc);
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationPlanningXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(educationPlanningXml, cid, planNo, true));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationServiceProvisionXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(educationServiceProvisionXml, cid, planNo, true));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationDevelopmentXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(educationDevelopmentXml, cid, planNo, true));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationProfileXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(educationProfiletXml, cid, planNo, false));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getEducationMilkXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(educationMilkXml, cid, planNo, false));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }
        #endregion

        public static XmlDocument getCasemanagerProfileXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(CasemanagerProfileXml, cid, planNo, false));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getCasemanagerQuestionareXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(CasemanagerQuestionareXml, cid, planNo, true));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

        public static XmlDocument getCasemanagerPlanningXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(CasemanagerPlanningXml, cid, planNo, true));
            //parser.setCDAcomponentPlanNumber();
            //parser.setCDAcomponentCaseNumber();
            // create component

            return parser.getDocument();
        }

    }
}