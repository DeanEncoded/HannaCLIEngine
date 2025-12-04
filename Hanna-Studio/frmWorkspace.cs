using LiteDB;
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
using Hanna_Studio.NodeEditor;

namespace Hanna_Studio
{
    public partial class frmWorkspace : Form
    {

        // status bar colors
        public static string statusBarColorReady { get; } = "#1c73ff";
        public static string statusBarColorRunning { get; } = "#ff810c";

        // create varibales to store all of our sequences and their data
        private Dictionary<string, Sequence> mySequences = new Dictionary<string, Sequence>();

        // "templated-properties" for the user
        public string defaultMainTextProp, defaultSecondaryTextProp;

        // for recognizing which sq showing props
        public string sqInProp;

        // project meta
        public string gameTitle, gameAuthor, gameDescription;
        public List<String> projectContainers;

        // Project File for remembering where to save a project
        private string projectFile;

        // Node Editor
        private NodeEditorPanel nodeEditorPanel;
        private NodeEditorState nodeEditorState;
        private SplitContainer nodeEditorSplitContainer;
        private ToolStripMenuItem viewNodeEditorMenuItem;
        private ToolStripMenuItem viewConsoleMenuItem;

        public frmWorkspace(string gameTitle = "New Project", string gameAuthor = "New Author", string gameDescription = "No Description", bool newProject = true, string projectFile = null)
        {
            InitializeComponent();
            DoubleBuffered = true; // double-buffering reduces flicker???
            // init templated-properties

            // init projectContainers
            projectContainers = new List<String>();

            // Initialize Node Editor
            InitializeNodeEditor();

            // Add View menu
            AddViewMenu();

            // if this is a new project. Go ahead and just leave it be. But name it with what came from the user
            if (newProject)
            {
                this.gameTitle = gameTitle;
                this.gameAuthor = gameAuthor;
                this.gameDescription = gameDescription;
                // we're alright!
                Text = gameTitle + " - Hanna Studio";

                showPopup("New project created!");
            }
            else {
                // this is an existing project being launched
                StudioObject so;
                BinarySystem bs = new BinarySystem();
                so = bs.ReadFromBinaryFile<StudioObject>(projectFile);

                Text = so.projectName + " - Hanna Studio";

                this.gameTitle = so.projectName;
                this.gameAuthor = so.projectAuthor;
                this.gameDescription = so.projectDescription;
                this.projectContainers = so.projectContainers;

                // load our sequences as well
                mySequences = so.sequences;

                // Load node editor state if available
                if (so.nodeEditorState != null)
                {
                    nodeEditorState = so.nodeEditorState;
                }

                displaySequences();
            }
            this.projectFile = projectFile;
            if(!newProject) checkRecentFile();

            workspaceConsole.InternalRichTextBox.BorderStyle = BorderStyle.None; // removing 3D border from the Console RichTextBox

        }

        private void InitializeNodeEditor()
        {
            // Create a split container to hold both node editor and console
            nodeEditorSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 400,
                BackColor = Color.FromArgb(30, 30, 30),
                SplitterWidth = 6
            };

            // Create node editor panel
            nodeEditorPanel = new NodeEditorPanel(this);
            nodeEditorPanel.Dock = DockStyle.Fill;

            // Wire up events
            nodeEditorPanel.SequenceSelected += NodeEditor_SequenceSelected;
            nodeEditorPanel.SequenceDoubleClicked += NodeEditor_SequenceDoubleClicked;
            nodeEditorPanel.RequestDeleteSequence += NodeEditor_RequestDeleteSequence;
            nodeEditorPanel.RequestNewSequence += NodeEditor_RequestNewSequence;

            // Move existing console to panel2
            consoleAreaPanel.Controls.Remove(workspaceConsole);

            // Create a panel to hold console with header
            var consolePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black
            };

            var consoleHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 28,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            var consoleLabel = new Label
            {
                Text = "Console Output",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 6),
                Font = new Font("Segoe UI", 9f)
            };

            consoleHeader.Controls.Add(consoleLabel);
            consolePanel.Controls.Add(workspaceConsole);
            consolePanel.Controls.Add(consoleHeader);
            workspaceConsole.Dock = DockStyle.Fill;

            // Add to split container
            nodeEditorSplitContainer.Panel1.Controls.Add(nodeEditorPanel);
            nodeEditorSplitContainer.Panel2.Controls.Add(consolePanel);

            // Add split container to console area
            consoleAreaPanel.Controls.Add(nodeEditorSplitContainer);
        }

        private void AddViewMenu()
        {
            // Create View menu
            var viewMenu = new ToolStripMenuItem("View");

            viewNodeEditorMenuItem = new ToolStripMenuItem("Node Editor")
            {
                Checked = true,
                CheckOnClick = true
            };
            viewNodeEditorMenuItem.Click += (s, e) =>
            {
                nodeEditorSplitContainer.Panel1Collapsed = !viewNodeEditorMenuItem.Checked;
            };

            viewConsoleMenuItem = new ToolStripMenuItem("Console")
            {
                Checked = true,
                CheckOnClick = true
            };
            viewConsoleMenuItem.Click += (s, e) =>
            {
                nodeEditorSplitContainer.Panel2Collapsed = !viewConsoleMenuItem.Checked;
            };

            var fitNodesMenuItem = new ToolStripMenuItem("Fit All Nodes")
            {
                ShortcutKeys = Keys.Control | Keys.F
            }; ;
            fitNodesMenuItem.Click += (s, e) =>
            {
                nodeEditorPanel?.Canvas?.FitAllNodes();
            };

            var resetViewMenuItem = new ToolStripMenuItem("Reset View")
            {
                ShortcutKeys = Keys.Control | Keys.Home
            };
            resetViewMenuItem.Click += (s, e) =>
            {
                nodeEditorPanel?.Canvas?.ResetView();
            };

            viewMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                viewNodeEditorMenuItem,
                viewConsoleMenuItem,
                new ToolStripSeparator(),
                fitNodesMenuItem,
                resetViewMenuItem
            });

            // Insert after Edit menu
            workspaceMenuStrip.Items.Insert(2, viewMenu);
        }

        private void NodeEditor_SequenceSelected(object sender, string sequenceId)
        {
            if (!string.IsNullOrEmpty(sequenceId))
            {
                showSequenceProps(sequenceId);
            }
            else
            {
                clearsqInProp();
            }
        }

        private void NodeEditor_SequenceDoubleClicked(object sender, string sequenceId)
        {
            if (!string.IsNullOrEmpty(sequenceId))
            {
                showSequenceProps(sequenceId);
                // Open edit dialog for mainText
                dlgEditProperty dlg = new dlgEditProperty("mainText", this);
                dlg.ShowDialog();
            }
        }

        private void NodeEditor_RequestDeleteSequence(object sender, string sequenceId)
        {
            if (!string.IsNullOrEmpty(sequenceId))
            {
                var result = MessageBox.Show($"Are you sure you want to delete sequence '{sequenceId}'?",
                    "Delete Sequence", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    deleteSequence(sequenceId);
                }
            }
        }

        private void NodeEditor_RequestNewSequence(object sender, Point position)
        {
            dlgNewSequence newSequence = new dlgNewSequence(this, new PointF(position.X, position.Y));
            newSequence.ShowDialog();
        }

        private void SyncNodeEditor()
        {
            if (nodeEditorPanel != null)
            {
                nodeEditorPanel.SyncWithSequences(mySequences, comboBoxStartSq.Text);

                // Load saved state if available
                if (nodeEditorState != null)
                {
                    nodeEditorPanel.LoadState(nodeEditorState);
                    nodeEditorState = null; // Clear after loading
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            // show new sequence dialog
            dlgNewSequence newSequence = new dlgNewSequence(this);
            newSequence.ShowDialog();

        }

        private void BtnAddChoice_Click(object sender, EventArgs e)
        {
            // show new choice dialog
            dlgNewChoice newChoice = new dlgNewChoice(this);
            newChoice.ShowDialog();

        }

        private void BtnManageContainers_Click(object sender, EventArgs e)
        {
            dlgManageContainers mC = new dlgManageContainers(this);
            mC.ShowDialog();
        }

        public void createSequence(string sqid, bool endsq) {
            createSequence(sqid, endsq, null);
        }

        public void createSequence(string sqid, bool endsq, PointF? nodePosition) {
            // check if this sequence exists or not
            if (!mySequences.ContainsKey(sqid)) // I should really look into these already-available functions, they help a lot
            {
                // this sequence can be created!
                SequencePanel sequencePanel = new SequencePanel(sqid, this);
                sqFlowPanel.Controls.Add(sequencePanel);
                // how are we going to map this sequence to the panel??? Through the tag I guess?
                Sequence newSequence = new Sequence();
                newSequence.id = sqid;
                if (endsq) newSequence.type = "end";
                // Add new sequence to sequences dictionary
                mySequences.Add(sqid, newSequence);
                loadSequencesIntoStartSq();

                // Add to node editor
                if (nodeEditorPanel != null)
                {
                    var node = nodeEditorPanel.AddSequenceNode(sqid, newSequence, nodePosition);
                    nodeEditorPanel.Canvas.CenterOnNode(node);
                }
            }
            else {
                // nope... you can't create that sequence
                MessageBox.Show("A Sequence with the same id already exists");
            }
            
        }

        public void createChoice(string choiceLetter, string choiceType, List<string> choiceCondition, string choiceText, string outcomeText, string containerAdd, string containerAddValue, string nextSq)
        {
            //string choiceLetter = new ChoiceLetters().letters[choiceLetterIndex];
            ChoicePanel choicePanel = new ChoicePanel(getCurrentSequence().id, choiceLetter, this);
            choicesFlowPanel.Controls.Add(choicePanel);
            //public Choice(string letter, string type, string text, string outcometext, string nextSq)
            Choice newChoice = new Choice(choiceLetter, choiceType, choiceCondition, choiceText, outcomeText, containerAdd, containerAddValue, nextSq);
            // Add new choice to current sequence choices
            getCurrentSequence().choices.Add(choiceLetter, newChoice);

            // Update node editor
            if (nodeEditorPanel != null)
            {
                var currentSeq = getCurrentSequence();
                nodeEditorPanel.UpdateSequenceNode(currentSeq.id, currentSeq);

                // Add connection if nextSq is specified
                if (!string.IsNullOrEmpty(nextSq))
                {
                    var sourceNode = nodeEditorPanel.Canvas.FindNodeBySequenceId(currentSeq.id);
                    var targetNode = nodeEditorPanel.Canvas.FindNodeBySequenceId(nextSq);
                    if (sourceNode != null && targetNode != null)
                    {
                        nodeEditorPanel.Canvas.AddConnection(sourceNode, choiceLetter, targetNode);
                    }
                }
            }
        }

        public void editChoice(string choiceLetter, string choiceType, List<string> choiceCondition, string choiceText, string outcomeText, string containerAdd, string containerAddValue, string nextSq)
        {
            Choice newChoice = new Choice(choiceLetter, choiceType, choiceCondition, choiceText, outcomeText, containerAdd, containerAddValue, nextSq);
            // Add new choice to current sequence choices
            getCurrentSequence().choices[choiceLetter] = newChoice;

            // Update node editor connections
            if (nodeEditorPanel != null)
            {
                var currentSeq = getCurrentSequence();
                var sourceNode = nodeEditorPanel.Canvas.FindNodeBySequenceId(currentSeq.id);
                
                if (sourceNode != null)
                {
                    // Remove old connections for this choice
                    nodeEditorPanel.Canvas.RemoveConnectionsForChoice(sourceNode, choiceLetter);

                    // Add new connection if nextSq is specified
                    if (!string.IsNullOrEmpty(nextSq))
                    {
                        var targetNode = nodeEditorPanel.Canvas.FindNodeBySequenceId(nextSq);
                        if (targetNode != null)
                        {
                            nodeEditorPanel.Canvas.AddConnection(sourceNode, choiceLetter, targetNode);
                        }
                    }
                }
            }
        }



        public void deleteSequence(string sqid) {
            // This below code has been moved from SequencePanel to here (frmWorkspace)
            // delete this control!
            // we need to get the flowpanel from workspace and delete this specific control
            foreach (SequencePanel sqPanel in getSequencesPanel().Controls)
            {
                if (sqPanel.Tag.ToString() == sqid)
                {
                    // Remove this panel!
                    getSequencesPanel().Controls.Remove(sqPanel);
                    // we need to also delete the sequence from the dictionary!
                    mySequences.Remove(sqid);
                    if (sqInProp == sqid)
                    {
                        // remove this as sqinProp!
                        // there should be a function at our refworkspace to do this for us!
                        clearsqInProp();
                    }
                }
            }

            // Remove from node editor
            if (nodeEditorPanel != null)
            {
                nodeEditorPanel.RemoveSequenceNode(sqid);
            }

            loadSequencesIntoStartSq();
        }
        public void deleteChoice(string sqid, string letter)
        {
            mySequences[sqid].choices.Remove(letter);

            // Update node editor
            if (nodeEditorPanel != null)
            {
                var sourceNode = nodeEditorPanel.Canvas.FindNodeBySequenceId(sqid);
                if (sourceNode != null)
                {
                    nodeEditorPanel.Canvas.RemoveConnectionsForChoice(sourceNode, letter);
                    nodeEditorPanel.UpdateSequenceNode(sqid, mySequences[sqid]);
                }
            }
        }
        public void clearsqInProp() {
            sqInProp = null;
            // disable the property controls becuase they're going to cause havoc if not disabled!
            // just disable the whole panel with the controls
            propsPanel.Enabled = false;
            choicesContainerPanel.Enabled = false;
            clearChoicesFlow(); // if any choices are visible in there
        }
        private void SqProp_TextChanged(object sender, EventArgs e)
        {
            var prop = (TextBox)sender;
            if (prop.Text != null)
            {

                string tag = prop.Tag.ToString();
                switch (tag)
                {
                    case "changeType":
                        // change the type only
                        mySequences[sqInProp].type = prop.Text.ToString();
                        break;
                    case "changeMainText":
                        // change the type only
                        mySequences[sqInProp].mainText = prop.Text.ToString();
                        break;
                    case "changeSecondaryText":
                        // change the type only
                        mySequences[sqInProp].secondaryText = prop.Text.ToString();
                        break;
                }
            }
        }

        public void showSequenceProps(string sqid) {
            // get the sequence needed using sqid
            Sequence sq = mySequences[sqid];
            sqInProp = sqid; // set sequence as current sequence
            sqIdTextBox.Text = sq.id;
            sqMainTextTextBox.Text = sq.mainText;
            sqSecondaryTextTextBox.Text = sq.secondaryText;
            // showing if this sequence is an end sequence or not
            checkBoxEndsq.Checked = false;
            if (sq.type == "end") checkBoxEndsq.Checked = true;

            // change the background color of this sequence panel! As well
            foreach (SequencePanel c in sqFlowPanel.Controls) {
                if (c.Tag.ToString() == sqid)
                {
                    c.setActive();
                }
                else {
                    c.setInActive();
                }
            }
            // enable the flow panel for props and choices
            propsPanel.Enabled = true;
            choicesContainerPanel.Enabled = true;
            // lets also display choices in this sequence!
            displayChoices();
        }

        // Function for displaying sequences
        private void displaySequences()
        {
            // loop through the sequences in my sequences
            foreach (KeyValuePair<string, Sequence> s in mySequences)
            {
                SequencePanel sP = new SequencePanel(s.Key, this);
                sqFlowPanel.Controls.Add(sP);
                // that should work right???
            }
            loadSequencesIntoStartSq();

            // Sync node editor after loading sequences
            SyncNodeEditor();
        }

        private void displayChoices() {
            // loop through the choices of the current sequence
            clearChoicesFlow(); // clear the flow panel just in case
            foreach (KeyValuePair<string, Choice> c in getCurrentSequence().choices) {
                // c.Key just points to the choice letter of this choice.
                ChoicePanel choicePanel = new ChoicePanel(getCurrentSequence().id, c.Key, this);
                choicesFlowPanel.Controls.Add(choicePanel);
            }
        }

        private void clearChoicesFlow()
        {
            choicesFlowPanel.Controls.Clear();
        }


        //public FlowLayoutPanel getSequencesPanel() => sqFlowPanel;
        public Panel getSequencesPanel() => sqFlowPanel;
        public Panel getChoicesPanel() => choicesFlowPanel;

        // loading sequences into the startsq combobox
        private void loadSequencesIntoStartSq() {
            List<string> s = new List<string>();
            foreach (Control c in getSequencesPanel().Controls)
            {
                s.Add(c.Tag.ToString());
            }
            comboBoxStartSq.DataSource = null;
            comboBoxStartSq.DataSource = s;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // check to see if projectFile is null
            // if it isn't check if that file in it exists.
            // if it doesn't..... show the dialog anyways...
            // if it does... just save the file
            if (projectFile != null)
            {
                // there's something in projectFile
                writeToFile(projectFile);
            }
            else {
                saveFileDialog.FileName = gameTitle;
                saveFileDialog.ShowDialog();
            }

        }

        private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            saveGameFile(saveFileDialog.FileName);
            // we've saved to a new file!..... let's save this to recent files
            checkRecentFile();
        }

        private void saveGameFile(string filename) {
            writeToFile(filename);
            projectFile = filename;
        }

        private void checkRecentFile() {
            using (var db = new LiteDatabase(@"hstudio.db"))
            {
                var recentFiles = db.GetCollection<RecentFile>("recentFiles");
                recentFiles.EnsureIndex(x => x.filePath);

                // Use Linq to query documents
                var results = recentFiles.Find(x => x.filePath.StartsWith(projectFile));
                if (results.Count() < 1)
                {
                    // there are no recent files with this path.... save it to recent files!
                    var rF = new RecentFile()
                    {
                        projectTitle = gameTitle,
                        filePath = projectFile
                    };

                    recentFiles.Insert(rF);
                    // that's it really isn't it????
                }
                else {
                    // delete and insert. To make it go on top.... don't judge me :)
                    recentFiles.Delete(x => x.filePath.StartsWith(projectFile));
                    var rF = new RecentFile()
                    {
                        projectTitle = gameTitle,
                        filePath = projectFile
                    };
                    recentFiles.Insert(rF);
                    // Reusing code....... Sigh.......
                }


            }
        }

        private void writeToFile(string filePath) {
            // Save node editor state
            NodeEditorState editorState = null;
            if (nodeEditorPanel != null)
            {
                editorState = nodeEditorPanel.GetState();
            }

            StudioObject so = new StudioObject(gameTitle, gameAuthor, projectDescription:gameDescription, projectContainers, mySequences, editorState);
            BinarySystem bs = new BinarySystem();
            bs.WriteToBinaryFile<StudioObject>(filePath, so);
            //MessageBox.Show("Project Saved!");
            // show our amateur custom popup
            showPopup("Project saved");
        }

        private void BtnRunP_Click(object sender, EventArgs e)
        {
            RunGame();
        }

        private void BtnStopRun_Click(object sender, EventArgs e)
        {
            StopConsole();
        }

        public Sequence getCurrentSequence() => mySequences[sqInProp];
        public Dictionary<string, Sequence> getMySequences() => mySequences;

        public void startChoiceEdit(string cletter, Choice choice) {
            dlgNewChoice newSequence = new dlgNewChoice(this, letter: cletter, choiceToEdit: choice);
            newSequence.ShowDialog();
        }

        private void FrmWorkspace_Load(object sender, EventArgs e)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey800, Primary.Grey400, Primary.Grey100, Accent.LightBlue200, TextShade.WHITE);
        }

        private void CheckBoxEndsq_Click(object sender, EventArgs e)
        {
            if (checkBoxEndsq.Checked)
            {
                // set current sequence as end sequence
                mySequences[sqInProp].type = "end";
            }
            else
            {
                // set current sequence as ordinary sequence
                mySequences[sqInProp].type = "ordinary";
            }

            // Update node editor
            if (nodeEditorPanel != null)
            {
                nodeEditorPanel.UpdateSequenceNode(sqInProp, mySequences[sqInProp]);
            }
        }

        private void ExportProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // set the name of the file to the project title
            exportGameFileDialog.FileName = gameTitle;
            exportGameFileDialog.ShowDialog();
        }

        private void ExportGameFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            // open to the requested file
            Exporter ex = new Exporter(gameTitle, gameAuthor, gameDesc: gameDescription, mySequences, projectContainers, comboBoxStartSq.Text);
            if (ex.exportToFile(true, (sender as SaveFileDialog).FileName)) MessageBox.Show("Game was exported successfully", "Export Successful");
            else MessageBox.Show("Game failed to export", "Export Failed");
        }


        // editing a sequence property
        private void BtnEditProp_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            // get this tag to see which property we're editing and send it to dlgEditProperty
            dlgEditProperty dlg = new dlgEditProperty(btn.Tag.ToString(), this);
            dlg.ShowDialog();
        }

        public void updateProperty(string prop, string text,string sqid) {
            string sqidUsed = sqInProp;
            if (sqid != null) sqidUsed = sqid;
            if (prop == "mainText")
            {
                mySequences[sqidUsed].mainText = text;
            }
            else if (prop == "secondaryText") {
                mySequences[sqidUsed].secondaryText = text;
            }
            if(sqid == null || sqid == sqInProp) showSequenceProps(sqInProp);

            // Update node editor
            if (nodeEditorPanel != null && mySequences.ContainsKey(sqidUsed))
            {
                nodeEditorPanel.UpdateSequenceNode(sqidUsed, mySequences[sqidUsed]);
            }
        }

        private void SaveASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = gameTitle;
            saveFileDialog.ShowDialog();
        }

        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This is not yet working. It will be working soon I promise. For now just restart Hanna Studio to create a new project");
            // let me actually make this work
            // we're going to need to call the dlgNewProject... but it needs somekind of context of frmHub....
            // We're also going to need to dispose of this current workspace.

            frmHub hub = new frmHub();
            dlgNewProject newProject = new dlgNewProject(hub,this);

            newProject.Show();
            // lets give it a test run!
            // seems to be working ¯\_(ツ)_/¯

        }

        private void AboutHannaStudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgAboutHanna dlg = new dlgAboutHanna();
            dlg.ShowDialog();
        }

        private void WorkspaceConsole_OnConsoleOutput(object sender, ConsoleControl.ConsoleEventArgs args)
        {
            if (workspaceConsole.IsProcessRunning)
            {
                // if a process is still running. Enable stop button
                btnStopP.Enabled = true;
                // disable run button
                btnRunP.Enabled = false;
            }
            else {
                updateStatusbar("Ready", statusBarColorReady);
                // disable stop button
                btnStopP.Enabled = false;
                // enable run button
                btnRunP.Enabled = true;
            }
        }

        // PLAY AREA
        private void RunGame()
        {
            if (comboBoxStartSq.Text.Length > 0)
            {
                Exporter ex = new Exporter(gameTitle, gameAuthor,gameDesc:gameDescription, mySequences, projectContainers, comboBoxStartSq.Text);
                if (ex.exportToFile())
                {
                    // clear the console first
                    workspaceConsole.ClearOutput();
                    // save the game file
                    // IMPORTANT: Sometimes the studio freezes when you run a game....
                    // only save gamefile if projectFile is not equal to null....
                    if (projectFile != null) saveGameFile(projectFile); // elon musk sent a car to space
                    // if debug mode is on... then include debug as an argument
                    string _params = "export.json";
                    if (checkBoxDebugMode.Checked) _params += " debug"; 
                    workspaceConsole.StartProcess("hannacli", _params);
                    // change status bar
                    updateStatusbar("Running " + gameTitle, statusBarColorRunning);

                    // Show console when running
                    if (nodeEditorSplitContainer != null && nodeEditorSplitContainer.Panel2Collapsed)
                    {
                        nodeEditorSplitContainer.Panel2Collapsed = false;
                        if (viewConsoleMenuItem != null) viewConsoleMenuItem.Checked = true;
                    }
                }
            }
            else {
                MessageBox.Show("What???? What are you trying to run?????");
            }
       
        }

        private void SequenceContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // When the SequenceContextMenuStrip opens... show the sequence id at the first item
            var menu = (ContextMenuStrip)sender;
            var menuItem = menu.Items[0];
            menuItem.Text = menu.SourceControl.Tag.ToString();
        }

        private void DeleteThisSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var menu = (ContextMenuStrip) menuItem.Owner;

            string idofpanel = menu.SourceControl.Tag.ToString();
            // delete this sequence
            deleteSequence(idofpanel);
        }


        // event handler for editing mainText and secondaryText from rightclicking a sequence
        private void EditPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripItem)sender;

            // // get the sequence id of the panel rightclicked
            var menu = (ContextMenuStrip)menuItem.Owner;
            string idofpanel = menu.SourceControl.Tag.ToString();

            // get this tag to see which property we're editing and send it to dlgEditProperty
            dlgEditProperty dlg = new dlgEditProperty(menuItem.Tag.ToString(), this,sqid:idofpanel);
            dlg.ShowDialog();
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            openProjectFile(openFileDialog.FileName);
        }

        public void openProjectFile(string file)
        {
            frmWorkspace newworkspace = new frmWorkspace(newProject: false, projectFile: file);
            this.Visible = false;
            newworkspace.Closed += (s, args) => this.Close();
            newworkspace.Show();
        }

        private void FrmWorkspace_Resize(object sender, EventArgs e)
        {
            // move the popup panel accordingly
            popupPanel.Location = new Point(popupPanel.Location.X, this.Bottom - 170);
        }

        private void PopupTimer_Tick(object sender, EventArgs e)
        {
            popupPanel.Visible = false;
            popupTimer.Enabled = false;
        }

        private void showPopup(string message) {
            popupPanel.Visible = true;
            popupTimer.Enabled = true;
            popupMessage.Text = message;
        }


        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void StopConsole()
        {
            workspaceConsole.StopProcess();
            workspaceConsole.ClearOutput();
        }

        private void updateStatusbar(string message, string colorHex) {
            panelStatusBar.BackColor = System.Drawing.ColorTranslator.FromHtml(colorHex);
            labelStatusBarMessage.Text = message;
        }

    }


}