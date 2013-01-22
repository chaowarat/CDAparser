﻿using System;
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

        public XmlElement getEducationEvaluation(string cid, string planNo, string caseNo)
        {
            return CDAsender.getEducationEvaluationXML(cid, planNo, caseNo).DocumentElement;
        }

        public XmlElement getEducationPlanning(string cid, string planNo, string caseNo)
        {
            return CDAsender.getEducationPlanningXML(cid, planNo, caseNo).DocumentElement;
        }

        public XmlElement getEducationServiceProvision(string cid, string planNo, string caseNo)
        {
            return CDAsender.getEducationServiceProvisionXML(cid, planNo, caseNo).DocumentElement;
        }

        public XmlElement getEducationDevelopment(string cid, string planNo, string caseNo)
        {
            return CDAsender.getEducationDevelopmentXML(cid, planNo, caseNo).DocumentElement;
        }

        public XmlElement getEducationProfile(string cid)
        {
            return CDAsender.getEducationProfileXML(cid).DocumentElement;
        }

        public XmlElement getEducationMilk()
        {
            return CDAsender.getEducationMilkXML().DocumentElement;
        }

        ///////////////////////////////////
        //// casemanager
        ///////////////////////////////////

        public XmlElement getCasemanagerProfile(string cid)
        {
            return CDAsender.getCasemanagerProfileXML(cid).DocumentElement;
        }

        public XmlElement getCasemanagerQuestionare(string cid, string planNo)
        {
            return CDAsender.getCasemanagerQuestionareXML(cid, planNo).DocumentElement;
        }

        public XmlElement getCasemanagerPlanning(string cid, string planNo)
        {
            return CDAsender.getCasemanagerPlanningXML(cid, planNo).DocumentElement;
        }

    }
}
