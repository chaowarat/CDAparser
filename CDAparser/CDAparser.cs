using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.XPath;
using System.Xml;

namespace CDAparser
{
    public class CDAparser
    {
        private XmlDocument document;

        private static String CDAclassCode = "@classCode";
        private static String CDAmoodCode = "@moodCode";
        private static String CDAtypeIdExtension = "typeId/@extension";
        private static String CDAtypeIdRoot = "typeId/@root";
        private static String CDAidExtension = "id/@extension";
        private static String CDAidRoot = "id/@root";
        private static String CDAcode = "code/@code";
        private static String CDAcodeCodeSystem = "code/@codeSystem";
        private static String CDAcodeCodeSystemName = "code/@codeSystemName";
        private static String CDAcodeDisplayName = "code/@displayName";
        private static String CDArelatedDocumentTypeCode = "relatedDocument/@typeCode";
        private static String CDAtitle = "title";
        private static String CDAeffectiveTime = "effectiveTime/@value";
        private static String CDAconfidentialityCode = "confidentialityCode/@code";
        private static String CDAconfidentialityCodeCodeSystem = "confidentialityCode/@codeSystem";
        private static String CDAconfidentialityCodeCodeSystemName = "confidentialityCode/@codeSystemName";
        private static String CDAconfidentialityCodeDisplayName = "confidentialityCode/@displayName";
        //*************************<!--Mapping Person Table-->******************************************************************
        private static String CDArecordTargetContextControlCode = "recordTarget/@contextControlCode";
        private static String CDArecordTargetTypeCode = "recordTarget/@typeCode";
        private static String CDArecordTargetPatientRoleClassCode = "recordTarget/patientRole/@classCode";
        private static String CDArecordTargetPatientRoleIdExtension = "recordTarget/patientRole/id/@extension";
        private static String CDArecordTargetPatientRoleIdRoot = "recordTarget/patientRole/id/@root";
        private static String CDArecordTargetPatientRolePatientNameGiven = "recordTarget/patientRole/patient/name/given";
        private static String CDArecordTargetPatientRolePatientNameFamily = "recordTarget/patientRole/patient/name/family";
        private static String CDArecordTargetPatientRolePatientAdministrativeGenderCode = "recordTarget/patientRole/patient/administrativeGenderCode/@code";
        private static String CDArecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem = "recordTarget/patientRole/patient/administrativeGenderCode/@codeSystem";
        private static String CDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName = "recordTarget/patientRole/patient/administrativeGenderCode/@displayName";
        private static String CDArecordTargetPatientRolePatientAdministrativeGenderCodeSystemName = "recordTarget/patientRole/patient/administrativeGenderCode/@codeSystemName";
        private static String CDArecordTargetPatientRolePatientBirthTime = "recordTarget/patientRole/patient/birthTime/@value";
        //*************************<!--Mapping Staff Table-->*******************************************************************
        private static String CDAauthorContextControlCode = "author/@contextControlCode";
        private static String CDAauthorTypeCode = "author/@typeCode";
        private static String CDAauthorTimeValue = "author/time/@value";
        private static String CDAauthorAssignedAuthorIdExtension = "author/assignedAuthor/id/@extension";
        private static String CDAauthorAssignedAuthorIdRoot = "author/assignedAuthor/id/@root";
        private static String CDAauthorAssignedAuthorAssignedPersonNameGiven = "author/assignedAuthor/assignedPerson/name/given";
        private static String CDAauthorAssignedAuthorAssignedPersonNameFamily = "author/assignedAuthor/assignedPerson/name/family";
        private static String CDAauthorAssignedAuthorAssignedPersonNameSuffix = "author/assignedAuthor/assignedPerson/name/suffix";
        //*************************<!-- Mapping Organization Table-->****************************************************************
        private static String CDAcustodianTypeCode = "custodian/@typeCode";
        private static String CDAcustodianAssignedCustodianClassCode = "custodian/assignedCustodian/@classCode";
        private static String CDAcustodianAssignedCustodianRepresentedCustodianOrganizationClassCode = "custodian/assignedCustodian/representedCustodianOrganization/@classCode";
        private static String CDAcustodianAssignedCustodianRepresentedCustodianOrganizationDeterminerCode = "custodian/assignedCustodian/representedCustodianOrganization/@determinerCode";
        private static String CDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdExtension = "custodian/assignedCustodian/representedCustodianOrganization/id/@extension";
        private static String CDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdRoot = "custodian/assignedCustodian/representedCustodianOrganization/id/@root";
        private static String CDAcustodianAssignedCustodianRepresentedCustodianOrganizationName = "custodian/assignedCustodian/representedCustodianOrganization/name";
        //*************************<!-- Mapping Province Chief CaseManager Data(Staff DB) -->*********************************************************
        private static String CDAlegalAuthenticatorContextControlCode = "legalAuthenticator/@contextControlCode";
        private static String CDAlegalAuthenticatorTypeCode = "legalAuthenticator/@typeCode";
        private static String CDAlegalAuthenticatorTimeValue = "legalAuthenticator/time/@value";
        private static String CDAlegalAuthenticatorSignatureCode = "legalAuthenticator/signatureCode/@code";
        private static String CDAlegalAuthenticatorAssignedEntityIdExtension = "legalAuthenticator/assignedEntity/id/@extension";
        private static String CDAlegalAuthenticatorAssignedEntityIdRoot = "legalAuthenticator/assignedEntity/id/@root";
        private static String CDAlegalAuthenticatorAssignedEntityAssignedPersonNameGiven = "legalAuthenticator/assignedEntity/assignedPerson/name/given";
        private static String CDAlegalAuthenticatorAssignedEntityAssignedPersonNameFamily = "legalAuthenticator/assignedEntity/assignedPerson/name/family";
        private static String CDAlegalAuthenticatorAssignedEntityAssignedPersonNameSuffix = "legalAuthenticator/assignedEntity/assignedPerson/name/suffix";
        //*************************<!-- Mapping CaseManager Data(Staff DB) -->*********************************************************
        private static String CDAauthenticatorTimeValue = "authenticator/time/@value";
        private static String CDAauthenticatorSignatureCode = "authenticator/signatureCode/@code";
        private static String CDAauthenticatorAssignedEntityClassCode = "authenticator/assignedEntity/@classCode";
        private static String CDAauthenticatorAssignedEntityIdExtension = "authenticator/assignedEntity/id/@extension";
        private static String CDAauthenticatorAssignedEntityIdRoot = "authenticator/assignedEntity/id/@root";
        private static String CDAauthenticatorAssignedEntityAssignedPersonNameGiven = "authenticator/assignedEntity/assignedPerson/name/given";
        private static String CDAauthenticatorAssignedEntityAssignedPersonNameFamily = "authenticator/assignedEntity/assignedPerson/name/family";
        private static String CDAauthenticatorAssignedEntityAssignedPersonNameSuffix = "authenticator/assignedEntity/assignedPerson/name/suffix";
        /// <summary>
        /// <!-- The claim associated with this CDA document is identified by the value XA728302 in data 
        ///      element TRN02-Attachment Control Number of Loop 2000A-Payer/Provider Control Number. 
        /// -->
        /// </summary>
        private static String CDAinFulfillmentOfOrderIdExtension = "inFulfillmentOf/order/id/@extension";
        private static String CDAinFulfillmentOfOrderIdRoot = "inFulfillmentOf/order/id/@root";
        //*************************<!--Date Range of Treatment --> and  <!--Date Range of Described by Plan-->*****************************
        private static String CDAdocumentationOfServiceEventCode = "documentationOf/serviceEvent/code/@code";
        private static String CDAdocumentationOfServiceEventCodeCodeSystem = "documentationOf/serviceEvent/code/@codeSystem";
        private static String CDAdocumentationOfServiceEventCodeCodeSystemName = "documentationOf/serviceEvent/code/@codeSystemName";
        private static String CDAdocumentationOfServiceEventCodeDisplayName = "documentationOf/serviceEvent/code/@displayName";
        private static String CDAdocumentationOfServiceEventEffectiveTime = "documentationOf/serviceEvent";
        private static String CDAdocumentationOfServiceEventEffectiveTimeLowValue = "effectiveTime/low/@value";
        private static String CDAdocumentationOfServiceEventEffectiveTimeHighValue = "effectiveTime/high/@value";
        //************************* component Head ****************************************************************
        private static String CDAcomponentContextConductionInd = "component/@contextConductionInd";
        private static String CDAcomponentTypeCode = "component/@typeCode";
        private static String CDAcomponentStructuredBodyComponentSectionCode = "component/structuredBody/component/section/code/@code";
        private static String CDAcomponentStructuredBodyComponentSectionCodeSystem = "component/structuredBody/component/section/code/@codeSystem";
        private static String CDAcomponentStructuredBodyComponentSectionCodeSystemName = "component/structuredBody/component/section/code/@codeSystemName";
        private static String CDAcomponentStructuredBodyComponentSectionCodeDisplayName = "component/structuredBody/component/section/code/@displayName";
        private static String CDAcomponentStructuredBodyComponentSectionTitle = "component/structuredBody/component/section/title";
        //************************* component body (return list of string)****************************************************************
        private static String CDAcomponentStructuredBodyComponentSectionEntryTypeCode = "component/structuredBody/component/section/entry/@typeCode";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationClassCode = "component/structuredBody/component/section/entry/observation/@classCode";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationMoodCode = "component/structuredBody/component/section/entry/observation/@moodCode";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationTemplateIdRoot = "component/structuredBody/component/section/entry/observation/templateId/@root";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationIdRoot = "component/structuredBody/component/section/entry/observation/id/@root";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationCode = "component/structuredBody/component/section/entry/observation/code/@code";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationCodeSystem = "component/structuredBody/component/section/entry/observation/code/@codeSystem";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationDisplayName = "component/structuredBody/component/section/entry/observation/code/@displayName";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationStatusCode = "component/structuredBody/component/section/entry/observation/statusCode/@code";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationEffectiveTimeCenterValue = "component/structuredBody/component/section/entry/observation/effectiveTime/center/@value";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationValue = "component/structuredBody/component/section/entry/observation/value";
        private static String CDAcomponentStructuredBodyComponentSectionEntryObservationValueXSItype = "component/structuredBody/component/section/entry/observation/value/@*[local-name()='type']";
        //*************************list of people******************************************************************
        private static String CDApathToPatientEntry = "component/structuredBody/component/section/entry/";


        #region construnctor
        public CDAparser(XmlReader mem)
        {
            document = new XmlDocument();
            document.Load(mem);
        }

        public CDAparser(string path)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(path, settings);
            document = new XmlDocument();
            document.Load(reader);
            reader.Close();
        }

        public CDAparser(XmlDocument doc)
        {
            this.document = doc;
        }
        #endregion

        public XmlDocument getDocument()
        {
            return this.document;
        }

        public void setDocument(XmlDocument doc)
        {
            this.document = doc;
        }

        public string getInnerTextSingle(string node)
        {
            try { return document.SelectSingleNode("//" + node).InnerText; }
            catch (Exception) { return null; }
        }

        public void setInnerTextSingle(string node, string value)
        {
            try { document.SelectSingleNode("//" + node).InnerText = value; }
            catch (Exception) { }
        }

        public List<string> getInnerTextMultiple(string node)
        {
            try
            {
                List<string> output = new List<string>();
                XmlNodeList nodes = document.SelectNodes("//" + node);
                foreach (XmlNode n in nodes)
                {
                    output.Add(n.InnerText);
                }
                return output;
            }
            catch (Exception) { return null; }
        }

        public void setInnerTextEntry(XmlElement entry)
        {
            try
            {
                XmlNode section = this.document.SelectSingleNode("//component/component/section");
                section.AppendChild(entry.Clone());
            }
            catch (Exception) { }
        }

        public void setCDAcomponentSectionEntry(CDAEntry entry)
        {
            setInnerTextEntry(entry.getXMLelement(this.document));
        }

        #region CDA Header
        public string getCDAclassCode()
        {
            return getInnerTextSingle(CDAclassCode);
        }

        public void setCDAclassCode(string value)
        {
            setInnerTextSingle(CDAclassCode, value);
        }

        public string getCDAmoodCode()
        {
            return getInnerTextSingle(CDAmoodCode);
        }

        public void setCDAmoodCode(string value)
        {
            setInnerTextSingle(CDAmoodCode, value);
        }

        public string getCDAtypeIdExtension()
        {
            return getInnerTextSingle(CDAtypeIdExtension);
        }

        public void setCDAtypeIdExtension(string value)
        {
            setInnerTextSingle(CDAtypeIdExtension, value);
        }

        public string getCDAtypeIdRoot()
        {
            return getInnerTextSingle(CDAtypeIdRoot);
        }

        public void setCDAtypeIdRoot(string value)
        {
            setInnerTextSingle(CDAtypeIdRoot, value);
        }

        public string getCDAidExtension()
        {
            return getInnerTextSingle(CDAidExtension);
        }

        public void setCDAidExtension(string value)
        {
            setInnerTextSingle(CDAidExtension, value);
        }

        public string getCDAidRoot()
        {
            return getInnerTextSingle(CDAidRoot);
        }

        public string getCDAcode()
        {
            return getInnerTextSingle(CDAcode);
        }

        public void setCDAcode(string value)
        {
            setInnerTextSingle(CDAcode, value);
        }

        public string getCDAcodeCodeSystem()
        {
            return getInnerTextSingle(CDAcodeCodeSystem);
        }

        public void setCDAcodeCodeSystem(string value)
        {
            setInnerTextSingle(CDAcodeCodeSystem, value);
        }

        public string getCDAcodeCodeSystemName()
        {
            return getInnerTextSingle(CDAcodeCodeSystemName);
        }

        public void setCDAcodeCodeSystemName(string value)
        {
            setInnerTextSingle(CDAcodeCodeSystemName, value);
        }

        public string getCDAcodeDisplayName()
        {
            return getInnerTextSingle(CDAcodeDisplayName);
        }

        public void setCDAcodeDisplayName(string value)
        {
            setInnerTextSingle(CDAcodeDisplayName, value);
        }

        public string getCDArelatedDocumentTypeCode()
        {
            return getInnerTextSingle(CDArelatedDocumentTypeCode);
        }

        public void setCDArelatedDocumentTypeCode(string value)
        {
            setInnerTextSingle(CDArelatedDocumentTypeCode, value);
        }

        public void setCDAidRoot(string value)
        {
            setInnerTextSingle(CDAidRoot, value);
        }

        public string getCDAtitle()
        {
            return getInnerTextSingle(CDAtitle);
        }

        public void setCDAtitle(string value)
        {
            setInnerTextSingle(CDAtitle, value);
        }

        public string getCDAeffectiveTime()
        {
            return getInnerTextSingle(CDAeffectiveTime);
        }

        public void setCDAeffectiveTime(string value)
        {
            setInnerTextSingle(CDAeffectiveTime, value);
        }

        public string getCDAconfidentialityCode()
        {
            return getInnerTextSingle(CDAconfidentialityCode);
        }

        public void setCDAconfidentialityCode(string value)
        {
            setInnerTextSingle(CDAconfidentialityCode, value);
        }

        public string getCDAconfidentialityCodeCodeSystem()
        {
            return getInnerTextSingle(CDAconfidentialityCodeCodeSystem);
        }

        public void setCDAconfidentialityCodeCodeSystem(string value)
        {
            setInnerTextSingle(CDAconfidentialityCodeCodeSystem, value);
        }

        public string getCDAconfidentialityCodeCodeSystemName()
        {
            return getInnerTextSingle(CDAconfidentialityCodeCodeSystemName);
        }

        public void setCDAconfidentialityCodeCodeSystemName(string value)
        {
            setInnerTextSingle(CDAconfidentialityCodeCodeSystemName, value);
        }

        public string getCDAconfidentialityCodeDisplayName()
        {
            return getInnerTextSingle(CDAconfidentialityCodeDisplayName);
        }

        public void setCDAconfidentialityCodeDisplayName(string value)
        {
            setInnerTextSingle(CDAconfidentialityCodeDisplayName, value);
        }
        #endregion

        #region <!--Mapping Person Table-->
        public string getCDArecordTargetContextControlCode()
        {
            return getInnerTextSingle(CDArecordTargetContextControlCode);
        }

        public void setCDArecordTargetContextControlCode(string value)
        {
            setInnerTextSingle(CDArecordTargetContextControlCode, value);
        }

        public string getCDArecordTargetTypeCode()
        {
            return getInnerTextSingle(CDArecordTargetTypeCode);
        }

        public void setCDArecordTargetTypeCode(string value)
        {
            setInnerTextSingle(CDArecordTargetTypeCode, value);
        }

        public string getCDArecordTargetPatientRoleClassCode()
        {
            return getInnerTextSingle(CDArecordTargetPatientRoleClassCode);
        }

        public void setCDArecordTargetPatientRoleClassCode(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRoleClassCode, value);
        }

        public string getCDArecordTargetPatientRoleIdExtension()
        {
            return getInnerTextSingle(CDArecordTargetPatientRoleIdExtension);
        }

        public void setCDArecordTargetPatientRoleIdExtension(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRoleIdExtension, value);
        }

        public string getCDArecordTargetPatientRoleIdRoot()
        {
            return getInnerTextSingle(CDArecordTargetPatientRoleIdRoot);
        }

        public void setCDArecordTargetPatientRoleIdRoot(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRoleIdRoot, value);
        }

        public string getCDArecordTargetPatientRolePatientNameGiven()
        {
            return getInnerTextSingle(CDArecordTargetPatientRolePatientNameGiven);
        }

        public void setCDArecordTargetPatientRolePatientNameGiven(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRolePatientNameGiven, value);
        }

        public string getCDArecordTargetPatientRolePatientAdministrativeGenderCode()
        {
            return getInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCode);
        }

        public void setCDArecordTargetPatientRolePatientAdministrativeGenderCode(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCode, value);
        }

        public string getCDArecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem()
        {
            return getInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem);
        }

        public void setCDArecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem, value);
        }

        public string getCDArecordTargetPatientRolePatientAdministrativeGenderCodeSystemName()
        {
            return getInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCodeSystemName);
        }

        public void setCDArecordTargetPatientRolePatientAdministrativeGenderCodeSystemName(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCodeSystemName, value);
        }

        public string getCDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName()
        {
            return getInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName);
        }

        public void setCDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName, value);
        }

        public string getCDArecordTargetPatientRolePatientNameFamily()
        {
            return getInnerTextSingle(CDArecordTargetPatientRolePatientNameFamily);
        }

        public void setCDArecordTargetPatientRolePatientNameFamily(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRolePatientNameFamily, value);
        }

        public string getCDArecordTargetPatientRolePatientBirthTime()
        {
            return getInnerTextSingle(CDArecordTargetPatientRolePatientBirthTime);
        }

        public void setCDArecordTargetPatientRolePatientBirthTime(string value)
        {
            setInnerTextSingle(CDArecordTargetPatientRolePatientBirthTime, value);
        }
        #endregion

        #region <!--Mapping Staff Table-->
        public string getCDAauthoContextControlCode()
        {
            return getInnerTextSingle(CDAauthorContextControlCode);
        }

        public void setCDAauthoContextControlCode(string value)
        {
            setInnerTextSingle(CDAauthorContextControlCode, value);
        }

        public string getCDAauthoTypeCode()
        {
            return getInnerTextSingle(CDAauthorTypeCode);
        }

        public void setCDAauthoTypeCode(string value)
        {
            setInnerTextSingle(CDAauthorTypeCode, value);
        }

        public string getCDAauthorTimeValue()
        {
            return getInnerTextSingle(CDAauthorTimeValue);
        }

        public void setCDAauthorTimeValue(string value)
        {
            setInnerTextSingle(CDAauthorTimeValue, value);
        }

        public string getCDAauthorAssignedAuthorIdExtension()
        {
            return getInnerTextSingle(CDAauthorAssignedAuthorIdExtension);
        }

        public void setCDAauthorAssignedAuthorIdExtension(string value)
        {
            setInnerTextSingle(CDAauthorAssignedAuthorIdExtension, value);
        }

        public string getCDAauthorAssignedAuthorIdRoot()
        {
            return getInnerTextSingle(CDAauthorAssignedAuthorIdRoot);
        }

        public void setCDAauthorAssignedAuthorIdRoot(string value)
        {
            setInnerTextSingle(CDAauthorAssignedAuthorIdRoot, value);
        }

        public string getCDAauthorAssignedAuthorAssignedPersonNameGiven()
        {
            return getInnerTextSingle(CDAauthorAssignedAuthorAssignedPersonNameGiven);
        }

        public void setCDAauthorAssignedAuthorAssignedPersonNameGiven(string value)
        {
            setInnerTextSingle(CDAauthorAssignedAuthorAssignedPersonNameGiven, value);
        }

        public string getCDAauthorAssignedAuthorAssignedPersonNameFamily()
        {
            return getInnerTextSingle(CDAauthorAssignedAuthorAssignedPersonNameFamily);
        }

        public void setCDAauthorAssignedAuthorAssignedPersonNameFamily(string value)
        {
            setInnerTextSingle(CDAauthorAssignedAuthorAssignedPersonNameFamily, value);
        }

        public string getCDAauthorAssignedAuthorAssignedPersonNameSuffix()
        {
            return getInnerTextSingle(CDAauthorAssignedAuthorAssignedPersonNameSuffix);
        }

        public void setCDAauthorAssignedAuthorAssignedPersonNameSuffix(string value)
        {
            setInnerTextSingle(CDAauthorAssignedAuthorAssignedPersonNameSuffix, value);
        }
        #endregion

        #region <!-- Mapping Organization Table-->
        public string getCDAcustodianTypeCode()
        {
            return getInnerTextSingle(CDAcustodianTypeCode);
        }

        public void setCDAcustodianTypeCode(string value)
        {
            setInnerTextSingle(CDAcustodianTypeCode, value);
        }

        public string getCDAcustodianAssignedCustodianClassCode()
        {
            return getInnerTextSingle(CDAcustodianAssignedCustodianClassCode);
        }

        public void setCDAcustodianAssignedCustodianClassCode(string value)
        {
            setInnerTextSingle(CDAcustodianAssignedCustodianClassCode, value);
        }

        public string getCDAcustodianAssignedCustodianRepresentedCustodianOrganizationClassCode()
        {
            return getInnerTextSingle(CDAcustodianAssignedCustodianRepresentedCustodianOrganizationClassCode);
        }

        public void setCDAcustodianAssignedCustodianRepresentedCustodianOrganizationClassCode(string value)
        {
            setInnerTextSingle(CDAcustodianAssignedCustodianRepresentedCustodianOrganizationClassCode, value);
        }

        public string getCDAcustodianAssignedCustodianRepresentedCustodianOrganizationDeterminerCode()
        {
            return getInnerTextSingle(CDAcustodianAssignedCustodianRepresentedCustodianOrganizationDeterminerCode);
        }

        public void setCDAcustodianAssignedCustodianRepresentedCustodianOrganizationDeterminerCode(string value)
        {
            setInnerTextSingle(CDAcustodianAssignedCustodianRepresentedCustodianOrganizationDeterminerCode, value);
        }

        public string getCDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdExtension()
        {
            return getInnerTextSingle(CDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdExtension);
        }

        public void setCDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdExtension(string value)
        {
            setInnerTextSingle(CDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdExtension, value);
        }

        public string getCDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdRoot()
        {
            return getInnerTextSingle(CDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdRoot);
        }

        public void setCDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdRoot(string value)
        {
            setInnerTextSingle(CDAcustodianAssignedCustodianAssignedCustodianRepresentedCustodianOrganizationIdRoot, value);
        }

        public string getCDAcustodianAssignedCustodianRepresentedCustodianOrganizationName()
        {
            return getInnerTextSingle(CDAcustodianAssignedCustodianRepresentedCustodianOrganizationName);
        }

        public void setCDAcustodianAssignedCustodianRepresentedCustodianOrganizationName(string value)
        {
            setInnerTextSingle(CDAcustodianAssignedCustodianRepresentedCustodianOrganizationName, value);
        }
        #endregion

        #region <!-- Mapping Province Chief CaseManager Data(Staff DB) -->
        public string getCDAlegalAuthenticatorContextControlCode()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorContextControlCode);
        }

        public void setCDAlegalAuthenticatorContextControlCode(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorContextControlCode, value);
        }

        public string getCDAlegalAuthenticatorTypeCode()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorTypeCode);
        }

        public void setCDAlegalAuthenticatorTypeCode(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorTypeCode, value);
        }

        public string getCDAlegalAuthenticatorTimeValue()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorTimeValue);
        }

        public void setCDAlegalAuthenticatorTimeValue(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorTimeValue, value);
        }

        public string getCDAlegalAuthenticatorSignatureCode()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorSignatureCode);
        }

        public void setCDAlegalAuthenticatorSignatureCode(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorSignatureCode, value);
        }

        public string getCDAlegalAuthenticatorAssignedEntityIdExtension()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorAssignedEntityIdExtension);
        }

        public void setCDAlegalAuthenticatorAssignedEntityIdExtension(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorAssignedEntityIdExtension, value);
        }

        public string getCDAlegalAuthenticatorAssignedEntityIdRoot()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorAssignedEntityIdRoot);
        }

        public void setCDAlegalAuthenticatorAssignedEntityIdRoot(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorAssignedEntityIdRoot, value);
        }

        public string getCDAlegalAuthenticatorAssignedEntityAssignedPersonNameGiven()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorAssignedEntityAssignedPersonNameGiven);
        }

        public void setCDAlegalAuthenticatorAssignedEntityAssignedPersonNameGiven(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorAssignedEntityAssignedPersonNameGiven, value);
        }

        public string getCDAlegalAuthenticatorAssignedEntityAssignedPersonNameFamily()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorAssignedEntityAssignedPersonNameFamily);
        }

        public void setCDAlegalAuthenticatorAssignedEntityAssignedPersonNameFamily(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorAssignedEntityAssignedPersonNameFamily, value);
        }

        public string getCDAlegalAuthenticatorAssignedEntityAssignedPersonNameSuffix()
        {
            return getInnerTextSingle(CDAlegalAuthenticatorAssignedEntityAssignedPersonNameSuffix);
        }

        public void setCDAlegalAuthenticatorAssignedEntityAssignedPersonNameSuffix(string value)
        {
            setInnerTextSingle(CDAlegalAuthenticatorAssignedEntityAssignedPersonNameSuffix, value);
        }
        #endregion

        #region <!-- Mapping CaseManager Data(Staff DB) -->
        public string getCDAauthenticatorTimeValue()
        {
            return getInnerTextSingle(CDAauthenticatorTimeValue);
        }

        public void setCDAauthenticatorTimeValue(string value)
        {
            setInnerTextSingle(CDAauthenticatorTimeValue, value);
        }

        public string getCDAauthenticatorSignatureCode()
        {
            return getInnerTextSingle(CDAauthenticatorSignatureCode);
        }

        public void setCDAauthenticatorSignatureCode(string value)
        {
            setInnerTextSingle(CDAauthenticatorSignatureCode, value);
        }

        public string getCDAauthenticatorAssignedEntityClassCode()
        {
            return getInnerTextSingle(CDAauthenticatorAssignedEntityClassCode);
        }

        public void setCDAauthenticatorAssignedEntityClassCode(string value)
        {
            setInnerTextSingle(CDAauthenticatorAssignedEntityClassCode, value);
        }

        public string getCDAauthenticatorAssignedEntityIdExtension()
        {
            return getInnerTextSingle(CDAauthenticatorAssignedEntityIdExtension);
        }

        public void setCDAauthenticatorAssignedEntityIdExtension(string value)
        {
            setInnerTextSingle(CDAauthenticatorAssignedEntityIdExtension, value);
        }

        public string getCDAauthenticatorAssignedEntityIdRoot()
        {
            return getInnerTextSingle(CDAauthenticatorAssignedEntityIdRoot);
        }

        public void setCDAauthenticatorAssignedEntityIdRoot(string value)
        {
            setInnerTextSingle(CDAauthenticatorAssignedEntityIdRoot, value);
        }

        public string getCDAauthenticatorAssignedEntityAssignedPersonNameGiven()
        {
            return getInnerTextSingle(CDAauthenticatorAssignedEntityAssignedPersonNameGiven);
        }

        public void setCDAauthenticatorAssignedEntityAssignedPersonNameGiven(string value)
        {
            setInnerTextSingle(CDAauthenticatorAssignedEntityAssignedPersonNameGiven, value);
        }

        public string getCDAauthenticatorAssignedEntityAssignedPersonNameFamily()
        {
            return getInnerTextSingle(CDAauthenticatorAssignedEntityAssignedPersonNameFamily);
        }

        public void setCDAauthenticatorAssignedEntityAssignedPersonNameFamily(string value)
        {
            setInnerTextSingle(CDAauthenticatorAssignedEntityAssignedPersonNameFamily, value);
        }

        public string getCDAauthenticatorAssignedEntityAssignedPersonNameSuffix()
        {
            return getInnerTextSingle(CDAauthenticatorAssignedEntityAssignedPersonNameSuffix);
        }

        public void setCDAauthenticatorAssignedEntityAssignedPersonNameSuffix(string value)
        {
            setInnerTextSingle(CDAauthenticatorAssignedEntityAssignedPersonNameSuffix, value);
        }
        #endregion

        #region The claim associated with this CDA.........
        public string getCDAinFulfillmentOfOrderIdExtension()
        {
            return getInnerTextSingle(CDAinFulfillmentOfOrderIdExtension);
        }

        public void setCDAinFulfillmentOfOrderIdExtension(string value)
        {
            setInnerTextSingle(CDAinFulfillmentOfOrderIdExtension, value);
        }

        public string getCDAinFulfillmentOfOrderIdRoot()
        {
            return getInnerTextSingle(CDAinFulfillmentOfOrderIdRoot);
        }

        public void setCDAinFulfillmentOfOrderIdRoot(string value)
        {
            setInnerTextSingle(CDAinFulfillmentOfOrderIdRoot, value);
        }
        #endregion

        #region <!--Date Range of Treatment -->
        public string getCDAdocumentationOfServiceEventCode()
        {
            return getInnerTextSingle(CDAdocumentationOfServiceEventCode);
        }

        public void setCDAdocumentationOfServiceEventCode(string value)
        {
            setInnerTextSingle(CDAdocumentationOfServiceEventCode, value);
        }

        public string getCDAdocumentationOfServiceEventCodeCodeSystem()
        {
            return getInnerTextSingle(CDAdocumentationOfServiceEventCodeCodeSystem);
        }

        public void setCDAdocumentationOfServiceEventCodeCodeSystem(string value)
        {
            setInnerTextSingle(CDAdocumentationOfServiceEventCodeCodeSystem, value);
        }

        public string getCDAdocumentationOfServiceEventCodeCodeSystemName()
        {
            return getInnerTextSingle(CDAdocumentationOfServiceEventCodeCodeSystemName);
        }

        public void setCDAdocumentationOfServiceEventCodeCodeSystemName(string value)
        {
            setInnerTextSingle(CDAdocumentationOfServiceEventCodeCodeSystemName, value);
        }

        public string getCDAdocumentationOfServiceEventCodeDisplayName()
        {
            return getInnerTextSingle(CDAdocumentationOfServiceEventCodeDisplayName);
        }

        public void setCDAdocumentationOfServiceEventCodeDisplayName(string value)
        {
            setInnerTextSingle(CDAdocumentationOfServiceEventCodeDisplayName, value);
        }

        public string getCDAdocumentationOfServiceEventEffectiveTimeLowValue(string code)
        {
            string tmp = CDAdocumentationOfServiceEventEffectiveTime + "[code/@code='" + code + "']/" + CDAdocumentationOfServiceEventEffectiveTimeLowValue;
            return getInnerTextSingle(tmp);
        }

        public void setCDAdocumentationOfServiceEventEffectiveTimeLowValue(string code, string value)
        {
            string tmp = CDAdocumentationOfServiceEventEffectiveTime + "[code/@code='" + code + "']/" + CDAdocumentationOfServiceEventEffectiveTimeLowValue;
            setInnerTextSingle(tmp, value);
        }

        public string getCDAdocumentationOfServiceEventEffectiveTimeHighValue(string code)
        {
            string tmp = CDAdocumentationOfServiceEventEffectiveTime + "[code/@code='" + code + "']/" + CDAdocumentationOfServiceEventEffectiveTimeHighValue;
            return getInnerTextSingle(tmp);
        }

        public void setCDAdocumentationOfServiceEventEffectiveTimeHighValue(string code, string value)
        {
            string tmp = CDAdocumentationOfServiceEventEffectiveTime + "[code/@code='" + code + "']/" + CDAdocumentationOfServiceEventEffectiveTimeHighValue;
            setInnerTextSingle(tmp, value);
        }
        #endregion

        #region component Header
        public string getCDAcomponentContextConductionInd()
        {
            return getInnerTextSingle(CDAcomponentContextConductionInd);
        }

        public void setCDAcomponentContextConductionInd(string value)
        {
            setInnerTextSingle(CDAcomponentContextConductionInd, value);
        }

        public string getCDAcomponentTypeCode()
        {
            return getInnerTextSingle(CDAcomponentTypeCode);
        }

        public void setCDAcomponentTypeCode(string value)
        {
            setInnerTextSingle(CDAcomponentTypeCode, value);
        }

        public string getCDAcomponentStructuredBodyComponentSectionCode()
        {
            return getInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCode);
        }

        public void setCDAcomponentStructuredBodyComponentSectionCode(string value)
        {
            setInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCode, value);
        }

        public string getCDAcomponentStructuredBodyComponentSectionCodeSystem()
        {
            return getInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCodeSystem);
        }

        public void setCDAcomponentStructuredBodyComponentSectionCodeSystem(string value)
        {
            setInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCodeSystem, value);
        }

        public string getCDAcomponentStructuredBodyComponentSectionCodeSystemName()
        {
            return getInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCodeSystemName);
        }

        public void setCDAcomponentStructuredBodyComponentSectionCodeSystemName(string value)
        {
            setInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCodeSystemName, value);
        }

        public string getCDAcomponentStructuredBodyComponentSectionCodeDisplayName()
        {
            return getInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCodeDisplayName);
        }

        public void setCDAcomponentStructuredBodyComponentSectionCodeDisplayName(string value)
        {
            setInnerTextSingle(CDAcomponentStructuredBodyComponentSectionCodeDisplayName, value);
        }

        public string getCDAcomponentStructuredBodyComponentSectionTitle()
        {
            return getInnerTextSingle(CDAcomponentStructuredBodyComponentSectionTitle);
        }

        public void setCDAcomponentStructuredBodyComponentSectionTitle(string value)
        {
            setInnerTextSingle(CDAcomponentStructuredBodyComponentSectionTitle, value);
        }
        #endregion

        #region component body
        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryTypeCode()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryTypeCode);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationClassCode()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationClassCode);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationMoodCode()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationMoodCode);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationTemplateIdRoot()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationTemplateIdRoot);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationIdRoot()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationIdRoot);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationCode()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationCode);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationCodeSystem()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationCodeSystem);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationDisplayName()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationDisplayName);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationStatusCode()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationStatusCode);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationEffectiveTimeCenterValue()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationEffectiveTimeCenterValue);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationValue()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationValue);
        }

        public List<string> getCDAListcomponentStructuredBodyComponentSectionEntryObservationValueXSItype()
        {
            return getInnerTextMultiple(CDAcomponentStructuredBodyComponentSectionEntryObservationValueXSItype);
        }
        #endregion

        public List<CDAEntry> getEntryList()
        {
            List<CDAEntry> output = new List<CDAEntry>();
            CDAEntry entry;

            List<string> typeCode = getCDAListcomponentStructuredBodyComponentSectionEntryTypeCode();
            List<string> observationClassCode = getCDAListcomponentStructuredBodyComponentSectionEntryObservationClassCode();
            List<string> observationMoodCode = getCDAListcomponentStructuredBodyComponentSectionEntryObservationMoodCode();
            List<string> observationTemplateIdRoot = getCDAListcomponentStructuredBodyComponentSectionEntryObservationTemplateIdRoot();
            List<string> observationIdRoot = getCDAListcomponentStructuredBodyComponentSectionEntryObservationIdRoot();
            List<string> observationCode = getCDAListcomponentStructuredBodyComponentSectionEntryObservationCode();
            List<string> observationCodeSystem = getCDAListcomponentStructuredBodyComponentSectionEntryObservationCodeSystem();
            List<string> observationDisplayName = getCDAListcomponentStructuredBodyComponentSectionEntryObservationDisplayName();
            List<string> observationStatusCode = getCDAListcomponentStructuredBodyComponentSectionEntryObservationStatusCode();
            List<string> observationEffectiveTimeCenterValue = getCDAListcomponentStructuredBodyComponentSectionEntryObservationEffectiveTimeCenterValue();
            List<string> observationValue = getCDAListcomponentStructuredBodyComponentSectionEntryObservationValue();
            List<string> observationValueXSItype = getCDAListcomponentStructuredBodyComponentSectionEntryObservationValueXSItype();

            for (int i = 0; i < typeCode.Count; i++)
            {
                entry = new CDAEntry();
                entry.typeCode = typeCode[i];
                entry.observationClassCode = observationClassCode[i];
                entry.observationMoodCode = observationMoodCode[i];
                entry.observationTemplateIdRoot = observationTemplateIdRoot[i];
                entry.observationIdRoot = observationIdRoot[i];
                entry.observationCode = observationCode[i];
                entry.observationCodeSystem = observationCodeSystem[i];
                entry.observationDisplayName = observationDisplayName[i];
                entry.observationStatusCode = observationStatusCode[i];
                entry.observationEffectiveTimeCenterValue = observationEffectiveTimeCenterValue[i];
                entry.observationValue = observationValue[i];
                entry.observationValueXSItype = observationValueXSItype[i];
                output.Add(entry);
            }
            return output;
        }

        #region List of patient
        public List<string> getCDAListComponentStructuredBodyComponentEntryTypeCode()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + "@typeCode");
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetContextControlCode()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetContextControlCode);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetTypeCode()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetTypeCode);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRoleClassCode()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRoleClassCode);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRoleIdExtension()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRoleIdExtension);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRoleIdRoot()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRoleIdRoot);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientNameGiven()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRolePatientNameGiven);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCode()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRolePatientAdministrativeGenderCode);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCodeSystemName()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRolePatientAdministrativeGenderCodeSystemName);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientNameFamily()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRolePatientNameFamily);
        }

        public List<string> getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientBirthTime()
        {
            return getInnerTextMultiple(CDApathToPatientEntry + CDArecordTargetPatientRolePatientBirthTime);
        }
        #endregion

        public List<CDAEntryOfPatient> getEntryOfPatientList()
        {
            List<CDAEntryOfPatient> output = new List<CDAEntryOfPatient>();
            CDAEntryOfPatient entry;

            List<string> typeCode = getCDAListComponentStructuredBodyComponentEntryTypeCode();
            List<string> recordTargetContextControlCode = getCDAListComponentStructuredBodyComponentEntryRecordTargetContextControlCode();
            List<string> recordTargetTypeCode = getCDAListComponentStructuredBodyComponentEntryRecordTargetTypeCode();
            List<string> patientRoleClassCode = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRoleClassCode();
            List<string> idExtension = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRoleIdExtension();
            List<string> idRoot = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRoleIdRoot();
            List<string> patientNameGiven = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientNameGiven();
            List<string> patientNameFamily = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCode();
            List<string> patientAdministrativeGenderCode = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCodeCodeSystem();
            List<string> patientAdministrativeGenderCodeSystem = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCodeSystemName();
            List<string> patientAdministrativeGenderCodeSystemName = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientAdministrativeGenderCodeDisplayName();
            List<string> patientAdministrativeGenderDisplayName = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientNameFamily();
            List<string> patientBirthTimeValue = getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientBirthTime();

            for (int i = 0; i < typeCode.Count; i++)
            {
                entry = new CDAEntryOfPatient();
                entry.typeCode = typeCode[i];
                entry.recordTargetContextControlCode = recordTargetContextControlCode[i];
                entry.recordTargetTypeCode = recordTargetTypeCode[i];
                entry.patientRoleClassCode = patientRoleClassCode[i];
                entry.idExtension = idExtension[i];
                entry.idRoot = idRoot[i];
                entry.patientNameGiven = patientNameGiven[i];
                entry.patientNameFamily = patientNameFamily[i];
                entry.patientAdministrativeGenderCode = patientAdministrativeGenderCode[i];
                entry.patientAdministrativeGenderCodeSystem = patientAdministrativeGenderCodeSystem[i];
                entry.patientAdministrativeGenderCodeSystemName = patientAdministrativeGenderCodeSystemName[i];
                entry.patientAdministrativeGenderDisplayName = patientAdministrativeGenderDisplayName[i];
                entry.patientBirthTimeValue = patientBirthTimeValue[i];
                output.Add(entry);
            }
            return output;
        }

    }
}