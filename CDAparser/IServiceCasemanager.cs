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
    [ServiceContract]
    public interface IServiceCasemanager
    {
        [OperationContract]
        [WebGet(UriTemplate = "/test")]
        string DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "/getCasemanagerPlanning")]
        XmlElement sendCasemanagerPlanning();
    }
}
