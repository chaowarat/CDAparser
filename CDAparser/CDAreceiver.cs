using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace CDAparser
{
    public class CDAreceiver
    {
        static string dateTimeFormat = "yyyyMMdd";
        static EducationDataContext database = new EducationDataContext();
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

        public static void headerXML(XmlDocument doc, bool hasRecordTarget)
        {
            CDAparser parser = new CDAparser(doc);
            Person person = new Person();
            string cid = parser.getCDArecordTargetPatientRoleIdExtension();
            //parser.getCDAeffectiveTime(); not use

            if (hasRecordTarget)
            {
                person.CID = cid;
                person.FirstName = parser.getCDArecordTargetPatientRolePatientNameGiven();
                person.LastName = parser.getCDArecordTargetPatientRolePatientNameFamily();
                person.Gender = parser.getCDArecordTargetPatientRolePatientAdministrativeGenderCode();
                //parser.getCDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName(gender.GenderEnglishDescription); not use
                person.DOB = DateTime.Parse(parser.getCDArecordTargetPatientRolePatientBirthTime());
                
            }
            try
            {
                int count = (from a in database.Persons where a.CID.Equals(cid) select a).Count();
                if (count <= 0)
                {
                    database.Persons.InsertOnSubmit(person);
                    database.SubmitChanges();
                }
            }
            catch{  }
            //parser.getCDAauthorTimeValue(time); not use
            Evaluation staff = new Evaluation();
            Employee staffName = new Employee();
            staff.StaffID = parser.getCDAauthorAssignedAuthorIdExtension();
            staffName.Firstname = parser.getCDAauthorAssignedAuthorAssignedPersonNameGiven();
            staffName.Lastname = parser.getCDAauthorAssignedAuthorAssignedPersonNameFamily();
            //parser.getCDAauthorAssignedAuthorHostIDExtension(); รหัส ศุนย์การศึกษา
            try
            {
                database.Evaluations.InsertOnSubmit(staff);    
            }
            catch { }
            //parser.getCDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdExtension();
            //parser.getCDAcustodianAssignedCustodianRepresentedCustodianOrganizationName();

            //parser.getCDAlegalAuthenticatorTimeValue(time);
            //parser.getCDAlegalAuthenticatorAssignedEntityIdExtension();
            //parser.getCDAlegalAuthenticatorAssignedEntityIdRoot();
            //parser.getCDAlegalAuthenticatorAssignedEntityAssignedPersonNameGiven();
            //parser.getCDAlegalAuthenticatorAssignedEntityAssignedPersonNameFamily();
            //parser.getCDAlegalAuthenticatorAssignedEntityHostIDExtension();

            //parser.getCDAauthenticatorTimeValue(time);
            //parser.getCDAauthenticatorAssignedEntityIdExtension();
            //parser.getCDAauthenticatorAssignedEntityIdRoot();
            //parser.getCDAauthenticatorAssignedEntityAssignedPersonNameGiven();
            //parser.getCDAauthenticatorAssignedEntityAssignedPersonNameFamily();
            //parser.getCDAauthenticatorAssignedEntityHostIDExtension();

        }
    }
}