using LiteDB;
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
    public partial class frmHub : Form
    {
        public frmHub()
        {
            InitializeComponent();
        }

        private void FrmHub_Load(object sender, EventArgs e)
        {
            loadRecentFiles();
        }

        private void loadRecentFiles() {
            // load recent projects
            panelRecentProjects.Controls.Clear(); // clear panelRecentProjects Controls
            using (var db = new LiteDatabase(@"hstudio.db"))
            {
                var recentFiles = db.GetCollection<RecentFile>("recentFiles");

                if (recentFiles.Count() > 0)
                {
                    // there are some recent files
                    var r = recentFiles.FindAll();

                    // hide (No Recent Projects) label
                    labelNoRecents.Visible = false;

                    foreach (var rf in r)
                    {
                        RecentFilePanel rfPanel = new RecentFilePanel(rf.projectTitle, rf.filePath, this);
                        panelRecentProjects.Controls.Add(rfPanel);
                    }
                }
                else {
                    // there are no recent projects! OH NO!
                    // show (No Recent Projects) label
                    labelNoRecents.Visible = true;
                }
            }
        }

        public void deleteRecentFile(string filepath) {
            using (var db = new LiteDatabase(@"hstudio.db"))
            {
                var recentFiles = db.GetCollection<RecentFile>("recentFiles");

                recentFiles.EnsureIndex(fp => fp.filePath);
                recentFiles.Delete(fp => fp.filePath.StartsWith(filepath));
                // deleted!

                // now re-show recent files!
                loadRecentFiles();
         
            }
        }

        private void BtnNewProject_Click(object sender, EventArgs e)
        {
            dlgNewProject dlg = new dlgNewProject(this);
            dlg.ShowDialog();
        }

        public void startProjectCreation(string title,string author,frmWorkspace otherworkspace = null) {
            frmWorkspace workspace = new frmWorkspace(gameTitle: title,gameAuthor:author);
            this.Visible = false;
            workspace.Closed += (s, args) => this.Close();

            if (otherworkspace != null)
            {
                workspace.Closed += (s, args) => otherworkspace.Close();
                otherworkspace.Visible = false;
            }
            workspace.Show();
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            openProjectFile(openFileDialog.FileName);
        }

        public void openProjectFile(string file) {
            frmWorkspace workspace = new frmWorkspace(newProject: false, projectFile: file);
            this.Visible = false;
            workspace.Closed += (s, args) => this.Close();
            workspace.Show();
        }
    }
}
