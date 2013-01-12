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
            string productsFile = Path.Combine(Request.PhysicalApplicationPath, "CDA_PMJ_ToVilage_ListofDisablePerson.xml");
            CDAEntryOfPatient ss = new CDAEntryOfPatient();
            ss.getXMLelement(new XmlDocument());

            CDAparser parser = new CDAparser(productsFile);
            //parser.setCDAclassCode("Test");
            CDAEntry cm = new CDAEntry();
            cm.observationValue = "555555";

            List<CDAEntryOfPatient> xxx = parser.getEntryOfPatientList();
            //TextBoxOutput.Text = parser.getCDAcomponentStructuredBodyComponentSectionTitle();

            List<string> tmp = parser.getCDAListComponentStructuredBodyComponentEntryRecordTargetPatientRolePatientBirthTime();
            foreach (string a in tmp)
            {
                ListBox1.Items.Add(a);
            }

        }
    }
}