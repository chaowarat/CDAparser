﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace CDAparser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISDHS" in both code and config file together.
    [ServiceContract]
    public interface ISDHS
    {
        [OperationContract]
        [WebGet(UriTemplate = "/getSDHSProfile")]
        XmlElement getSDHSProfile();
    }
}