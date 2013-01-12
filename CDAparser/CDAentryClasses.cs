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
        public string observationTemplateIdRoot { get; set; }
        public string observationIdRoot { get; set; }
        public string observationCode { get; set; }
        public string observationCodeSystem { get; set; }
        public string observationDisplayName { get; set; }
        public string observationStatusCode { get; set; }
        public string observationEffectiveTimeCenterValue { get; set; }
        public string observationValue { get; set; }
        public string observationValueXSItype { get; set; }

        public CDAEntry()
        {
            this.typeCode = "DRIV";
            this.observationClassCode = "OBS";
            this.observationMoodCode = "RQO";
            this.observationTemplateIdRoot = "2.16.840.1.113883.10.20.1.25";
            this.observationIdRoot = "9a6d1bac-17d3-4195-89a4-1121bc809b4a";
            this.observationCode = ".......";
            this.observationCodeSystem = "2.16.840.1.113883.6.96";
            this.observationDisplayName = "";
            this.observationStatusCode = "new";
            this.observationEffectiveTimeCenterValue = "";
            this.observationValue = "";
            this.observationValueXSItype = "ED";
        }

        public XmlElement getXMLelement(XmlDocument document)
        {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XElement entry =new XElement("entry",
                                new XAttribute("typeCode", this.typeCode),
                            new XElement("observation",
                                new XAttribute("classCode", this.observationClassCode),
                                new XAttribute("moodCode", this.observationMoodCode),
                            new XElement("templateId",
                                new XAttribute("root", this.observationTemplateIdRoot)),
                            new XElement("id",
                                new XAttribute("root", this.observationIdRoot)),
                            new XElement("code",
                                new XAttribute("code", this.observationCode),
                                new XAttribute("codeSystem", this.observationCodeSystem),
                                new XAttribute("displayName", this.observationDisplayName)),
                            new XElement("statusCode",
                                new XAttribute("code", this.observationStatusCode)),
                            new XElement("effectiveTime",
                                new XElement("center",
                                    new XAttribute("value", this.observationEffectiveTimeCenterValue))    
                            ),
                            new XElement("value",this.observationValue,
                                new XAttribute(xsi + "type", this.observationValueXSItype))
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
}