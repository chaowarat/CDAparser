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
        [WebGet(UriTemplate = "/getEducationEvaluation")]
        XmlElement getEducationEvaluation();

        [OperationContract]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        void setEducationEvaluation(string data);
    }
}
