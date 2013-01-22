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
        /// <summary>
        /// getEducationDevelopmentXML -- observationEffectiveTimeCenterValue
        /// รอพี่รงค์ แก้ดาต้าเบส
        /// </summary>
        
        static EducationDataContext education = new EducationDataContext();
        static CDAparserCS.CasemanagerDataContext casemanager = new CDAparserCS.CasemanagerDataContext();

        static string dateTimeFormat = "yyyyMMdd";
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
            Person person = null;
            HL7_Gender_Standard gender = null;
            Evaluation staff = null;

            parser.setCDAeffectiveTime(time);
            parser.setCDAauthorTimeValue(time);

            if (cid != null)
            {
                try
                {
                    person = (from a in education.Persons where a.CID.Equals(cid) select a).First();
                    gender = (from a in education.HL7_Gender_Standards where a.GenderCode.Equals(person.Gender) select a).First();
                    staff = (from a in education.Evaluations where a.CID.Equals(cid) select a).First();
                }
                catch { }
            }

            if (hasRecordTarget && person != null && gender != null)
            {
                parser.setCDArecordTargetPatientRoleIdExtension(cid);
                parser.setCDArecordTargetPatientRolePatientNameGiven(person.FirstName);
                parser.setCDArecordTargetPatientRolePatientNameFamily(person.LastName);
                parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCode(person.Gender);
                parser.setCDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName(gender.GenderEnglishDescription);
                parser.setCDArecordTargetPatientRolePatientBirthTime(((DateTime)person.DOB).ToString(dateTimeFormat, CultureInfo.InvariantCulture));
            }

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

        public static XmlDocument getEducationEvaluationXML(string cid, string planNo, string caseNo)
        {
            XmlDocument doc = initXML(educationEvaluationXml, cid, planNo, true);
            CDAparser parser = new CDAparser(doc);
            parser.setCDAcomponentPlanNumber(planNo);
            parser.setCDAcomponentCaseNumber(caseNo);
            var disType = from a in education.DisabilityTypes select a;

            //<section code="ED001" displayName="การวินิจฉัยความบกพร่องทางการแพทย์">
            #region
            var disEval = from a in education.DisabilityEvaluations where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            List<CDAEntry> disEntryList = new List<CDAEntry>();
            foreach (DisabilityEvaluation dis in disEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = dis.DisabilityCode != null ? dis.DisabilityCode.Trim(): "";
                string display = (from a in disType where a.DisabilityCode.Equals(dis.DisabilityCode) select a.Name).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = ((DateTime)dis.EvalDateTime).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                disEntryList.Add(entry);
            }
            parser.setEntryList(disEntryList, "ED001", "การวินิจฉัยความบกพร่องทางการแพทย์");
            #endregion

            //<section code="ED002" displayName="ข้อควรพิจารณาประวัติทางการแพทย์">
            #region
            var healthEval = from a in education.HealthEvaluations where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var healthEvalType = from a in education.HealthProblems select a;
            List<CDAEntry> healthEntryList = new List<CDAEntry>();
            foreach (HealthEvaluation health in healthEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = health.HealthEvalCode != null ? health.HealthEvalCode .Trim() : "";
                string display = (from a in healthEvalType where a.HealthEvalCode.Equals(health.HealthEvalCode) select a.Name).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = ((DateTime)health.EvalDateTime).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                healthEntryList.Add(entry);
            }
            parser.setEntryList(healthEntryList, "ED002", "การวินิจฉัยความบกพร่องทางการแพทย์");
            #endregion

            //<section code="ED003" displayName="การได้ยิน">
            #region
            var hearEval = from a in education.HearEvaluations where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var hearEvalType = from a in education.HearProblems select a;
            List<CDAEntry> hearEntryList = new List<CDAEntry>();
            foreach (HearEvaluation hear in hearEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = hear.HearEvalCode != null ? hear.HearEvalCode .Trim() : "";
                string display = (from a in hearEvalType where a.HearEvalCode.Equals(hear.HearEvalCode) select a.Name).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = ((DateTime)hear.EvalDateTime).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                hearEntryList.Add(entry);
            }
            parser.setEntryList(hearEntryList, "ED003", "การได้ยิน");
            #endregion

            //<section code="ED004" displayName="การเคลื่อนไหว">
            #region
            var phyEval = from a in education.PhysicalEvaluations where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var phyEvalType = from a in education.PhysicalProblems select a;
            List<CDAEntry> phyEntryList = new List<CDAEntry>();
            foreach (PhysicalEvaluation phy in phyEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = phy.PhysicalEvalCode != null ? phy.PhysicalEvalCode .Trim() : "";
                string display = (from a in phyEvalType where a.PhysicalEvalCode.Equals(phy.PhysicalEvalCode) select a.Name).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = ((DateTime)phy.EvalDateTime).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                phyEntryList.Add(entry);
            }
            parser.setEntryList(phyEntryList, "ED004", "การเคลื่อนไหว");
            #endregion

            //<section code="ED005" displayName="การช่วยเหลือตนเอง">
            #region
            var selfEval = from a in education.SelfCareEvaluations where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var selfEvalType = from a in education.SelfCareAbilities select a;
            List<CDAEntry> selfEntryList = new List<CDAEntry>();
            foreach (SelfCareEvaluation self in selfEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = self.SelfEvalCode != null ? self.SelfEvalCode .Trim() : "";
                string display = (from a in selfEvalType where a.SelfEvalCode.Equals(self.SelfEvalCode) select a.Name).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = ((DateTime)self.EvalDateTime).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                selfEntryList.Add(entry);
            }
            parser.setEntryList(selfEntryList, "ED005", "การช่วยเหลือตนเอง");
            #endregion

            //<section code="ED006" displayName="ข้อมูลเกี่ยวกับกิจกรรม/สิ่งของ/บุคคลที่นักเรียนชอบ">
            #region
            var favEval = from a in education.FavorLists where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var favEvalType = from a in education.FavorItems select a;
            List<CDAEntry> favEntryList = new List<CDAEntry>();
            foreach (FavorList fav in favEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = fav.FavorItemCode != null ? fav.FavorItemCode .Trim() : "";
                string display = (from a in favEvalType where a.FavorItemCode.Equals(fav.FavorItemCode) select a.Name).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = ((DateTime)fav.EvalDateTime).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                favEntryList.Add(entry);
            }
            parser.setEntryList(favEntryList, "ED006", "ข้อมูลเกี่ยวกับกิจกรรม/สิ่งของ/บุคคลที่นักเรียนชอบ");
            #endregion

            return parser.getDocument();
        }

        public static XmlDocument getEducationPlanningXML(string cid, string planNo, string caseNo)
        {
            XmlDocument doc = initXML(educationPlanningXml, cid, planNo, true);
            CDAparser parser = new CDAparser(doc);
            parser.setCDAcomponentPlanNumber(planNo);
            parser.setCDAcomponentCaseNumber(caseNo);

            //<section code="ED007" displayName="การวางแผนการให้บริการจากศูนย์การศึกษาพิเศษ">
            #region
            var planEval = from a in education.Plannigs where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var planEvalType = from a in education.Activities select a;
            List<CDAEntry> planEntryList = new List<CDAEntry>();
            foreach (Plannig plan in planEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = plan.SACTCode != null ? plan.SACTCode .Trim() : "";
                string display = (from a in planEvalType where a.ACTCode.Equals(plan.SACTCode) select a.ACTDesc).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = ((DateTime)plan.ExpectedDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                planEntryList.Add(entry);
            }
            parser.setEntryList(planEntryList, "ED007", "การวางแผนการให้บริการจากศูนย์การศึกษาพิเศษ");
            #endregion

            return parser.getDocument();
        }

        public static XmlDocument getEducationServiceProvisionXML(string cid, string planNo, string caseNo)
        {
            CDAparser parser = new CDAparser(initXML(educationServiceProvisionXml, cid, planNo, true));
            parser.setCDAcomponentPlanNumber(planNo);
            parser.setCDAcomponentCaseNumber(caseNo);

            //<section code="ED008" displayName="การให้บริการจากศูนย์การศึกษาพิเศษ">
            #region
            var sppEval = from a in education.ServiceProvisionVsPersons where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var sppEvalType = from a in education.Activities select a;
            List<CDAEntry> sppEntryList = new List<CDAEntry>();
            foreach (ServiceProvisionVsPerson spp in sppEval)
            {
                CDAEntry entry = new CDAEntry();
                string code = (from a in education.ServiceProvisionDetails where a.id.Equals(spp.id) select a.ACTCode).First();
                entry.observationLocalCode = code != null ? code.Trim() : "";
                string display = (from a in sppEvalType where a.ACTCode.Equals(code) select a.ACTDesc).First();
                entry.observationDisplayName = display;
                DateTime time = (from a in education.ServiceProvisionDetails where a.id.Equals(spp.id) select a.logDateTime).First();
                entry.observationEffectiveTimeCenterValue = time.ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                sppEntryList.Add(entry);
            }
            parser.setEntryList(sppEntryList, "ED008", "การให้บริการจากศูนย์การศึกษาพิเศษ");
            #endregion

            return parser.getDocument();
        }

        public static XmlDocument getEducationDevelopmentXML(string cid, string planNo, string caseNo)
        {
            CDAparser parser = new CDAparser(initXML(educationDevelopmentXml, cid, planNo, true));
            parser.setCDAcomponentPlanNumber(planNo);
            parser.setCDAcomponentCaseNumber(caseNo);

            //<section code="E009" displayName="กิจกรรมส่งเสริมพัฒนาการ">
            #region
            var growEval = from a in education.GrowthEvaluation2s where a.CID.Equals(cid) && a.CaseNo.Equals(caseNo) select a;
            var growEvalType = from a in education.SubActivity2s select a;
            List<CDAEntry> growEntryList = new List<CDAEntry>();
            foreach (GrowthEvaluation2 grow in growEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationLocalCode = grow.SACT2Code != null ? grow.SACT2Code .Trim() : "";
                string display = (from a in growEvalType where a.SACT2Code.Equals(grow.SACT2Code) select a.SACT2Desc).First();
                entry.observationDisplayName = display;
                //entry.observationEffectiveTimeCenterValue = ((DateTime)plan.ExpectedDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                growEntryList.Add(entry);
            }
            parser.setEntryList(growEntryList, "ED009", "กิจกรรมส่งเสริมพัฒนาการ");
            #endregion

            return parser.getDocument();
        }

        public static XmlDocument getEducationProfileXML(string cid)
        {
            CDAparser parser = new CDAparser(initXML(educationProfiletXml, cid, null, false));

            //<section code="ED010" displayName="รายละเอียดการลงทะเบียนเด็กใหม่จากศูนย์การศึกษาพิเศษ">
            Person newPerson = (from a in education.Persons where a.CID.Equals(cid) select a).First();
            List<CDAEntryOfProfile> profileList = new List<CDAEntryOfProfile>();
            CDAEntryOfProfile profile = new CDAEntryOfProfile();
            profile.recordTargetpPatientRoleIdExtension = newPerson.CID != null ? newPerson.CID .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveHomeStatus = newPerson.LiveHomeStatus != null ? newPerson.LiveHomeStatus .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveHouseNumber = newPerson.LiveHouseNumber != null ? newPerson.LiveHouseNumber .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveMooNumber = newPerson.LiveMooNumber != null ? newPerson.LiveMooNumber .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveVillageName = newPerson.LiveVillageName != null ? newPerson.LiveVillageName .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveAlley = newPerson.LiveAlley != null ? newPerson.LiveAlley .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveStreetName = newPerson.LiveStreetName != null ? newPerson.LiveStreetName .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveTumbon = newPerson.LiveTumbon != null ? newPerson.LiveTumbon .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveCity = newPerson.LiveCity != null ? newPerson.LiveCity .Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveProvince = newPerson.LiveProvince != null ? newPerson.LiveProvince .Trim() : "";
            profile.recordTargetpPatientRoleAddrLivePostCode = newPerson.LivePostCode != null ? newPerson.LivePostCode .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusHouseNumber = newPerson.CensusHouseNumber != null ? newPerson.CensusHouseNumber .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusMooNumber = newPerson.CensusMooNumber != null ? newPerson.CensusMooNumber .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusVillageName = newPerson.CensusVillageName != null ? newPerson.CensusVillageName .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusAlley = newPerson.CensusAlley != null ? newPerson.CensusAlley .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusStreetName = newPerson.CensusStreetName != null ? newPerson.CensusStreetName .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusTumbon = newPerson.CensusTumbon != null ? newPerson.CensusTumbon .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusCity = newPerson.CensusCity != null ? newPerson.CensusCity .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusProvince = newPerson.CensusProvince != null ? newPerson.CensusProvince .Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusMoveInDate = ((DateTime)newPerson.CensusMoveInDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            profile.recordTargetpPatientRoleAddrCensusMoveOutDate = ((DateTime)newPerson.CensusMoveOutDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            profile.recordTargetpPatientRoleAddrCensusMoveOutReason = newPerson.CensusMoveOutReason != null ? newPerson.CensusMoveOutReason .Trim() : "";

            profile.recordTargetpPatientRoleHomePhoneValue = newPerson.HomePhone != null ? newPerson.HomePhone .Trim() : "";
            profile.recordTargetpPatientRoleMobileValue = newPerson.Mobile != null ? newPerson.Mobile .Trim() : "";
            profile.recordTargetpPatientRolePatientNameTitle = newPerson.Title != null ? newPerson.Title .Trim() : "";
            profile.recordTargetpPatientRolePatientNameGiven = newPerson.FirstName != null ? newPerson.FirstName.Trim(): "";
            profile.recordTargetpPatientRolePatientNameFamily = newPerson.LastName != null ? newPerson.LastName .Trim(): "";

            profile.recordTargetpPatientRolePatientAdministrativeGenderCodeCode = newPerson.Gender != null ? newPerson.Gender .Trim(): "";
            profile.recordTargetpPatientRolePatientBirthTimeValue = ((DateTime)newPerson.DOB).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            profile.recordTargetpPatientRolePatientSubWelfareIDValue = "04";
            profile.recordTargetpPatientRolePatientBloodTypeValue = newPerson.BloodType != null ? newPerson.BloodType .Trim(): "";
            profile.recordTargetpPatientRolePatientNationValue = newPerson.Nation != null ? newPerson.Nation .Trim(): "";
            profile.recordTargetpPatientRolePatientRaceValue = newPerson.Race != null ? newPerson.Race .Trim(): "";
            profile.recordTargetpPatientRolePatientReligionValue = newPerson.Religion != null ? newPerson.Religion .Trim(): "";
            profile.recordTargetpPatientRolePatientForeignerValue = newPerson.Foreigner != null ? newPerson.Foreigner .Trim(): "";
            profile.recordTargetpPatientRolePatientChildPicValue = newPerson.Child_Pic != null ? newPerson.Child_Pic .Trim(): "";
            profile.recordTargetpPatientRolePatientChildPicCIDValue = newPerson.Child_PicCID != null ? newPerson.Child_PicCID .Trim(): "";

            profileList.Add(profile);
            parser.setEntryOfProfile(profileList, "ED010", "รายละเอียดการลงทะเบียนเด็กใหม่จากศูนย์การศึกษาพิเศษ");

            return parser.getDocument();
        }

        public static XmlDocument getEducationMilkXML()
        {
            CDAparser parser = new CDAparser(initXML(educationMilkXml, null, null, false));

            //<section code="ED011" displayName="รายชื่อเด็กได้รับอาหารเสริมนมและอาหารกลางวัน">
            var allPerson = from a in education.Persons  select a;
            List<CDAEntryOfProfile> profileList = new List<CDAEntryOfProfile>();
            DateTime timeNow = DateTime.Now;
            foreach (Person person in allPerson)
            {
                DateTime dtUpdate = (DateTime)person.DateTimeUpdate;
                if (dtUpdate.Month == timeNow.Month && dtUpdate.Year == timeNow.Year)
                {
                    CDAEntryOfProfile profile = new CDAEntryOfProfile();
                    profile.recordTargetpPatientRoleIdExtension = person.CID != null ? person.CID.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveHomeStatus = person.LiveHomeStatus != null ? person.LiveHomeStatus.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveHouseNumber = person.LiveHouseNumber != null ? person.LiveHouseNumber.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveMooNumber = person.LiveMooNumber != null ? person.LiveMooNumber.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveVillageName = person.LiveVillageName != null ? person.LiveVillageName.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveAlley = person.LiveAlley != null ? person.LiveAlley.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveStreetName = person.LiveStreetName != null ? person.LiveStreetName.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveTumbon = person.LiveTumbon != null ? person.LiveTumbon.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveCity = person.LiveCity != null ? person.LiveCity.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLiveProvince = person.LiveProvince != null ? person.LiveProvince.Trim() : "";
                    profile.recordTargetpPatientRoleAddrLivePostCode = person.LivePostCode != null ? person.LivePostCode.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusHouseNumber = person.CensusHouseNumber != null ? person.CensusHouseNumber.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusMooNumber = person.CensusMooNumber != null ? person.CensusMooNumber.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusVillageName = person.CensusVillageName != null ? person.CensusVillageName.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusAlley = person.CensusAlley != null ? person.CensusAlley.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusStreetName = person.CensusStreetName != null ? person.CensusStreetName.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusTumbon = person.CensusTumbon != null ? person.CensusTumbon.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusCity = person.CensusCity != null ? person.CensusCity.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusProvince = person.CensusProvince != null ? person.CensusProvince.Trim() : "";
                    profile.recordTargetpPatientRoleAddrCensusMoveInDate = ((DateTime)person.CensusMoveInDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                    profile.recordTargetpPatientRoleAddrCensusMoveOutDate = ((DateTime)person.CensusMoveOutDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                    profile.recordTargetpPatientRoleAddrCensusMoveOutReason = person.CensusMoveOutReason != null ? person.CensusMoveOutReason.Trim() : "";

                    profile.recordTargetpPatientRoleHomePhoneValue = person.HomePhone != null ? person.HomePhone.Trim() : "";
                    profile.recordTargetpPatientRoleMobileValue = person.Mobile != null ? person.Mobile.Trim() : "";
                    profile.recordTargetpPatientRolePatientNameTitle = person.Title != null ? person.Title.Trim() : "";
                    profile.recordTargetpPatientRolePatientNameGiven = person.FirstName != null ? person.FirstName.Trim() : "";
                    profile.recordTargetpPatientRolePatientNameFamily = person.LastName != null ? person.LastName.Trim() : "";

                    profile.recordTargetpPatientRolePatientAdministrativeGenderCodeCode = person.Gender != null ? person.Gender.Trim() : "";
                    profile.recordTargetpPatientRolePatientBirthTimeValue = ((DateTime)person.DOB).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
                    profile.recordTargetpPatientRolePatientSubWelfareIDValue = "04";
                    profile.recordTargetpPatientRolePatientBloodTypeValue = person.BloodType != null ? person.BloodType.Trim() : "";
                    profile.recordTargetpPatientRolePatientNationValue = person.Nation != null ? person.Nation.Trim() : "";
                    profile.recordTargetpPatientRolePatientRaceValue = person.Race != null ? person.Race.Trim() : "";
                    profile.recordTargetpPatientRolePatientReligionValue = person.Religion != null ? person.Religion.Trim() : "";
                    profile.recordTargetpPatientRolePatientForeignerValue = person.Foreigner != null ? person.Foreigner.Trim() : "";
                    profile.recordTargetpPatientRolePatientChildPicValue = person.Child_Pic != null ? person.Child_Pic.Trim() : "";
                    profile.recordTargetpPatientRolePatientChildPicCIDValue = person.Child_PicCID != null ? person.Child_PicCID.Trim() : "";

                    profileList.Add(profile);
                }
            }

            parser.setEntryOfProfile(profileList, "ED011", "รายชื่อเด็กได้รับอาหารเสริมนมและอาหารกลางวัน");

            return parser.getDocument();
        }
        #endregion

        public static XmlDocument getCasemanagerProfileXML(string cid)
        {
            CDAparser parser = new CDAparser(initXML(CasemanagerProfileXml, cid, null, false));

            //<section code="CA001" displayName="ข้อมูลลงทะเบียนจาก Case Manager">
            Person newPerson = (from a in education.Persons where a.CID.Equals(cid) select a).First();
            List<CDAEntryOfProfile> profileList = new List<CDAEntryOfProfile>();
            CDAEntryOfProfile profile = new CDAEntryOfProfile();
            profile.recordTargetpPatientRoleIdExtension = newPerson.CID != null ? newPerson.CID.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveHomeStatus = newPerson.LiveHomeStatus != null ? newPerson.LiveHomeStatus.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveHouseNumber = newPerson.LiveHouseNumber != null ? newPerson.LiveHouseNumber.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveMooNumber = newPerson.LiveMooNumber != null ? newPerson.LiveMooNumber.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveVillageName = newPerson.LiveVillageName != null ? newPerson.LiveVillageName.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveAlley = newPerson.LiveAlley != null ? newPerson.LiveAlley.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveStreetName = newPerson.LiveStreetName != null ? newPerson.LiveStreetName.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveTumbon = newPerson.LiveTumbon != null ? newPerson.LiveTumbon.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveCity = newPerson.LiveCity != null ? newPerson.LiveCity.Trim() : "";
            profile.recordTargetpPatientRoleAddrLiveProvince = newPerson.LiveProvince != null ? newPerson.LiveProvince.Trim() : "";
            profile.recordTargetpPatientRoleAddrLivePostCode = newPerson.LivePostCode != null ? newPerson.LivePostCode.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusHouseNumber = newPerson.CensusHouseNumber != null ? newPerson.CensusHouseNumber.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusMooNumber = newPerson.CensusMooNumber != null ? newPerson.CensusMooNumber.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusVillageName = newPerson.CensusVillageName != null ? newPerson.CensusVillageName.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusAlley = newPerson.CensusAlley != null ? newPerson.CensusAlley.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusStreetName = newPerson.CensusStreetName != null ? newPerson.CensusStreetName.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusTumbon = newPerson.CensusTumbon != null ? newPerson.CensusTumbon.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusCity = newPerson.CensusCity != null ? newPerson.CensusCity.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusProvince = newPerson.CensusProvince != null ? newPerson.CensusProvince.Trim() : "";
            profile.recordTargetpPatientRoleAddrCensusMoveInDate = ((DateTime)newPerson.CensusMoveInDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            profile.recordTargetpPatientRoleAddrCensusMoveOutDate = ((DateTime)newPerson.CensusMoveOutDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            profile.recordTargetpPatientRoleAddrCensusMoveOutReason = newPerson.CensusMoveOutReason != null ? newPerson.CensusMoveOutReason.Trim() : "";

            profile.recordTargetpPatientRoleHomePhoneValue = newPerson.HomePhone != null ? newPerson.HomePhone.Trim() : "";
            profile.recordTargetpPatientRoleMobileValue = newPerson.Mobile != null ? newPerson.Mobile.Trim() : "";
            profile.recordTargetpPatientRolePatientNameTitle = newPerson.Title != null ? newPerson.Title.Trim() : "";
            profile.recordTargetpPatientRolePatientNameGiven = newPerson.FirstName != null ? newPerson.FirstName.Trim() : "";
            profile.recordTargetpPatientRolePatientNameFamily = newPerson.LastName != null ? newPerson.LastName.Trim() : "";

            profile.recordTargetpPatientRolePatientAdministrativeGenderCodeCode = newPerson.Gender != null ? newPerson.Gender.Trim() : "";
            profile.recordTargetpPatientRolePatientBirthTimeValue = ((DateTime)newPerson.DOB).ToString(dateTimeFormat, CultureInfo.InvariantCulture);
            profile.recordTargetpPatientRolePatientSubWelfareIDValue = "04";
            profile.recordTargetpPatientRolePatientBloodTypeValue = newPerson.BloodType != null ? newPerson.BloodType.Trim() : "";
            profile.recordTargetpPatientRolePatientNationValue = newPerson.Nation != null ? newPerson.Nation.Trim() : "";
            profile.recordTargetpPatientRolePatientRaceValue = newPerson.Race != null ? newPerson.Race.Trim() : "";
            profile.recordTargetpPatientRolePatientReligionValue = newPerson.Religion != null ? newPerson.Religion.Trim() : "";
            profile.recordTargetpPatientRolePatientForeignerValue = newPerson.Foreigner != null ? newPerson.Foreigner.Trim() : "";
            profile.recordTargetpPatientRolePatientChildPicValue = newPerson.Child_Pic != null ? newPerson.Child_Pic.Trim() : "";
            profile.recordTargetpPatientRolePatientChildPicCIDValue = newPerson.Child_PicCID != null ? newPerson.Child_PicCID.Trim() : "";

            profileList.Add(profile);
            parser.setEntryOfProfile(profileList, "CA001", "ข้อมูลลงทะเบียนจาก Case Manager");

            return parser.getDocument();
        }

        public static XmlDocument getCasemanagerQuestionareXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(CasemanagerQuestionareXml, cid, planNo, true));
            parser.setCDAcomponentPlanNumber(planNo);

            //<section code="CA002" displayName="แบบสอบถามจาก Case Manager">
            var qType = from a in casemanager.Quastionares select a;
            #region
            var qEval = from a in casemanager.ResultQuastionares where a.CID.Equals(cid) && a.CaseNumber.Equals(planNo) select a;
            List<CDAEntry> disEntryList = new List<CDAEntry>();
            foreach (CDAparserCS.ResultQuastionare q in qEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationCode = q.ICFCode != null ? q.ICFCode.Trim() : "";
                string display = (from a in qType where a.QNCode.Equals(q.ICFCode) select a.QNName).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = q.DateTimeUpDate != null ? ((DateTime)q.DateTimeUpDate).ToString(dateTimeFormat, CultureInfo.InvariantCulture) : "";
                disEntryList.Add(entry);
            }
            parser.setEntryList(disEntryList, "CA002", "แบบสอบถามจาก Case Manager");
            #endregion

            return parser.getDocument();
        }

        public static XmlDocument getCasemanagerPlanningXML(string cid, string planNo)
        {
            CDAparser parser = new CDAparser(initXML(CasemanagerPlanningXml, cid, planNo, true));
            parser.setCDAcomponentPlanNumber(planNo);

            //<section code="CA003" displayName="การวางแผนบริการจาก Case Manager">
            var svcType = from a in casemanager.Services select a;
            #region
            var svcEval = from a in casemanager.PlanServices where a.CID.Equals(cid) && a.CaseNo.Equals(planNo) select a;
            List<CDAEntry> svcEntryList = new List<CDAEntry>();
            foreach (CDAparserCS.PlanService svc in svcEval)
            {
                CDAEntry entry = new CDAEntry();
                entry.observationCode = svc.SVCCode != null ? svc.SVCCode.Trim() : "";
                string display = (from a in svcType where a.SVCCode.Equals(svc.SVCCode) select a.SVCName).First();
                entry.observationDisplayName = display;
                entry.observationEffectiveTimeCenterValue = svc.DateTimeUpdate != null ? ((DateTime)svc.DateTimeUpdate).ToString(dateTimeFormat, CultureInfo.InvariantCulture) : "";
                svcEntryList.Add(entry);
            }
            parser.setEntryList(svcEntryList, "CA003", "การวางแผนบริการจาก Case Manager");
            #endregion

            return parser.getDocument();
        }

    }
}