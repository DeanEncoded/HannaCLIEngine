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
    public partial class dlgManageContainers : Form
    {

        frmWorkspace refworkpace;
        public dlgManageContainers(frmWorkspace workspace)
        {
            InitializeComponent();
            refworkpace = workspace;
            showAvailableContainers();
        }

        private void BtnAddContainer_Click(object sender, EventArgs e)
        {
            // lets add to containers using our ref workspace

            if (textBoxContainerName.Text.Length > 0) {
            refworkpace.projectContainers.Add(textBoxContainerName.Text);
            // testing so let this just go in
            MessageBox.Show(textBoxContainerName.Text + " added to containers!");
            textBoxContainerName.Text = "";
            showAvailableContainers();
            }

        }

        private void showAvailableContainers() {
            listBoxContainers.DataSource = null;
            listBoxContainers.DataSource = refworkpace.projectContainers;
        }
    }
}
