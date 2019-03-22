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
    public partial class dlgNewProject : Form
    {

        frmHub hub;
        frmWorkspace otherworkspace;
        public dlgNewProject(frmHub gethub,frmWorkspace otherworkspace = null)
        {
            InitializeComponent();
            hub = gethub;
            this.otherworkspace = otherworkspace;
        }

        private void DlgNewProject_Load(object sender, EventArgs e)
        {
            // hmmm
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {

            if (textBoxTitle.Text.Length > 0 && textBoxAuthor.Text.Length > 0)
            {
                hub.startProjectCreation(textBoxTitle.Text, textBoxAuthor.Text,otherworkspace:otherworkspace);
                this.Close();
            }
            else {
                MessageBox.Show("Please type in a title and an author name for your project");
            }

        }
    }
}
