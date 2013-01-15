using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

namespace CDAparser
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string productsFile = Path.Combine(Request.PhysicalApplicationPath, @"XML/01_CDA_Education_Evaluation.xml");
            CDAEntryOfPatient ss = new CDAEntryOfPatient();
            XmlElement test1 = ss.getXMLelement(new XmlDocument());

            CDAparser parser = new CDAparser(productsFile);
            //parser.setCDAclassCode("Test");
            //CDAEntry cm = new CDAEntry();
            //cm.observationValue = "555555";

            //List<CDAEntryOfPatient> xxx = parser.getEntryOfPatientList();
            //TextBoxOutput.Text = parser.test();

            //List<string> tmp = parser.getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientBirthTime();
            //foreach (string a in tmp)
            //{
                //ListBox1.Items.Add(a);
            //}

            //CDAEntryOfProfile cda = new CDAEntryOfProfile();
            //XmlElement test = cda.getXMLelement(new XmlDocument());
            //int a = 0;
            List<CDAEntry> test = parser.getEntryList("ED006");
            foreach (CDAEntry a in test)
            {
                ListBox1.Items.Add(a.typeCode + " " + a.observationDisplayName);
            }

        }
    }
}