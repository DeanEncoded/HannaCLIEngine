namespace Hanna_Studio
{
    partial class frmWorkspace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkspace));
            this.workspaceMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProjectMetadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHannaStudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterSplitContainer = new System.Windows.Forms.SplitContainer();
            this.sqFlowPanel = new System.Windows.Forms.Panel();
            this.gradientPanel1 = new Hanna_Studio.GradientPanel();
            this.btnManageContainers = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.consoleAreaPanel = new System.Windows.Forms.Panel();
            this.workspaceConsole = new ConsoleControl.ConsoleControl();
            this.runConfigPanel = new System.Windows.Forms.Panel();
            this.btnStopP = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxDebugMode = new MaterialSkin.Controls.MaterialCheckBox();
            this.comboBoxStartSq = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRunP = new System.Windows.Forms.Button();
            this.popupPanel = new System.Windows.Forms.Panel();
            this.popupMessage = new System.Windows.Forms.Label();
            this.ultimateSplitContainer = new System.Windows.Forms.SplitContainer();
            this.choicesContainerPanel = new System.Windows.Forms.Panel();
            this.choicesFlowPanel = new System.Windows.Forms.Panel();
            this.gradientPanel3 = new Hanna_Studio.GradientPanel();
            this.btnAddChoice = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.propsPanel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBoxEndsq = new MaterialSkin.Controls.MaterialCheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnEditProp2 = new System.Windows.Forms.Button();
            this.sqSecondaryTextTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEditProp1 = new System.Windows.Forms.Button();
            this.sqMainTextTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelProp = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.sqIdTextBox = new System.Windows.Forms.TextBox();
            this.gradientPanel2 = new Hanna_Studio.GradientPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.labelStatusBarMessage = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.workspacePanel = new System.Windows.Forms.Panel();
            this.sequenceContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sequenceIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMainTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSecTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteThisSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.popupTimer = new System.Windows.Forms.Timer(this.components);
            this.exportGameFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.workspaceMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.masterSplitContainer)).BeginInit();
            this.masterSplitContainer.Panel1.SuspendLayout();
            this.masterSplitContainer.Panel2.SuspendLayout();
            this.masterSplitContainer.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.consoleAreaPanel.SuspendLayout();
            this.runConfigPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.popupPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultimateSplitContainer)).BeginInit();
            this.ultimateSplitContainer.Panel1.SuspendLayout();
            this.ultimateSplitContainer.Panel2.SuspendLayout();
            this.ultimateSplitContainer.SuspendLayout();
            this.choicesContainerPanel.SuspendLayout();
            this.gradientPanel3.SuspendLayout();
            this.propsPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelProp.SuspendLayout();
            this.gradientPanel2.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            this.workspacePanel.SuspendLayout();
            this.sequenceContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // workspaceMenuStrip
            // 
            this.workspaceMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.workspaceMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.workspaceMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.workspaceMenuStrip.Name = "workspaceMenuStrip";
            this.workspaceMenuStrip.Size = new System.Drawing.Size(1327, 28);
            this.workspaceMenuStrip.TabIndex = 0;
            this.workspaceMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveASToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.NewProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.OpenProjectToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveASToolStripMenuItem
            // 
            this.saveASToolStripMenuItem.Name = "saveASToolStripMenuItem";
            this.saveASToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.saveASToolStripMenuItem.Text = "Save AS";
            this.saveASToolStripMenuItem.Click += new System.EventHandler(this.SaveASToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem});
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editProjectMetadataToolStripMenuItem,
            this.exportProjectToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.exportToolStripMenuItem.Text = "Project";
            // 
            // editProjectMetadataToolStripMenuItem
            // 
            this.editProjectMetadataToolStripMenuItem.Name = "editProjectMetadataToolStripMenuItem";
            this.editProjectMetadataToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.editProjectMetadataToolStripMenuItem.Text = "Edit Project Metadata";
            // 
            // exportProjectToolStripMenuItem
            // 
            this.exportProjectToolStripMenuItem.Name = "exportProjectToolStripMenuItem";
            this.exportProjectToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.exportProjectToolStripMenuItem.Text = "Export Game";
            this.exportProjectToolStripMenuItem.Click += new System.EventHandler(this.ExportProjectToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutHannaStudioToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutHannaStudioToolStripMenuItem
            // 
            this.aboutHannaStudioToolStripMenuItem.Name = "aboutHannaStudioToolStripMenuItem";
            this.aboutHannaStudioToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.aboutHannaStudioToolStripMenuItem.Text = "About Hanna Studio";
            this.aboutHannaStudioToolStripMenuItem.Click += new System.EventHandler(this.AboutHannaStudioToolStripMenuItem_Click);
            // 
            // masterSplitContainer
            // 
            this.masterSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.masterSplitContainer.Name = "masterSplitContainer";
            // 
            // masterSplitContainer.Panel1
            // 
            this.masterSplitContainer.Panel1.Controls.Add(this.sqFlowPanel);
            this.masterSplitContainer.Panel1.Controls.Add(this.gradientPanel1);
            // 
            // masterSplitContainer.Panel2
            // 
            this.masterSplitContainer.Panel2.BackColor = System.Drawing.Color.Black;
            this.masterSplitContainer.Panel2.Controls.Add(this.consoleAreaPanel);
            this.masterSplitContainer.Panel2.Controls.Add(this.runConfigPanel);
            this.masterSplitContainer.Size = new System.Drawing.Size(1000, 694);
            this.masterSplitContainer.SplitterDistance = 332;
            this.masterSplitContainer.TabIndex = 1;
            // 
            // sqFlowPanel
            // 
            this.sqFlowPanel.AutoScroll = true;
            this.sqFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqFlowPanel.Location = new System.Drawing.Point(0, 56);
            this.sqFlowPanel.Name = "sqFlowPanel";
            this.sqFlowPanel.Size = new System.Drawing.Size(332, 638);
            this.sqFlowPanel.TabIndex = 2;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.gradientPanel1.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.gradientPanel1.Controls.Add(this.btnManageContainers);
            this.gradientPanel1.Controls.Add(this.button1);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(332, 56);
            this.gradientPanel1.TabIndex = 0;
            // 
            // btnManageContainers
            // 
            this.btnManageContainers.BackColor = System.Drawing.Color.Transparent;
            this.btnManageContainers.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnManageContainers.FlatAppearance.BorderSize = 0;
            this.btnManageContainers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnManageContainers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnManageContainers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageContainers.Image = global::Hanna_Studio.Properties.Resources.ic_container;
            this.btnManageContainers.Location = new System.Drawing.Point(230, 0);
            this.btnManageContainers.Name = "btnManageContainers";
            this.btnManageContainers.Size = new System.Drawing.Size(51, 56);
            this.btnManageContainers.TabIndex = 3;
            this.btnManageContainers.UseVisualStyleBackColor = false;
            this.btnManageContainers.Click += new System.EventHandler(this.BtnManageContainers_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Hanna_Studio.Properties.Resources.btnimage_plus_white;
            this.button1.Location = new System.Drawing.Point(281, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 56);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(26, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sequences";
            // 
            // consoleAreaPanel
            // 
            this.consoleAreaPanel.Controls.Add(this.workspaceConsole);
            this.consoleAreaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleAreaPanel.Location = new System.Drawing.Point(0, 56);
            this.consoleAreaPanel.Name = "consoleAreaPanel";
            this.consoleAreaPanel.Size = new System.Drawing.Size(664, 638);
            this.consoleAreaPanel.TabIndex = 3;
            // 
            // workspaceConsole
            // 
            this.workspaceConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspaceConsole.IsInputEnabled = true;
            this.workspaceConsole.Location = new System.Drawing.Point(0, 0);
            this.workspaceConsole.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.workspaceConsole.Name = "workspaceConsole";
            this.workspaceConsole.SendKeyboardCommandsToProcess = false;
            this.workspaceConsole.ShowDiagnostics = false;
            this.workspaceConsole.Size = new System.Drawing.Size(664, 638);
            this.workspaceConsole.TabIndex = 1;
            this.workspaceConsole.OnConsoleOutput += new ConsoleControl.ConsoleEventHanlder(this.WorkspaceConsole_OnConsoleOutput);
            // 
            // runConfigPanel
            // 
            this.runConfigPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.runConfigPanel.Controls.Add(this.btnStopP);
            this.runConfigPanel.Controls.Add(this.panel1);
            this.runConfigPanel.Controls.Add(this.btnRunP);
            this.runConfigPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.runConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.runConfigPanel.Name = "runConfigPanel";
            this.runConfigPanel.Size = new System.Drawing.Size(664, 56);
            this.runConfigPanel.TabIndex = 2;
            // 
            // btnStopP
            // 
            this.btnStopP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnStopP.Enabled = false;
            this.btnStopP.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnStopP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopP.ForeColor = System.Drawing.Color.White;
            this.btnStopP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStopP.Location = new System.Drawing.Point(161, 8);
            this.btnStopP.Name = "btnStopP";
            this.btnStopP.Size = new System.Drawing.Size(104, 41);
            this.btnStopP.TabIndex = 7;
            this.btnStopP.Text = "Stop";
            this.btnStopP.UseVisualStyleBackColor = false;
            this.btnStopP.Click += new System.EventHandler(this.BtnStopRun_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxDebugMode);
            this.panel1.Controls.Add(this.comboBoxStartSq);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(271, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 56);
            this.panel1.TabIndex = 2;
            // 
            // checkBoxDebugMode
            // 
            this.checkBoxDebugMode.AutoSize = true;
            this.checkBoxDebugMode.Depth = 0;
            this.checkBoxDebugMode.Font = new System.Drawing.Font("Roboto", 10F);
            this.checkBoxDebugMode.Location = new System.Drawing.Point(0, 16);
            this.checkBoxDebugMode.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxDebugMode.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBoxDebugMode.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBoxDebugMode.Name = "checkBoxDebugMode";
            this.checkBoxDebugMode.Ripple = true;
            this.checkBoxDebugMode.Size = new System.Drawing.Size(80, 30);
            this.checkBoxDebugMode.TabIndex = 10;
            this.checkBoxDebugMode.Text = "Debug";
            this.checkBoxDebugMode.UseVisualStyleBackColor = true;
            // 
            // comboBoxStartSq
            // 
            this.comboBoxStartSq.BackColor = System.Drawing.Color.Black;
            this.comboBoxStartSq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartSq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxStartSq.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.comboBoxStartSq.ForeColor = System.Drawing.Color.White;
            this.comboBoxStartSq.FormattingEnabled = true;
            this.comboBoxStartSq.Location = new System.Drawing.Point(218, 9);
            this.comboBoxStartSq.Name = "comboBoxStartSq";
            this.comboBoxStartSq.Size = new System.Drawing.Size(154, 37);
            this.comboBoxStartSq.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(91, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Start Sequence";
            // 
            // btnRunP
            // 
            this.btnRunP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btnRunP.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRunP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunP.ForeColor = System.Drawing.Color.White;
            this.btnRunP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRunP.Location = new System.Drawing.Point(12, 8);
            this.btnRunP.Name = "btnRunP";
            this.btnRunP.Size = new System.Drawing.Size(135, 41);
            this.btnRunP.TabIndex = 6;
            this.btnRunP.Text = "Run Game";
            this.btnRunP.UseVisualStyleBackColor = false;
            this.btnRunP.Click += new System.EventHandler(this.BtnRunP_Click);
            // 
            // popupPanel
            // 
            this.popupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.popupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(115)))), ((int)(((byte)(255)))));
            this.popupPanel.Controls.Add(this.popupMessage);
            this.popupPanel.Location = new System.Drawing.Point(30, 630);
            this.popupPanel.Margin = new System.Windows.Forms.Padding(10);
            this.popupPanel.Name = "popupPanel";
            this.popupPanel.Size = new System.Drawing.Size(299, 42);
            this.popupPanel.TabIndex = 2;
            this.popupPanel.Visible = false;
            // 
            // popupMessage
            // 
            this.popupMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.popupMessage.AutoSize = true;
            this.popupMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.8F);
            this.popupMessage.ForeColor = System.Drawing.Color.White;
            this.popupMessage.Location = new System.Drawing.Point(21, 12);
            this.popupMessage.Name = "popupMessage";
            this.popupMessage.Size = new System.Drawing.Size(82, 18);
            this.popupMessage.TabIndex = 0;
            this.popupMessage.Text = "MESSAGE";
            // 
            // ultimateSplitContainer
            // 
            this.ultimateSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultimateSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ultimateSplitContainer.Name = "ultimateSplitContainer";
            // 
            // ultimateSplitContainer.Panel1
            // 
            this.ultimateSplitContainer.Panel1.Controls.Add(this.masterSplitContainer);
            // 
            // ultimateSplitContainer.Panel2
            // 
            this.ultimateSplitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ultimateSplitContainer.Panel2.Controls.Add(this.choicesContainerPanel);
            this.ultimateSplitContainer.Panel2.Controls.Add(this.propsPanel);
            this.ultimateSplitContainer.Panel2.Controls.Add(this.gradientPanel2);
            this.ultimateSplitContainer.Size = new System.Drawing.Size(1327, 694);
            this.ultimateSplitContainer.SplitterDistance = 1000;
            this.ultimateSplitContainer.TabIndex = 2;
            // 
            // choicesContainerPanel
            // 
            this.choicesContainerPanel.Controls.Add(this.choicesFlowPanel);
            this.choicesContainerPanel.Controls.Add(this.gradientPanel3);
            this.choicesContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.choicesContainerPanel.Enabled = false;
            this.choicesContainerPanel.Location = new System.Drawing.Point(0, 282);
            this.choicesContainerPanel.Name = "choicesContainerPanel";
            this.choicesContainerPanel.Size = new System.Drawing.Size(323, 412);
            this.choicesContainerPanel.TabIndex = 3;
            // 
            // choicesFlowPanel
            // 
            this.choicesFlowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.choicesFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.choicesFlowPanel.Location = new System.Drawing.Point(0, 56);
            this.choicesFlowPanel.Name = "choicesFlowPanel";
            this.choicesFlowPanel.Size = new System.Drawing.Size(323, 356);
            this.choicesFlowPanel.TabIndex = 3;
            // 
            // gradientPanel3
            // 
            this.gradientPanel3.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.gradientPanel3.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.gradientPanel3.Controls.Add(this.btnAddChoice);
            this.gradientPanel3.Controls.Add(this.label7);
            this.gradientPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel3.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel3.Name = "gradientPanel3";
            this.gradientPanel3.Size = new System.Drawing.Size(323, 56);
            this.gradientPanel3.TabIndex = 2;
            // 
            // btnAddChoice
            // 
            this.btnAddChoice.BackColor = System.Drawing.Color.Transparent;
            this.btnAddChoice.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddChoice.FlatAppearance.BorderSize = 0;
            this.btnAddChoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnAddChoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnAddChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddChoice.Image = global::Hanna_Studio.Properties.Resources.btnimage_plus_white;
            this.btnAddChoice.Location = new System.Drawing.Point(272, 0);
            this.btnAddChoice.Name = "btnAddChoice";
            this.btnAddChoice.Size = new System.Drawing.Size(51, 56);
            this.btnAddChoice.TabIndex = 2;
            this.btnAddChoice.UseVisualStyleBackColor = false;
            this.btnAddChoice.Click += new System.EventHandler(this.BtnAddChoice_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(23, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sequence Choices";
            // 
            // propsPanel
            // 
            this.propsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.propsPanel.Controls.Add(this.panel4);
            this.propsPanel.Controls.Add(this.panel3);
            this.propsPanel.Controls.Add(this.panel2);
            this.propsPanel.Controls.Add(this.panelProp);
            this.propsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.propsPanel.Enabled = false;
            this.propsPanel.Location = new System.Drawing.Point(0, 56);
            this.propsPanel.Name = "propsPanel";
            this.propsPanel.Size = new System.Drawing.Size(323, 226);
            this.propsPanel.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel4.Controls.Add(this.checkBoxEndsq);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 176);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(323, 43);
            this.panel4.TabIndex = 6;
            this.panel4.Click += new System.EventHandler(this.CheckBoxEndsq_Click);
            // 
            // checkBoxEndsq
            // 
            this.checkBoxEndsq.AutoSize = true;
            this.checkBoxEndsq.Depth = 0;
            this.checkBoxEndsq.Font = new System.Drawing.Font("Roboto", 10F);
            this.checkBoxEndsq.Location = new System.Drawing.Point(20, 8);
            this.checkBoxEndsq.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxEndsq.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBoxEndsq.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBoxEndsq.Name = "checkBoxEndsq";
            this.checkBoxEndsq.Ripple = true;
            this.checkBoxEndsq.Size = new System.Drawing.Size(140, 30);
            this.checkBoxEndsq.TabIndex = 9;
            this.checkBoxEndsq.Text = "End Sequence";
            this.checkBoxEndsq.UseVisualStyleBackColor = true;
            this.checkBoxEndsq.Click += new System.EventHandler(this.CheckBoxEndsq_Click);
            // 
            // panel3
            // 
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.btnEditProp2);
            this.panel3.Controls.Add(this.sqSecondaryTextTextBox);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 112);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(323, 64);
            this.panel3.TabIndex = 5;
            // 
            // btnEditProp2
            // 
            this.btnEditProp2.BackColor = System.Drawing.Color.Transparent;
            this.btnEditProp2.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEditProp2.FlatAppearance.BorderSize = 0;
            this.btnEditProp2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnEditProp2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnEditProp2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProp2.Image = global::Hanna_Studio.Properties.Resources.ic_edit_small;
            this.btnEditProp2.Location = new System.Drawing.Point(272, 0);
            this.btnEditProp2.Name = "btnEditProp2";
            this.btnEditProp2.Size = new System.Drawing.Size(51, 64);
            this.btnEditProp2.TabIndex = 10;
            this.btnEditProp2.Tag = "secondaryText";
            this.btnEditProp2.UseVisualStyleBackColor = false;
            this.btnEditProp2.Click += new System.EventHandler(this.BtnEditProp_Click);
            // 
            // sqSecondaryTextTextBox
            // 
            this.sqSecondaryTextTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.sqSecondaryTextTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sqSecondaryTextTextBox.Enabled = false;
            this.sqSecondaryTextTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8F);
            this.sqSecondaryTextTextBox.ForeColor = System.Drawing.Color.White;
            this.sqSecondaryTextTextBox.Location = new System.Drawing.Point(91, 23);
            this.sqSecondaryTextTextBox.Name = "sqSecondaryTextTextBox";
            this.sqSecondaryTextTextBox.Size = new System.Drawing.Size(160, 23);
            this.sqSecondaryTextTextBox.TabIndex = 3;
            this.sqSecondaryTextTextBox.Tag = "changeSecondaryText";
            this.sqSecondaryTextTextBox.TextChanged += new System.EventHandler(this.SqProp_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "secText";
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.btnEditProp1);
            this.panel2.Controls.Add(this.sqMainTextTextBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(323, 56);
            this.panel2.TabIndex = 4;
            // 
            // btnEditProp1
            // 
            this.btnEditProp1.BackColor = System.Drawing.Color.Transparent;
            this.btnEditProp1.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEditProp1.FlatAppearance.BorderSize = 0;
            this.btnEditProp1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnEditProp1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnEditProp1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProp1.Image = global::Hanna_Studio.Properties.Resources.ic_edit_small;
            this.btnEditProp1.Location = new System.Drawing.Point(272, 0);
            this.btnEditProp1.Name = "btnEditProp1";
            this.btnEditProp1.Size = new System.Drawing.Size(51, 56);
            this.btnEditProp1.TabIndex = 11;
            this.btnEditProp1.Tag = "mainText";
            this.btnEditProp1.UseVisualStyleBackColor = false;
            this.btnEditProp1.Click += new System.EventHandler(this.BtnEditProp_Click);
            // 
            // sqMainTextTextBox
            // 
            this.sqMainTextTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.sqMainTextTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sqMainTextTextBox.Enabled = false;
            this.sqMainTextTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8F);
            this.sqMainTextTextBox.ForeColor = System.Drawing.Color.White;
            this.sqMainTextTextBox.Location = new System.Drawing.Point(91, 23);
            this.sqMainTextTextBox.Name = "sqMainTextTextBox";
            this.sqMainTextTextBox.Size = new System.Drawing.Size(160, 23);
            this.sqMainTextTextBox.TabIndex = 2;
            this.sqMainTextTextBox.Tag = "changeMainText";
            this.sqMainTextTextBox.TextChanged += new System.EventHandler(this.SqProp_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(17, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "mainText";
            // 
            // panelProp
            // 
            this.panelProp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelProp.Controls.Add(this.label3);
            this.panelProp.Controls.Add(this.sqIdTextBox);
            this.panelProp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProp.Location = new System.Drawing.Point(0, 0);
            this.panelProp.Name = "panelProp";
            this.panelProp.Size = new System.Drawing.Size(323, 56);
            this.panelProp.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "id";
            // 
            // sqIdTextBox
            // 
            this.sqIdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.sqIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sqIdTextBox.Enabled = false;
            this.sqIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8F);
            this.sqIdTextBox.ForeColor = System.Drawing.Color.Silver;
            this.sqIdTextBox.Location = new System.Drawing.Point(91, 20);
            this.sqIdTextBox.Name = "sqIdTextBox";
            this.sqIdTextBox.Size = new System.Drawing.Size(160, 23);
            this.sqIdTextBox.TabIndex = 1;
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.gradientPanel2.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.gradientPanel2.Controls.Add(this.label2);
            this.gradientPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel2.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(323, 56);
            this.gradientPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Properties";
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(115)))), ((int)(((byte)(255)))));
            this.panelStatusBar.Controls.Add(this.labelStatusBarMessage);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 722);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Size = new System.Drawing.Size(1327, 27);
            this.panelStatusBar.TabIndex = 3;
            // 
            // labelStatusBarMessage
            // 
            this.labelStatusBarMessage.AutoSize = true;
            this.labelStatusBarMessage.ForeColor = System.Drawing.Color.White;
            this.labelStatusBarMessage.Location = new System.Drawing.Point(12, 5);
            this.labelStatusBarMessage.Name = "labelStatusBarMessage";
            this.labelStatusBarMessage.Size = new System.Drawing.Size(49, 17);
            this.labelStatusBarMessage.TabIndex = 0;
            this.labelStatusBarMessage.Text = "Ready";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "hprj";
            this.saveFileDialog.Filter = "Hanna Project (*.hprj)|*.hprj";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog_FileOk);
            // 
            // workspacePanel
            // 
            this.workspacePanel.Controls.Add(this.ultimateSplitContainer);
            this.workspacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspacePanel.Location = new System.Drawing.Point(0, 28);
            this.workspacePanel.Name = "workspacePanel";
            this.workspacePanel.Size = new System.Drawing.Size(1327, 694);
            this.workspacePanel.TabIndex = 4;
            // 
            // sequenceContextMenuStrip
            // 
            this.sequenceContextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.sequenceContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sequenceContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequenceIDToolStripMenuItem,
            this.editMainTextToolStripMenuItem,
            this.editSecTextToolStripMenuItem,
            this.deleteThisSequenceToolStripMenuItem});
            this.sequenceContextMenuStrip.Name = "sequenceContextMenuStrip";
            this.sequenceContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.sequenceContextMenuStrip.Size = new System.Drawing.Size(216, 100);
            this.sequenceContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.SequenceContextMenuStrip_Opening);
            // 
            // sequenceIDToolStripMenuItem
            // 
            this.sequenceIDToolStripMenuItem.Enabled = false;
            this.sequenceIDToolStripMenuItem.ForeColor = System.Drawing.Color.LightGray;
            this.sequenceIDToolStripMenuItem.Name = "sequenceIDToolStripMenuItem";
            this.sequenceIDToolStripMenuItem.Size = new System.Drawing.Size(215, 24);
            this.sequenceIDToolStripMenuItem.Text = "sequenceID";
            // 
            // editMainTextToolStripMenuItem
            // 
            this.editMainTextToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editMainTextToolStripMenuItem.Name = "editMainTextToolStripMenuItem";
            this.editMainTextToolStripMenuItem.Size = new System.Drawing.Size(215, 24);
            this.editMainTextToolStripMenuItem.Tag = "mainText";
            this.editMainTextToolStripMenuItem.Text = "Edit mainText";
            this.editMainTextToolStripMenuItem.Click += new System.EventHandler(this.EditPropertyToolStripMenuItem_Click);
            // 
            // editSecTextToolStripMenuItem
            // 
            this.editSecTextToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editSecTextToolStripMenuItem.Name = "editSecTextToolStripMenuItem";
            this.editSecTextToolStripMenuItem.Size = new System.Drawing.Size(215, 24);
            this.editSecTextToolStripMenuItem.Tag = "secondaryText";
            this.editSecTextToolStripMenuItem.Text = "Edit secText";
            this.editSecTextToolStripMenuItem.Click += new System.EventHandler(this.EditPropertyToolStripMenuItem_Click);
            // 
            // deleteThisSequenceToolStripMenuItem
            // 
            this.deleteThisSequenceToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteThisSequenceToolStripMenuItem.Name = "deleteThisSequenceToolStripMenuItem";
            this.deleteThisSequenceToolStripMenuItem.Size = new System.Drawing.Size(215, 24);
            this.deleteThisSequenceToolStripMenuItem.Text = "Delete this sequence";
            this.deleteThisSequenceToolStripMenuItem.Click += new System.EventHandler(this.DeleteThisSequenceToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "hprj";
            this.openFileDialog.Filter = "Hanna Project (*.hprj)|*.hprj";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // popupTimer
            // 
            this.popupTimer.Interval = 3200;
            this.popupTimer.Tick += new System.EventHandler(this.PopupTimer_Tick);
            // 
            // exportGameFileDialog
            // 
            this.exportGameFileDialog.DefaultExt = "hgm";
            this.exportGameFileDialog.Filter = "Hanna Game (*.hgm)|*.hgm";
            this.exportGameFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.ExportGameFileDialog_FileOk);
            // 
            // frmWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1327, 749);
            this.Controls.Add(this.popupPanel);
            this.Controls.Add(this.workspacePanel);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.workspaceMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.workspaceMenuStrip;
            this.MinimumSize = new System.Drawing.Size(1345, 796);
            this.Name = "frmWorkspace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workspace";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmWorkspace_Load);
            this.Resize += new System.EventHandler(this.FrmWorkspace_Resize);
            this.workspaceMenuStrip.ResumeLayout(false);
            this.workspaceMenuStrip.PerformLayout();
            this.masterSplitContainer.Panel1.ResumeLayout(false);
            this.masterSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.masterSplitContainer)).EndInit();
            this.masterSplitContainer.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.consoleAreaPanel.ResumeLayout(false);
            this.runConfigPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.popupPanel.ResumeLayout(false);
            this.popupPanel.PerformLayout();
            this.ultimateSplitContainer.Panel1.ResumeLayout(false);
            this.ultimateSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultimateSplitContainer)).EndInit();
            this.ultimateSplitContainer.ResumeLayout(false);
            this.choicesContainerPanel.ResumeLayout(false);
            this.gradientPanel3.ResumeLayout(false);
            this.gradientPanel3.PerformLayout();
            this.propsPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelProp.ResumeLayout(false);
            this.panelProp.PerformLayout();
            this.gradientPanel2.ResumeLayout(false);
            this.gradientPanel2.PerformLayout();
            this.panelStatusBar.ResumeLayout(false);
            this.panelStatusBar.PerformLayout();
            this.workspacePanel.ResumeLayout(false);
            this.sequenceContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip workspaceMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveASToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.SplitContainer masterSplitContainer;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer ultimateSplitContainer;
        private GradientPanel gradientPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelProp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox sqIdTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel choicesContainerPanel;
        private GradientPanel gradientPanel3;
        private System.Windows.Forms.Button btnAddChoice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox sqMainTextTextBox;
        private System.Windows.Forms.TextBox sqSecondaryTextTextBox;
        private System.Windows.Forms.Panel panelStatusBar;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelStatusBarMessage;
        private System.Windows.Forms.ToolStripMenuItem exportProjectToolStripMenuItem;
        private ConsoleControl.ConsoleControl workspaceConsole;
        private System.Windows.Forms.Panel runConfigPanel;
        private System.Windows.Forms.Panel consoleAreaPanel;
        private System.Windows.Forms.Button btnManageContainers;
        private System.Windows.Forms.Panel workspacePanel;
        private System.Windows.Forms.Panel choicesFlowPanel;
        private System.Windows.Forms.ToolStripMenuItem editProjectMetadataToolStripMenuItem;
        private MaterialSkin.Controls.MaterialCheckBox checkBoxEndsq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStartSq;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditProp2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnEditProp1;
        private System.Windows.Forms.Panel propsPanel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHannaStudioToolStripMenuItem;
        private System.Windows.Forms.Button btnStopP;
        private System.Windows.Forms.Button btnRunP;
        private MaterialSkin.Controls.MaterialCheckBox checkBoxDebugMode;
        public System.Windows.Forms.Panel sqFlowPanel;
        private System.Windows.Forms.ToolStripMenuItem editMainTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSecTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteThisSequenceToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip sequenceContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem sequenceIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel popupPanel;
        private System.Windows.Forms.Label popupMessage;
        private System.Windows.Forms.Timer popupTimer;
        private System.Windows.Forms.SaveFileDialog exportGameFileDialog;
    }
}