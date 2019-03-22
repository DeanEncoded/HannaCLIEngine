using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hanna_Studio
{
    public partial class dlgEditProperty : Form
    {

        string prop;
        frmWorkspace refworkspace;
        string sqid;
        public dlgEditProperty(string propToEdit,frmWorkspace workspace,string sqid=null)
        {
            InitializeComponent();
            prop = propToEdit;
            refworkspace = workspace;
            this.sqid = sqid;
            // we have an sqid because when we're editing a property with right click... we only want to affect sequence with id sqid... If it's null it will just edit the property that is selected in the workspace
        }

        private void DlgEditProperty_Load(object sender, EventArgs e)
        {
            labelEditProp.Text = "Editing property " + prop;
            // get the text of the current property from the current sequence
            if (prop == "mainText")
            {
                if(sqid == null)richTextBoxUpdateText.Text = refworkspace.getCurrentSequence().mainText.Replace("\\n","\n");
                else richTextBoxUpdateText.Text = refworkspace.getMySequences()[sqid].mainText.Replace("\\n", "\n");
            }
            else if (prop == "secondaryText") {
                if (sqid == null) richTextBoxUpdateText.Text = refworkspace.getCurrentSequence().secondaryText.Replace("\\n", "\n"); 
                else richTextBoxUpdateText.Text = refworkspace.getMySequences()[sqid].secondaryText.Replace("\\n", "\n");
            }
        }

        private void BtnCommit_Click(object sender, EventArgs e)
        {
            this.Close();
            string result = Regex.Replace(richTextBoxUpdateText.Text.ToString(), @"\r\n?|\n", "\\n"); // replace newlines with "new line string"
            refworkspace.updateProperty(prop,result,sqid);
        }
    }
}
