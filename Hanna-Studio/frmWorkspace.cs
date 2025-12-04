using LiteDB;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hanna_Studio.NodeEditor;
using Hanna_Studio.UI;

namespace Hanna_Studio
{
    public partial class frmWorkspace : Form
    {

        // status bar colors
        public static Color statusBarColorReady = UIStyles.StatusReady;
        public static Color statusBarColorRunning = UIStyles.StatusRunning;

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

        // Toast notification
        private ToastNotification toastNotification;

        public frmWorkspace(string gameTitle = "New Project", string gameAuthor = "New Author", string gameDescription = "No Description", bool newProject = true, string projectFile = null)
        {
            InitializeComponent();
            
            // Apply professional UI styling
            ApplyProfessionalStyling();

            DoubleBuffered = true;

            // init projectContainers
            projectContainers = new List<String>();

            // Initialize Node Editor
            InitializeNodeEditor();

            // Add View menu
            AddViewMenu();

            // Initialize toast notification
            InitializeToast();

            // if this is a new project
            if (newProject)
            {
                this.gameTitle = gameTitle;
                this.gameAuthor = gameAuthor;
                this.gameDescription = gameDescription;
                Text = gameTitle + " - Hanna Studio";
                ShowToast("New project created!", ToastNotification.ToastType.Success);
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
                mySequences = so.sequences;

                if (so.nodeEditorState != null)
                {
                    nodeEditorState = so.nodeEditorState;
                }

                displaySequences();
            }
            this.projectFile = projectFile;
            if(!newProject) checkRecentFile();

            workspaceConsole.InternalRichTextBox.BorderStyle = BorderStyle.None;
        }

        private void ApplyProfessionalStyling()
        {
            // Form styling
            BackColor = UIStyles.BackgroundDark;
            ForeColor = UIStyles.TextPrimary;

            // Menu strip styling
            UIStyles.StyleMenuStrip(workspaceMenuStrip);

            // Status bar styling
            panelStatusBar.BackColor = UIStyles.StatusReady;
            panelStatusBar.Height = 24;
            labelStatusBarMessage.Font = UIStyles.FontSmall;
            labelStatusBarMessage.ForeColor = Color.White;
            labelStatusBarMessage.Location = new Point(10, 4);

            // Style left panel (Sequences)
            StyleSequencesPanel();

            // Style right panel (Properties/Choices)
            StylePropertiesPanel();

            // Style run config panel
            StyleRunConfigPanel();

            // Style context menus
            StyleContextMenus();
        }

        private void StyleSequencesPanel()
        {
            // Header panel
            gradientPanel1.Controls.Clear();
            gradientPanel1.ColorTop = UIStyles.BackgroundLight;
            gradientPanel1.ColorBottom = UIStyles.BackgroundLight;
            gradientPanel1.Height = 44;

            var headerLabel = new Label
            {
                Text = "SEQUENCES",
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = UIStyles.TextSecondary,
                AutoSize = true,
                Location = new Point(14, 14),
                BackColor = Color.Transparent
            };

            var btnAdd = UIStyles.CreateIconButton("+", "Add Sequence", Button1_Click);
            btnAdd.Dock = DockStyle.Right;
            btnAdd.Width = 40;
            btnAdd.BackColor = Color.Transparent;

            var btnContainers = UIStyles.CreateIconButton("≡", "Manage Containers", BtnManageContainers_Click);
            btnContainers.Dock = DockStyle.Right;
            btnContainers.Width = 40;
            btnContainers.BackColor = Color.Transparent;

            gradientPanel1.Controls.Add(headerLabel);
            gradientPanel1.Controls.Add(btnAdd);
            gradientPanel1.Controls.Add(btnContainers);

            // Flow panel
            sqFlowPanel.BackColor = UIStyles.BackgroundMedium;
            sqFlowPanel.Padding = new Padding(4);
        }

        private void StylePropertiesPanel()
        {
            // Properties header
            gradientPanel2.Controls.Clear();
            gradientPanel2.ColorTop = UIStyles.BackgroundLight;
            gradientPanel2.ColorBottom = UIStyles.BackgroundLight;
            gradientPanel2.Height = 44;

            var propsLabel = new Label
            {
                Text = "PROPERTIES",
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = UIStyles.TextSecondary,
                AutoSize = true,
                Location = new Point(14, 14),
                BackColor = Color.Transparent
            };
            gradientPanel2.Controls.Add(propsLabel);

            // Properties panel
            propsPanel.BackColor = UIStyles.BackgroundMedium;
            
            // Style property rows
            StylePropertyRow(panelProp, label3, sqIdTextBox, null);
            StylePropertyRow(panel2, label5, sqMainTextTextBox, btnEditProp1);
            StylePropertyRow(panel3, label6, sqSecondaryTextTextBox, btnEditProp2);
            
            panel4.BackColor = UIStyles.BackgroundMedium;

            // Choices header
            gradientPanel3.Controls.Clear();
            gradientPanel3.ColorTop = UIStyles.BackgroundLight;
            gradientPanel3.ColorBottom = UIStyles.BackgroundLight;
            gradientPanel3.Height = 44;

            var choicesLabel = new Label
            {
                Text = "CHOICES",
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = UIStyles.TextSecondary,
                AutoSize = true,
                Location = new Point(14, 14),
                BackColor = Color.Transparent
            };

            var btnAddChoice = UIStyles.CreateIconButton("+", "Add Choice", BtnAddChoice_Click);
            btnAddChoice.Dock = DockStyle.Right;
            btnAddChoice.Width = 40;
            btnAddChoice.BackColor = Color.Transparent;

            gradientPanel3.Controls.Add(choicesLabel);
            gradientPanel3.Controls.Add(btnAddChoice);

            // Choices panel
            choicesFlowPanel.BackColor = UIStyles.BackgroundMedium;
        }

        private void StylePropertyRow(Panel panel, Label label, TextBox textBox, Button editBtn)
        {
            panel.BackColor = UIStyles.BackgroundMedium;
            panel.Height = 48;
            panel.Padding = new Padding(12, 8, 8, 8);

            label.Font = UIStyles.FontSmall;
            label.ForeColor = UIStyles.TextMuted;
            label.Location = new Point(14, 8);

            textBox.BackColor = UIStyles.BackgroundDark;
            textBox.ForeColor = UIStyles.TextPrimary;
            textBox.Font = UIStyles.FontRegular;
            textBox.BorderStyle = BorderStyle.None;
            textBox.Location = new Point(14, 24);
            textBox.Width = panel.Width - 70;
            textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            if (editBtn != null)
            {
                editBtn.FlatStyle = FlatStyle.Flat;
                editBtn.FlatAppearance.BorderSize = 0;
                editBtn.BackColor = Color.Transparent;
                editBtn.ForeColor = UIStyles.TextSecondary;
                editBtn.Text = "✎";
                editBtn.Font = new Font("Segoe UI", 12f);
                editBtn.Width = 36;
                editBtn.FlatAppearance.MouseOverBackColor = UIStyles.BackgroundHover;
            }
        }

        private void StyleRunConfigPanel()
        {
            runConfigPanel.BackColor = UIStyles.BackgroundLight;
            runConfigPanel.Height = 52;
            runConfigPanel.Padding = new Padding(8, 8, 8, 8);

            // Style Run button
            btnRunP.FlatStyle = FlatStyle.Flat;
            btnRunP.BackColor = UIStyles.AccentGreen;
            btnRunP.ForeColor = Color.White;
            btnRunP.Font = UIStyles.FontMedium;
            btnRunP.FlatAppearance.BorderSize = 0;
            btnRunP.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 180, 100);
            btnRunP.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 140, 60);
            btnRunP.Text = "▶  Run";
            btnRunP.Size = new Size(100, 36);
            btnRunP.Location = new Point(10, 8);
            btnRunP.Cursor = Cursors.Hand;

            // Style Stop button
            btnStopP.FlatStyle = FlatStyle.Flat;
            btnStopP.BackColor = UIStyles.BackgroundPanel;
            btnStopP.ForeColor = UIStyles.TextPrimary;
            btnStopP.Font = UIStyles.FontMedium;
            btnStopP.FlatAppearance.BorderSize = 1;
            btnStopP.FlatAppearance.BorderColor = UIStyles.BorderDark;
            btnStopP.FlatAppearance.MouseOverBackColor = UIStyles.AccentRed;
            btnStopP.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 50, 50);
            btnStopP.Text = "■  Stop";
            btnStopP.Size = new Size(90, 36);
            btnStopP.Location = new Point(118, 8);
            btnStopP.Cursor = Cursors.Hand;

            // Style the right panel containing debug and start sequence
            panel1.BackColor = UIStyles.BackgroundLight;
            panel1.Dock = DockStyle.Right;
            panel1.Width = 360;
            panel1.Padding = new Padding(0);

            // Clear and rebuild panel1 with styled controls
            panel1.Controls.Clear();

            // Create a toggle button for debug mode instead of checkbox
            var debugToggle = new Button
            {
                Text = "Debug",
                Font = UIStyles.FontMedium,
                ForeColor = UIStyles.TextSecondary,
                BackColor = UIStyles.BackgroundPanel,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(90, 36),
                Location = new Point(10, 8),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            debugToggle.FlatAppearance.BorderSize = 1;
            debugToggle.FlatAppearance.BorderColor = UIStyles.BorderDark;
            debugToggle.FlatAppearance.MouseOverBackColor = UIStyles.BackgroundHover;

            // Sync initial state
            UpdateDebugToggleAppearance(debugToggle, checkBoxDebugMode.Checked);

            debugToggle.Click += (s, e) =>
            {
                checkBoxDebugMode.Checked = !checkBoxDebugMode.Checked;
                UpdateDebugToggleAppearance(debugToggle, checkBoxDebugMode.Checked);
            };

            // Create start sequence label
            var startLabel = new Label
            {
                Text = "START SEQUENCE",
                Font = new Font("Segoe UI", 7f, FontStyle.Bold),
                ForeColor = UIStyles.TextMuted,
                AutoSize = true,
                Location = new Point(115, 4),
                BackColor = Color.Transparent
            };

            // Style the combo box container
            var comboContainer = new Panel
            {
                Size = new Size(220, 32),
                Location = new Point(112, 16),
                BackColor = UIStyles.BackgroundDark,
                Padding = new Padding(1)
            };
            comboContainer.Paint += (s, e) =>
            {
                using (var pen = new Pen(UIStyles.BorderDark))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, comboContainer.Width - 1, comboContainer.Height - 1);
                }
            };

            // Style combo box
            comboBoxStartSq.Parent = comboContainer;
            comboBoxStartSq.BackColor = UIStyles.BackgroundDark;
            comboBoxStartSq.ForeColor = UIStyles.TextPrimary;
            comboBoxStartSq.Font = UIStyles.FontRegular;
            comboBoxStartSq.FlatStyle = FlatStyle.Flat;
            comboBoxStartSq.Location = new Point(4, 4);
            comboBoxStartSq.Size = new Size(212, 24);
            comboBoxStartSq.DropDownStyle = ComboBoxStyle.DropDownList;

            // Add controls to panel1
            panel1.Controls.Add(debugToggle);
            panel1.Controls.Add(startLabel);
            panel1.Controls.Add(comboContainer);

            // Hide original controls that are replaced
            checkBoxDebugMode.Visible = false;
            label4.Visible = false;
        }

        private void UpdateDebugToggleAppearance(Button toggle, bool isChecked)
        {
            if (isChecked)
            {
                toggle.BackColor = UIStyles.AccentBlue;
                toggle.ForeColor = Color.White;
                toggle.FlatAppearance.BorderColor = UIStyles.AccentBlue;
                toggle.Text = "● Debug";
            }
            else
            {
                toggle.BackColor = UIStyles.BackgroundPanel;
                toggle.ForeColor = UIStyles.TextSecondary;
                toggle.FlatAppearance.BorderColor = UIStyles.BorderDark;
                toggle.Text = "○ Debug";
            }
        }

        private void StyleContextMenus()
        {
            sequenceContextMenuStrip.BackColor = UIStyles.BackgroundLight;
            sequenceContextMenuStrip.ForeColor = UIStyles.TextPrimary;
            sequenceContextMenuStrip.Renderer = new DarkMenuRenderer();

            foreach (ToolStripItem item in sequenceContextMenuStrip.Items)
            {
                item.ForeColor = UIStyles.TextPrimary;
            }
        }

        private void InitializeToast()
        {
            toastNotification = new ToastNotification();
            toastNotification.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(toastNotification);
            toastNotification.BringToFront();
            UpdateToastPosition();
        }

        private void UpdateToastPosition()
        {
            if (toastNotification != null)
            {
                toastNotification.Location = new Point(20, Height - toastNotification.Height - 50);
            }
        }

        private void ShowToast(string message, ToastNotification.ToastType type = ToastNotification.ToastType.Info)
        {
            toastNotification?.Show(message, type);
        }

        private void InitializeNodeEditor()
        {
            // Create a split container to hold both node editor and console
            nodeEditorSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 400,
                BackColor = UIStyles.BackgroundDark,
                SplitterWidth = 4
            };

            // Style the splitter
            nodeEditorSplitContainer.Panel1.BackColor = UIStyles.BackgroundMedium;
            nodeEditorSplitContainer.Panel2.BackColor = UIStyles.BackgroundMedium;

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
                BackColor = UIStyles.BackgroundDark
            };

            var consoleHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 32,
                BackColor = UIStyles.BackgroundLight
            };

            var consoleLabel = new Label
            {
                Text = "CONSOLE OUTPUT",
                Font = new Font("Segoe UI Semibold", 8f, FontStyle.Bold),
                ForeColor = UIStyles.TextMuted,
                AutoSize = true,
                Location = new Point(12, 9)
            };

            var clearBtn = UIStyles.CreateIconButton("✕", "Clear Console");
            clearBtn.Dock = DockStyle.Right;
            clearBtn.Width = 32;
            clearBtn.BackColor = Color.Transparent;
            clearBtn.Click += (s, e) => workspaceConsole.ClearOutput();

            consoleHeader.Controls.Add(consoleLabel);
            consoleHeader.Controls.Add(clearBtn);
            
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
                    ShowToast($"Sequence '{sequenceId}' deleted", ToastNotification.ToastType.Info);
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

                if (nodeEditorState != null)
                {
                    nodeEditorPanel.LoadState(nodeEditorState);
                    nodeEditorState = null;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dlgNewSequence newSequence = new dlgNewSequence(this);
            newSequence.ShowDialog();
        }

        private void BtnAddChoice_Click(object sender, EventArgs e)
        {
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
            if (!mySequences.ContainsKey(sqid))
            {
                SequencePanel sequencePanel = new SequencePanel(sqid, this);
                sqFlowPanel.Controls.Add(sequencePanel);
                Sequence newSequence = new Sequence();
                newSequence.id = sqid;
                if (endsq) newSequence.type = "end";
                mySequences.Add(sqid, newSequence);
                loadSequencesIntoStartSq();

                if (nodeEditorPanel != null)
                {
                    var node = nodeEditorPanel.AddSequenceNode(sqid, newSequence, nodePosition);
                    nodeEditorPanel.Canvas.CenterOnNode(node);
                }

                ShowToast($"Sequence '{sqid}' created", ToastNotification.ToastType.Success);
            }
            else {
                MessageBox.Show("A Sequence with the same id already exists", "Duplicate Sequence", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void createChoice(string choiceLetter, string choiceType, List<string> choiceCondition, string choiceText, string outcomeText, string containerAdd, string containerAddValue, string nextSq)
        {
            ChoicePanel choicePanel = new ChoicePanel(getCurrentSequence().id, choiceLetter, this);
            choicesFlowPanel.Controls.Add(choicePanel);
            Choice newChoice = new Choice(choiceLetter, choiceType, choiceCondition, choiceText, outcomeText, containerAdd, containerAddValue, nextSq);
            getCurrentSequence().choices.Add(choiceLetter, newChoice);

            if (nodeEditorPanel != null)
            {
                var currentSeq = getCurrentSequence();
                nodeEditorPanel.UpdateSequenceNode(currentSeq.id, currentSeq);

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
            getCurrentSequence().choices[choiceLetter] = newChoice;

            if (nodeEditorPanel != null)
            {
                var currentSeq = getCurrentSequence();
                var sourceNode = nodeEditorPanel.Canvas.FindNodeBySequenceId(currentSeq.id);
                
                if (sourceNode != null)
                {
                    nodeEditorPanel.Canvas.RemoveConnectionsForChoice(sourceNode, choiceLetter);

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
            foreach (SequencePanel sqPanel in getSequencesPanel().Controls)
            {
                if (sqPanel.Tag.ToString() == sqid)
                {
                    getSequencesPanel().Controls.Remove(sqPanel);
                    mySequences.Remove(sqid);
                    if (sqInProp == sqid)
                    {
                        clearsqInProp();
                    }
                }
            }

            if (nodeEditorPanel != null)
            {
                nodeEditorPanel.RemoveSequenceNode(sqid);
            }

            loadSequencesIntoStartSq();
        }

        public void deleteChoice(string sqid, string letter)
        {
            mySequences[sqid].choices.Remove(letter);

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
            propsPanel.Enabled = false;
            choicesContainerPanel.Enabled = false;
            clearChoicesFlow();
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
                        mySequences[sqInProp].type = prop.Text.ToString();
                        break;
                    case "changeMainText":
                        mySequences[sqInProp].mainText = prop.Text.ToString();
                        break;
                    case "changeSecondaryText":
                        mySequences[sqInProp].secondaryText = prop.Text.ToString();
                        break;
                }
            }
        }

        public void showSequenceProps(string sqid) {
            Sequence sq = mySequences[sqid];
            sqInProp = sqid;
            sqIdTextBox.Text = sq.id;
            sqMainTextTextBox.Text = sq.mainText;
            sqSecondaryTextTextBox.Text = sq.secondaryText;
            checkBoxEndsq.Checked = false;
            if (sq.type == "end") checkBoxEndsq.Checked = true;

            foreach (SequencePanel c in sqFlowPanel.Controls) {
                if (c.Tag.ToString() == sqid)
                {
                    c.setActive();
                }
                else {
                    c.setInActive();
                }
            }
            propsPanel.Enabled = true;
            choicesContainerPanel.Enabled = true;
            displayChoices();
        }

        private void displaySequences()
        {
            foreach (KeyValuePair<string, Sequence> s in mySequences)
            {
                SequencePanel sP = new SequencePanel(s.Key, this);
                sqFlowPanel.Controls.Add(sP);
            }
            loadSequencesIntoStartSq();
            SyncNodeEditor();
        }

        private void displayChoices() {
            clearChoicesFlow();
            foreach (KeyValuePair<string, Choice> c in getCurrentSequence().choices) {
                ChoicePanel choicePanel = new ChoicePanel(getCurrentSequence().id, c.Key, this);
                choicesFlowPanel.Controls.Add(choicePanel);
            }
        }

        private void clearChoicesFlow()
        {
            choicesFlowPanel.Controls.Clear();
        }

        public Panel getSequencesPanel() => sqFlowPanel;
        public Panel getChoicesPanel() => choicesFlowPanel;

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
            if (projectFile != null)
            {
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

                var results = recentFiles.Find(x => x.filePath.StartsWith(projectFile));
                if (results.Count() < 1)
                {
                    var rF = new RecentFile()
                    {
                        projectTitle = gameTitle,
                        filePath = projectFile
                    };
                    recentFiles.Insert(rF);
                }
                else {
                    recentFiles.Delete(x => x.filePath.StartsWith(projectFile));
                    var rF = new RecentFile()
                    {
                        projectTitle = gameTitle,
                        filePath = projectFile
                    };
                    recentFiles.Insert(rF);
                }
            }
        }

        private void writeToFile(string filePath) {
            NodeEditorState editorState = null;
            if (nodeEditorPanel != null)
            {
                editorState = nodeEditorPanel.GetState();
            }

            StudioObject so = new StudioObject(gameTitle, gameAuthor, projectDescription:gameDescription, projectContainers, mySequences, editorState);
            BinarySystem bs = new BinarySystem();
            bs.WriteToBinaryFile<StudioObject>(filePath, so);
            ShowToast("Project saved successfully", ToastNotification.ToastType.Success);
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
                mySequences[sqInProp].type = "end";
            }
            else
            {
                mySequences[sqInProp].type = "ordinary";
            }

            if (nodeEditorPanel != null)
            {
                nodeEditorPanel.UpdateSequenceNode(sqInProp, mySequences[sqInProp]);
            }
        }

        private void ExportProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportGameFileDialog.FileName = gameTitle;
            exportGameFileDialog.ShowDialog();
        }

        private void ExportGameFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            Exporter ex = new Exporter(gameTitle, gameAuthor, gameDesc: gameDescription, mySequences, projectContainers, comboBoxStartSq.Text);
            if (ex.exportToFile(true, (sender as SaveFileDialog).FileName))
            {
                ShowToast("Game exported successfully!", ToastNotification.ToastType.Success);
            }
            else
            {
                ShowToast("Export failed", ToastNotification.ToastType.Error);
            }
        }

        private void BtnEditProp_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            dlgEditProperty dlg = new dlgEditProperty(btn.Tag.ToString(), this);
            dlg.ShowDialog();
        }

        public void updateProperty(string prop, string text, string sqid) {
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
            frmHub hub = new frmHub();
            dlgNewProject newProject = new dlgNewProject(hub, this);
            newProject.Show();
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
                btnStopP.Enabled = true;
                btnRunP.Enabled = false;
            }
            else {
                updateStatusbar("Ready", UIStyles.StatusReady);
                btnStopP.Enabled = false;
                btnRunP.Enabled = true;
            }
        }

        private void RunGame()
        {
            if (comboBoxStartSq.Text.Length > 0)
            {
                Exporter ex = new Exporter(gameTitle, gameAuthor, gameDesc: gameDescription, mySequences, projectContainers, comboBoxStartSq.Text);
                if (ex.exportToFile())
                {
                    workspaceConsole.ClearOutput();
                    if (projectFile != null) saveGameFile(projectFile);
                    string _params = "export.json";
                    if (checkBoxDebugMode.Checked) _params += " debug"; 
                    workspaceConsole.StartProcess("hannacli", _params);
                    updateStatusbar("Running " + gameTitle, UIStyles.StatusRunning);

                    // Show console when running
                    if (nodeEditorSplitContainer != null && nodeEditorSplitContainer.Panel2Collapsed)
                    {
                        nodeEditorSplitContainer.Panel2Collapsed = false;
                        if (viewConsoleMenuItem != null) viewConsoleMenuItem.Checked = true;
                    }
                }
            }
            else {
                ShowToast("Please select a start sequence", ToastNotification.ToastType.Warning);
            }
        }

        private void SequenceContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var menu = (ContextMenuStrip)sender;
            var menuItem = menu.Items[0];
            menuItem.Text = menu.SourceControl.Tag.ToString();
        }

        private void DeleteThisSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var menu = (ContextMenuStrip) menuItem.Owner;
            string idofpanel = menu.SourceControl.Tag.ToString();
            deleteSequence(idofpanel);
        }

        private void EditPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripItem)sender;
            var menu = (ContextMenuStrip)menuItem.Owner;
            string idofpanel = menu.SourceControl.Tag.ToString();
            dlgEditProperty dlg = new dlgEditProperty(menuItem.Tag.ToString(), this, sqid: idofpanel);
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
            UpdateToastPosition();
        }

        private void PopupTimer_Tick(object sender, EventArgs e)
        {
            popupPanel.Visible = false;
            popupTimer.Enabled = false;
        }

        private void showPopup(string message) {
            ShowToast(message);
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

        private void updateStatusbar(string message, Color color) {
            panelStatusBar.BackColor = color;
            labelStatusBarMessage.Text = message;
        }
    }
}