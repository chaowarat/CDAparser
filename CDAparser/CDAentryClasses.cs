using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace CDAparser
{
    public class CDAentryClasses
    {
    }

    public class CDAEntry
    {
        public string typeCode { get; set; }
        public string observationClassCode { get; set; }
        public string observationMoodCode { get; set; }
        public string observationCode { get; set; }
        public string observationValueCode { get; set; }
        public string observationLocalCode { get; set; }
        public string observationCodeSystem { get; set; }
        public string observationDisplayName { get; set; }
        public string observationEffectiveTimeCenterValue { get; set; }

        public CDAEntry()
        {
            this.typeCode = "DRIV";
            this.observationClassCode = "OBS";
            this.observationMoodCode = "RQO";
            this.observationValueCode = "";
            this.observationCode = ".......";
            this.observationLocalCode = ".......";
            this.observationCodeSystem = "2.16.840.1.113883.6.96";
            this.observationDisplayName = "";
            this.observationEffectiveTimeCenterValue = "";
        }

        public XmlElement getXMLelement(XmlDocument document)
        {
            XElement entry = new XElement("entry",
                                new XAttribute("typeCode", this.typeCode),
                            new XElement("observation",
                                new XAttribute("classCode", this.observationClassCode),
                                new XAttribute("moodCode", this.observationMoodCode),
                            new XElement("code",
                                new XAttribute("localCode", this.observationLocalCode),
                                new XAttribute("code", this.observationCode),
                                new XAttribute("codeSystem", this.observationCodeSystem),
                                new XAttribute("displayName", this.observationDisplayName)),
                            new XElement("value",
                                new XAttribute("code", this.observationValueCode)),
                            new XElement("effectiveTime",
                                new XElement("center",
                                    new XAttribute("value", this.observationEffectiveTimeCenterValue))
                            )
                        )
                    );
            return toXmlElement(entry, document);
        }

        public XmlElement toXmlElement(XElement xelement, XmlDocument document)
        {
            return document.ReadNode(xelement.CreateReader()) as XmlElement;
        }
    }

    public class CDAEntryOfPatient
    {
        public string typeCode { get; set; }
        public string recordTargetContextControlCode { get; set; }
        public string recordTargetTypeCode { get; set; }
        public string patientRoleClassCode { get; set; }
        public string idExtension { get; set; }
        public string idRoot { get; set; }
        public string patientNameGiven { get; set; }
        public string patientNameFamily { get; set; }
        public string patientAdministrativeGenderCode { get; set; }
        public string patientAdministrativeGenderCodeSystem { get; set; }
        public string patientAdministrativeGenderCodeSystemName { get; set; }
        public string patientAdministrativeGenderDisplayName { get; set; }
        public string patientBirthTimeValue { get; set; }

        public CDAEntryOfPatient()
        {
            this.typeCode = "DRIV";
            this.recordTargetContextControlCode = "OP";
            this.recordTargetTypeCode = "RCT";
            this.patientRoleClassCode = "PAT";
            this.idExtension = "";
            this.idRoot = "2.16.840.1.113883.19.2744.1.2";
            this.patientNameGiven = "";
            this.patientNameFamily = "";
            this.patientAdministrativeGenderCode = "";
            this.patientAdministrativeGenderCodeSystem = "2.16.840.1.113883.5.1";
            this.patientAdministrativeGenderCodeSystemName = "AdministrativeGender";
            this.patientAdministrativeGenderDisplayName = "";
            this.patientBirthTimeValue = "";
        }

        public XmlElement getXMLelement(XmlDocument document)
        {
            XElement entry = new XElement("entry",
                                new XAttribute("typeCode", this.typeCode),
                            new XElement("recordTarget",
                                new XAttribute("contextControlCode", this.recordTargetContextControlCode),
                                new XAttribute("typeCode", this.recordTargetTypeCode)),
                            new XElement("patientRole",
                                new XAttribute("classCode", this.patientRoleClassCode)),
                            new XElement("id",
                                new XAttribute("extension", this.idExtension),
                                new XAttribute("root", this.idRoot)),
                            new XElement("patient",
                                new XElement("name",
                                     new XElement("given", this.patientNameGiven),
                                     new XElement("family", this.patientNameFamily)),
                                new XElement("administrativeGenderCode",
                                    new XAttribute("code", this.patientAdministrativeGenderCode),
                                    new XAttribute("codeSystem", this.patientAdministrativeGenderCodeSystem),
                                    new XAttribute("codeSystemName", this.patientAdministrativeGenderCodeSystemName),
                                    new XAttribute("displayName", this.patientAdministrativeGenderDisplayName)),
                                new XElement("birthTime",
                                     new XAttribute("value", this.patientBirthTimeValue))
                            )
                        );
            return toXmlElement(entry, document);
        }

        public XmlElement toXmlElement(XElement xelement, XmlDocument document)
        {
            return document.ReadNode(xelement.CreateReader()) as XmlElement;
        }
    }


    public class CDAEntryOfProfile
    {
        public string typeCode { get; set; }
        public string recordTargetpPatientRoleIdExtension { get; set; }
        public string recordTargetpPatientRoleIdRoot { get; set; }
        //   Address
        public string recordTargetpPatientRoleAddrLiveHomeStatus { get; set; }
        public string recordTargetpPatientRoleAddrLiveHouseNumber { get; set; }
        public string recordTargetpPatientRoleAddrLiveMooNumber { get; set; }
        public string recordTargetpPatientRoleAddrLiveVillageName { get; set; }
        public string recordTargetpPatientRoleAddrLiveAlley { get; set; }
        public string recordTargetpPatientRoleAddrLiveStreetName { get; set; }
        public string recordTargetpPatientRoleAddrLiveTumbon { get; set; }
        public string recordTargetpPatientRoleAddrLiveCity { get; set; }
        public string recordTargetpPatientRoleAddrLiveProvince { get; set; }
        public string recordTargetpPatientRoleAddrLivePostCode { get; set; }
        public string recordTargetpPatientRoleAddrCensusHouseNumber { get; set; }
        public string recordTargetpPatientRoleAddrCensusMooNumber { get; set; }
        public string recordTargetpPatientRoleAddrCensusVillageName { get; set; }
        public string recordTargetpPatientRoleAddrCensusAlley { get; set; }
        public string recordTargetpPatientRoleAddrCensusStreetName { get; set; }
        public string recordTargetpPatientRoleAddrCensusTumbon { get; set; }
        public string recordTargetpPatientRoleAddrCensusCity { get; set; }
        public string recordTargetpPatientRoleAddrCensusProvince { get; set; }
        public string recordTargetpPatientRoleAddrCensusMoveInDate { get; set; }
        public string recordTargetpPatientRoleAddrCensusMoveOutDate { get; set; }
        public string recordTargetpPatientRoleAddrCensusMoveOutReason { get; set; }
        public string recordTargetpPatientRoleHomePhoneValue { get; set; }
        public string recordTargetpPatientRoleHomePhoneUse { get; set; }
        public string recordTargetpPatientRoleMobileValue { get; set; }
        // End address
        public string recordTargetpPatientRolePatientNameTitle { get; set; }
        public string recordTargetpPatientRolePatientNameGiven { get; set; }
        public string recordTargetpPatientRolePatientNameFamily { get; set; }
        public string recordTargetpPatientRolePatientAdministrativeGenderCodeCode { get; set; }
        public string recordTargetpPatientRolePatientAdministrativeGenderCodeCodeSystem { get; set; }
        public string recordTargetpPatientRolePatientBirthTimeValue { get; set; }
        public string recordTargetpPatientRolePatientSubWelfareIDValue { get; set; }
        public string recordTargetpPatientRolePatientBloodTypeValue { get; set; }
        public string recordTargetpPatientRolePatientNationValue { get; set; }
        public string recordTargetpPatientRolePatientRaceValue { get; set; }
        public string recordTargetpPatientRolePatientReligionValue { get; set; }
        public string recordTargetpPatientRolePatientForeignerValue { get; set; }
        public string recordTargetpPatientRolePatientChildPicValue { get; set; }
        public string recordTargetpPatientRolePatientChildPicCIDValue { get; set; }

        public CDAEntryOfProfile()
        {
            this.typeCode = "DRIV";
            this.recordTargetpPatientRoleIdExtension = "1111111111111";
            this.recordTargetpPatientRoleIdRoot = "2.16.840.1.113883.19.5";
            //   Address
            this.recordTargetpPatientRoleAddrLiveHomeStatus = "1";
            this.recordTargetpPatientRoleAddrLiveHouseNumber = "2222 Home Street";
            this.recordTargetpPatientRoleAddrLiveMooNumber = "2";
            this.recordTargetpPatientRoleAddrLiveVillageName = "...";
            this.recordTargetpPatientRoleAddrLiveAlley = "...";
            this.recordTargetpPatientRoleAddrLiveStreetName = "...";
            this.recordTargetpPatientRoleAddrLiveTumbon = "440101";
            this.recordTargetpPatientRoleAddrLiveCity = "4401";
            this.recordTargetpPatientRoleAddrLiveProvince = "44";
            this.recordTargetpPatientRoleAddrLivePostCode = "...";
            this.recordTargetpPatientRoleAddrCensusHouseNumber = "...";
            this.recordTargetpPatientRoleAddrCensusMooNumber = "...";
            this.recordTargetpPatientRoleAddrCensusVillageName = "...";
            this.recordTargetpPatientRoleAddrCensusAlley = "...";
            this.recordTargetpPatientRoleAddrCensusStreetName = "...";
            this.recordTargetpPatientRoleAddrCensusTumbon = "440101";
            this.recordTargetpPatientRoleAddrCensusCity = "4401";
            this.recordTargetpPatientRoleAddrCensusProvince = "44";
            this.recordTargetpPatientRoleAddrCensusMoveInDate = "20120501";
            this.recordTargetpPatientRoleAddrCensusMoveOutDate = "";
            this.recordTargetpPatientRoleAddrCensusMoveOutReason = "";
            // End address
            this.recordTargetpPatientRoleHomePhoneValue = "";
            this.recordTargetpPatientRoleHomePhoneUse = "";
            this.recordTargetpPatientRoleMobileValue = "";
            this.recordTargetpPatientRolePatientNameTitle = "1";
            this.recordTargetpPatientRolePatientNameGiven = "A";
            this.recordTargetpPatientRolePatientNameFamily = "B";
            this.recordTargetpPatientRolePatientAdministrativeGenderCodeCode = "M";
            this.recordTargetpPatientRolePatientAdministrativeGenderCodeCodeSystem = "2.16.840.1.113883.5.1";
            this.recordTargetpPatientRolePatientBirthTimeValue = "19720924";
            this.recordTargetpPatientRolePatientSubWelfareIDValue = "";
            this.recordTargetpPatientRolePatientBloodTypeValue = "";
            this.recordTargetpPatientRolePatientNationValue = "";
            this.recordTargetpPatientRolePatientRaceValue = "";
            this.recordTargetpPatientRolePatientReligionValue = "";
            this.recordTargetpPatientRolePatientForeignerValue = "";
            this.recordTargetpPatientRolePatientChildPicValue = "";
            this.recordTargetpPatientRolePatientChildPicCIDValue = "";
        }

        public XmlElement getXMLelement(XmlDocument document)
        {
            XElement entry = new XElement("entry",
                                new XAttribute("typeCode", this.typeCode),
                            new XElement("recordTarget",
                            new XElement("patientRole",
                            new XElement("id",
                                new XAttribute("extension", this.recordTargetpPatientRoleIdExtension),
                                new XAttribute("root", this.recordTargetpPatientRoleIdRoot)),
                            new XElement("addr",
                                new XElement("LiveHomeStatus", this.recordTargetpPatientRoleAddrLiveHomeStatus),
                                new XElement("LiveHouseNumber", this.recordTargetpPatientRoleAddrLiveHouseNumber),
                                new XElement("LiveMooNumber", this.recordTargetpPatientRoleAddrLiveMooNumber),
                                new XElement("LiveVillageName", this.recordTargetpPatientRoleAddrLiveVillageName),
                                new XElement("LiveAlley", this.recordTargetpPatientRoleAddrLiveAlley),
                                new XElement("LiveStreetName", this.recordTargetpPatientRoleAddrLiveStreetName),
                                new XElement("LiveTumbon", this.recordTargetpPatientRoleAddrLiveTumbon),
                                new XElement("LiveCity", this.recordTargetpPatientRoleAddrLiveCity),
                                new XElement("LiveProvince", this.recordTargetpPatientRoleAddrLiveProvince),
                                new XElement("LivePostCode", this.recordTargetpPatientRoleAddrLivePostCode),
                                new XElement("CensusHouseNumber", this.recordTargetpPatientRoleAddrCensusHouseNumber),
                                new XElement("CensusMooNumber", this.recordTargetpPatientRoleAddrCensusMooNumber),
                                new XElement("CensusVillageName", this.recordTargetpPatientRoleAddrCensusVillageName),
                                new XElement("CensusAlley", this.recordTargetpPatientRoleAddrCensusAlley),
                                new XElement("CensusStreetName", this.recordTargetpPatientRoleAddrCensusStreetName),
                                new XElement("CensusTumbon", this.recordTargetpPatientRoleAddrCensusTumbon),
                                new XElement("CensusCity", this.recordTargetpPatientRoleAddrCensusCity),
                                new XElement("CensusProvince", this.recordTargetpPatientRoleAddrCensusProvince),
                                new XElement("CensusMoveInDate", this.recordTargetpPatientRoleAddrCensusMoveInDate),
                                new XElement("CensusMoveOutDate", this.recordTargetpPatientRoleAddrCensusMoveOutDate),
                                new XElement("CensusMoveOutReason", this.recordTargetpPatientRoleAddrCensusMoveOutReason)),
                            new XElement("HomePhone",
                                new XAttribute("value", this.recordTargetpPatientRoleHomePhoneValue),
                                new XAttribute("use", this.recordTargetpPatientRoleHomePhoneUse)),
                            new XElement("Mobile",
                                new XAttribute("value", this.recordTargetpPatientRoleMobileValue)),
                            new XElement("patient",
                                new XElement("name",
                                    new XElement("title", this.recordTargetpPatientRolePatientNameTitle),
                                    new XElement("given", this.recordTargetpPatientRolePatientNameGiven),
                                    new XElement("family", this.recordTargetpPatientRolePatientNameFamily)),
                            new XElement("administrativeGenderCode",
                                new XAttribute("code", this.recordTargetpPatientRolePatientAdministrativeGenderCodeCode),
                                new XAttribute("codeSystem", this.recordTargetpPatientRolePatientAdministrativeGenderCodeCodeSystem)),
                            new XElement("birthTime",
                                new XAttribute("value", this.recordTargetpPatientRolePatientBirthTimeValue)),
                            new XElement("subWelfareID",
                                new XAttribute("value", this.recordTargetpPatientRolePatientSubWelfareIDValue)),
                            new XElement("bloodType",
                                new XAttribute("value", this.recordTargetpPatientRolePatientBloodTypeValue)),
                            new XElement("nation",
                                new XAttribute("value", this.recordTargetpPatientRolePatientNationValue)),
                            new XElement("race",
                                new XAttribute("value", this.recordTargetpPatientRolePatientRaceValue)),
                            new XElement("religion",
                                new XAttribute("value", this.recordTargetpPatientRolePatientReligionValue)),
                            new XElement("foreigner",
                                new XAttribute("value", this.recordTargetpPatientRolePatientForeignerValue)),
                            new XElement("childPic",
                                new XAttribute("value", this.recordTargetpPatientRolePatientChildPicValue)),
                            new XElement("childPicCID",
                                new XAttribute("value", this.recordTargetpPatientRolePatientChildPicCIDValue))
                            )))
                        );
            return toXmlElement(entry, document);
        }

        public XmlElement toXmlElement(XElement xelement, XmlDocument document)
        {
            return document.ReadNode(xelement.CreateReader()) as XmlElement;
        }
    }

}