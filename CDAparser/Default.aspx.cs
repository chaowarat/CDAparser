using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Net;
using System.Text;

namespace CDAparser
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string productsFile = HttpContext.Current.Server.MapPath(@"XML/01_CDA_Education_Evaluation.xml");
            CDAEntryOfPatient ss = new CDAEntryOfPatient();
            XmlElement test1 = ss.getXMLelement(new XmlDocument());

            CDAparser parser = new CDAparser(productsFile);
            //parser.setCDAclassCode("Test");
            //CDAEntry cm = new CDAEntry();
            //cm.observationValue = "555555";

            //List<CDAEntryOfPatient> xxx = parser.getEntryOfPatientList();
            TextBoxOutput.Text = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

            //List<string> tmp = parser.getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientBirthTime();
            //foreach (string a in tmp)
            //{
                //ListBox1.Items.Add(a);
            //}

            //CDAEntryOfProfile cda = new CDAEntryOfProfile();
            //XmlElement test = cda.getXMLelement(new XmlDocument());
            //int a = 0;
            List<CDAEntry> test = parser.getEntryList("ED003");
            foreach (CDAEntry a in test)
            {
                ListBox1.Items.Add(a.typeCode + " " + a.observationCode);
            }
            callService();

        }

        public void callService()
        {
            try
            {
                string content;
                string Method = "POST";
                string uri = "http://localhost:64933/ServiceEducation.svc";

                HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = Method.ToUpper();

                string FilePath = HttpContext.Current.Server.MapPath(@"XML/01_CDA_Education_Evaluation.xml");
                content = (File.OpenText(@FilePath)).ReadToEnd();

                byte[] buffer = Encoding.ASCII.GetBytes(content);
                req.ContentLength = buffer.Length;
                req.ContentType = "text/xml";
                Stream PostData = req.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                if (req.HaveResponse == true && resp == null)
                {
                    String msg = "response was not returned or is null";
                    throw new WebException(msg);
                }

                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    String msg = "response with status: " + resp.StatusCode + " " + resp.StatusDescription;
                    throw new WebException(msg);
                }

                StreamReader ResponseStream = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                string Response = ResponseStream.ReadToEnd();
                ResponseStream.Close();
                resp.Close();
                Console.WriteLine(Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

    }
}