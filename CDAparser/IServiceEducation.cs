using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace CDAparser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceEducation" in both code and config file together.
    [ServiceContract]
    public interface IServiceEducation
    {
        [OperationContract]
        [WebGet(UriTemplate = "/getEducationEvaluation?cid={cid}&planNo={planNo}&caseNo={caseNo}")]
        XmlElement getEducationEvaluation(string cid, string planNo, string caseNo);

        [OperationContract]
        [WebGet(UriTemplate = "/getEducationPlanning?cid={cid}&planNo={planNo}&caseNo={caseNo}")]
        XmlElement getEducationPlanning(string cid, string planNo, string caseNo);

        [OperationContract]
        [WebGet(UriTemplate = "/getEducationServiceProvision?cid={cid}&planNo={planNo}&caseNo={caseNo}")]
        XmlElement getEducationServiceProvision(string cid, string planNo, string caseNo);

        [OperationContract]
        [WebGet(UriTemplate = "/getEducationDevelopment?cid={cid}&planNo={planNo}&caseNo={caseNo}")]
        XmlElement getEducationDevelopment(string cid, string planNo, string caseNo);

        [OperationContract]
        [WebGet(UriTemplate = "/getEducationProfile?cid={cid}&planNo={planNo}&caseNo={caseNo}")]
        XmlElement getEducationProfile(string cid, string planNo, string caseNo);

        [OperationContract]
        [WebGet(UriTemplate = "/getEducationMilk")]
        XmlElement getEducationMilk();

        //[OperationContract]
        //[WebInvoke(UriTemplate = "", Method = "POST")]
        //void setEducationEvaluation(string data);
    }
}
