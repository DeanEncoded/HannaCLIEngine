using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hanna_Studio
{
    public partial class dlgNewSequence : Form
    {

        frmWorkspace refWorkspace;

        public dlgNewSequence(frmWorkspace workspace)
        {
            InitializeComponent();
            refWorkspace = workspace;
            //this.Sw
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            this.Close();
            refWorkspace.createSequence(textBoxSequenceId.Text,checkBoxEndsq.Checked);
        }

        private void DlgNewSequence_Load(object sender, EventArgs e)
        {
            // Dark theme for MaterialSkin items
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey800, Primary.Grey900, Primary.Grey500, Accent.LightBlue200, TextShade.WHITE);


        }
    }
}
