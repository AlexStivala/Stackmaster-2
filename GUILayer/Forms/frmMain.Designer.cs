namespace GUILayer.Forms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectDefaultShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.resetStatusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.usePrimaryMediaSequencerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useBackupMediaSequencerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAboutBox = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentShow = new System.Windows.Forms.Label();
            this.lblCurrentShowHeader = new System.Windows.Forms.Label();
            this.lblPlaylistNameHeader = new System.Windows.Forms.Label();
            this.lblPlaylistName = new System.Windows.Forms.Label();
            this.lblTrioChannelHeader = new System.Windows.Forms.Label();
            this.lblTrioChannel = new System.Windows.Forms.Label();
            this.dataModeSelect = new System.Windows.Forms.TabControl();
            this.tpRaces = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtNextPollClosingTime = new System.Windows.Forms.Label();
            this.txtNextPollClosingTimeHeader = new System.Windows.Forms.Label();
            this.lblAvailRaceCnt = new System.Windows.Forms.Label();
            this.gbSpF = new System.Windows.Forms.GroupBox();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbPollClosing = new System.Windows.Forms.RadioButton();
            this.rbBattleground = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnAddRace4Way = new System.Windows.Forms.Button();
            this.btnSelect4 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAddRace3WaySelect = new System.Windows.Forms.Button();
            this.btnAddRace3Way = new System.Windows.Forms.Button();
            this.gbRCF = new System.Windows.Forms.GroupBox();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbCalled = new System.Windows.Forms.RadioButton();
            this.rbJustCalled = new System.Windows.Forms.RadioButton();
            this.rbTCTC = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAddRace2WaySelect = new System.Windows.Forms.Button();
            this.btnAddRace2Way = new System.Windows.Forms.Button();
            this.gbROF = new System.Windows.Forms.GroupBox();
            this.rbShowAll = new System.Windows.Forms.RadioButton();
            this.rbGovernor = new System.Windows.Forms.RadioButton();
            this.rbHouse = new System.Windows.Forms.RadioButton();
            this.rbSenate = new System.Windows.Forms.RadioButton();
            this.rbPresident = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddRace1Way = new System.Windows.Forms.Button();
            this.btnAddRace1WaySelect = new System.Windows.Forms.Button();
            this.availableRacesGrid = new System.Windows.Forms.DataGridView();
            this.Race_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Race_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpExitPolls = new System.Windows.Forms.TabPage();
            this.lblVAcnt = new System.Windows.Forms.Label();
            this.dgvVoterAnalysis = new System.Windows.Forms.DataGridView();
            this.tcVoterAnalysis = new System.Windows.Forms.TabControl();
            this.tpFullScreen = new System.Windows.Forms.TabPage();
            this.tpTicker = new System.Windows.Forms.TabPage();
            this.btnAddExitPoll = new System.Windows.Forms.Button();
            this.tpBalanceOfPower = new System.Windows.Forms.TabPage();
            this.BOPdataGridView = new System.Windows.Forms.DataGridView();
            this.eType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Session = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddBalanceOfPower = new System.Windows.Forms.Button();
            this.tpReferendums = new System.Windows.Forms.TabPage();
            this.btnAddReferendum = new System.Windows.Forms.Button();
            this.ReferendumsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.st = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Question = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timerStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.pnlStack = new System.Windows.Forms.Panel();
            this.pnlUpDn = new System.Windows.Forms.Panel();
            this.btnStackElementDown = new System.Windows.Forms.Button();
            this.btnStackElementUp = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TakePanel = new System.Windows.Forms.Panel();
            this.cbAutoCalledRaces = new System.Windows.Forms.CheckBox();
            this.cbLooping = new System.Windows.Forms.CheckBox();
            this.btnTake = new System.Windows.Forms.Button();
            this.LockPanel = new System.Windows.Forms.Panel();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.SaveActivatePanel = new System.Windows.Forms.Panel();
            this.cbPromptForInfo = new System.Windows.Forms.CheckBox();
            this.btnSaveActivateStack = new System.Windows.Forms.Button();
            this.StackPanel = new System.Windows.Forms.Panel();
            this.btnClearStack = new System.Windows.Forms.Button();
            this.btnDeleteStackElement = new System.Windows.Forms.Button();
            this.btnLoadStack = new System.Windows.Forms.Button();
            this.btnSaveStack = new System.Windows.Forms.Button();
            this.stackGrid = new System.Windows.Forms.DataGridView();
            this.Element_Type_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stack_Entry_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbGraphicConcept = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStackName = new System.Windows.Forms.Label();
            this.txtStackEntriesCount = new System.Windows.Forms.Label();
            this.lblStackEntriesCount = new System.Windows.Forms.Label();
            this.lblStackHeader = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.lblHostName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMediaSequencer = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LiveUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.connectionPanel = new System.Windows.Forms.Panel();
            this.gbViz6 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.gbLEDOn6 = new System.Windows.Forms.PictureBox();
            this.gbLEDOff6 = new System.Windows.Forms.PictureBox();
            this.gbPortlbl6 = new System.Windows.Forms.Label();
            this.gbIPlbl6 = new System.Windows.Forms.Label();
            this.gbViz5 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.gbLEDOn5 = new System.Windows.Forms.PictureBox();
            this.gbLEDOff5 = new System.Windows.Forms.PictureBox();
            this.gbPortlbl5 = new System.Windows.Forms.Label();
            this.gbIPlbl5 = new System.Windows.Forms.Label();
            this.gbViz4 = new System.Windows.Forms.GroupBox();
            this.gbNamelbl4 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.gbLEDOn4 = new System.Windows.Forms.PictureBox();
            this.gbLEDOff4 = new System.Windows.Forms.PictureBox();
            this.gbPortlbl4 = new System.Windows.Forms.Label();
            this.gbIPlbl4 = new System.Windows.Forms.Label();
            this.gbViz3 = new System.Windows.Forms.GroupBox();
            this.gbNamelbl3 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.gbLEDOn3 = new System.Windows.Forms.PictureBox();
            this.gbLEDOff3 = new System.Windows.Forms.PictureBox();
            this.gbPortlbl3 = new System.Windows.Forms.Label();
            this.gbIPlbl3 = new System.Windows.Forms.Label();
            this.gbViz2 = new System.Windows.Forms.GroupBox();
            this.gbNamelbl2 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.gbLEDOn2 = new System.Windows.Forms.PictureBox();
            this.gbLEDOff2 = new System.Windows.Forms.PictureBox();
            this.gbPortlbl2 = new System.Windows.Forms.Label();
            this.gbIPlbl2 = new System.Windows.Forms.Label();
            this.gbViz1 = new System.Windows.Forms.GroupBox();
            this.gbNamelbl1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.gbLEDOn1 = new System.Windows.Forms.PictureBox();
            this.gbLEDOff1 = new System.Windows.Forms.PictureBox();
            this.gbPortlbl1 = new System.Windows.Forms.Label();
            this.gbIPlbl1 = new System.Windows.Forms.Label();
            this.lblConfig = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.enginePanel = new System.Windows.Forms.Panel();
            this.lblScenes = new System.Windows.Forms.Label();
            this.gbEngines = new System.Windows.Forms.GroupBox();
            this.gbEng4 = new System.Windows.Forms.GroupBox();
            this.pbEng4 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.gbEng3 = new System.Windows.Forms.GroupBox();
            this.pbEng3 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.gbEng2 = new System.Windows.Forms.GroupBox();
            this.pbEng2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.gbEng1 = new System.Windows.Forms.GroupBox();
            this.pbEng1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.LoopTimer = new System.Windows.Forms.Timer(this.components);
            this.lblDB = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.dataModeSelect.SuspendLayout();
            this.tpRaces.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbSpF.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbRCF.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbROF.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.availableRacesGrid)).BeginInit();
            this.tpExitPolls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoterAnalysis)).BeginInit();
            this.tcVoterAnalysis.SuspendLayout();
            this.tpBalanceOfPower.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BOPdataGridView)).BeginInit();
            this.tpReferendums.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReferendumsGrid)).BeginInit();
            this.gbTime.SuspendLayout();
            this.pnlStack.SuspendLayout();
            this.pnlUpDn.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.TakePanel.SuspendLayout();
            this.LockPanel.SuspendLayout();
            this.SaveActivatePanel.SuspendLayout();
            this.StackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackGrid)).BeginInit();
            this.connectionPanel.SuspendLayout();
            this.gbViz6.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff6)).BeginInit();
            this.gbViz5.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff5)).BeginInit();
            this.gbViz4.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff4)).BeginInit();
            this.gbViz3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff3)).BeginInit();
            this.gbViz2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff2)).BeginInit();
            this.gbViz1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff1)).BeginInit();
            this.enginePanel.SuspendLayout();
            this.gbEngines.SuspendLayout();
            this.gbEng4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEng4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.gbEng3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEng3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.gbEng2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEng2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.gbEng1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEng1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.utilitiesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(2150, 35);
            this.menuStrip1.TabIndex = 48;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExit});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(93, 29);
            this.programToolStripMenuItem.Text = "&Program";
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(123, 30);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // utilitiesToolStripMenuItem
            // 
            this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSelectDefaultShow,
            this.toolStripMenuItem1,
            this.resetStatusBarToolStripMenuItem,
            this.toolStripMenuItem2,
            this.usePrimaryMediaSequencerToolStripMenuItem,
            this.useBackupMediaSequencerToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadConfigurationToolStripMenuItem});
            this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(81, 29);
            this.utilitiesToolStripMenuItem.Text = "&Utilities";
            // 
            // miSelectDefaultShow
            // 
            this.miSelectDefaultShow.Name = "miSelectDefaultShow";
            this.miSelectDefaultShow.Size = new System.Drawing.Size(331, 30);
            this.miSelectDefaultShow.Text = "&Select Default Show";
            this.miSelectDefaultShow.Click += new System.EventHandler(this.miSelectDefaultShow_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(328, 6);
            // 
            // resetStatusBarToolStripMenuItem
            // 
            this.resetStatusBarToolStripMenuItem.Name = "resetStatusBarToolStripMenuItem";
            this.resetStatusBarToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.resetStatusBarToolStripMenuItem.Text = "&Reset Status Bar";
            this.resetStatusBarToolStripMenuItem.Click += new System.EventHandler(this.resetStatusBarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(328, 6);
            // 
            // usePrimaryMediaSequencerToolStripMenuItem
            // 
            this.usePrimaryMediaSequencerToolStripMenuItem.CheckOnClick = true;
            this.usePrimaryMediaSequencerToolStripMenuItem.Name = "usePrimaryMediaSequencerToolStripMenuItem";
            this.usePrimaryMediaSequencerToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.usePrimaryMediaSequencerToolStripMenuItem.Text = "Use Primary Media Sequencer";
            this.usePrimaryMediaSequencerToolStripMenuItem.Click += new System.EventHandler(this.usePrimaryMediaSequencerToolStripMenuItem_Click);
            // 
            // useBackupMediaSequencerToolStripMenuItem
            // 
            this.useBackupMediaSequencerToolStripMenuItem.CheckOnClick = true;
            this.useBackupMediaSequencerToolStripMenuItem.Name = "useBackupMediaSequencerToolStripMenuItem";
            this.useBackupMediaSequencerToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.useBackupMediaSequencerToolStripMenuItem.Text = "Use Backup Media Sequencer";
            this.useBackupMediaSequencerToolStripMenuItem.Click += new System.EventHandler(this.useBackupMediaSequencerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(328, 6);
            // 
            // loadConfigurationToolStripMenuItem
            // 
            this.loadConfigurationToolStripMenuItem.Name = "loadConfigurationToolStripMenuItem";
            this.loadConfigurationToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.loadConfigurationToolStripMenuItem.Text = "Load Configuration";
            this.loadConfigurationToolStripMenuItem.Click += new System.EventHandler(this.loadConfigurationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAboutBox});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // miAboutBox
            // 
            this.miAboutBox.Name = "miAboutBox";
            this.miAboutBox.Size = new System.Drawing.Size(146, 30);
            this.miAboutBox.Text = "&About";
            this.miAboutBox.Click += new System.EventHandler(this.miAboutBox_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 1701);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(2150, 30);
            this.statusStrip.TabIndex = 53;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ActiveLinkColor = System.Drawing.SystemColors.AppWorkspace;
            this.toolStripStatusLabel.BackColor = System.Drawing.Color.SpringGreen;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(44, 25);
            this.toolStripStatusLabel.Text = "N/A";
            // 
            // lblCurrentShow
            // 
            this.lblCurrentShow.AutoSize = true;
            this.lblCurrentShow.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCurrentShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentShow.Location = new System.Drawing.Point(183, 89);
            this.lblCurrentShow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentShow.Name = "lblCurrentShow";
            this.lblCurrentShow.Size = new System.Drawing.Size(49, 25);
            this.lblCurrentShow.TabIndex = 86;
            this.lblCurrentShow.Text = "N/A";
            // 
            // lblCurrentShowHeader
            // 
            this.lblCurrentShowHeader.AutoSize = true;
            this.lblCurrentShowHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentShowHeader.Location = new System.Drawing.Point(15, 89);
            this.lblCurrentShowHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentShowHeader.Name = "lblCurrentShowHeader";
            this.lblCurrentShowHeader.Size = new System.Drawing.Size(164, 25);
            this.lblCurrentShowHeader.TabIndex = 85;
            this.lblCurrentShowHeader.Text = "Selected Show:";
            // 
            // lblPlaylistNameHeader
            // 
            this.lblPlaylistNameHeader.AutoSize = true;
            this.lblPlaylistNameHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistNameHeader.Location = new System.Drawing.Point(400, 89);
            this.lblPlaylistNameHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlaylistNameHeader.Name = "lblPlaylistNameHeader";
            this.lblPlaylistNameHeader.Size = new System.Drawing.Size(150, 25);
            this.lblPlaylistNameHeader.TabIndex = 88;
            this.lblPlaylistNameHeader.Text = "Playlist Name:";
            // 
            // lblPlaylistName
            // 
            this.lblPlaylistName.AutoSize = true;
            this.lblPlaylistName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistName.Location = new System.Drawing.Point(560, 89);
            this.lblPlaylistName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlaylistName.Name = "lblPlaylistName";
            this.lblPlaylistName.Size = new System.Drawing.Size(49, 25);
            this.lblPlaylistName.TabIndex = 89;
            this.lblPlaylistName.Text = "N/A";
            // 
            // lblTrioChannelHeader
            // 
            this.lblTrioChannelHeader.AutoSize = true;
            this.lblTrioChannelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrioChannelHeader.Location = new System.Drawing.Point(734, 89);
            this.lblTrioChannelHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrioChannelHeader.Name = "lblTrioChannelHeader";
            this.lblTrioChannelHeader.Size = new System.Drawing.Size(144, 25);
            this.lblTrioChannelHeader.TabIndex = 90;
            this.lblTrioChannelHeader.Text = "Trio Channel:";
            // 
            // lblTrioChannel
            // 
            this.lblTrioChannel.AutoSize = true;
            this.lblTrioChannel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrioChannel.Location = new System.Drawing.Point(880, 89);
            this.lblTrioChannel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrioChannel.Name = "lblTrioChannel";
            this.lblTrioChannel.Size = new System.Drawing.Size(49, 25);
            this.lblTrioChannel.TabIndex = 91;
            this.lblTrioChannel.Text = "N/A";
            // 
            // dataModeSelect
            // 
            this.dataModeSelect.Controls.Add(this.tpRaces);
            this.dataModeSelect.Controls.Add(this.tpExitPolls);
            this.dataModeSelect.Controls.Add(this.tpBalanceOfPower);
            this.dataModeSelect.Controls.Add(this.tpReferendums);
            this.dataModeSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataModeSelect.Location = new System.Drawing.Point(8, 129);
            this.dataModeSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataModeSelect.Name = "dataModeSelect";
            this.dataModeSelect.SelectedIndex = 0;
            this.dataModeSelect.Size = new System.Drawing.Size(1029, 1222);
            this.dataModeSelect.TabIndex = 93;
            this.dataModeSelect.SelectedIndexChanged += new System.EventHandler(this.dataModeSelect_SelectedIndexChanged);
            // 
            // tpRaces
            // 
            this.tpRaces.BackColor = System.Drawing.Color.Navy;
            this.tpRaces.Controls.Add(this.panel4);
            this.tpRaces.Controls.Add(this.gbSpF);
            this.tpRaces.Controls.Add(this.groupBox1);
            this.tpRaces.Controls.Add(this.groupBox5);
            this.tpRaces.Controls.Add(this.groupBox4);
            this.tpRaces.Controls.Add(this.gbRCF);
            this.tpRaces.Controls.Add(this.groupBox3);
            this.tpRaces.Controls.Add(this.gbROF);
            this.tpRaces.Controls.Add(this.groupBox2);
            this.tpRaces.Controls.Add(this.availableRacesGrid);
            this.tpRaces.Location = new System.Drawing.Point(4, 38);
            this.tpRaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpRaces.Name = "tpRaces";
            this.tpRaces.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpRaces.Size = new System.Drawing.Size(1021, 1180);
            this.tpRaces.TabIndex = 0;
            this.tpRaces.Text = "Race Boards";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gray;
            this.panel4.Controls.Add(this.txtNextPollClosingTime);
            this.panel4.Controls.Add(this.txtNextPollClosingTimeHeader);
            this.panel4.Controls.Add(this.lblAvailRaceCnt);
            this.panel4.Location = new System.Drawing.Point(9, 232);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(999, 49);
            this.panel4.TabIndex = 121;
            // 
            // txtNextPollClosingTime
            // 
            this.txtNextPollClosingTime.AutoSize = true;
            this.txtNextPollClosingTime.Location = new System.Drawing.Point(633, 12);
            this.txtNextPollClosingTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtNextPollClosingTime.Name = "txtNextPollClosingTime";
            this.txtNextPollClosingTime.Size = new System.Drawing.Size(56, 29);
            this.txtNextPollClosingTime.TabIndex = 2;
            this.txtNextPollClosingTime.Text = "N/A";
            // 
            // txtNextPollClosingTimeHeader
            // 
            this.txtNextPollClosingTimeHeader.AutoSize = true;
            this.txtNextPollClosingTimeHeader.Location = new System.Drawing.Point(386, 12);
            this.txtNextPollClosingTimeHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtNextPollClosingTimeHeader.Name = "txtNextPollClosingTimeHeader";
            this.txtNextPollClosingTimeHeader.Size = new System.Drawing.Size(289, 29);
            this.txtNextPollClosingTimeHeader.TabIndex = 1;
            this.txtNextPollClosingTimeHeader.Text = "Next Poll Closing Time:";
            // 
            // lblAvailRaceCnt
            // 
            this.lblAvailRaceCnt.AutoSize = true;
            this.lblAvailRaceCnt.Location = new System.Drawing.Point(10, 12);
            this.lblAvailRaceCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailRaceCnt.Name = "lblAvailRaceCnt";
            this.lblAvailRaceCnt.Size = new System.Drawing.Size(214, 29);
            this.lblAvailRaceCnt.TabIndex = 0;
            this.lblAvailRaceCnt.Text = "Available Races: ";
            // 
            // gbSpF
            // 
            this.gbSpF.BackColor = System.Drawing.Color.DodgerBlue;
            this.gbSpF.Controls.Add(this.rbNone);
            this.gbSpF.Controls.Add(this.rbPollClosing);
            this.gbSpF.Controls.Add(this.rbBattleground);
            this.gbSpF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSpF.Location = new System.Drawing.Point(9, 157);
            this.gbSpF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSpF.Name = "gbSpF";
            this.gbSpF.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSpF.Size = new System.Drawing.Size(999, 69);
            this.gbSpF.TabIndex = 120;
            this.gbSpF.TabStop = false;
            this.gbSpF.Text = "Additional Filters";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(796, 28);
            this.rbNone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(175, 29);
            this.rbNone.TabIndex = 9;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "Show All(F12)";
            this.rbNone.UseVisualStyleBackColor = false;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // rbPollClosing
            // 
            this.rbPollClosing.AutoSize = true;
            this.rbPollClosing.Location = new System.Drawing.Point(390, 28);
            this.rbPollClosing.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbPollClosing.Name = "rbPollClosing";
            this.rbPollClosing.Size = new System.Drawing.Size(255, 29);
            this.rbPollClosing.TabIndex = 6;
            this.rbPollClosing.Text = "Next Poll Closing(F11)";
            this.rbPollClosing.UseVisualStyleBackColor = true;
            this.rbPollClosing.CheckedChanged += new System.EventHandler(this.rbPollClosing_CheckedChanged);
            // 
            // rbBattleground
            // 
            this.rbBattleground.AutoSize = true;
            this.rbBattleground.Location = new System.Drawing.Point(15, 28);
            this.rbBattleground.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbBattleground.Name = "rbBattleground";
            this.rbBattleground.Size = new System.Drawing.Size(280, 29);
            this.rbBattleground.TabIndex = 5;
            this.rbBattleground.Text = "Battleground States(F10)";
            this.rbBattleground.UseVisualStyleBackColor = true;
            this.rbBattleground.CheckedChanged += new System.EventHandler(this.rbBattleground_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Controls.Add(this.btnAddAll);
            this.groupBox1.Controls.Add(this.btnInsert);
            this.groupBox1.Location = new System.Drawing.Point(636, 1002);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(372, 169);
            this.groupBox1.TabIndex = 119;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quick Add";
            // 
            // btnAddAll
            // 
            this.btnAddAll.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAll.Image = global::GUILayer.Properties.Resources.AddDataItem;
            this.btnAddAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddAll.Location = new System.Drawing.Point(64, 49);
            this.btnAddAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(228, 85);
            this.btnAddAll.TabIndex = 70;
            this.btnAddAll.Text = "All\r\n(Ctrl-A)";
            this.btnAddAll.UseVisualStyleBackColor = false;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.BackColor = System.Drawing.SystemColors.Control;
            this.btnInsert.Image = ((System.Drawing.Image)(resources.GetObject("btnInsert.Image")));
            this.btnInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsert.Location = new System.Drawing.Point(110, 58);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(136, 71);
            this.btnInsert.TabIndex = 71;
            this.btnInsert.Text = "Insert\r\n(Ctrl-I)";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox5.Controls.Add(this.btnAddRace4Way);
            this.groupBox5.Controls.Add(this.btnSelect4);
            this.groupBox5.Location = new System.Drawing.Point(636, 823);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(372, 172);
            this.groupBox5.TabIndex = 69;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "4 - Way";
            // 
            // btnAddRace4Way
            // 
            this.btnAddRace4Way.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRace4Way.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddRace4Way.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRace4Way.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRace4Way.Image")));
            this.btnAddRace4Way.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRace4Way.Location = new System.Drawing.Point(16, 51);
            this.btnAddRace4Way.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddRace4Way.Name = "btnAddRace4Way";
            this.btnAddRace4Way.Size = new System.Drawing.Size(150, 85);
            this.btnAddRace4Way.TabIndex = 67;
            this.btnAddRace4Way.Text = "Top\r\n(Ctrl-4)";
            this.btnAddRace4Way.UseVisualStyleBackColor = false;
            this.btnAddRace4Way.Click += new System.EventHandler(this.btnAddRace4Way_Click);
            // 
            // btnSelect4
            // 
            this.btnSelect4.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelect4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelect4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect4.Image = global::GUILayer.Properties.Resources.AddDataItem;
            this.btnSelect4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelect4.Location = new System.Drawing.Point(208, 51);
            this.btnSelect4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelect4.Name = "btnSelect4";
            this.btnSelect4.Size = new System.Drawing.Size(150, 85);
            this.btnSelect4.TabIndex = 66;
            this.btnSelect4.Text = "Select\r\n(Alt-4)";
            this.btnSelect4.UseVisualStyleBackColor = false;
            this.btnSelect4.Click += new System.EventHandler(this.btnSelect4_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox4.Controls.Add(this.btnAddRace3WaySelect);
            this.groupBox4.Controls.Add(this.btnAddRace3Way);
            this.groupBox4.Location = new System.Drawing.Point(636, 645);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(372, 172);
            this.groupBox4.TabIndex = 68;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3 - Way";
            // 
            // btnAddRace3WaySelect
            // 
            this.btnAddRace3WaySelect.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRace3WaySelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddRace3WaySelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRace3WaySelect.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRace3WaySelect.Image")));
            this.btnAddRace3WaySelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRace3WaySelect.Location = new System.Drawing.Point(208, 51);
            this.btnAddRace3WaySelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddRace3WaySelect.Name = "btnAddRace3WaySelect";
            this.btnAddRace3WaySelect.Size = new System.Drawing.Size(150, 85);
            this.btnAddRace3WaySelect.TabIndex = 65;
            this.btnAddRace3WaySelect.Text = "Select\r\n(Alt-3)";
            this.btnAddRace3WaySelect.UseVisualStyleBackColor = false;
            this.btnAddRace3WaySelect.Click += new System.EventHandler(this.btnSelect3_Click);
            // 
            // btnAddRace3Way
            // 
            this.btnAddRace3Way.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRace3Way.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddRace3Way.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRace3Way.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRace3Way.Image")));
            this.btnAddRace3Way.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRace3Way.Location = new System.Drawing.Point(16, 51);
            this.btnAddRace3Way.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddRace3Way.Name = "btnAddRace3Way";
            this.btnAddRace3Way.Size = new System.Drawing.Size(150, 85);
            this.btnAddRace3Way.TabIndex = 62;
            this.btnAddRace3Way.Text = "Top\r\n(Ctrl-3)";
            this.btnAddRace3Way.UseVisualStyleBackColor = false;
            this.btnAddRace3Way.Click += new System.EventHandler(this.btnAddRace3Way_Click);
            // 
            // gbRCF
            // 
            this.gbRCF.BackColor = System.Drawing.Color.SkyBlue;
            this.gbRCF.Controls.Add(this.rbAll);
            this.gbRCF.Controls.Add(this.rbCalled);
            this.gbRCF.Controls.Add(this.rbJustCalled);
            this.gbRCF.Controls.Add(this.rbTCTC);
            this.gbRCF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRCF.Location = new System.Drawing.Point(9, 82);
            this.gbRCF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbRCF.Name = "gbRCF";
            this.gbRCF.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbRCF.Size = new System.Drawing.Size(999, 69);
            this.gbRCF.TabIndex = 118;
            this.gbRCF.TabStop = false;
            this.gbRCF.Text = "Race Call Filters";
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(796, 28);
            this.rbAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(163, 29);
            this.rbAll.TabIndex = 9;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "Show All(F9)";
            this.rbAll.UseVisualStyleBackColor = false;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbCalled
            // 
            this.rbCalled.AutoSize = true;
            this.rbCalled.Location = new System.Drawing.Point(588, 28);
            this.rbCalled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbCalled.Name = "rbCalled";
            this.rbCalled.Size = new System.Drawing.Size(140, 29);
            this.rbCalled.TabIndex = 7;
            this.rbCalled.Text = "Called(F8)";
            this.rbCalled.UseVisualStyleBackColor = true;
            this.rbCalled.CheckedChanged += new System.EventHandler(this.rbCalled_CheckedChanged);
            // 
            // rbJustCalled
            // 
            this.rbJustCalled.AutoSize = true;
            this.rbJustCalled.Location = new System.Drawing.Point(332, 28);
            this.rbJustCalled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbJustCalled.Name = "rbJustCalled";
            this.rbJustCalled.Size = new System.Drawing.Size(187, 29);
            this.rbJustCalled.TabIndex = 6;
            this.rbJustCalled.Text = "Just Called(F7)";
            this.rbJustCalled.UseVisualStyleBackColor = true;
            this.rbJustCalled.CheckedChanged += new System.EventHandler(this.rbJustCalled_CheckedChanged);
            // 
            // rbTCTC
            // 
            this.rbTCTC.AutoSize = true;
            this.rbTCTC.Location = new System.Drawing.Point(15, 28);
            this.rbTCTC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTCTC.Name = "rbTCTC";
            this.rbTCTC.Size = new System.Drawing.Size(254, 29);
            this.rbTCTC.TabIndex = 5;
            this.rbTCTC.Text = "Too Close To Call(F6)";
            this.rbTCTC.UseVisualStyleBackColor = true;
            this.rbTCTC.CheckedChanged += new System.EventHandler(this.rbTCTC_CheckedChanged);
            this.rbTCTC.Click += new System.EventHandler(this.rbTCTC_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox3.Controls.Add(this.btnAddRace2WaySelect);
            this.groupBox3.Controls.Add(this.btnAddRace2Way);
            this.groupBox3.Location = new System.Drawing.Point(636, 466);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(372, 172);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2 - Way";
            // 
            // btnAddRace2WaySelect
            // 
            this.btnAddRace2WaySelect.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRace2WaySelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddRace2WaySelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRace2WaySelect.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRace2WaySelect.Image")));
            this.btnAddRace2WaySelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRace2WaySelect.Location = new System.Drawing.Point(208, 51);
            this.btnAddRace2WaySelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddRace2WaySelect.Name = "btnAddRace2WaySelect";
            this.btnAddRace2WaySelect.Size = new System.Drawing.Size(150, 85);
            this.btnAddRace2WaySelect.TabIndex = 64;
            this.btnAddRace2WaySelect.Text = "Select\r\n(Alt-2)";
            this.btnAddRace2WaySelect.UseVisualStyleBackColor = false;
            this.btnAddRace2WaySelect.Click += new System.EventHandler(this.btnSelect2_Click);
            // 
            // btnAddRace2Way
            // 
            this.btnAddRace2Way.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRace2Way.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddRace2Way.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRace2Way.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRace2Way.Image")));
            this.btnAddRace2Way.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRace2Way.Location = new System.Drawing.Point(16, 51);
            this.btnAddRace2Way.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddRace2Way.Name = "btnAddRace2Way";
            this.btnAddRace2Way.Size = new System.Drawing.Size(150, 85);
            this.btnAddRace2Way.TabIndex = 61;
            this.btnAddRace2Way.Text = "Top\r\n(Ctrl-2)";
            this.btnAddRace2Way.UseVisualStyleBackColor = false;
            this.btnAddRace2Way.Click += new System.EventHandler(this.btnAddRace2Way_Click);
            // 
            // gbROF
            // 
            this.gbROF.BackColor = System.Drawing.Color.PowderBlue;
            this.gbROF.Controls.Add(this.rbShowAll);
            this.gbROF.Controls.Add(this.rbGovernor);
            this.gbROF.Controls.Add(this.rbHouse);
            this.gbROF.Controls.Add(this.rbSenate);
            this.gbROF.Controls.Add(this.rbPresident);
            this.gbROF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbROF.Location = new System.Drawing.Point(9, 6);
            this.gbROF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbROF.Name = "gbROF";
            this.gbROF.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbROF.Size = new System.Drawing.Size(999, 69);
            this.gbROF.TabIndex = 95;
            this.gbROF.TabStop = false;
            this.gbROF.Text = "Race Office Filters";
            // 
            // rbShowAll
            // 
            this.rbShowAll.AutoSize = true;
            this.rbShowAll.Checked = true;
            this.rbShowAll.Location = new System.Drawing.Point(796, 28);
            this.rbShowAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbShowAll.Name = "rbShowAll";
            this.rbShowAll.Size = new System.Drawing.Size(163, 29);
            this.rbShowAll.TabIndex = 9;
            this.rbShowAll.TabStop = true;
            this.rbShowAll.Text = "Show All(F5)";
            this.rbShowAll.UseVisualStyleBackColor = true;
            this.rbShowAll.CheckedChanged += new System.EventHandler(this.rbShowAll_CheckedChanged);
            // 
            // rbGovernor
            // 
            this.rbGovernor.AutoSize = true;
            this.rbGovernor.Location = new System.Drawing.Point(597, 28);
            this.rbGovernor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbGovernor.Name = "rbGovernor";
            this.rbGovernor.Size = new System.Drawing.Size(167, 29);
            this.rbGovernor.TabIndex = 8;
            this.rbGovernor.Text = "Governor(F4)";
            this.rbGovernor.UseVisualStyleBackColor = true;
            this.rbGovernor.CheckedChanged += new System.EventHandler(this.rbGovernor_CheckedChanged);
            // 
            // rbHouse
            // 
            this.rbHouse.AutoSize = true;
            this.rbHouse.Location = new System.Drawing.Point(417, 28);
            this.rbHouse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbHouse.Name = "rbHouse";
            this.rbHouse.Size = new System.Drawing.Size(140, 29);
            this.rbHouse.TabIndex = 7;
            this.rbHouse.Text = "House(F3)";
            this.rbHouse.UseVisualStyleBackColor = true;
            this.rbHouse.CheckedChanged += new System.EventHandler(this.rbHouse_CheckedChanged);
            // 
            // rbSenate
            // 
            this.rbSenate.AutoSize = true;
            this.rbSenate.Location = new System.Drawing.Point(230, 28);
            this.rbSenate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbSenate.Name = "rbSenate";
            this.rbSenate.Size = new System.Drawing.Size(147, 29);
            this.rbSenate.TabIndex = 6;
            this.rbSenate.Text = "Senate(F2)";
            this.rbSenate.UseVisualStyleBackColor = true;
            this.rbSenate.CheckedChanged += new System.EventHandler(this.rbSenate_CheckedChanged);
            // 
            // rbPresident
            // 
            this.rbPresident.AutoSize = true;
            this.rbPresident.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPresident.Location = new System.Drawing.Point(15, 28);
            this.rbPresident.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbPresident.Name = "rbPresident";
            this.rbPresident.Size = new System.Drawing.Size(169, 29);
            this.rbPresident.TabIndex = 5;
            this.rbPresident.Text = "President(F1)";
            this.rbPresident.UseVisualStyleBackColor = true;
            this.rbPresident.CheckedChanged += new System.EventHandler(this.rbPresident_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox2.Controls.Add(this.btnAddRace1Way);
            this.groupBox2.Controls.Add(this.btnAddRace1WaySelect);
            this.groupBox2.Location = new System.Drawing.Point(636, 288);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(372, 172);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1 - Way";
            // 
            // btnAddRace1Way
            // 
            this.btnAddRace1Way.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRace1Way.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddRace1Way.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRace1Way.Image = global::GUILayer.Properties.Resources.AddDataItem;
            this.btnAddRace1Way.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRace1Way.Location = new System.Drawing.Point(16, 51);
            this.btnAddRace1Way.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddRace1Way.Name = "btnAddRace1Way";
            this.btnAddRace1Way.Size = new System.Drawing.Size(150, 85);
            this.btnAddRace1Way.TabIndex = 64;
            this.btnAddRace1Way.Text = "Top\r\n(Ctrl-1)";
            this.btnAddRace1Way.UseVisualStyleBackColor = false;
            this.btnAddRace1Way.Click += new System.EventHandler(this.btnAddRace1Way_Click);
            // 
            // btnAddRace1WaySelect
            // 
            this.btnAddRace1WaySelect.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRace1WaySelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddRace1WaySelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRace1WaySelect.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRace1WaySelect.Image")));
            this.btnAddRace1WaySelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRace1WaySelect.Location = new System.Drawing.Point(208, 51);
            this.btnAddRace1WaySelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddRace1WaySelect.Name = "btnAddRace1WaySelect";
            this.btnAddRace1WaySelect.Size = new System.Drawing.Size(150, 85);
            this.btnAddRace1WaySelect.TabIndex = 63;
            this.btnAddRace1WaySelect.Text = "Select\r\n(Alt-1)";
            this.btnAddRace1WaySelect.UseVisualStyleBackColor = false;
            this.btnAddRace1WaySelect.Click += new System.EventHandler(this.btnSelect1_Click);
            // 
            // availableRacesGrid
            // 
            this.availableRacesGrid.AllowUserToAddRows = false;
            this.availableRacesGrid.AllowUserToDeleteRows = false;
            this.availableRacesGrid.AllowUserToResizeColumns = false;
            this.availableRacesGrid.AllowUserToResizeRows = false;
            this.availableRacesGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.availableRacesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.availableRacesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Race_ID,
            this.Race_Description});
            this.availableRacesGrid.Location = new System.Drawing.Point(9, 288);
            this.availableRacesGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.availableRacesGrid.MultiSelect = false;
            this.availableRacesGrid.Name = "availableRacesGrid";
            this.availableRacesGrid.ReadOnly = true;
            this.availableRacesGrid.RowHeadersWidth = 15;
            this.availableRacesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.availableRacesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.availableRacesGrid.Size = new System.Drawing.Size(618, 883);
            this.availableRacesGrid.TabIndex = 57;
            this.availableRacesGrid.DoubleClick += new System.EventHandler(this.availableRacesGrid_DoubleClick);
            // 
            // Race_ID
            // 
            this.Race_ID.DataPropertyName = "Race_ID";
            this.Race_ID.HeaderText = "Race ID";
            this.Race_ID.Name = "Race_ID";
            this.Race_ID.ReadOnly = true;
            this.Race_ID.Width = 90;
            // 
            // Race_Description
            // 
            this.Race_Description.DataPropertyName = "Race_Description";
            this.Race_Description.HeaderText = "Race Description";
            this.Race_Description.Name = "Race_Description";
            this.Race_Description.ReadOnly = true;
            this.Race_Description.Width = 302;
            // 
            // tpExitPolls
            // 
            this.tpExitPolls.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tpExitPolls.Controls.Add(this.lblVAcnt);
            this.tpExitPolls.Controls.Add(this.dgvVoterAnalysis);
            this.tpExitPolls.Controls.Add(this.tcVoterAnalysis);
            this.tpExitPolls.Controls.Add(this.btnAddExitPoll);
            this.tpExitPolls.Location = new System.Drawing.Point(4, 38);
            this.tpExitPolls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpExitPolls.Name = "tpExitPolls";
            this.tpExitPolls.Size = new System.Drawing.Size(1021, 1180);
            this.tpExitPolls.TabIndex = 1;
            this.tpExitPolls.Text = "Voter Analysis";
            // 
            // lblVAcnt
            // 
            this.lblVAcnt.AutoSize = true;
            this.lblVAcnt.Location = new System.Drawing.Point(6, 13);
            this.lblVAcnt.Name = "lblVAcnt";
            this.lblVAcnt.Size = new System.Drawing.Size(317, 29);
            this.lblVAcnt.TabIndex = 134;
            this.lblVAcnt.Text = "Voter Analysis Questions: ";
            // 
            // dgvVoterAnalysis
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVoterAnalysis.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVoterAnalysis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoterAnalysis.Location = new System.Drawing.Point(16, 118);
            this.dgvVoterAnalysis.MultiSelect = false;
            this.dgvVoterAnalysis.Name = "dgvVoterAnalysis";
            this.dgvVoterAnalysis.ReadOnly = true;
            this.dgvVoterAnalysis.RowHeadersWidth = 20;
            this.dgvVoterAnalysis.RowTemplate.ReadOnly = true;
            this.dgvVoterAnalysis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoterAnalysis.Size = new System.Drawing.Size(992, 832);
            this.dgvVoterAnalysis.TabIndex = 133;
            this.dgvVoterAnalysis.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVoterAnalysis_CellContentDoubleClick);
            // 
            // tcVoterAnalysis
            // 
            this.tcVoterAnalysis.Controls.Add(this.tpFullScreen);
            this.tcVoterAnalysis.Controls.Add(this.tpTicker);
            this.tcVoterAnalysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcVoterAnalysis.Location = new System.Drawing.Point(7, 55);
            this.tcVoterAnalysis.Name = "tcVoterAnalysis";
            this.tcVoterAnalysis.SelectedIndex = 0;
            this.tcVoterAnalysis.Size = new System.Drawing.Size(1011, 908);
            this.tcVoterAnalysis.TabIndex = 132;
            this.tcVoterAnalysis.SelectedIndexChanged += new System.EventHandler(this.tcVoterAnalysis_SelectedIndexChanged);
            // 
            // tpFullScreen
            // 
            this.tpFullScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpFullScreen.Location = new System.Drawing.Point(4, 38);
            this.tpFullScreen.Name = "tpFullScreen";
            this.tpFullScreen.Padding = new System.Windows.Forms.Padding(3);
            this.tpFullScreen.Size = new System.Drawing.Size(1003, 866);
            this.tpFullScreen.TabIndex = 0;
            this.tpFullScreen.Text = "Full Screen";
            this.tpFullScreen.UseVisualStyleBackColor = true;
            // 
            // tpTicker
            // 
            this.tpTicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpTicker.Location = new System.Drawing.Point(4, 38);
            this.tpTicker.Name = "tpTicker";
            this.tpTicker.Padding = new System.Windows.Forms.Padding(3);
            this.tpTicker.Size = new System.Drawing.Size(1003, 866);
            this.tpTicker.TabIndex = 1;
            this.tpTicker.Text = "Ticker";
            this.tpTicker.UseVisualStyleBackColor = true;
            // 
            // btnAddExitPoll
            // 
            this.btnAddExitPoll.Image = global::GUILayer.Properties.Resources.action_add_16xLG;
            this.btnAddExitPoll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddExitPoll.Location = new System.Drawing.Point(338, 995);
            this.btnAddExitPoll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddExitPoll.Name = "btnAddExitPoll";
            this.btnAddExitPoll.Size = new System.Drawing.Size(328, 92);
            this.btnAddExitPoll.TabIndex = 2;
            this.btnAddExitPoll.Text = "Add Exit Poll";
            this.btnAddExitPoll.UseVisualStyleBackColor = true;
            this.btnAddExitPoll.Click += new System.EventHandler(this.btnAddExitPoll_Click);
            // 
            // tpBalanceOfPower
            // 
            this.tpBalanceOfPower.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tpBalanceOfPower.Controls.Add(this.BOPdataGridView);
            this.tpBalanceOfPower.Controls.Add(this.btnAddBalanceOfPower);
            this.tpBalanceOfPower.Location = new System.Drawing.Point(4, 38);
            this.tpBalanceOfPower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpBalanceOfPower.Name = "tpBalanceOfPower";
            this.tpBalanceOfPower.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpBalanceOfPower.Size = new System.Drawing.Size(1021, 1180);
            this.tpBalanceOfPower.TabIndex = 2;
            this.tpBalanceOfPower.Text = "Balance of Power";
            // 
            // BOPdataGridView
            // 
            this.BOPdataGridView.AllowUserToAddRows = false;
            this.BOPdataGridView.AllowUserToDeleteRows = false;
            this.BOPdataGridView.AllowUserToResizeColumns = false;
            this.BOPdataGridView.AllowUserToResizeRows = false;
            this.BOPdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BOPdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eType,
            this.Branch,
            this.Session});
            this.BOPdataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.BOPdataGridView.Location = new System.Drawing.Point(150, 91);
            this.BOPdataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BOPdataGridView.Name = "BOPdataGridView";
            this.BOPdataGridView.RowHeadersWidth = 15;
            this.BOPdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BOPdataGridView.Size = new System.Drawing.Size(710, 554);
            this.BOPdataGridView.TabIndex = 2;
            this.BOPdataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BOPdataGridView_CellDoubleClick);
            // 
            // eType
            // 
            this.eType.HeaderText = "eType";
            this.eType.Name = "eType";
            this.eType.Width = 70;
            // 
            // Branch
            // 
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.Width = 200;
            // 
            // Session
            // 
            this.Session.HeaderText = "Session";
            this.Session.Name = "Session";
            this.Session.Width = 200;
            // 
            // btnAddBalanceOfPower
            // 
            this.btnAddBalanceOfPower.Image = global::GUILayer.Properties.Resources.action_add_16xLG;
            this.btnAddBalanceOfPower.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddBalanceOfPower.Location = new System.Drawing.Point(345, 709);
            this.btnAddBalanceOfPower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddBalanceOfPower.Name = "btnAddBalanceOfPower";
            this.btnAddBalanceOfPower.Size = new System.Drawing.Size(328, 92);
            this.btnAddBalanceOfPower.TabIndex = 1;
            this.btnAddBalanceOfPower.Text = "Add Balance of Power";
            this.btnAddBalanceOfPower.UseVisualStyleBackColor = true;
            this.btnAddBalanceOfPower.Click += new System.EventHandler(this.button2_Click);
            // 
            // tpReferendums
            // 
            this.tpReferendums.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tpReferendums.Controls.Add(this.btnAddReferendum);
            this.tpReferendums.Controls.Add(this.ReferendumsGrid);
            this.tpReferendums.Location = new System.Drawing.Point(4, 38);
            this.tpReferendums.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpReferendums.Name = "tpReferendums";
            this.tpReferendums.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpReferendums.Size = new System.Drawing.Size(1021, 1180);
            this.tpReferendums.TabIndex = 3;
            this.tpReferendums.Text = "Referendums";
            // 
            // btnAddReferendum
            // 
            this.btnAddReferendum.Image = global::GUILayer.Properties.Resources.action_add_16xLG;
            this.btnAddReferendum.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddReferendum.Location = new System.Drawing.Point(387, 895);
            this.btnAddReferendum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddReferendum.Name = "btnAddReferendum";
            this.btnAddReferendum.Size = new System.Drawing.Size(258, 92);
            this.btnAddReferendum.TabIndex = 2;
            this.btnAddReferendum.Text = "Add Referendum";
            this.btnAddReferendum.UseVisualStyleBackColor = true;
            this.btnAddReferendum.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReferendumsGrid
            // 
            this.ReferendumsGrid.AllowUserToAddRows = false;
            this.ReferendumsGrid.AllowUserToDeleteRows = false;
            this.ReferendumsGrid.AllowUserToResizeColumns = false;
            this.ReferendumsGrid.AllowUserToResizeRows = false;
            this.ReferendumsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ReferendumsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReferendumsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6});
            this.ReferendumsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ReferendumsGrid.Location = new System.Drawing.Point(10, 91);
            this.ReferendumsGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ReferendumsGrid.MultiSelect = false;
            this.ReferendumsGrid.Name = "ReferendumsGrid";
            this.ReferendumsGrid.RowHeadersWidth = 15;
            this.ReferendumsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ReferendumsGrid.Size = new System.Drawing.Size(998, 742);
            this.ReferendumsGrid.TabIndex = 1;
            this.ReferendumsGrid.DoubleClick += new System.EventHandler(this.ReferendumsGrid_DoubleClick);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "race_Description";
            this.dataGridViewTextBoxColumn6.HeaderText = "Referendum Description";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 700;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Type.DataPropertyName = "questionType";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // st
            // 
            this.st.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.st.DataPropertyName = "stateOffice";
            this.st.HeaderText = "mxID/st/ofc";
            this.st.Name = "st";
            // 
            // Category
            // 
            this.Category.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Category.DataPropertyName = "shortMXLabel";
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            // 
            // Question
            // 
            this.Question.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Question.DataPropertyName = "fullMXLabel";
            this.Question.HeaderText = "Question";
            this.Question.Name = "Question";
            // 
            // rowText
            // 
            this.rowText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.rowText.DataPropertyName = "rowText";
            this.rowText.HeaderText = "rowText";
            this.rowText.Name = "rowText";
            // 
            // Subset
            // 
            this.Subset.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Subset.DataPropertyName = "subsetName";
            this.Subset.HeaderText = "Subset";
            this.Subset.Name = "Subset";
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.Color.Black;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Red;
            this.timeLabel.Location = new System.Drawing.Point(9, 26);
            this.timeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(372, 31);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "Time";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerStatusUpdate
            // 
            this.timerStatusUpdate.Interval = 1000;
            this.timerStatusUpdate.Tick += new System.EventHandler(this.timerStatusUpdate_Tick);
            // 
            // gbTime
            // 
            this.gbTime.Controls.Add(this.timeLabel);
            this.gbTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTime.Location = new System.Drawing.Point(1752, 42);
            this.gbTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbTime.Name = "gbTime";
            this.gbTime.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbTime.Size = new System.Drawing.Size(390, 65);
            this.gbTime.TabIndex = 119;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "SIMULATED TIME";
            // 
            // pnlStack
            // 
            this.pnlStack.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlStack.Controls.Add(this.pnlUpDn);
            this.pnlStack.Controls.Add(this.panel2);
            this.pnlStack.Controls.Add(this.cbGraphicConcept);
            this.pnlStack.Controls.Add(this.label2);
            this.pnlStack.Controls.Add(this.txtStackName);
            this.pnlStack.Controls.Add(this.txtStackEntriesCount);
            this.pnlStack.Controls.Add(this.lblStackEntriesCount);
            this.pnlStack.Controls.Add(this.lblStackHeader);
            this.pnlStack.Location = new System.Drawing.Point(1044, 169);
            this.pnlStack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlStack.Name = "pnlStack";
            this.pnlStack.Size = new System.Drawing.Size(1098, 1185);
            this.pnlStack.TabIndex = 120;
            // 
            // pnlUpDn
            // 
            this.pnlUpDn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlUpDn.Controls.Add(this.btnStackElementDown);
            this.pnlUpDn.Controls.Add(this.btnStackElementUp);
            this.pnlUpDn.Location = new System.Drawing.Point(1014, 305);
            this.pnlUpDn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlUpDn.Name = "pnlUpDn";
            this.pnlUpDn.Size = new System.Drawing.Size(75, 248);
            this.pnlUpDn.TabIndex = 140;
            // 
            // btnStackElementDown
            // 
            this.btnStackElementDown.BackColor = System.Drawing.SystemColors.Control;
            this.btnStackElementDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStackElementDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStackElementDown.Image = ((System.Drawing.Image)(resources.GetObject("btnStackElementDown.Image")));
            this.btnStackElementDown.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStackElementDown.Location = new System.Drawing.Point(6, 143);
            this.btnStackElementDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStackElementDown.Name = "btnStackElementDown";
            this.btnStackElementDown.Size = new System.Drawing.Size(63, 92);
            this.btnStackElementDown.TabIndex = 73;
            this.btnStackElementDown.Text = "DN";
            this.btnStackElementDown.UseVisualStyleBackColor = false;
            this.btnStackElementDown.Click += new System.EventHandler(this.btnStackElementDown_Click);
            // 
            // btnStackElementUp
            // 
            this.btnStackElementUp.BackColor = System.Drawing.SystemColors.Control;
            this.btnStackElementUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStackElementUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStackElementUp.Image = ((System.Drawing.Image)(resources.GetObject("btnStackElementUp.Image")));
            this.btnStackElementUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStackElementUp.Location = new System.Drawing.Point(6, 15);
            this.btnStackElementUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStackElementUp.Name = "btnStackElementUp";
            this.btnStackElementUp.Size = new System.Drawing.Size(63, 92);
            this.btnStackElementUp.TabIndex = 72;
            this.btnStackElementUp.Text = "UP";
            this.btnStackElementUp.UseVisualStyleBackColor = false;
            this.btnStackElementUp.Click += new System.EventHandler(this.btnStackElementUp_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Navy;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.stackGrid);
            this.panel2.Location = new System.Drawing.Point(8, 83);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(999, 1092);
            this.panel2.TabIndex = 139;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel3.Controls.Add(this.TakePanel);
            this.panel3.Controls.Add(this.LockPanel);
            this.panel3.Controls.Add(this.SaveActivatePanel);
            this.panel3.Controls.Add(this.StackPanel);
            this.panel3.Location = new System.Drawing.Point(12, 740);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(972, 340);
            this.panel3.TabIndex = 143;
            // 
            // TakePanel
            // 
            this.TakePanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TakePanel.Controls.Add(this.cbAutoCalledRaces);
            this.TakePanel.Controls.Add(this.cbLooping);
            this.TakePanel.Controls.Add(this.btnTake);
            this.TakePanel.Location = new System.Drawing.Point(638, 5);
            this.TakePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TakePanel.Name = "TakePanel";
            this.TakePanel.Size = new System.Drawing.Size(330, 331);
            this.TakePanel.TabIndex = 146;
            // 
            // cbAutoCalledRaces
            // 
            this.cbAutoCalledRaces.AutoSize = true;
            this.cbAutoCalledRaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoCalledRaces.Location = new System.Drawing.Point(33, 60);
            this.cbAutoCalledRaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAutoCalledRaces.Name = "cbAutoCalledRaces";
            this.cbAutoCalledRaces.Size = new System.Drawing.Size(217, 29);
            this.cbAutoCalledRaces.TabIndex = 145;
            this.cbAutoCalledRaces.Text = "Auto Called Races";
            this.cbAutoCalledRaces.UseVisualStyleBackColor = true;
            // 
            // cbLooping
            // 
            this.cbLooping.AutoSize = true;
            this.cbLooping.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLooping.Location = new System.Drawing.Point(30, 18);
            this.cbLooping.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbLooping.Name = "cbLooping";
            this.cbLooping.Size = new System.Drawing.Size(115, 29);
            this.cbLooping.TabIndex = 144;
            this.cbLooping.Text = "Looping";
            this.cbLooping.UseVisualStyleBackColor = true;
            this.cbLooping.CheckedChanged += new System.EventHandler(this.cbLooping_CheckedChanged);
            // 
            // btnTake
            // 
            this.btnTake.BackColor = System.Drawing.SystemColors.Control;
            this.btnTake.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTake.Image = ((System.Drawing.Image)(resources.GetObject("btnTake.Image")));
            this.btnTake.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTake.Location = new System.Drawing.Point(30, 102);
            this.btnTake.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(270, 214);
            this.btnTake.TabIndex = 143;
            this.btnTake.Text = "Take\r\n Next(Space)";
            this.btnTake.UseVisualStyleBackColor = false;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // LockPanel
            // 
            this.LockPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.LockPanel.Controls.Add(this.btnLock);
            this.LockPanel.Controls.Add(this.btnUnlock);
            this.LockPanel.Location = new System.Drawing.Point(2, 2);
            this.LockPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LockPanel.Name = "LockPanel";
            this.LockPanel.Size = new System.Drawing.Size(630, 117);
            this.LockPanel.TabIndex = 148;
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.SystemColors.Control;
            this.btnLock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLock.Image = ((System.Drawing.Image)(resources.GetObject("btnLock.Image")));
            this.btnLock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLock.Location = new System.Drawing.Point(22, 22);
            this.btnLock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(270, 85);
            this.btnLock.TabIndex = 141;
            this.btnLock.Text = "Lock (Ctrl-L)";
            this.btnLock.UseVisualStyleBackColor = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnlock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUnlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlock.Image = ((System.Drawing.Image)(resources.GetObject("btnUnlock.Image")));
            this.btnUnlock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnlock.Location = new System.Drawing.Point(338, 22);
            this.btnUnlock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(270, 85);
            this.btnUnlock.TabIndex = 140;
            this.btnUnlock.Text = "Unlock\r\n(Ctrl-U)";
            this.btnUnlock.UseVisualStyleBackColor = false;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // SaveActivatePanel
            // 
            this.SaveActivatePanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SaveActivatePanel.Controls.Add(this.cbPromptForInfo);
            this.SaveActivatePanel.Controls.Add(this.btnSaveActivateStack);
            this.SaveActivatePanel.Location = new System.Drawing.Point(636, 123);
            this.SaveActivatePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaveActivatePanel.Name = "SaveActivatePanel";
            this.SaveActivatePanel.Size = new System.Drawing.Size(334, 214);
            this.SaveActivatePanel.TabIndex = 145;
            // 
            // cbPromptForInfo
            // 
            this.cbPromptForInfo.AutoSize = true;
            this.cbPromptForInfo.Checked = true;
            this.cbPromptForInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPromptForInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPromptForInfo.Location = new System.Drawing.Point(33, 171);
            this.cbPromptForInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbPromptForInfo.Name = "cbPromptForInfo";
            this.cbPromptForInfo.Size = new System.Drawing.Size(179, 29);
            this.cbPromptForInfo.TabIndex = 139;
            this.cbPromptForInfo.Text = "Prompt for Info";
            this.cbPromptForInfo.UseVisualStyleBackColor = true;
            // 
            // btnSaveActivateStack
            // 
            this.btnSaveActivateStack.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveActivateStack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveActivateStack.Image = global::GUILayer.Properties.Resources.StatusAnnotations_Complete_and_ok_16xLG_color;
            this.btnSaveActivateStack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveActivateStack.Location = new System.Drawing.Point(33, 12);
            this.btnSaveActivateStack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveActivateStack.Name = "btnSaveActivateStack";
            this.btnSaveActivateStack.Size = new System.Drawing.Size(270, 154);
            this.btnSaveActivateStack.TabIndex = 138;
            this.btnSaveActivateStack.Text = "Save && Activate\r\nStack\r\n(Ctrl-S)";
            this.btnSaveActivateStack.UseVisualStyleBackColor = false;
            this.btnSaveActivateStack.Click += new System.EventHandler(this.btnSaveActivateStack_Click);
            // 
            // StackPanel
            // 
            this.StackPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.StackPanel.Controls.Add(this.btnClearStack);
            this.StackPanel.Controls.Add(this.btnDeleteStackElement);
            this.StackPanel.Controls.Add(this.btnLoadStack);
            this.StackPanel.Controls.Add(this.btnSaveStack);
            this.StackPanel.Location = new System.Drawing.Point(2, 118);
            this.StackPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StackPanel.Name = "StackPanel";
            this.StackPanel.Size = new System.Drawing.Size(630, 218);
            this.StackPanel.TabIndex = 147;
            // 
            // btnClearStack
            // 
            this.btnClearStack.BackColor = System.Drawing.SystemColors.Control;
            this.btnClearStack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearStack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearStack.Image = global::GUILayer.Properties.Resources.action_Cancel_16xLG;
            this.btnClearStack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearStack.Location = new System.Drawing.Point(22, 120);
            this.btnClearStack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClearStack.Name = "btnClearStack";
            this.btnClearStack.Size = new System.Drawing.Size(270, 85);
            this.btnClearStack.TabIndex = 140;
            this.btnClearStack.Text = "Clear Stack\r\n(Ctrl-C)";
            this.btnClearStack.UseVisualStyleBackColor = false;
            this.btnClearStack.Click += new System.EventHandler(this.btnClearStack_Click);
            // 
            // btnDeleteStackElement
            // 
            this.btnDeleteStackElement.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteStackElement.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteStackElement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteStackElement.Image = global::GUILayer.Properties.Resources.action_Cancel_16xLG;
            this.btnDeleteStackElement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteStackElement.Location = new System.Drawing.Point(22, 15);
            this.btnDeleteStackElement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDeleteStackElement.Name = "btnDeleteStackElement";
            this.btnDeleteStackElement.Size = new System.Drawing.Size(270, 85);
            this.btnDeleteStackElement.TabIndex = 139;
            this.btnDeleteStackElement.Text = "Delete Element\r\n(Ctrl-D)";
            this.btnDeleteStackElement.UseVisualStyleBackColor = false;
            this.btnDeleteStackElement.Click += new System.EventHandler(this.btnDeleteStackElement_Click);
            // 
            // btnLoadStack
            // 
            this.btnLoadStack.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoadStack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadStack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadStack.Image = global::GUILayer.Properties.Resources.folder_Open_16xLG;
            this.btnLoadStack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadStack.Location = new System.Drawing.Point(338, 15);
            this.btnLoadStack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadStack.Name = "btnLoadStack";
            this.btnLoadStack.Size = new System.Drawing.Size(270, 85);
            this.btnLoadStack.TabIndex = 138;
            this.btnLoadStack.Text = "Recall Stack\r\n(Ctrl-R)";
            this.btnLoadStack.UseVisualStyleBackColor = false;
            this.btnLoadStack.Click += new System.EventHandler(this.btnLoadStack_Click);
            // 
            // btnSaveStack
            // 
            this.btnSaveStack.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveStack.Enabled = false;
            this.btnSaveStack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveStack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveStack.Image = global::GUILayer.Properties.Resources.folder_Closed_16xLG;
            this.btnSaveStack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveStack.Location = new System.Drawing.Point(338, 120);
            this.btnSaveStack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveStack.Name = "btnSaveStack";
            this.btnSaveStack.Size = new System.Drawing.Size(270, 85);
            this.btnSaveStack.TabIndex = 137;
            this.btnSaveStack.Text = "Save Stack Only\r\n(Ctrl-O)";
            this.btnSaveStack.UseVisualStyleBackColor = false;
            this.btnSaveStack.Click += new System.EventHandler(this.btnSaveStack_Click);
            // 
            // stackGrid
            // 
            this.stackGrid.AllowUserToAddRows = false;
            this.stackGrid.AllowUserToDeleteRows = false;
            this.stackGrid.AllowUserToResizeColumns = false;
            this.stackGrid.AllowUserToResizeRows = false;
            this.stackGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.stackGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stackGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Element_Type_Description,
            this.TemplateID,
            this.Stack_Entry_Description});
            this.stackGrid.Location = new System.Drawing.Point(12, 12);
            this.stackGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stackGrid.MultiSelect = false;
            this.stackGrid.Name = "stackGrid";
            this.stackGrid.ReadOnly = true;
            this.stackGrid.RowHeadersVisible = false;
            this.stackGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.stackGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.stackGrid.Size = new System.Drawing.Size(972, 715);
            this.stackGrid.TabIndex = 142;
            this.stackGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.stackGrid_CellDoubleClick);
            // 
            // Element_Type_Description
            // 
            this.Element_Type_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Element_Type_Description.DataPropertyName = "Stack_Element_Description";
            this.Element_Type_Description.HeaderText = "Entry Type";
            this.Element_Type_Description.Name = "Element_Type_Description";
            this.Element_Type_Description.ReadOnly = true;
            this.Element_Type_Description.Width = 160;
            // 
            // TemplateID
            // 
            this.TemplateID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TemplateID.DataPropertyName = "Stack_Element_TemplateID";
            this.TemplateID.HeaderText = "Template Name";
            this.TemplateID.Name = "TemplateID";
            this.TemplateID.ReadOnly = true;
            this.TemplateID.Width = 185;
            // 
            // Stack_Entry_Description
            // 
            this.Stack_Entry_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Stack_Entry_Description.DataPropertyName = "Listbox_Description";
            this.Stack_Entry_Description.HeaderText = "Entry Description";
            this.Stack_Entry_Description.Name = "Stack_Entry_Description";
            this.Stack_Entry_Description.ReadOnly = true;
            this.Stack_Entry_Description.Width = 280;
            // 
            // cbGraphicConcept
            // 
            this.cbGraphicConcept.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cbGraphicConcept.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGraphicConcept.FormattingEnabled = true;
            this.cbGraphicConcept.Location = new System.Drawing.Point(744, 9);
            this.cbGraphicConcept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbGraphicConcept.Name = "cbGraphicConcept";
            this.cbGraphicConcept.Size = new System.Drawing.Size(278, 33);
            this.cbGraphicConcept.TabIndex = 138;
            this.cbGraphicConcept.SelectedIndexChanged += new System.EventHandler(this.cbGraphicConcept_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(555, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 25);
            this.label2.TabIndex = 137;
            this.label2.Text = "Graphic Concept:";
            // 
            // txtStackName
            // 
            this.txtStackName.AutoSize = true;
            this.txtStackName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStackName.Location = new System.Drawing.Point(92, 14);
            this.txtStackName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtStackName.Name = "txtStackName";
            this.txtStackName.Size = new System.Drawing.Size(154, 25);
            this.txtStackName.TabIndex = 136;
            this.txtStackName.Text = "None Selected";
            // 
            // txtStackEntriesCount
            // 
            this.txtStackEntriesCount.AutoSize = true;
            this.txtStackEntriesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStackEntriesCount.Location = new System.Drawing.Point(243, 48);
            this.txtStackEntriesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtStackEntriesCount.Name = "txtStackEntriesCount";
            this.txtStackEntriesCount.Size = new System.Drawing.Size(24, 25);
            this.txtStackEntriesCount.TabIndex = 135;
            this.txtStackEntriesCount.Text = "0";
            // 
            // lblStackEntriesCount
            // 
            this.lblStackEntriesCount.AutoSize = true;
            this.lblStackEntriesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStackEntriesCount.Location = new System.Drawing.Point(20, 48);
            this.lblStackEntriesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStackEntriesCount.Name = "lblStackEntriesCount";
            this.lblStackEntriesCount.Size = new System.Drawing.Size(213, 25);
            this.lblStackEntriesCount.TabIndex = 134;
            this.lblStackEntriesCount.Text = "Number of Elements:";
            // 
            // lblStackHeader
            // 
            this.lblStackHeader.AutoSize = true;
            this.lblStackHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStackHeader.Location = new System.Drawing.Point(20, 14);
            this.lblStackHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStackHeader.Name = "lblStackHeader";
            this.lblStackHeader.Size = new System.Drawing.Size(74, 25);
            this.lblStackHeader.TabIndex = 133;
            this.lblStackHeader.Text = "Stack:";
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpAddress.Location = new System.Drawing.Point(1021, 48);
            this.lblIpAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(49, 25);
            this.lblIpAddress.TabIndex = 121;
            this.lblIpAddress.Text = "N/A";
            // 
            // lblHostName
            // 
            this.lblHostName.AutoSize = true;
            this.lblHostName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHostName.Location = new System.Drawing.Point(1183, 48);
            this.lblHostName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHostName.Name = "lblHostName";
            this.lblHostName.Size = new System.Drawing.Size(49, 25);
            this.lblHostName.TabIndex = 122;
            this.lblHostName.Text = "N/A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(880, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 25);
            this.label3.TabIndex = 123;
            this.label3.Text = "Host PC Info:";
            // 
            // lblMediaSequencer
            // 
            this.lblMediaSequencer.BackColor = System.Drawing.Color.White;
            this.lblMediaSequencer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMediaSequencer.Location = new System.Drawing.Point(1044, 122);
            this.lblMediaSequencer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMediaSequencer.Name = "lblMediaSequencer";
            this.lblMediaSequencer.Size = new System.Drawing.Size(1098, 42);
            this.lblMediaSequencer.TabIndex = 124;
            this.lblMediaSequencer.Text = "USING PRIMARY MEDIA SEQUENCER:";
            this.lblMediaSequencer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(10, 1526);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(374, 164);
            this.listBox1.TabIndex = 126;
            // 
            // LiveUpdateTimer
            // 
            this.LiveUpdateTimer.Interval = 5000;
            this.LiveUpdateTimer.Tick += new System.EventHandler(this.LiveUpdateTimer_Tick);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(388, 1526);
            this.listBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(1759, 164);
            this.listBox2.TabIndex = 127;
            // 
            // connectionPanel
            // 
            this.connectionPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.connectionPanel.Controls.Add(this.gbViz6);
            this.connectionPanel.Controls.Add(this.gbViz5);
            this.connectionPanel.Controls.Add(this.gbViz4);
            this.connectionPanel.Controls.Add(this.gbViz3);
            this.connectionPanel.Controls.Add(this.gbViz2);
            this.connectionPanel.Controls.Add(this.gbViz1);
            this.connectionPanel.Location = new System.Drawing.Point(10, 1362);
            this.connectionPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Size = new System.Drawing.Size(1422, 155);
            this.connectionPanel.TabIndex = 128;
            // 
            // gbViz6
            // 
            this.gbViz6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbViz6.Controls.Add(this.groupBox12);
            this.gbViz6.Controls.Add(this.gbPortlbl6);
            this.gbViz6.Controls.Add(this.gbIPlbl6);
            this.gbViz6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbViz6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbViz6.Location = new System.Drawing.Point(1778, 11);
            this.gbViz6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz6.Name = "gbViz6";
            this.gbViz6.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz6.Size = new System.Drawing.Size(338, 108);
            this.gbViz6.TabIndex = 5;
            this.gbViz6.TabStop = false;
            this.gbViz6.Text = "Viz  6";
            this.gbViz6.Visible = false;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.gbLEDOn6);
            this.groupBox12.Controls.Add(this.gbLEDOff6);
            this.groupBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox12.Location = new System.Drawing.Point(192, 23);
            this.groupBox12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox12.Size = new System.Drawing.Size(135, 74);
            this.groupBox12.TabIndex = 178;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Connected";
            // 
            // gbLEDOn6
            // 
            this.gbLEDOn6.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOn6.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOn6.Image")));
            this.gbLEDOn6.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOn6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOn6.Name = "gbLEDOn6";
            this.gbLEDOn6.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOn6.TabIndex = 164;
            this.gbLEDOn6.TabStop = false;
            this.gbLEDOn6.Visible = false;
            // 
            // gbLEDOff6
            // 
            this.gbLEDOff6.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOff6.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOff6.Image")));
            this.gbLEDOff6.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOff6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOff6.Name = "gbLEDOff6";
            this.gbLEDOff6.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOff6.TabIndex = 163;
            this.gbLEDOff6.TabStop = false;
            // 
            // gbPortlbl6
            // 
            this.gbPortlbl6.AutoSize = true;
            this.gbPortlbl6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPortlbl6.Location = new System.Drawing.Point(6, 71);
            this.gbPortlbl6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbPortlbl6.Name = "gbPortlbl6";
            this.gbPortlbl6.Size = new System.Drawing.Size(102, 25);
            this.gbPortlbl6.TabIndex = 1;
            this.gbPortlbl6.Text = "Port: 6100";
            // 
            // gbIPlbl6
            // 
            this.gbIPlbl6.AutoSize = true;
            this.gbIPlbl6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIPlbl6.Location = new System.Drawing.Point(6, 40);
            this.gbIPlbl6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbIPlbl6.Name = "gbIPlbl6";
            this.gbIPlbl6.Size = new System.Drawing.Size(155, 25);
            this.gbIPlbl6.TabIndex = 0;
            this.gbIPlbl6.Text = "IP: 10.232.86.84";
            // 
            // gbViz5
            // 
            this.gbViz5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbViz5.Controls.Add(this.groupBox11);
            this.gbViz5.Controls.Add(this.gbPortlbl5);
            this.gbViz5.Controls.Add(this.gbIPlbl5);
            this.gbViz5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbViz5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbViz5.Location = new System.Drawing.Point(1425, 11);
            this.gbViz5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz5.Name = "gbViz5";
            this.gbViz5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz5.Size = new System.Drawing.Size(338, 108);
            this.gbViz5.TabIndex = 4;
            this.gbViz5.TabStop = false;
            this.gbViz5.Text = "Viz  5";
            this.gbViz5.Visible = false;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.gbLEDOn5);
            this.groupBox11.Controls.Add(this.gbLEDOff5);
            this.groupBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox11.Location = new System.Drawing.Point(192, 23);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox11.Size = new System.Drawing.Size(135, 74);
            this.groupBox11.TabIndex = 178;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Connected";
            // 
            // gbLEDOn5
            // 
            this.gbLEDOn5.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOn5.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOn5.Image")));
            this.gbLEDOn5.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOn5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOn5.Name = "gbLEDOn5";
            this.gbLEDOn5.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOn5.TabIndex = 164;
            this.gbLEDOn5.TabStop = false;
            this.gbLEDOn5.Visible = false;
            // 
            // gbLEDOff5
            // 
            this.gbLEDOff5.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOff5.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOff5.Image")));
            this.gbLEDOff5.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOff5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOff5.Name = "gbLEDOff5";
            this.gbLEDOff5.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOff5.TabIndex = 163;
            this.gbLEDOff5.TabStop = false;
            // 
            // gbPortlbl5
            // 
            this.gbPortlbl5.AutoSize = true;
            this.gbPortlbl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPortlbl5.Location = new System.Drawing.Point(6, 71);
            this.gbPortlbl5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbPortlbl5.Name = "gbPortlbl5";
            this.gbPortlbl5.Size = new System.Drawing.Size(102, 25);
            this.gbPortlbl5.TabIndex = 1;
            this.gbPortlbl5.Text = "Port: 6100";
            // 
            // gbIPlbl5
            // 
            this.gbIPlbl5.AutoSize = true;
            this.gbIPlbl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIPlbl5.Location = new System.Drawing.Point(6, 40);
            this.gbIPlbl5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbIPlbl5.Name = "gbIPlbl5";
            this.gbIPlbl5.Size = new System.Drawing.Size(155, 25);
            this.gbIPlbl5.TabIndex = 0;
            this.gbIPlbl5.Text = "IP: 10.232.86.84";
            // 
            // gbViz4
            // 
            this.gbViz4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbViz4.Controls.Add(this.gbNamelbl4);
            this.gbViz4.Controls.Add(this.groupBox10);
            this.gbViz4.Controls.Add(this.gbPortlbl4);
            this.gbViz4.Controls.Add(this.gbIPlbl4);
            this.gbViz4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbViz4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbViz4.Location = new System.Drawing.Point(1072, 8);
            this.gbViz4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz4.Name = "gbViz4";
            this.gbViz4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz4.Size = new System.Drawing.Size(338, 128);
            this.gbViz4.TabIndex = 3;
            this.gbViz4.TabStop = false;
            this.gbViz4.Text = "Viz  4";
            this.gbViz4.Visible = false;
            // 
            // gbNamelbl4
            // 
            this.gbNamelbl4.AutoSize = true;
            this.gbNamelbl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNamelbl4.Location = new System.Drawing.Point(4, 32);
            this.gbNamelbl4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbNamelbl4.Name = "gbNamelbl4";
            this.gbNamelbl4.Size = new System.Drawing.Size(162, 25);
            this.gbNamelbl4.TabIndex = 182;
            this.gbNamelbl4.Text = "VIZENG-DEV-05";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.gbLEDOn4);
            this.groupBox10.Controls.Add(this.gbLEDOff4);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(192, 28);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox10.Size = new System.Drawing.Size(140, 74);
            this.groupBox10.TabIndex = 178;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Connected";
            // 
            // gbLEDOn4
            // 
            this.gbLEDOn4.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOn4.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOn4.Image")));
            this.gbLEDOn4.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOn4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOn4.Name = "gbLEDOn4";
            this.gbLEDOn4.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOn4.TabIndex = 164;
            this.gbLEDOn4.TabStop = false;
            this.gbLEDOn4.Visible = false;
            // 
            // gbLEDOff4
            // 
            this.gbLEDOff4.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOff4.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOff4.Image")));
            this.gbLEDOff4.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOff4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOff4.Name = "gbLEDOff4";
            this.gbLEDOff4.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOff4.TabIndex = 163;
            this.gbLEDOff4.TabStop = false;
            // 
            // gbPortlbl4
            // 
            this.gbPortlbl4.AutoSize = true;
            this.gbPortlbl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPortlbl4.Location = new System.Drawing.Point(4, 85);
            this.gbPortlbl4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbPortlbl4.Name = "gbPortlbl4";
            this.gbPortlbl4.Size = new System.Drawing.Size(102, 25);
            this.gbPortlbl4.TabIndex = 1;
            this.gbPortlbl4.Text = "Port: 6100";
            // 
            // gbIPlbl4
            // 
            this.gbIPlbl4.AutoSize = true;
            this.gbIPlbl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIPlbl4.Location = new System.Drawing.Point(4, 58);
            this.gbIPlbl4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbIPlbl4.Name = "gbIPlbl4";
            this.gbIPlbl4.Size = new System.Drawing.Size(155, 25);
            this.gbIPlbl4.TabIndex = 0;
            this.gbIPlbl4.Text = "IP: 10.232.86.84";
            // 
            // gbViz3
            // 
            this.gbViz3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbViz3.Controls.Add(this.gbNamelbl3);
            this.gbViz3.Controls.Add(this.groupBox8);
            this.gbViz3.Controls.Add(this.gbPortlbl3);
            this.gbViz3.Controls.Add(this.gbIPlbl3);
            this.gbViz3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbViz3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbViz3.Location = new System.Drawing.Point(714, 8);
            this.gbViz3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz3.Name = "gbViz3";
            this.gbViz3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz3.Size = new System.Drawing.Size(338, 128);
            this.gbViz3.TabIndex = 2;
            this.gbViz3.TabStop = false;
            this.gbViz3.Text = "Viz  3";
            this.gbViz3.Visible = false;
            // 
            // gbNamelbl3
            // 
            this.gbNamelbl3.AutoSize = true;
            this.gbNamelbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNamelbl3.Location = new System.Drawing.Point(4, 32);
            this.gbNamelbl3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbNamelbl3.Name = "gbNamelbl3";
            this.gbNamelbl3.Size = new System.Drawing.Size(162, 25);
            this.gbNamelbl3.TabIndex = 181;
            this.gbNamelbl3.Text = "VIZENG-DEV-05";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.gbLEDOn3);
            this.groupBox8.Controls.Add(this.gbLEDOff3);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(192, 28);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Size = new System.Drawing.Size(140, 74);
            this.groupBox8.TabIndex = 178;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Connected";
            // 
            // gbLEDOn3
            // 
            this.gbLEDOn3.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOn3.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOn3.Image")));
            this.gbLEDOn3.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOn3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOn3.Name = "gbLEDOn3";
            this.gbLEDOn3.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOn3.TabIndex = 164;
            this.gbLEDOn3.TabStop = false;
            this.gbLEDOn3.Visible = false;
            // 
            // gbLEDOff3
            // 
            this.gbLEDOff3.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOff3.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOff3.Image")));
            this.gbLEDOff3.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOff3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOff3.Name = "gbLEDOff3";
            this.gbLEDOff3.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOff3.TabIndex = 163;
            this.gbLEDOff3.TabStop = false;
            // 
            // gbPortlbl3
            // 
            this.gbPortlbl3.AutoSize = true;
            this.gbPortlbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPortlbl3.Location = new System.Drawing.Point(4, 85);
            this.gbPortlbl3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbPortlbl3.Name = "gbPortlbl3";
            this.gbPortlbl3.Size = new System.Drawing.Size(102, 25);
            this.gbPortlbl3.TabIndex = 1;
            this.gbPortlbl3.Text = "Port: 6100";
            // 
            // gbIPlbl3
            // 
            this.gbIPlbl3.AutoSize = true;
            this.gbIPlbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIPlbl3.Location = new System.Drawing.Point(4, 58);
            this.gbIPlbl3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbIPlbl3.Name = "gbIPlbl3";
            this.gbIPlbl3.Size = new System.Drawing.Size(155, 25);
            this.gbIPlbl3.TabIndex = 0;
            this.gbIPlbl3.Text = "IP: 10.232.86.84";
            // 
            // gbViz2
            // 
            this.gbViz2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbViz2.Controls.Add(this.gbNamelbl2);
            this.gbViz2.Controls.Add(this.groupBox9);
            this.gbViz2.Controls.Add(this.gbPortlbl2);
            this.gbViz2.Controls.Add(this.gbIPlbl2);
            this.gbViz2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbViz2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbViz2.Location = new System.Drawing.Point(368, 8);
            this.gbViz2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz2.Name = "gbViz2";
            this.gbViz2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz2.Size = new System.Drawing.Size(338, 128);
            this.gbViz2.TabIndex = 1;
            this.gbViz2.TabStop = false;
            this.gbViz2.Text = "Viz  2";
            this.gbViz2.Visible = false;
            // 
            // gbNamelbl2
            // 
            this.gbNamelbl2.AutoSize = true;
            this.gbNamelbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNamelbl2.Location = new System.Drawing.Point(4, 32);
            this.gbNamelbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbNamelbl2.Name = "gbNamelbl2";
            this.gbNamelbl2.Size = new System.Drawing.Size(162, 25);
            this.gbNamelbl2.TabIndex = 180;
            this.gbNamelbl2.Text = "VIZENG-DEV-05";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.gbLEDOn2);
            this.groupBox9.Controls.Add(this.gbLEDOff2);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(192, 28);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox9.Size = new System.Drawing.Size(140, 74);
            this.groupBox9.TabIndex = 178;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Connected";
            // 
            // gbLEDOn2
            // 
            this.gbLEDOn2.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOn2.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOn2.Image")));
            this.gbLEDOn2.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOn2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOn2.Name = "gbLEDOn2";
            this.gbLEDOn2.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOn2.TabIndex = 164;
            this.gbLEDOn2.TabStop = false;
            this.gbLEDOn2.Visible = false;
            // 
            // gbLEDOff2
            // 
            this.gbLEDOff2.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOff2.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOff2.Image")));
            this.gbLEDOff2.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOff2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOff2.Name = "gbLEDOff2";
            this.gbLEDOff2.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOff2.TabIndex = 163;
            this.gbLEDOff2.TabStop = false;
            // 
            // gbPortlbl2
            // 
            this.gbPortlbl2.AutoSize = true;
            this.gbPortlbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPortlbl2.Location = new System.Drawing.Point(4, 85);
            this.gbPortlbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbPortlbl2.Name = "gbPortlbl2";
            this.gbPortlbl2.Size = new System.Drawing.Size(102, 25);
            this.gbPortlbl2.TabIndex = 1;
            this.gbPortlbl2.Text = "Port: 6100";
            // 
            // gbIPlbl2
            // 
            this.gbIPlbl2.AutoSize = true;
            this.gbIPlbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIPlbl2.Location = new System.Drawing.Point(4, 58);
            this.gbIPlbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbIPlbl2.Name = "gbIPlbl2";
            this.gbIPlbl2.Size = new System.Drawing.Size(155, 25);
            this.gbIPlbl2.TabIndex = 0;
            this.gbIPlbl2.Text = "IP: 10.232.86.84";
            // 
            // gbViz1
            // 
            this.gbViz1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbViz1.Controls.Add(this.gbNamelbl1);
            this.gbViz1.Controls.Add(this.groupBox7);
            this.gbViz1.Controls.Add(this.gbPortlbl1);
            this.gbViz1.Controls.Add(this.gbIPlbl1);
            this.gbViz1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbViz1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbViz1.Location = new System.Drawing.Point(15, 8);
            this.gbViz1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz1.Name = "gbViz1";
            this.gbViz1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbViz1.Size = new System.Drawing.Size(338, 128);
            this.gbViz1.TabIndex = 0;
            this.gbViz1.TabStop = false;
            this.gbViz1.Text = "Viz  1";
            this.gbViz1.Visible = false;
            // 
            // gbNamelbl1
            // 
            this.gbNamelbl1.AutoSize = true;
            this.gbNamelbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNamelbl1.Location = new System.Drawing.Point(4, 32);
            this.gbNamelbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbNamelbl1.Name = "gbNamelbl1";
            this.gbNamelbl1.Size = new System.Drawing.Size(162, 25);
            this.gbNamelbl1.TabIndex = 179;
            this.gbNamelbl1.Text = "VIZENG-DEV-05";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.gbLEDOn1);
            this.groupBox7.Controls.Add(this.gbLEDOff1);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(192, 28);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Size = new System.Drawing.Size(140, 74);
            this.groupBox7.TabIndex = 178;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Connected";
            // 
            // gbLEDOn1
            // 
            this.gbLEDOn1.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOn1.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOn1.Image")));
            this.gbLEDOn1.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOn1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOn1.Name = "gbLEDOn1";
            this.gbLEDOn1.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOn1.TabIndex = 164;
            this.gbLEDOn1.TabStop = false;
            this.gbLEDOn1.Visible = false;
            // 
            // gbLEDOff1
            // 
            this.gbLEDOff1.BackColor = System.Drawing.Color.Transparent;
            this.gbLEDOff1.Image = ((System.Drawing.Image)(resources.GetObject("gbLEDOff1.Image")));
            this.gbLEDOff1.Location = new System.Drawing.Point(64, 32);
            this.gbLEDOff1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLEDOff1.Name = "gbLEDOff1";
            this.gbLEDOff1.Size = new System.Drawing.Size(24, 25);
            this.gbLEDOff1.TabIndex = 163;
            this.gbLEDOff1.TabStop = false;
            // 
            // gbPortlbl1
            // 
            this.gbPortlbl1.AutoSize = true;
            this.gbPortlbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPortlbl1.Location = new System.Drawing.Point(4, 85);
            this.gbPortlbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbPortlbl1.Name = "gbPortlbl1";
            this.gbPortlbl1.Size = new System.Drawing.Size(102, 25);
            this.gbPortlbl1.TabIndex = 1;
            this.gbPortlbl1.Text = "Port: 6100";
            // 
            // gbIPlbl1
            // 
            this.gbIPlbl1.AutoSize = true;
            this.gbIPlbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIPlbl1.Location = new System.Drawing.Point(4, 58);
            this.gbIPlbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbIPlbl1.Name = "gbIPlbl1";
            this.gbIPlbl1.Size = new System.Drawing.Size(155, 25);
            this.gbIPlbl1.TabIndex = 0;
            this.gbIPlbl1.Text = "IP: 10.232.86.84";
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfig.Location = new System.Drawing.Point(298, 48);
            this.lblConfig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(49, 25);
            this.lblConfig.TabIndex = 132;
            this.lblConfig.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(208, 48);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 25);
            this.label6.TabIndex = 131;
            this.label6.Text = "Config:";
            // 
            // lblNetwork
            // 
            this.lblNetwork.AutoSize = true;
            this.lblNetwork.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetwork.Location = new System.Drawing.Point(128, 48);
            this.lblNetwork.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNetwork.Name = "lblNetwork";
            this.lblNetwork.Size = new System.Drawing.Size(49, 25);
            this.lblNetwork.TabIndex = 134;
            this.lblNetwork.Text = "N/A";
            this.lblNetwork.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 25);
            this.label4.TabIndex = 133;
            this.label4.Text = "Network:";
            // 
            // enginePanel
            // 
            this.enginePanel.Controls.Add(this.lblScenes);
            this.enginePanel.Controls.Add(this.gbEngines);
            this.enginePanel.Location = new System.Drawing.Point(1438, 1362);
            this.enginePanel.Name = "enginePanel";
            this.enginePanel.Size = new System.Drawing.Size(696, 155);
            this.enginePanel.TabIndex = 135;
            // 
            // lblScenes
            // 
            this.lblScenes.AutoSize = true;
            this.lblScenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScenes.Location = new System.Drawing.Point(8, 125);
            this.lblScenes.Name = "lblScenes";
            this.lblScenes.Size = new System.Drawing.Size(90, 25);
            this.lblScenes.TabIndex = 1;
            this.lblScenes.Text = "Scenes: ";
            // 
            // gbEngines
            // 
            this.gbEngines.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbEngines.Controls.Add(this.gbEng4);
            this.gbEngines.Controls.Add(this.gbEng3);
            this.gbEngines.Controls.Add(this.gbEng2);
            this.gbEngines.Controls.Add(this.gbEng1);
            this.gbEngines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEngines.Location = new System.Drawing.Point(12, 3);
            this.gbEngines.Name = "gbEngines";
            this.gbEngines.Size = new System.Drawing.Size(675, 115);
            this.gbEngines.TabIndex = 0;
            this.gbEngines.TabStop = false;
            this.gbEngines.Text = "Engines Used For RaceBoards";
            this.gbEngines.UseWaitCursor = true;
            // 
            // gbEng4
            // 
            this.gbEng4.Controls.Add(this.pbEng4);
            this.gbEng4.Controls.Add(this.pictureBox8);
            this.gbEng4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEng4.Location = new System.Drawing.Point(477, 35);
            this.gbEng4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng4.Name = "gbEng4";
            this.gbEng4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng4.Size = new System.Drawing.Size(100, 68);
            this.gbEng4.TabIndex = 180;
            this.gbEng4.TabStop = false;
            this.gbEng4.Text = "Viz 4";
            this.gbEng4.UseWaitCursor = true;
            this.gbEng4.Visible = false;
            // 
            // pbEng4
            // 
            this.pbEng4.BackColor = System.Drawing.Color.Transparent;
            this.pbEng4.Image = ((System.Drawing.Image)(resources.GetObject("pbEng4.Image")));
            this.pbEng4.Location = new System.Drawing.Point(38, 29);
            this.pbEng4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbEng4.Name = "pbEng4";
            this.pbEng4.Size = new System.Drawing.Size(24, 25);
            this.pbEng4.TabIndex = 164;
            this.pbEng4.TabStop = false;
            this.pbEng4.UseWaitCursor = true;
            this.pbEng4.Visible = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(38, 29);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(24, 25);
            this.pictureBox8.TabIndex = 163;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.UseWaitCursor = true;
            // 
            // gbEng3
            // 
            this.gbEng3.Controls.Add(this.pbEng3);
            this.gbEng3.Controls.Add(this.pictureBox6);
            this.gbEng3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEng3.Location = new System.Drawing.Point(326, 35);
            this.gbEng3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng3.Name = "gbEng3";
            this.gbEng3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng3.Size = new System.Drawing.Size(100, 68);
            this.gbEng3.TabIndex = 180;
            this.gbEng3.TabStop = false;
            this.gbEng3.Text = "Viz 3";
            this.gbEng3.UseWaitCursor = true;
            this.gbEng3.Visible = false;
            // 
            // pbEng3
            // 
            this.pbEng3.BackColor = System.Drawing.Color.Transparent;
            this.pbEng3.Image = ((System.Drawing.Image)(resources.GetObject("pbEng3.Image")));
            this.pbEng3.Location = new System.Drawing.Point(38, 29);
            this.pbEng3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbEng3.Name = "pbEng3";
            this.pbEng3.Size = new System.Drawing.Size(24, 25);
            this.pbEng3.TabIndex = 164;
            this.pbEng3.TabStop = false;
            this.pbEng3.UseWaitCursor = true;
            this.pbEng3.Visible = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(38, 29);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(24, 25);
            this.pictureBox6.TabIndex = 163;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.UseWaitCursor = true;
            // 
            // gbEng2
            // 
            this.gbEng2.Controls.Add(this.pbEng2);
            this.gbEng2.Controls.Add(this.pictureBox4);
            this.gbEng2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEng2.Location = new System.Drawing.Point(172, 35);
            this.gbEng2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng2.Name = "gbEng2";
            this.gbEng2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng2.Size = new System.Drawing.Size(100, 68);
            this.gbEng2.TabIndex = 180;
            this.gbEng2.TabStop = false;
            this.gbEng2.Text = "Viz 2";
            this.gbEng2.UseWaitCursor = true;
            this.gbEng2.Visible = false;
            // 
            // pbEng2
            // 
            this.pbEng2.BackColor = System.Drawing.Color.Transparent;
            this.pbEng2.Image = ((System.Drawing.Image)(resources.GetObject("pbEng2.Image")));
            this.pbEng2.Location = new System.Drawing.Point(38, 29);
            this.pbEng2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbEng2.Name = "pbEng2";
            this.pbEng2.Size = new System.Drawing.Size(24, 25);
            this.pbEng2.TabIndex = 164;
            this.pbEng2.TabStop = false;
            this.pbEng2.UseWaitCursor = true;
            this.pbEng2.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(38, 29);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 25);
            this.pictureBox4.TabIndex = 163;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.UseWaitCursor = true;
            // 
            // gbEng1
            // 
            this.gbEng1.Controls.Add(this.pbEng1);
            this.gbEng1.Controls.Add(this.pictureBox2);
            this.gbEng1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEng1.Location = new System.Drawing.Point(21, 35);
            this.gbEng1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng1.Name = "gbEng1";
            this.gbEng1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEng1.Size = new System.Drawing.Size(100, 68);
            this.gbEng1.TabIndex = 179;
            this.gbEng1.TabStop = false;
            this.gbEng1.Text = "Viz 1";
            this.gbEng1.UseWaitCursor = true;
            this.gbEng1.Visible = false;
            // 
            // pbEng1
            // 
            this.pbEng1.BackColor = System.Drawing.Color.Transparent;
            this.pbEng1.Image = ((System.Drawing.Image)(resources.GetObject("pbEng1.Image")));
            this.pbEng1.Location = new System.Drawing.Point(38, 29);
            this.pbEng1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbEng1.Name = "pbEng1";
            this.pbEng1.Size = new System.Drawing.Size(24, 25);
            this.pbEng1.TabIndex = 164;
            this.pbEng1.TabStop = false;
            this.pbEng1.UseWaitCursor = true;
            this.pbEng1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(38, 29);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 25);
            this.pictureBox2.TabIndex = 163;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.UseWaitCursor = true;
            // 
            // LoopTimer
            // 
            this.LoopTimer.Interval = 5000;
            this.LoopTimer.Tick += new System.EventHandler(this.LoopTimer_Tick);
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDB.Location = new System.Drawing.Point(1039, 89);
            this.lblDB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(170, 25);
            this.lblDB.TabIndex = 136;
            this.lblDB.Text = "DB Connection: ";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(2150, 1731);
            this.Controls.Add(this.lblDB);
            this.Controls.Add(this.enginePanel);
            this.Controls.Add(this.lblNetwork);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.connectionPanel);
            this.Controls.Add(this.pnlStack);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblMediaSequencer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblHostName);
            this.Controls.Add(this.lblIpAddress);
            this.Controls.Add(this.gbTime);
            this.Controls.Add(this.dataModeSelect);
            this.Controls.Add(this.lblTrioChannel);
            this.Controls.Add(this.lblTrioChannelHeader);
            this.Controls.Add(this.lblPlaylistName);
            this.Controls.Add(this.lblPlaylistNameHeader);
            this.Controls.Add(this.lblCurrentShow);
            this.Controls.Add(this.lblCurrentShowHeader);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Election Graphics Stack Builder Application  Version  1.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.dataModeSelect.ResumeLayout(false);
            this.tpRaces.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbSpF.ResumeLayout(false);
            this.gbSpF.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.gbRCF.ResumeLayout(false);
            this.gbRCF.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.gbROF.ResumeLayout(false);
            this.gbROF.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.availableRacesGrid)).EndInit();
            this.tpExitPolls.ResumeLayout(false);
            this.tpExitPolls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoterAnalysis)).EndInit();
            this.tcVoterAnalysis.ResumeLayout(false);
            this.tpBalanceOfPower.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BOPdataGridView)).EndInit();
            this.tpReferendums.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReferendumsGrid)).EndInit();
            this.gbTime.ResumeLayout(false);
            this.pnlStack.ResumeLayout(false);
            this.pnlStack.PerformLayout();
            this.pnlUpDn.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.TakePanel.ResumeLayout(false);
            this.TakePanel.PerformLayout();
            this.LockPanel.ResumeLayout(false);
            this.SaveActivatePanel.ResumeLayout(false);
            this.SaveActivatePanel.PerformLayout();
            this.StackPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stackGrid)).EndInit();
            this.connectionPanel.ResumeLayout(false);
            this.gbViz6.ResumeLayout(false);
            this.gbViz6.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff6)).EndInit();
            this.gbViz5.ResumeLayout(false);
            this.gbViz5.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff5)).EndInit();
            this.gbViz4.ResumeLayout(false);
            this.gbViz4.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff4)).EndInit();
            this.gbViz3.ResumeLayout(false);
            this.gbViz3.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff3)).EndInit();
            this.gbViz2.ResumeLayout(false);
            this.gbViz2.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff2)).EndInit();
            this.gbViz1.ResumeLayout(false);
            this.gbViz1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLEDOff1)).EndInit();
            this.enginePanel.ResumeLayout(false);
            this.enginePanel.PerformLayout();
            this.gbEngines.ResumeLayout(false);
            this.gbEng4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEng4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.gbEng3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEng3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.gbEng2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEng2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.gbEng1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEng1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miAboutBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSelectDefaultShow;
        private System.Windows.Forms.Label lblCurrentShow;
        private System.Windows.Forms.Label lblCurrentShowHeader;
        private System.Windows.Forms.Label lblPlaylistNameHeader;
        private System.Windows.Forms.Label lblPlaylistName;
        private System.Windows.Forms.Label lblTrioChannelHeader;
        private System.Windows.Forms.Label lblTrioChannel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetStatusBarToolStripMenuItem;
        private System.Windows.Forms.TabControl dataModeSelect;
        private System.Windows.Forms.TabPage tpRaces;
        private System.Windows.Forms.TabPage tpExitPolls;
        private System.Windows.Forms.TabPage tpBalanceOfPower;
        private System.Windows.Forms.DataGridView availableRacesGrid;
        private System.Windows.Forms.Button btnAddBalanceOfPower;
        private System.Windows.Forms.DataGridView BOPdataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn st;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Question;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subset;
        private System.Windows.Forms.GroupBox gbROF;
        private System.Windows.Forms.RadioButton rbShowAll;
        private System.Windows.Forms.RadioButton rbGovernor;
        private System.Windows.Forms.RadioButton rbHouse;
        private System.Windows.Forms.RadioButton rbSenate;
        private System.Windows.Forms.RadioButton rbPresident;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnAddRace4Way;
        private System.Windows.Forms.Button btnSelect4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAddRace3WaySelect;
        private System.Windows.Forms.Button btnAddRace3Way;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAddRace2WaySelect;
        private System.Windows.Forms.Button btnAddRace2Way;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddRace1Way;
        private System.Windows.Forms.Button btnAddRace1WaySelect;
        private System.Windows.Forms.GroupBox gbRCF;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbCalled;
        private System.Windows.Forms.RadioButton rbJustCalled;
        private System.Windows.Forms.RadioButton rbTCTC;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Timer timerStatusUpdate;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Panel pnlStack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbSpF;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbPollClosing;
        private System.Windows.Forms.RadioButton rbBattleground;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAvailRaceCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn eType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Session;
        private System.Windows.Forms.TabPage tpReferendums;
        private System.Windows.Forms.DataGridView ReferendumsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button btnAddReferendum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Race_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Race_Description;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.Label lblHostName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtNextPollClosingTime;
        private System.Windows.Forms.Label txtNextPollClosingTimeHeader;
        private System.Windows.Forms.Button btnAddExitPoll;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem usePrimaryMediaSequencerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useBackupMediaSequencerToolStripMenuItem;
        private System.Windows.Forms.Label lblMediaSequencer;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel TakePanel;
        private System.Windows.Forms.CheckBox cbAutoCalledRaces;
        private System.Windows.Forms.CheckBox cbLooping;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Panel LockPanel;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Panel SaveActivatePanel;
        private System.Windows.Forms.CheckBox cbPromptForInfo;
        private System.Windows.Forms.Button btnSaveActivateStack;
        private System.Windows.Forms.Panel StackPanel;
        private System.Windows.Forms.Button btnClearStack;
        private System.Windows.Forms.Button btnDeleteStackElement;
        private System.Windows.Forms.Button btnLoadStack;
        private System.Windows.Forms.Button btnSaveStack;
        private System.Windows.Forms.DataGridView stackGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Element_Type_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn TemplateID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stack_Entry_Description;
        private System.Windows.Forms.ComboBox cbGraphicConcept;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtStackName;
        private System.Windows.Forms.Label txtStackEntriesCount;
        private System.Windows.Forms.Label lblStackEntriesCount;
        private System.Windows.Forms.Label lblStackHeader;
        private System.Windows.Forms.Panel pnlUpDn;
        private System.Windows.Forms.Button btnStackElementDown;
        private System.Windows.Forms.Button btnStackElementUp;
        private System.Windows.Forms.Timer LiveUpdateTimer;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Panel connectionPanel;
        private System.Windows.Forms.GroupBox gbViz6;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.PictureBox gbLEDOn6;
        private System.Windows.Forms.PictureBox gbLEDOff6;
        private System.Windows.Forms.Label gbPortlbl6;
        private System.Windows.Forms.Label gbIPlbl6;
        private System.Windows.Forms.GroupBox gbViz5;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.PictureBox gbLEDOn5;
        private System.Windows.Forms.PictureBox gbLEDOff5;
        private System.Windows.Forms.Label gbPortlbl5;
        private System.Windows.Forms.Label gbIPlbl5;
        private System.Windows.Forms.GroupBox gbViz4;
        private System.Windows.Forms.Label gbNamelbl4;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.PictureBox gbLEDOn4;
        private System.Windows.Forms.PictureBox gbLEDOff4;
        private System.Windows.Forms.Label gbPortlbl4;
        private System.Windows.Forms.Label gbIPlbl4;
        private System.Windows.Forms.GroupBox gbViz3;
        private System.Windows.Forms.Label gbNamelbl3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.PictureBox gbLEDOn3;
        private System.Windows.Forms.PictureBox gbLEDOff3;
        private System.Windows.Forms.Label gbPortlbl3;
        private System.Windows.Forms.Label gbIPlbl3;
        private System.Windows.Forms.GroupBox gbViz2;
        private System.Windows.Forms.Label gbNamelbl2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.PictureBox gbLEDOn2;
        private System.Windows.Forms.PictureBox gbLEDOff2;
        private System.Windows.Forms.Label gbPortlbl2;
        private System.Windows.Forms.Label gbIPlbl2;
        private System.Windows.Forms.GroupBox gbViz1;
        private System.Windows.Forms.Label gbNamelbl1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PictureBox gbLEDOn1;
        private System.Windows.Forms.PictureBox gbLEDOff1;
        private System.Windows.Forms.Label gbPortlbl1;
        private System.Windows.Forms.Label gbIPlbl1;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNetwork;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel enginePanel;
        private System.Windows.Forms.Label lblScenes;
        private System.Windows.Forms.GroupBox gbEngines;
        private System.Windows.Forms.GroupBox gbEng4;
        private System.Windows.Forms.PictureBox pbEng4;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.GroupBox gbEng3;
        private System.Windows.Forms.PictureBox pbEng3;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.GroupBox gbEng2;
        private System.Windows.Forms.PictureBox pbEng2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.GroupBox gbEng1;
        private System.Windows.Forms.PictureBox pbEng1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer LoopTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private System.Windows.Forms.TabControl tcVoterAnalysis;
        private System.Windows.Forms.TabPage tpFullScreen;
        private System.Windows.Forms.TabPage tpTicker;
        private System.Windows.Forms.DataGridView dgvVoterAnalysis;
        private System.Windows.Forms.Label lblVAcnt;
        private System.Windows.Forms.Label lblDB;
    }
}

