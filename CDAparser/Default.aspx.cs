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
            ServiceReference1.CasemanagerReceiverClient test = new ServiceReference1.CasemanagerReceiverClient();

            //XmlDocument doc = CDAsender.getCasemanagerProfileXML("2679900019289");
            //string a = test.getCasemanagerProfile("2679900019289");
            string[] result = test.getProfileList();
        }

    }
}