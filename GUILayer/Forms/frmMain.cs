//////////////////////////////////////////////////////////////////////////////
// MAIN APPLICATION FORM
// Version 1.0.0
// M. Dilworth  Rev: 08/17/2016
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text;
using DataInterface.DataAccess;
using DataInterface.DataModel;
using DataInterface.Enums;
using DataInterface.SQL;
using log4net.Appender;
using LogicLayer.Collections;
using LogicLayer.CommonClasses;
using MSEInterface;
using MSEInterface.Constants;
using AsyncClientSocket;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using VoterAnalysisParser;
using LogicLayer.GeneralDataProcessingFunctions;
using System.Linq;
using System.Reflection;


// Required for implementing logging to status bar

//
// NOTES:
//
// 1. Code modified 8/17/2016 to use new approach provided by Viz for managing groups and elements within groups.
//

namespace GUILayer.Forms
{
    // Implement IAppender interface from log4net

        
    public partial class frmMain : Form, IAppender
    {

        #region Globals

        Boolean nonNumberEntered = false;
        public static Boolean UseSimulatedTime = false;
        public static Boolean PollClosinglockout = false;
        DateTime referenceTime = DateTime.MaxValue;

        string elementCollectionURIShow;
        string templateCollectionURIShow;
        string elementCollectionURIPlaylist;
        string templateModel;
        // Parameters for current (working) stack
        // Specify stackID as double - will use encoded date/time string converted to float
        Double stackID = -1;
        string stackDescription = "";

        // For future use
        Boolean insertNext = true;
        Int32 insertPoint;

        // For no use what so ever
        ulong useless = 0;

        Int16 conceptID;
        string conceptName;

        public Boolean enableShowSelectControls = false;
        public List<EngineModel> vizEngines = new List<EngineModel>();
        public bool builderOnlyMode = false;
        public bool isNonPresidentialPrimary = false;
        public string Network = "";
        public string quot = "\"";
        public string term = "\0";

        public int currentRaceIndex = -1;
        public bool stackLocked = false;
        public bool takeIn = false;
        public string computerName = string.Empty;
        public string configName = string.Empty;
        public string ipAddress = string.Empty;
        public string hostName = string.Empty;
        public string stacksDB = string.Empty;
        public int stackType = 0;
        public int stackTypeOffset = 0;
        public int lastIndex;
        public int takeCnt = 0;
        public int tabIndex = 0;
        public string[] IPs = new string[4] { string.Empty, string.Empty, string.Empty, string.Empty };
        public bool[] enables = new bool[4] { false, false, false, false };
        public bool MindyMode = false;
        public bool VictoriaMode = false;

        public bool autoCalledRacesActive;
        public bool autoCalledRacesEnable;
        public bool autoCalledRacesByOffice;
        public bool President;
        public bool Senate;
        public bool House;
        public bool Governor;
        public List<string> autoOfc = new List<string>();
        public int acrIndx = -1;

        public static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


        public List<TabDefinitionModel> tabConfig = new List<TabDefinitionModel>();

        public List<ClientSocket> vizClientSockets = new List<ClientSocket>();

        public string[] lastSceneLoaded = new string[4];
        //public List<VoterAnalysisQuestionsModel> VA_Qdata_Tkr = new List<VoterAnalysisQuestionsModel>();
        //public List<VoterAnalysisQuestionsModel> VA_Qdata_FS = new List<VoterAnalysisQuestionsModel>();
        public List<VoterAnalysisMapQuestionsModel> VA_Qdata_Map = new List<VoterAnalysisMapQuestionsModel>();
        public List<VoterAnalysisQuestionsModelNew> VA_Qdata_FS = new List<VoterAnalysisQuestionsModelNew>();
        public List<VoterAnalysisQuestionsModelNew> VA_Qdata_Tkr = new List<VoterAnalysisQuestionsModelNew>();
        public List<VoterAnalysisManualQuestionsModelNew> VA_Qdata_Man = new List<VoterAnalysisManualQuestionsModelNew>();

        public TcpListener server;
        public TcpClient client;
        public NetworkStream stream;
        public string electionMode = "General";
        public int remotePort;
        public bool isPrimary = false;
        public bool UseCandidateFirstName = false;
        public string stackNameRB = "";
        public string stackNameVA = "";
        public string stackNameBOP = "";
        public string stackNameREF = "";
        public string stackNameMAP = "";
        public string stackNameSP = "";
        public bool unreal = false;
        public string unrealEng = "UNREAL1";
        public bool clickFlag = false;
        public int clickIndex = -1;


        // This class added to allow for configuration of application config file in a location other than the folder where the .EXE resides
        // 09/24/2020
        public abstract class AppConfig : IDisposable
        {
            public static AppConfig Change(string path)
            {
                return new ChangeAppConfig(path);
            }

            public abstract void Dispose();

            private class ChangeAppConfig : AppConfig
            {
                private readonly string oldConfig =
                    AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString();

                private bool disposedValue;

                public ChangeAppConfig(string path)
                {
                    AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", path);
                    ResetConfigMechanism();
                }

                public override void Dispose()
                {
                    if (!disposedValue)
                    {
                        AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", oldConfig);
                        ResetConfigMechanism();


                        disposedValue = true;
                    }
                    GC.SuppressFinalize(this);
                }

                private static void ResetConfigMechanism()
                {
                    typeof(ConfigurationManager)
                        .GetField("s_initState", BindingFlags.NonPublic |
                                                 BindingFlags.Static)
                        .SetValue(null, 0);

                    typeof(ConfigurationManager)
                        .GetField("s_configSystem", BindingFlags.NonPublic |
                                                    BindingFlags.Static)
                        .SetValue(null, null);

                    typeof(ConfigurationManager)
                        .Assembly.GetTypes()
                        .Where(x => x.FullName ==
                                    "System.Configuration.ClientConfigPaths")
                        .First()
                        .GetField("s_current", BindingFlags.NonPublic |
                                               BindingFlags.Static)
                        .SetValue(null, null);
                }
            }
        }


        #endregion

        #region Collection & binding list definitions
        /// <summary>
        /// Define classes for collections and logic
        /// </summary>

        // Define the binding list object for the list of available shows
        //BindingList<ShowObject> showNames;

        // Define the collection object for the list of available stacks
        private StacksCollection stacksCollection;
        BindingList<StackModel> stacks;

        // Define the collection object for the elements within a specified working stack
        public StackElementsCollection stackElementsCollection;
        BindingList<StackElementModel> stackElements;

        // Define the collection object for the elements within a specified stack to be activated
        public StackElementsCollection activateStackElementsCollection;
        BindingList<StackElementModel> activateStackElements;

        // Define the collection object for the list of available races
        private AvailableRacesCollection availableRacesCollection;
        BindingList<AvailableRaceModel> availableRaces;
        BindingList<AvailableRaceModel> availableRacesSP;

        // Define the collection object for the list of Referendums
        private ReferendumsCollection referendumsCollection;
        BindingList<ReferendumModel> referendums;

        // Define the collection object for the list of Referendums data
        private ReferendumsDataCollection referendumsDataCollection;
        BindingList<ReferendumDataModel> referendumsData;

        
        /*
        // Define the collection object for the list of Referendums data
        private VoterAnalysisDataCollection voterAnalysisDataCollection;
        BindingList<VoterAnalysisDataModel> voterAnalysisData;


        // Define the collection object for the list of Exit Poll questions
        private ExitPollQuestionsCollection exitPollQuestionsCollection;
        BindingList<ExitPollQuestionsModel> exitPollQuestions;
        */
        // Define the collection object for the list of Exit Poll questions
        //private ExitPollDataCollection exitPollDataCollection;
        //BindingList<ExitPollDataCollection> exitPollData;

        // Define the collection used for storing candidate data for a specific race
        private RaceDataCollection raceDataCollection;
        BindingList<RaceDataModel> raceData;

        // Define the collection used for storing race preview data for multi-play
        private RacePreviewCollection racePreviewCollection;
        BindingList<RacePreviewModel> racePreview;

        // Define the collection used for storing state metadata for all 50 states + US
        private StateMetadataCollection stateMetadataCollection;
        BindingList<StateMetadataModel> stateMetadata;

        // Define the collection used for storing state metadata for all 50 states + US
        private GraphicsConceptsCollection graphicsConceptsCollection;
        BindingList<GraphicsConceptsModel> graphicsConcepts;
        BindingList<GraphicsConceptsModel> graphicsConceptTypes;

        // Define the collection object for the list of available races
        private ApplicationSettingsFlagsCollection applicationSettingsFlagsCollection;
        BindingList<ApplicationSettingsFlagsModel> applicationFlags;

        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";

        // Instantiate MSE classes
        MANAGE_GROUPS group = new MANAGE_GROUPS();
        MANAGE_PLAYLISTS playlist = new MANAGE_PLAYLISTS();
        MANAGE_TEMPLATES template = new MANAGE_TEMPLATES();
        MANAGE_SHOWS show = new MANAGE_SHOWS();
        MANAGE_ELEMENTS element = new MANAGE_ELEMENTS();

        // Read in MSE settings from config file and set default directories and parameters
        Boolean usingPrimaryMediaSequencer = true;
        Boolean mseEndpoint1_Enable = false;
        string mseEndpoint1 = string.Empty;
        Boolean mseEndpoint2_Enable = false;
        string mseEndpoint2 = string.Empty;
        string topLevelShowsDirectoryURI = string.Empty;
        string masterPlaylistsDirectoryURI = string.Empty;
        string profilesURI = string.Empty;
        string currentShowName = string.Empty;
        string currentPlaylistName = string.Empty;

        /*
        Boolean mseEndpoint1_Enable = Properties.Settings.Default.MSEEndpoint1_Enable;
        string mseEndpoint1 = Properties.Settings.Default.MSEEndpoint1;
        Boolean mseEndpoint2_Enable = Properties.Settings.Default.MSEEndpoint2_Enable;
        string mseEndpoint2 = Properties.Settings.Default.MSEEndpoint1;
        string topLevelShowsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.TopLevelShowsDirectory;
        string masterPlaylistsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.MasterPlaylistsDirectory;
        string profilesURI = Properties.Settings.Default.MSEEndpoint1 + "profiles";
        string currentShowName = Properties.Settings.Default.CurrentShowName;
        string currentPlaylistName = Properties.Settings.Default.CurrentSelectedPlaylist;
        */

        // database connection strings
        
        public static string GraphicsDBConnectionString = string.Empty;
        public static string ElectionsDBConnectionString = string.Empty;
        public static string StacksDBConnectionString = string.Empty;
        public static string ConfigDBConnectionString = string.Empty;



        bool useBackupServer = Convert.ToBoolean(config.AppSettings.Settings["UseBackupServer"].Value);

        //Read in default Trio profile and channel
        //string defaultTrioProfile = Properties.Settings.Default.DefaultTrioProfile;
        //string defaultTrioChannel = Properties.Settings.Default.DefaultTrioChannel;

        public class SavedData
        {
            public BindingList<StackElementModel> stkRB = new BindingList<StackElementModel>();
            public BindingList<StackElementModel> stkVA = new BindingList<StackElementModel>();
            public BindingList<StackElementModel> stkRef = new BindingList<StackElementModel>();
            public BindingList<StackElementModel> stkBOP = new BindingList<StackElementModel>();
            public BindingList<StackElementModel> stkSP = new BindingList<StackElementModel>();
            public BindingList<StackElementModel> stkMap = new BindingList<StackElementModel>();

        }

        public SavedData tabsave = new SavedData();
        public int tabId = -1;


        #endregion

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Logging & status setup
        // This method used to implement IAppender interface from log4net; to support custom appends to status strip
        public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
        {
            // Set text on status bar only if logging level is DEBUG or ERROR
            if ((loggingEvent.Level.Name == "ERROR") | (loggingEvent.Level.Name == "DEBUG"))
            {
                //toolStripStatusLabel.BackColor = System.Drawing.Color.Red;
                //toolStripStatusLabel.Text = String.Format("Error Logging Message: {0}: {1}", loggingEvent.Level.Name, loggingEvent.MessageObject.ToString());
            }
            else
            {
                //toolStripStatusLabel.BackColor = System.Drawing.Color.SpringGreen;
                //toolStripStatusLabel.Text = String.Format("Status Logging Message: {0}: {1}", loggingEvent.Level.Name, loggingEvent.MessageObject.ToString());
            }
        }

        // Handler to clear status bar message and reset color
        private void resetStatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.BackColor = System.Drawing.Color.SpringGreen;
            toolStripStatusLabel.Text = "Status Logging Message: Statusbar reset";
        }
        #endregion

        #region Main form init, activation & close
        /// <summary>
        /// Main form init, activation and close
        /// </summary>

        public frmMain()
        {
            InitializeComponent();

            try
            {

                //// Setup show controls
                //if (Properties.Settings.Default.EnableShowSelectControls)
                //    enableShowSelectControls = true;
                //else
                //    enableShowSelectControls = false;
                //if (enableShowSelectControls)
                //{
                //    miSelectDefaultShow.Enabled = true;
                //}
                //else
                //{
                //    miSelectDefaultShow.Enabled = false;
                //}

                // Set version number
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                this.Text = String.Format("Stackmaster II   Version {0}", version);


                // Log application start
                log.Info($"\n********** Starting Stackmaster II    version {version} **********\n ");

                // Update status
                toolStripStatusLabel.Text = "Starting program initialization - loading data from SQL database.";

                // Query the graphics DB to get the list of already saved stacks
                //RefreshStacksList();

                // Query the elections DB to get the list of exit poll questions
                //RefreshExitPollQuestions();
                
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred during program init: " + ex.Message);
                //log.Debug("frmMain Exception occurred during program init", ex);
            }

            // Update status labels
            toolStripStatusLabel.Text = "Program initialization complete.";
            //lblPlaylistName.Text = currentPlaylistName;
            //lblTrioChannel.Text = defaultTrioChannel;
        }

        // Lister function for server socket process
        public void StartListener(object sender, EventArgs e)
        {
            try
            {
                //Sets up the listener
                server = null;
                server = new TcpListener(IPAddress.Parse(ipAddress), remotePort);
                string data = "";
                Byte[] bytes = new byte[2048];

                while (true)
                {
                    server.Start();
                    client = server.AcceptTcpClient();
                    stream = client.GetStream();
                    if (client.Connected)
                        Invoke(new Action(() => pbExt.Visible = true)); 
                    else
                        Invoke(new Action(() => pbExt.Visible = false));

                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        data = data.ToUpper();
                        Invoke(new Action(() => listBox2.Items.Add("[" + DateTime.Now + "] RECEIVED: " + data)));

                        switch (data)
                        {
                            case "LK":
                                Invoke(new Action(() => Lock()));
                                break;
                            case "UL":
                                Invoke(new Action(() => Unlock()));
                                break;
                            case "TN":
                                Invoke(new Action(() => TakeNext()));
                                break;

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                server.Stop();
                log.Error($"StartListener - Error  {ex.Message}");
            }
        }

        // Handler for main form load
        private void frmMain_Load(object sender, EventArgs e)
        {

            // Get host IP
            ipAddress = HostIPNameFunctions.GetLocalIPAddress();
            hostName = HostIPNameFunctions.GetHostName(ipAddress);
            lblIpAddress.Text = ipAddress;
            lblHostName.Text = hostName;

            log.Debug($"{ipAddress}  {hostName}");// Read in config settings


            //mseEndpoint1 = Properties.Settings.Default.MSEEndpoint1;
            //mseEndpoint2 = Properties.Settings.Default.MSEEndpoint2;
            //mseEndpoint1_Enable = Properties.Settings.Default.MSEEndpoint1_Enable;
            //mseEndpoint2_Enable = Properties.Settings.Default.MSEEndpoint2_Enable;
            //topLevelShowsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.TopLevelShowsDirectory;
            //masterPlaylistsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.MasterPlaylistsDirectory;
            //profilesURI = Properties.Settings.Default.MSEEndpoint1 + "profiles";
            //currentShowName = Properties.Settings.Default.CurrentShowName;
            //currentPlaylistName = Properties.Settings.Default.CurrentSelectedPlaylist;

            autoCalledRacesEnable = Properties.Settings.Default.AutoCalledRacesEnable;
            autoCalledRacesByOffice = Properties.Settings.Default.AutoCalledRacesByOffice;

            President = Properties.Settings.Default.President;
            if (President)
                autoOfc.Add("P");

            Senate = Properties.Settings.Default.Senate;
            if (Senate)
                autoOfc.Add("S");

            House = Properties.Settings.Default.House;
            if (House)
                autoOfc.Add("H");

            Governor = Properties.Settings.Default.Governor;
            if (Governor)
                autoOfc.Add("G");

            cbAutoCalledRaces.Enabled = autoCalledRacesEnable;


            MindyMode = Properties.Settings.Default.MindyMode;
            VictoriaMode = Properties.Settings.Default.VictoriaMode;
            electionMode = Properties.Settings.Default.ElectionMode;
            remotePort = Properties.Settings.Default.RemotePort;
            UseCandidateFirstName = Properties.Settings.Default.UseCandidateFirstName;
            unreal = Properties.Settings.Default.Unreal;
            unrealEng = Properties.Settings.Default.UnrealEng;

            if (electionMode == "Primary")
                isPrimary = true;
            else
                isPrimary = false;

            string primaryServer = Properties.Settings.Default.Server_Pri;
            string backupServer = Properties.Settings.Default.Server_Bk;
            string mainDB = Properties.Settings.Default.MainDB;
            string stacksDB = Properties.Settings.Default.StacksDB;
            string MP_stacksDB = Properties.Settings.Default.MP_StacksDB;
            string configDB =  Properties.Settings.Default.ConfigDB;

            AppConfig.Change(@"C:\Windows\BMTAppData\BMTAppDataParams.exe.config");

            string User = ConfigurationManager.AppSettings["Param1"];
            string Pw = ConfigurationManager.AppSettings["Param2"];

            //string User = Properties.Settings.Default.User;
            //string Pw = Properties.Settings.Default.PW;

            bool encrypt = true;

            string server;

            if (useBackupServer)
            {
                server = backupServer;
            }
            else
            {
                server = primaryServer;
            }
            
            var builder = new SqlConnectionStringBuilder();

            builder.UserID = User;
            builder.Password = Pw;
            builder.DataSource = server;
            //builder.InitialCatalog = Properties.Settings.Default.MainDB;
            builder.InitialCatalog = mainDB;
            builder.PersistSecurityInfo = true;
            builder.Encrypt = encrypt;
            builder.TrustServerCertificate = true;

            //"Data Source=enygdb1;Initial Catalog=ElectionProd;Persist Security Info=True;User ID=electReadersParsers;Password=pw;trustServerCertificate=false;encrypt=true"										
            
            ElectionsDBConnectionString = builder.ConnectionString;

            var dataSource = builder.DataSource;
            var initCat = builder.InitialCatalog;

            lblDB.Text = $"{dataSource}  {initCat}";
            log.Info($"{dataSource}  {initCat}");

            
            //log.Debug($"ElectionsDBConnectionString {ElectionsDBConnectionString}");


            var sbuilder = new SqlConnectionStringBuilder("");
            sbuilder.UserID = User;
            sbuilder.Password = Pw;
            sbuilder.DataSource = server;
            sbuilder.InitialCatalog = stacksDB;
            sbuilder.PersistSecurityInfo = true;
            sbuilder.Encrypt = encrypt;
            sbuilder.TrustServerCertificate = true;

            StacksDBConnectionString = sbuilder.ConnectionString;
            //log.Debug($"StacksDBConnectionString {StacksDBConnectionString}");

            var MPsbuilder = new SqlConnectionStringBuilder("");
            MPsbuilder.UserID = User;
            MPsbuilder.Password = Pw;
            MPsbuilder.DataSource = server;
            MPsbuilder.InitialCatalog = MP_stacksDB;
            MPsbuilder.PersistSecurityInfo = true;
            MPsbuilder.Encrypt = encrypt;
            MPsbuilder.TrustServerCertificate = true;
            GraphicsDBConnectionString = MPsbuilder.ConnectionString;
            //log.Debug($"GraphicsDBConnectionString {GraphicsDBConnectionString}");


            var cbuilder = new SqlConnectionStringBuilder("");
            cbuilder.UserID = User;
            cbuilder.Password = Pw;
            cbuilder.DataSource = server;
            cbuilder.InitialCatalog = configDB;
            cbuilder.PersistSecurityInfo = true;
            cbuilder.Encrypt = encrypt;
            cbuilder.TrustServerCertificate = true;
            ConfigDBConnectionString = cbuilder.ConnectionString;
            //log.Debug($"ConfigDBConnectionString {ConfigDBConnectionString}");

            //usingPrimaryMediaSequencer = true;
            //lblMediaSequencer.Text = "USING PRIMARY MEDIA SEQUENCER: " + Convert.ToString(Properties.Settings.Default.MSEEndpoint1);
            //lblMediaSequencer.BackColor = System.Drawing.Color.White;
            //usePrimaryMediaSequencerToolStripMenuItem.Checked = true;
            //useBackupMediaSequencerToolStripMenuItem.Checked = false;


            //LoadConfig();

            if (electionMode == "Primary")
            {
                gbAllCand.Visible = true;
                gbSP2Way.Visible = false;
                CandSelPanel5.Visible = true;
                CandSelPanel3.Visible = false;
            }
            else
            {
                gbAllCand.Visible = false;
                gbSP2Way.Visible = true;
                CandSelPanel5.Visible = false;
                CandSelPanel3.Visible = true;
            }

            // Init the state metadata collection
            CreateStateMetadataCollection();

            // Query the elections DB to get the list of available races
            //RefreshAvailableRacesList(isPrimary);

            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);

            

            //Query the elections DB to get the list of Referendums
            RefreshReferendums();

            // Query the elections DB to get application flags(option settings)
            RefreshApplicationFlags();

            // Init the stack elements collections
            CreateStackElementsCollections();

            // Setup data binding for stacks grid
            stackGrid.AutoGenerateColumns = false;
            var stackGridDataSource = new BindingSource(stackElements, null);
            stackGrid.DataSource = stackGridDataSource;

            // Init the race data collection
            CreateRaceDataCollection();

            // Init the race preview collection
            CreateRacePreviewCollection();

            // Init Graphics Concepts Collection
            //CreateGraphicsConceptsCollection();

            // Set current show label
            lblCurrentShow.Text = currentShowName;

            // Load the BOP Grid
            LoadBOPDGV();

            // initialize Voter Analysis grids
            GetVoterAnalysisGridData();
            GetVoterAnalysisManualGridData();
            GetVoterAnalysisMapGridData();

            // Enable handling of function keys; setup method for function keys and assign delegate
            KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);

            timerStatusUpdate.Enabled = true;

            // Set connection string for functions to get simulated time
            TimeFunctions.ElectionsDBConnectionString = ElectionsDBConnectionString;
            LoadConfig();

            rbSenateSP.Enabled = false;
            rbHouseSP.Enabled = false;
            rbGovernorSP.Enabled = false;
            rbShowAllSP.Enabled = false;

            rbPresidentSP.Checked = true;



        }

        private void loadConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        public void LoadConfig()
        {

            try
            {

                string applicationLogComments;

                //builderOnlyMode = Properties.Settings.Default.builderOnly;
                //Network = Properties.Settings.Default.Network;

                // get configuration info based on computer's IP Address 
                DataTable dtComp = new DataTable();
                string cmdStr = $"SELECT * FROM FE_Devices WHERE IP_Address = '{ipAddress}'";
                dtComp = GetDBData(cmdStr, ConfigDBConnectionString);
                DataRow row;

                if (dtComp.Rows.Count > 0)
                {
                    row = dtComp.Rows[0];
                    computerName = row["Name"].ToString() ?? "";
                    configName = row["Config1"].ToString() ?? "";
                    if (configName == null)
                        configName = "DEFAULT";
                }
                else
                    configName = "DEFAULT";

                log.Debug($"{configName}");


                // get tab enables and mode and network
                DataTable dtEnab = new DataTable();
                cmdStr = $"SELECT * FROM FE_Tabs WHERE Config = '{configName}'";
                dtEnab = GetDBData(cmdStr, ConfigDBConnectionString);

                row = dtEnab.Rows[0];
                bool RBenable = Convert.ToBoolean(row["RaceBoards"] ?? 0);
                bool VAenable = Convert.ToBoolean(row["VoterAnalysis"] ?? 0);
                bool BOPenable = Convert.ToBoolean(row["BOP"] ?? 0);
                bool REFenable = Convert.ToBoolean(row["Referendums"] ?? 0);
                bool SPenable = Convert.ToBoolean(row["SidePanel"] ?? 0);
                bool MAPenable = Convert.ToBoolean(row["Maps"] ?? 0);
                builderOnlyMode = Convert.ToBoolean(row["StackBuildOnly"] ?? 0);
                Network = row["Network"].ToString() ?? "";
                bool NPVenable = Convert.ToBoolean(row["NPV"] ?? 0);

                //bool useBackup = Convert.ToBoolean(row["useBackup"] ?? 0);
                //bool useBackup = !useBackupServer;
                //string ub = "false";
                //if (useBackup)
                //ub = "true";

                //UpdateSetting("UseBackupServer", ub);


                if (Network == "FNC")
                    stackTypeOffset = 0;
                if (Network == "FBN")
                    stackTypeOffset = 100;
                if (Network == "FBC")
                    stackTypeOffset = 200;


                
                applicationLogComments = $"{Network}; Config: {configName}; ";

                if (builderOnlyMode)
                {
                    //stacksDB = GraphicsDBConnectionString;
                    stacksDB = StacksDBConnectionString;
                    stackType = 0;
                    label2.Visible = true;
                    cbGraphicConcept.Visible = true;
                }
                else
                {
                    stacksDB = StacksDBConnectionString;
                    //stackType = (short)(10 * (dataModeSelect.SelectedIndex + 1));
                    stackType = (short)(10 * (tabIndex + 1));
                    int ti = tabIndex;

                    //if (stackType == 50)
                        //stackType = 10;

                    //if (dataModeSelect.SelectedIndex == 1)
                    if (tabIndex == 1)
                        stackType += tcVoterAnalysis.SelectedIndex;

                    stackType += stackTypeOffset;

                    label2.Visible = false;
                    cbGraphicConcept.Visible = false;
                    btnSaveStack.Text = "Save Stack \n  (Ctrl-O) or (Ctrl-S)";
                    btnTake.Text = "Take Next \n (Space)";
                }
                
                this.Size = new Size(1462, 991);
                connectionPanel.Visible = false;
                enginePanel.Visible = false;
                listBox1.Visible = false;
                listBox2.Visible = false;
                panel3.Size = new Size(648, 155);
                panel3.Location = new Point(8, 549);
                StackPanel.Location = new Point(3, 8);
                SaveActivatePanel.Location = new Point(424, 8);
                stackGrid.Size = new Size(648, 536);
                LockPanel.Visible = false;
                TakePanel.Visible = false;
                SaveActivatePanel.Visible = true;
                pnlUpDn.Location = new Point(676, 250);

                if (builderOnlyMode == false)
                {
                    // Enlarge form
                    this.Size = new Size(1462, 1165);
                    lblMediaSequencer.Visible = false;

                    lblConfig.Text = $"{configName}  {DateTime.Now}";
                    lblNetwork.Text = Network;
                    connectionPanel.Visible = true;
                    enginePanel.Visible = true;
                    listBox1.Visible = true;
                    listBox2.Visible = true;
                    stackGrid.Size = new Size(648, 468);
                    panel3.Size = new Size(648, 221);
                    panel3.Location = new Point(8, 483);
                    StackPanel.Location = new Point(1, 78);
                    LockPanel.Visible = true;
                    TakePanel.Visible = true;
                    SaveActivatePanel.Visible = false;
                    SaveActivatePanel.Location = new Point(424, 3);
                    btnSaveStack.Enabled = true;
                    pnlUpDn.Location = new Point(676, 216);

                    // Set the default to show all for race filters, Auto for exit pool questions
                    rbShowAll.BackColor = Color.Gold;
                    rbShowAll.Checked = true;
                    rbAll.BackColor = Color.Gold;
                    rbAll.Checked = true;

                    //rbEPAuto.Checked = true;
                    //rbEPAuto.BackColor = Color.Gold;
                    rbNone.Checked = true;
                    rbNone.BackColor = Color.Gold;

                    rbShowAllSP.BackColor = Color.Gold;
                    rbShowAllSP.Checked = true;
                    rbAllSP.BackColor = Color.Gold;
                    rbAllSP.Checked = true;

                    //rbEPAuto.Checked = true;
                    //rbEPAuto.BackColor = Color.Gold;
                    rbNoneSP.Checked = true;
                    rbNoneSP.BackColor = Color.Gold;

                    
                    // Set enable/disable state of tab pages
                    if (RBenable)
                    {
                        tpRaces.Enabled = true;
                        if (tabId == -1)
                            tabId = 0;
                    }
                    else
                    {
                        tpRaces.Enabled = false;
                        dataModeSelect.TabPages.Remove(tpRaces);

                    }

                    if (VAenable)
                    {
                        tpVoterAnalysis.Enabled = true;
                        if (tabId == -1)
                            tabId = 1;

                    }
                    else
                    {
                        tpVoterAnalysis.Enabled = false;
                        dataModeSelect.TabPages.Remove(tpVoterAnalysis);

                    }

                    if (BOPenable)
                    {
                        tpBalanceOfPower.Enabled = true;
                        if (tabId == -1)
                            tabId = 2;

                    }
                    else
                    {
                        tpBalanceOfPower.Enabled = false;
                        dataModeSelect.TabPages.Remove(tpBalanceOfPower);

                    }

                    if (REFenable)
                    {
                        tpReferendums.Enabled = true;
                        if (tabId == -1)
                            tabId = 3;

                    }
                    else
                    {
                        tpReferendums.Enabled = false;
                        dataModeSelect.TabPages.Remove(tpReferendums);

                    }

                    if (SPenable)
                    {
                        tpSidePanel.Enabled = true;
                        if (tabId == -1)
                            tabId = 4;

                    }
                    else
                    {
                        tpSidePanel.Enabled = false;
                        dataModeSelect.TabPages.Remove(tpSidePanel);

                    }

                    if (MAPenable)
                    {
                        tpMaps.Enabled = true;
                        if (tabId == -1)
                            tabId = 5;

                    }
                    else
                    {
                        tpMaps.Enabled = false;
                        dataModeSelect.TabPages.Remove(tpMaps);

                    }

                    if (NPVenable)
                    {
                        tpNPV.Enabled = true;
                        if (tabId == -1)
                            tabId = 6;

                    }
                    else
                    {
                        tpNPV.Enabled = false;
                        dataModeSelect.TabPages.Remove(tpNPV);

                    }
                    if (VictoriaMode)
                    {
                        tpTicker.Enabled = true;
                    }
                    else
                    {
                        tpTicker.Enabled = false;
                    }

                    
                    dataModeSelect.SelectedIndexChanged += dataModeSelect_SelectedIndexChanged;
                    //dataModeSelect.SelectedIndex = tabId;
                    dataModeSelect.SelectedIndex = 0;


                    // Setup background process for server socket
                    var startListening = new BackgroundWorker();
                    startListening.DoWork += new DoWorkEventHandler(StartListener);
                    startListening.RunWorkerAsync();

                    // get viz engine info

                    cmdStr = $"SELECT * FROM FE_EngineDefs WHERE Config = '{configName}'";
                    DataTable dtEng = new DataTable();
                    dtEng = GetDBData(cmdStr, ConfigDBConnectionString);
                    row = dtEng.Rows[0];


                    int i = 0;
                    bool done = false;
                    string engineParam;
                    //var engineInfo = Properties.Settings.Default["Engine1_IPAddress"];
                    string engineData;
                    string engineStr;

                    // read engine info until no more engines found
                    while (done == false)
                    {
                        EngineModel viz = new EngineModel();
                        i++;
                        engineParam = $"Engine{i}_IPAddress";

                        try
                        {
                            engineStr = $"Eng{i}_";

                            engineData = row[engineStr + "Name"].ToString() ?? "";
                            if (engineData != null)
                            {


                                viz.EngineName = engineData;
                                viz.IPAddress = GetIP(engineData);
                                viz.Port = Convert.ToInt32(row[engineStr + "Port"] ?? 0);
                                viz.enable = Convert.ToBoolean(row[engineStr + "Enable"] ?? 0);
                                viz.id = i;
                                viz.systemIP = System.Net.IPAddress.Parse(viz.IPAddress);


                                /*
                                engineInfo = Properties.Settings.Default[engineParam];
                                viz.IPAddress = (string)engineInfo;

                                engineParam = $"Engine{i}_Port";
                                engineInfo = Properties.Settings.Default[engineParam];
                                viz.Port = (int)engineInfo;

                                engineParam = $"Engine{i}_Enable";
                                engineInfo = Properties.Settings.Default[engineParam];
                                viz.enable = (bool)engineInfo;

                                viz.id = i;
                                viz.systemIP = System.Net.IPAddress.Parse(viz.IPAddress);
                                */

                                vizEngines.Add(viz);

                                vizClientSockets.Add(new ClientSocket(viz.systemIP, viz.Port));
                                vizClientSockets[i - 1].DataReceived += vizDataReceived;
                                vizClientSockets[i - 1].ConnectionStatusChanged += vizConnectionStatusChanged;

                                //gbNamelbl1.Text = viz.EngineName;
                                //gbIPlbl1.Text = "IP: " + viz.IPAddress;
                                //gbPortlbl1.Text = "Port: " + viz.Port.ToString();

                                log.Debug($"{viz.EngineName}  IP: {viz.IPAddress}  Port: {viz.Port}");
                                // set viz address labels
                                switch (i)
                                {
                                    case 1:
                                        gbNamelbl1.Text = viz.EngineName;
                                        gbIPlbl1.Text = "IP: " + viz.IPAddress;
                                        gbPortlbl1.Text = "Port: " + viz.Port.ToString();
                                        gbViz1.Visible = true;
                                        gbViz1.Enabled = viz.enable;
                                        gbEng1.Visible = true;
                                        gbEng1.Enabled = viz.enable;
                                        IPs[0] = viz.IPAddress;
                                        enables[0] = viz.enable;
                                        if (viz.enable)
                                            vizClientSockets[i - 1].Connect();
                                        break;

                                    case 2:
                                        gbNamelbl2.Text = viz.EngineName;
                                        gbIPlbl2.Text = "IP: " + viz.IPAddress;
                                        gbPortlbl2.Text = "Port: " + viz.Port.ToString();
                                        gbViz2.Visible = true;
                                        gbViz2.Enabled = viz.enable;
                                        gbEng2.Visible = true;
                                        gbEng2.Enabled = viz.enable;
                                        IPs[1] = viz.IPAddress;
                                        enables[1] = viz.enable;
                                        if (viz.enable)
                                            vizClientSockets[i - 1].Connect();
                                        break;

                                    case 3:
                                        gbNamelbl3.Text = viz.EngineName;
                                        gbIPlbl3.Text = "IP: " + viz.IPAddress;
                                        gbPortlbl3.Text = "Port: " + viz.Port.ToString();
                                        gbViz3.Visible = true;
                                        gbViz3.Enabled = viz.enable;
                                        gbEng3.Visible = true;
                                        gbEng3.Enabled = viz.enable;
                                        IPs[2] = viz.IPAddress;
                                        enables[2] = viz.enable;
                                        if (viz.enable)
                                            vizClientSockets[i - 1].Connect();
                                        break;

                                    case 4:
                                        gbNamelbl4.Text = viz.EngineName;
                                        gbIPlbl4.Text = "IP: " + viz.IPAddress;
                                        gbPortlbl4.Text = "Port: " + viz.Port.ToString();
                                        gbViz4.Visible = true;
                                        gbViz4.Enabled = viz.enable;
                                        gbEng4.Visible = true;
                                        gbEng4.Enabled = viz.enable;
                                        IPs[3] = viz.IPAddress;
                                        enables[3] = viz.enable;
                                        if (viz.enable)
                                            vizClientSockets[i - 1].Connect();
                                        break;

                                    case 5:
                                        gbIPlbl5.Text = "IP: " + viz.IPAddress;
                                        gbPortlbl5.Text = "Port: " + viz.Port.ToString();
                                        gbViz5.Visible = true;
                                        gbViz5.Enabled = viz.enable;
                                        if (viz.enable)
                                            vizClientSockets[i - 1].Connect();
                                        break;

                                    case 6:
                                        gbIPlbl6.Text = "IP: " + viz.IPAddress;
                                        gbPortlbl6.Text = "Port: " + viz.Port.ToString();
                                        gbViz6.Visible = true;
                                        gbViz6.Enabled = viz.enable;
                                        if (viz.enable)
                                            vizClientSockets[i - 1].Connect();
                                        break;

                                }

                                if (i == 4)
                                    done = true;
                            }
                        }
                        catch
                        {
                            // Next engine not found
                            done = true;
                        }
                    }
                    //ConnectToVizEngines();

                }

                // get tab enables and mode and network
                //cmdStr = $"SELECT * FROM FE_TabConfig WHERE Config = '{configName}'";
                DataTable dt = new DataTable();
                //dt = GetDBData(cmdStr, ElectionsDBConnectionString);
                string tabName;
                bool enab;

                for (int tabNo = 0; tabNo < 7; tabNo++)
                {
                    switch (tabNo)
                    {
                        case 0:
                            tabName = "RaceBoards";
                            enab = RBenable;
                            break;
                        case 1:
                            tabName = "VoterAnalysis";
                            enab = VAenable;
                            break;
                        case 2:
                            tabName = "BOP";
                            enab = BOPenable;
                            break;
                        case 3:
                            tabName = "Referendums";
                            enab = REFenable;
                            break;
                        case 4:
                            tabName = "SidePanel";
                            enab = SPenable;
                            break;
                        case 5:
                            tabName = "Maps";
                            enab = MAPenable;
                            break;
                        case 6:
                            tabName = "NPV";
                            enab = NPVenable;
                            break;
                        default:
                            tabName = "RaceBoards";
                            enab = RBenable;
                            break;
                    }


                    cmdStr = $"SELECT * FROM FE_TabConfig WHERE Config = '{configName}' AND TabName = '{tabName}'";
                    dt = GetDBData(cmdStr, ConfigDBConnectionString);

                    string tabN;
                    TabDefinitionModel td = new TabDefinitionModel();
                    List<TabOutputDef> tod = new List<TabOutputDef>();

                    string sceneCode;
                    string sceneName = "";
                    int engine = 0;
                    bool[] outEng = new bool[4] { false, false, false, false };

                    if (dt.Rows.Count > 0)
                    {
                        if (enab)
                            applicationLogComments += $"{tabName}:";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            row = dt.Rows[i];

                            tabN = row["TabName"].ToString() ?? "";
                            engine = Convert.ToInt32(row["EngineNumber"] ?? 0);
                            sceneCode = row["SceneCode"].ToString() ?? "";

                            TabOutputDef to = new TabOutputDef();
                            to.engine = engine;
                            to.sceneCode = sceneCode;
                            to.sceneName = GetScenePath(sceneCode);
                            tod.Add(to);
                            sceneName += $"{engine}: {GetSceneName(sceneCode)}; ";
                            outEng[engine - 1] = true;
                            applicationLogComments += $" {engine}: {sceneCode}";

                        }
                        td.tabName = tabName;
                        td.showTab = enab;
                        //td.outputEngine[engine - 1] = true;
                        td.outputEngine = outEng;
                        //td.engineSceneDef += $"{engine}: {sceneName}; ";
                        td.engineSceneDef = sceneName;
                        td.TabOutput.AddRange(tod);
                        tabConfig.Add(td);
                        if (enab)
                            applicationLogComments += $"; ";

                    }
                    else
                    {
                        //TabOutputDef to = new TabOutputDef();
                        td.tabName = tabName;
                        td.showTab = enab;
                        td.engineSceneDef = "";
                        td.TabOutput.AddRange(tod);
                        tabConfig.Add(td);

                    }

                }

                SetOutput(0);


                // Initial load all scenes
                for (int index = 0; index < 6; index++)
                {
                    for (int i = 0; i < tabConfig[index].TabOutput.Count; i++)
                    {
                        string scenename = tabConfig[index].TabOutput[i].sceneName;
                        int engine = tabConfig[index].TabOutput[i].engine;
                        if (vizEngines.Count > 0)
                        {
                            if (vizEngines[engine - 1].enable)
                            {
                                //LoadScene(scenename, engine);
                                InitialLoadScene(scenename, engine);
                            }

                        }
                    }
                    if (index == 1 && takeIn == true)
                    {
                        SendCmdToViz("TRIGGER_ACTION", "TAKEOUT", index);
                        takeIn = false;
                    }
                }



                // Make entry into applications log
                ApplicationSettingsFlagsAccess applicationSettingsFlagsAccess = new ApplicationSettingsFlagsAccess();

                // Post application log entry
                applicationSettingsFlagsAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                applicationSettingsFlagsAccess.PostApplicationLogEntry(
                    Properties.Settings.Default.ApplicationName,
                    Properties.Settings.Default.ApplicationName,
                    hostName,
                    ipAddress,
                    enables[0],
                    IPs[0],
                    enables[1],
                    IPs[1],
                    "Launched application",
                    Convert.ToString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version),
                    Properties.Settings.Default.ApplicationID,
                    applicationLogComments,
                    System.DateTime.Now,
                    enables[2],
                    IPs[2],
                    enables[3],
                    IPs[3]

                );

            }
            catch (Exception ex)
            {
                listBox2.Items.Add($"Load Config Error: {ex}");
                log.Error($"Load Config Error: {ex}");

            }


        }

        private void DataModeSelect_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void UpdateSetting(string key, string value)
        {
            try
            {
                config.AppSettings.Settings["UseBackupServer"].Value = value;
                config.Save(ConfigurationSaveMode.Full);

                ConfigurationManager.RefreshSection("appSettings");

            }
            catch (Exception ex)
            {
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        public void ConnectToVizEngines()
        {

            for (int i = 0; i < vizEngines.Count; i++)
            {
                // Connect to the ClientSocket; call-backs for connection status will indicate status of client sockets
                if (vizEngines[i].enable)
                {
                    vizClientSockets[i].AutoReconnect = true;
                    vizClientSockets[i].Connect();
                }

            }
        }

        public string GetIP(string name)
        {
            DataTable dt = new DataTable();
            string cmdStr = $"SELECT * FROM FE_Devices WHERE Name = '{name}'";
            //dt = GetDBData(cmdStr, ElectionsDBConnectionString);
            dt = GetDBData(cmdStr, ConfigDBConnectionString);

            DataRow row = dt.Rows[0];
            return row["IP_Address"].ToString() ?? "";

        }
        public string GetSceneName(string sceneCode)
        {
            DataTable dt = new DataTable();
            string cmdStr = $"SELECT * FROM FE_Scenes WHERE SceneCode = '{sceneCode}'";
            //dt = GetDBData(cmdStr, ElectionsDBConnectionString);
            dt = GetDBData(cmdStr, ConfigDBConnectionString);
            string[] sceneNames;
            string[] strSeparator = new string[] { "/" };
            string sceneName = "";


            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string scenePath = row["ScenePath"].ToString() ?? "";
                sceneNames = scenePath.Split(strSeparator, StringSplitOptions.None);
                int i = sceneNames.Length;
                sceneName = sceneNames[i - 1];
                return sceneName;
                //return row["ScenePath"].ToString() ?? "";

            }
            else
                return "";
        }
        public string GetScenePath(string sceneCode)
        {
            DataTable dt = new DataTable();
            string cmdStr = $"SELECT * FROM FE_Scenes WHERE SceneCode = '{sceneCode}'";
            //dt = GetDBData(cmdStr, ElectionsDBConnectionString);
            dt = GetDBData(cmdStr, ConfigDBConnectionString);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string scenePath = row["ScenePath"].ToString() ?? "";
                return row["ScenePath"].ToString() ?? "";
            }
            else
                return "";
        }

        private void vizDataReceived(ClientSocket sender, byte[] data)
        {
            // receive the data and determine the type
            //string vizIP = sender.Ip;
            System.Net.IPAddress IP = sender.Ip;
            int port = sender.Port;
            int i = GetVizEngineNumber(IP, port);
            string resp = System.Text.Encoding.Default.GetString(data);
            //listBox1.Items.Add(resp);

        }

        public int GetVizEngineNumber(System.Net.IPAddress IP, int port)
        {
            for (int i = 0; i < vizEngines.Count; i++)
            {
                if (vizEngines[i].systemIP == IP && vizEngines[i].Port == port)
                {
                    return i;
                }
            }
            return -1;
        }

        // Handler for source & destination MSE connection status change
        public void vizConnectionStatusChanged(ClientSocket sender, ClientSocket.ConnectionStatus status)
        {

            System.Net.IPAddress IP = sender.Ip;
            int port = sender.Port;
            int i = GetVizEngineNumber(IP, port);

            // Set status
            if (status == ClientSocket.ConnectionStatus.Connected)
            {
                vizEngines[i].connected = true;
            }
            else
            {
                vizEngines[i].connected = false;
            }
            SetConnectionLED(i);
            // Send to log - DEBUG ONLY
            //log.Debug($"Viz Engine {i + 1}: {status}");
        }

        delegate void Invoker(int i);

        public void SetConnectionLED(int i)
        {

            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.BeginInvoke(new Invoker(SetConnectionLED), i);

                // we return immedeately
                return;
            }


            bool connected = vizEngines[i].connected;
            switch (i + 1)
            {
                case 1:
                    if (connected)
                        gbLEDOn1.Visible = true;
                    else
                        gbLEDOn1.Visible = false;
                    break;

                case 2:
                    if (connected)
                        gbLEDOn2.Visible = true;
                    else
                        gbLEDOn2.Visible = false;
                    break;

                case 3:
                    if (connected)
                        gbLEDOn3.Visible = true;
                    else
                        gbLEDOn3.Visible = false;
                    break;

                case 4:
                    if (connected)
                        gbLEDOn4.Visible = true;
                    else
                        gbLEDOn4.Visible = false;
                    break;

                case 5:
                    if (connected)
                        gbLEDOn5.Visible = true;
                    else
                        gbLEDOn5.Visible = false;
                    break;

                case 6:
                    if (connected)
                        gbLEDOn6.Visible = true;
                    else
                        gbLEDOn6.Visible = false;
                    break;

            }

        }

        // Handler for main menu program exit button click
        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Method for main form close - confirm with operator
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Are you sure you want to exit the Election Graphics Stack Builder Application?", "Warning",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                // Make entry into applications log
                ApplicationSettingsFlagsAccess applicationSettingsFlagsAccess = new ApplicationSettingsFlagsAccess();
                string ipAddress = string.Empty;
                string hostName = string.Empty;
                ipAddress = HostIPNameFunctions.GetLocalIPAddress();
                hostName = HostIPNameFunctions.GetHostName(ipAddress);
                lblIpAddress.Text = ipAddress;
                lblHostName.Text = hostName;

                // Post application log entry
                applicationSettingsFlagsAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                applicationSettingsFlagsAccess.PostApplicationLogEntry(
                    Properties.Settings.Default.ApplicationName,
                    Properties.Settings.Default.ApplicationName,
                    hostName,
                    ipAddress,
                    enables[0],
                IPs[0],
                enables[1],
                IPs[1],
                "Closed application",
                    Convert.ToString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version),
                    Properties.Settings.Default.ApplicationID,
                    "",
                    System.DateTime.Now,
                    enables[2],
                IPs[2],
                enables[3],
                IPs[3]

                );
            }
        }

        // Handler for main form closed - log info
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Log application quit
            log.Info("Quitting Stack Builder application");
        }
        #endregion

        #region General Form related methods
        // General update timer
        private void timerStatusUpdate_Tick(object sender, EventArgs e)
        {
            if (ApplicationSettingsFlagsCollection.UseSimulatedTime == true)
            {
                referenceTime = TimeFunctions.GetSimulatedTime();
                gbTime.Text = @"SIMULATED TIME";
            }
            else
            {
                referenceTime = DateTime.Now;
                gbTime.Text = @"ACTUAL TIME";
            }

            timeLabel.Text = String.Format("{0:h:mm:ss tt  MMM dd, yyyy}", referenceTime);

        }

        // Handler for change to main data mode select tab control
        private void dataModeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (dataModeSelect.SelectedIndex == 1 && tcVoterAnalysis.SelectedIndex == 1 && VictoriaMode == false)

            if (tabIndex == 1 && tcVoterAnalysis.SelectedIndex == 1 && VictoriaMode == false)
                btnSaveStack.Enabled = false;
            else
                btnSaveStack.Enabled = true;

            VAQRefreshTimer.Enabled = false;

            if (dataModeSelect.SelectedTab.Text == "Race Boards")
                tabIndex = 0;

            if (dataModeSelect.SelectedTab.Text == "Voter Analysis")
            {
                tabIndex = 1;
                //VAQRefreshTimer.Enabled = true;
            }

            if (dataModeSelect.SelectedTab.Text == "Balance of Power")
                tabIndex = 2;

            if (dataModeSelect.SelectedTab.Text == "Referendums")
                tabIndex = 3;

            if (dataModeSelect.SelectedTab.Text == "Side Panel")
                tabIndex = 4;

            if (dataModeSelect.SelectedTab.Text == "Maps")
            {
                tabIndex = 5;
                //VAQRefreshTimer.Enabled = true;
            }

            if (dataModeSelect.SelectedTab.Text == "NPV")
                tabIndex = 6;

            if (stackLocked == false)
            {
                switch (tabId)
                {
                    case 0:
                        tabsave.stkRB.Clear();
                        foreach (StackElementModel se in stackElements)
                        {
                            tabsave.stkRB.Add(se);
                        }
                        break;
                    case 1:
                        tabsave.stkVA.Clear();
                        foreach (StackElementModel se in stackElements)
                        {
                            tabsave.stkVA.Add(se);
                        }
                        break;
                    case 2:
                        tabsave.stkBOP.Clear();
                        foreach (StackElementModel se in stackElements)
                        {
                            tabsave.stkBOP.Add(se);
                        }
                        break;
                    case 3:
                        tabsave.stkRef.Clear();
                        foreach (StackElementModel se in stackElements)
                        {
                            tabsave.stkRef.Add(se);
                        }
                        break;
                    case 4:
                        tabsave.stkSP.Clear();
                        foreach (StackElementModel se in stackElements)
                        {
                            tabsave.stkSP.Add(se);
                        }
                        break;
                    case 5:
                        tabsave.stkMap.Clear();
                        foreach (StackElementModel se in stackElements)
                        {
                            tabsave.stkMap.Add(se);
                        }
                        break;
                }

                stackElements.Clear();

                //switch (dataModeSelect.SelectedIndex)
                switch (tabIndex)
                {
                    case 0:
                        //stackElements.Clear();
                        txtStackName.Text = stackNameRB;
                        foreach (StackElementModel se in tabsave.stkRB)
                        {
                            stackElements.Add(se);
                        }
                        break;
                    case 1:
                        txtStackName.Text = stackNameVA;
                        //stackElements.Clear();
                        foreach (StackElementModel se in tabsave.stkVA)
                        {
                            stackElements.Add(se);
                        }
                        break;
                    case 2:
                        txtStackName.Text = stackNameBOP;
                        //stackElements.Clear();
                        foreach (StackElementModel se in tabsave.stkBOP)
                        {
                            stackElements.Add(se);
                        }
                        break;
                    case 3:
                        txtStackName.Text = stackNameREF;
                        //stackElements.Clear();
                        foreach (StackElementModel se in tabsave.stkRef)
                        {
                            stackElements.Add(se);
                        }
                        break;
                    case 4:
                        txtStackName.Text = stackNameSP;
                        //stackElements.Clear();
                        foreach (StackElementModel se in tabsave.stkSP)
                        {
                            stackElements.Add(se);
                        }
                        break;
                    case 5:
                        txtStackName.Text = stackNameMAP;
                        //stackElements.Clear();
                        foreach (StackElementModel se in tabsave.stkMap)
                        {
                            stackElements.Add(se);
                        }
                        break;
                    case 6:
                        txtStackName.Text = "";
                        stackElements.Clear();
                        AddNPVBoardToStack();
                        break;
                }


                if (builderOnlyMode == false)
                {
                    //stackType = (short)(10 * (dataModeSelect.SelectedIndex + 1));
                    stackType = (short)(10 * (tabIndex + 1));
                    //if (dataModeSelect.SelectedIndex == 1)
                    if (tabIndex == 1)
                    {
                        stackType += tcVoterAnalysis.SelectedIndex;
                    }
                }
                else
                    stackType = 0;

                //if (stackType == 50)
                    //stackType = 10;

                stackType += stackTypeOffset;

                //if (dataModeSelect.SelectedIndex == 1)
                if (tabIndex == 1)
                {
                    if (Network == "FBC")
                        stackType -= stackTypeOffset;
                    GetVoterAnalysisGridData();
                }

                //if (dataModeSelect.SelectedIndex == 5)
                if (tabIndex == 5)
                    GetVoterAnalysisMapGridData();

                if (!builderOnlyMode)
                    SetOutput(tabIndex);
                //SetOutput(dataModeSelect.SelectedIndex);

                //tabId = dataModeSelect.SelectedIndex;
                tabId = tabIndex;

                //switch (dataModeSelect.SelectedIndex)
                switch (tabIndex)
                {
                    case 0:
                        tpRaces.Focus();
                        break;
                    case 1:
                        tpVoterAnalysis.Focus();
                        dgvVoterAnalysis.Focus();
                        break;
                    case 2:
                        tpBalanceOfPower.Focus();
                        break;
                    case 3:
                        tpReferendums.Focus();
                        break;
                    case 4:
                        tpSidePanel.Focus();
                        break;
                    case 5:
                        tpMaps.Focus();
                        break;
                    case 6:
                        tpNPV.Focus();
                        break;
                }
            }
        }
        #endregion

        #region Utility functions
        // Refresh the list of available stacks for the grid

        private void RefreshStacksList()
        {
            try
            {
                // Setup the available stacks collection
                this.stacksCollection = new StacksCollection();

                //if (builderOnlyMode)
                //this.stacksCollection.MainDBConnectionString = GraphicsDBConnectionString;
                //else
                //this.stacksCollection.MainDBConnectionString = StacksDBConnectionString;

                this.stacksCollection.MainDBConnectionString = stacksDB;


                stacks = this.stacksCollection.GetStackCollection(stackType);
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred during stacks list refresh: " + ex.Message);
                //log.Debug("frmMain Exception occurred during stacks list refresh", ex);
            }
        }
        #endregion

        # region Data refresh functions
        // Refresh the list of available races for the races list
        private void RefreshAvailableRacesList(bool isPrimary)
        {
            try
            {
                // Setup the available races collection
                this.availableRacesCollection = new AvailableRacesCollection();
                this.availableRacesCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                availableRaces = this.availableRacesCollection.GetAvailableRaceCollection(isPrimary);

                // Setup the available races grid
                availableRacesGrid.AutoGenerateColumns = false;
                var availableRacesGridDataSource = new BindingSource(availableRaces, null);
                availableRacesGrid.DataSource = availableRacesGridDataSource;

                availableRacesGridSP.AutoGenerateColumns = false;
                availableRacesGridSP.DataSource = availableRacesGridDataSource;

                lblAvailRaceCnt.Text = "Available Races: " + Convert.ToString(availableRaces.Count);
                lblAvailRaceCntSP.Text = "Available Races: " + Convert.ToString(availableRaces.Count);

                // Race Boards
                availableRacesGrid.Columns[0].HeaderText = "Race ID";
                availableRacesGrid.Columns[0].DataPropertyName = "Race_ID";
                availableRacesGrid.Columns[0].Width = 50;
                
                availableRacesGrid.Columns[1].HeaderText = "Ofc";
                availableRacesGrid.Columns[1].DataPropertyName = "Race_Office";
                availableRacesGrid.Columns[1].Width = 40;

                if (electionMode == "Primary")
                {
                    //availableRacesGrid.Columns[2].HeaderText = "eType";
                    //availableRacesGrid.Columns[2].DataPropertyName = "Election_Type";
                    availableRacesGrid.Columns[2].HeaderText = "Party";
                    availableRacesGrid.Columns[2].DataPropertyName = "Party";
                    availableRacesGrid.Columns[2].Width = 50;
                
                    availableRacesGrid.Columns[3].HeaderText = "Race Description";
                    availableRacesGrid.Columns[3].DataPropertyName = "Race_Description";
                    availableRacesGrid.Columns[3].Width = 400;

                }
                else
                {
                    availableRacesGrid.Columns[2].HeaderText = "Race Description";
                    availableRacesGrid.Columns[2].DataPropertyName = "Race_Description";
                    availableRacesGrid.Columns[2].Width = 400;
                }

                // Side Panel
                availableRacesGridSP.Columns[0].HeaderText = "Race ID";
                availableRacesGridSP.Columns[0].DataPropertyName = "Race_ID";
                availableRacesGridSP.Columns[0].Width = 50;

                availableRacesGridSP.Columns[1].HeaderText = "Ofc";
                availableRacesGridSP.Columns[1].DataPropertyName = "Race_Office";
                availableRacesGridSP.Columns[1].Width = 40;

                if (electionMode == "Primary")
                {
                    //availableRacesGridSP.Columns[2].HeaderText = "eType";
                    //availableRacesGridSP.Columns[2].DataPropertyName = "Election_Type";
                    availableRacesGridSP.Columns[2].HeaderText = "Party";
                    availableRacesGridSP.Columns[2].DataPropertyName = "Party";
                    availableRacesGridSP.Columns[2].Width = 50;

                    availableRacesGridSP.Columns[3].HeaderText = "Race Description";
                    availableRacesGridSP.Columns[3].DataPropertyName = "Race_Description";
                    availableRacesGridSP.Columns[3].Width = 400;

                }
                else
                {
                    availableRacesGridSP.Columns[2].HeaderText = "Race Description";
                    availableRacesGridSP.Columns[2].DataPropertyName = "Race_Description";
                    availableRacesGridSP.Columns[2].Width = 400;
                }


            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }
        // Refresh the Application settings from the Flags DB
        private void RefreshApplicationFlags()
        {
            try
            {
                // Setup the application flags
                this.applicationSettingsFlagsCollection = new ApplicationSettingsFlagsCollection();
                this.applicationSettingsFlagsCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                applicationFlags = this.applicationSettingsFlagsCollection.GetFlagsCollection();

                ApplicationSettingsFlagsModel flags = null;
                flags = applicationFlags[0];
                UseSimulatedTime = flags.UseSimulatedElectionDayTime;
                PollClosinglockout = flags.PollClosingLockoutEnable;
            }

            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Refresh the list of available races for the races list
        //private void RefreshAvailableRacesListFiltered(string ofc, Int16 cStatus, Int16 scfm, BindingList<StateMetadataModel> stateMetadata, bool isPrimary)
        //{
        //    try
        //    {
        //        // Setup the available races collection
        //        this.availableRacesCollection = new AvailableRacesCollection();
        //        this.availableRacesCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
        //        availableRaces = this.availableRacesCollection.GetFilteredRaceCollection(ofc, cStatus, scfm, stateMetadata, isPrimary);

        //        // Set next poll closing label
        //        if (scfm == (short)SpecialCaseFilterModes.Next_Poll_Closing_States_Only)
        //        {
        //            txtNextPollClosingTime.Text = Convert.ToString(this.availableRacesCollection.NextPollClosingTime);
        //            txtNextPollClosingTimeHeader.Visible = true;
        //            txtNextPollClosingTime.Visible = true;
        //        }
        //        else
        //        {
        //            txtNextPollClosingTimeHeader.Visible = false;
        //            txtNextPollClosingTime.Visible = false;
        //            txtNextPollClosingTime.Text = "N/A";
        //        }


        //        // Setup the available races grid
        //        availableRacesGrid.AutoGenerateColumns = false;
        //        var availableRacesGridDataSource = new BindingSource(availableRaces, null);
        //        availableRacesGrid.DataSource = availableRacesGridDataSource;

        //        availableRacesGridSP.AutoGenerateColumns = false;
        //        availableRacesGridSP.DataSource = availableRacesGridDataSource;

        //        lblAvailRaceCnt.Text = "Available Races: " + Convert.ToString(availableRaces.Count);
        //        lblAvailRaceCntSP.Text = "Available Races: " + Convert.ToString(availableRaces.Count);
        //        // Race Boards
        //        availableRacesGrid.Columns[0].HeaderText = "Race ID";
        //        availableRacesGrid.Columns[0].DataPropertyName = "Race_ID";
        //        availableRacesGrid.Columns[0].Width = 50;

        //        availableRacesGrid.Columns[1].HeaderText = "Ofc";
        //        availableRacesGrid.Columns[1].DataPropertyName = "Race_Office";
        //        availableRacesGrid.Columns[1].Width = 40;

        //        if (electionMode == "Primary")
        //        {
        //            //availableRacesGrid.Columns[2].HeaderText = "eType";
        //            //availableRacesGrid.Columns[2].DataPropertyName = "Election_Type";
        //            availableRacesGrid.Columns[2].HeaderText = "Party";
        //            availableRacesGrid.Columns[2].DataPropertyName = "Party";
        //            availableRacesGrid.Columns[2].Width = 50;

        //            availableRacesGrid.Columns[3].HeaderText = "Race Description";
        //            availableRacesGrid.Columns[3].DataPropertyName = "Race_Description";
        //            availableRacesGrid.Columns[3].Width = 400;

        //        }
        //        else
        //        {
        //            availableRacesGrid.Columns[2].HeaderText = "Race Description";
        //            availableRacesGrid.Columns[2].DataPropertyName = "Race_Description";
        //            availableRacesGrid.Columns[2].Width = 400;
        //        }

        //        // Side Panel
        //        availableRacesGridSP.Columns[0].HeaderText = "Race ID";
        //        availableRacesGridSP.Columns[0].DataPropertyName = "Race_ID";
        //        availableRacesGridSP.Columns[0].Width = 50;

        //        availableRacesGridSP.Columns[1].HeaderText = "Ofc";
        //        availableRacesGridSP.Columns[1].DataPropertyName = "Race_Office";
        //        availableRacesGridSP.Columns[1].Width = 40;

        //        if (electionMode == "Primary")
        //        {
        //            //availableRacesGridSP.Columns[2].HeaderText = "eType";
        //            //availableRacesGridSP.Columns[2].DataPropertyName = "Election_Type";
        //            availableRacesGridSP.Columns[2].HeaderText = "Party";
        //            availableRacesGridSP.Columns[2].DataPropertyName = "Party";
        //            availableRacesGridSP.Columns[2].Width = 50;

        //            availableRacesGridSP.Columns[3].HeaderText = "Race Description";
        //            availableRacesGridSP.Columns[3].DataPropertyName = "Race_Description";
        //            availableRacesGridSP.Columns[3].Width = 400;

        //        }
        //        else
        //        {
        //            availableRacesGridSP.Columns[2].HeaderText = "Race Description";
        //            availableRacesGridSP.Columns[2].DataPropertyName = "Race_Description";
        //            availableRacesGridSP.Columns[2].Width = 400;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error
        //        log.Error("frmMain Exception occurred: " + ex.Message);
        //        //log.Debug("frmMain Exception occurred", ex);
        //    }
        //}

        private void RefreshAvailableRacesListFiltered(string ofc, Int16 cStatus, Int16 scfm, BindingList<StateMetadataModel> stateMetadata, bool isPrimary)
        {
            try
            {
                // Setup the available races collection
                this.availableRacesCollection = new AvailableRacesCollection();
                this.availableRacesCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                availableRaces = this.availableRacesCollection.GetFilteredRaceCollection(ofc, cStatus, scfm, stateMetadata, isPrimary);

                // Set next poll closing label
                if (scfm == (short)SpecialCaseFilterModes.Next_Poll_Closing_States_Only)
                {
                    txtNextPollClosingTime.Text = Convert.ToString(this.availableRacesCollection.NextPollClosingTime);
                    txtNextPollClosingTimeHeader.Visible = true;
                    txtNextPollClosingTime.Visible = true;
                }
                else
                {
                    txtNextPollClosingTimeHeader.Visible = false;
                    txtNextPollClosingTime.Visible = false;
                    txtNextPollClosingTime.Text = "N/A";
                }


                // Setup the available races grid
                availableRacesGrid.AutoGenerateColumns = false;
                var availableRacesGridDataSource = new BindingSource(availableRaces, null);
                availableRacesGrid.DataSource = availableRacesGridDataSource;

                
                lblAvailRaceCnt.Text = "Available Races: " + Convert.ToString(availableRaces.Count);
                // Race Boards
                availableRacesGrid.Columns[0].HeaderText = "Race ID";
                availableRacesGrid.Columns[0].DataPropertyName = "Race_ID";
                availableRacesGrid.Columns[0].Width = 50;

                availableRacesGrid.Columns[1].HeaderText = "Ofc";
                availableRacesGrid.Columns[1].DataPropertyName = "Race_Office";
                availableRacesGrid.Columns[1].Width = 40;

                if (electionMode == "Primary")
                {
                    //availableRacesGrid.Columns[2].HeaderText = "eType";
                    //availableRacesGrid.Columns[2].DataPropertyName = "Election_Type";
                    availableRacesGrid.Columns[2].HeaderText = "Party";
                    availableRacesGrid.Columns[2].DataPropertyName = "Party";
                    availableRacesGrid.Columns[2].Width = 50;

                    availableRacesGrid.Columns[3].HeaderText = "Race Description";
                    availableRacesGrid.Columns[3].DataPropertyName = "Race_Description";
                    availableRacesGrid.Columns[3].Width = 400;

                }
                else
                {
                    availableRacesGrid.Columns[2].HeaderText = "Race Description";
                    availableRacesGrid.Columns[2].DataPropertyName = "Race_Description";
                    availableRacesGrid.Columns[2].Width = 400;
                }

                

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Refresh the list of available races for the races list
        private void RefreshAvailableRacesListFilteredSP(string ofc, Int16 cStatus, Int16 scfm, BindingList<StateMetadataModel> stateMetadata, bool isPrimary)
        {
            try
            {
                // Setup the available races collection
                this.availableRacesCollection = new AvailableRacesCollection();
                this.availableRacesCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                availableRacesSP = this.availableRacesCollection.GetFilteredRaceCollection(ofc, cStatus, scfm, stateMetadata, isPrimary);

                // Set next poll closing label
                if (scfm == (short)SpecialCaseFilterModes.Next_Poll_Closing_States_Only)
                {
                    txtNextPollClosingTime.Text = Convert.ToString(this.availableRacesCollection.NextPollClosingTime);
                    txtNextPollClosingTimeHeader.Visible = true;
                    txtNextPollClosingTime.Visible = true;
                }
                else
                {
                    txtNextPollClosingTimeHeader.Visible = false;
                    txtNextPollClosingTime.Visible = false;
                    txtNextPollClosingTime.Text = "N/A";
                }


                // Setup the available races grid
                var availableRacesGridDataSourceSP = new BindingSource(availableRacesSP, null);
                
                availableRacesGridSP.AutoGenerateColumns = false;
                availableRacesGridSP.DataSource = availableRacesGridDataSourceSP;

                lblAvailRaceCntSP.Text = "Available Races: " + Convert.ToString(availableRaces.Count);
                // Race Boards
                
                // Side Panel
                availableRacesGridSP.Columns[0].HeaderText = "Race ID";
                availableRacesGridSP.Columns[0].DataPropertyName = "Race_ID";
                availableRacesGridSP.Columns[0].Width = 50;

                availableRacesGridSP.Columns[1].HeaderText = "Ofc";
                availableRacesGridSP.Columns[1].DataPropertyName = "Race_Office";
                availableRacesGridSP.Columns[1].Width = 40;

                if (electionMode == "Primary")
                {
                    //availableRacesGridSP.Columns[2].HeaderText = "eType";
                    //availableRacesGridSP.Columns[2].DataPropertyName = "Election_Type";
                    availableRacesGridSP.Columns[2].HeaderText = "Party";
                    availableRacesGridSP.Columns[2].DataPropertyName = "Party";
                    availableRacesGridSP.Columns[2].Width = 50;

                    availableRacesGridSP.Columns[3].HeaderText = "Race Description";
                    availableRacesGridSP.Columns[3].DataPropertyName = "Race_Description";
                    availableRacesGridSP.Columns[3].Width = 400;

                }
                else
                {
                    availableRacesGridSP.Columns[2].HeaderText = "Race Description";
                    availableRacesGridSP.Columns[2].DataPropertyName = "Race_Description";
                    availableRacesGridSP.Columns[2].Width = 400;
                }

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }
     

        // Refresh the list of exit polls for the list

        /*
    private void RefreshExitPollQuestions()
    {
        try
        {
            // Setup the exit polls collection
            this.exitPollQuestionsCollection = new ExitPollQuestionsCollection();
            this.exitPollQuestionsCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
            //exitPollQuestions = this.exitPollQuestionsCollection.GetExitPollQuestionsCollection(manualEPQuestions);
            exitPollQuestions = this.exitPollQuestionsCollection.GetExitPollQuestionsCollection(manualEPQuestions);

            // Setup the available exit polls grid
            availableExitPollsGrid.AutoGenerateColumns = false;
            var availableExitPollsGridDataSource = new BindingSource(exitPollQuestions, null);
            availableExitPollsGrid.DataSource = availableExitPollsGridDataSource;
        }
        catch (Exception ex)
        {
            // Log error
            log.Error("frmMain Exception occurred: " + ex.Message);
            log.Debug("frmMain Exception occurred", ex);
        }
    }
    */

        private void RefreshExitPollQuestions()
        {
            try
            {
                //tcVoterAnalysis_SelectedIndexChanged(object sender, EventArgs e)
                // Setup the exit polls collection
                DataTable dt = GetDBData(SQLCommands.sqlGetVoterAnalysisQuestions_FullScreen, ElectionsDBConnectionString);
                //VA_Qdata_FS = DataTableToList

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }





        // Refresh the list of referendums for the list
        private void RefreshReferendums()
        {
            try
            {
                // Setup the referendums collection
                this.referendumsCollection = new ReferendumsCollection();
                this.referendumsCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                referendums = this.referendumsCollection.GetReferendumsCollection();

                // Setup the available referendums grid
                ReferendumsGrid.AutoGenerateColumns = false;
                var ReferendumsGridDataSource = new BindingSource(referendums, null);
                ReferendumsGrid.DataSource = ReferendumsGridDataSource;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        #endregion

        #region Race data & preview functions
        /// <summary>
        /// RACE DATA & PREVIEW FUNCTIONS
        /// </summary>
        // Define the collection used for storing candidate data for a specific race
        private void CreateRaceDataCollection()
        {
            try
            {
                this.raceDataCollection = new RaceDataCollection();
                this.raceDataCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                // Specify state ID = -1 => Don't query database for candidate data until requesting actual race data
                raceData = this.raceDataCollection.GetRaceDataCollection(electionMode, -1, "P", 0, "G", 1, false, 0, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Define the collection used for storing data strings to be sent to multi-play for preview purposes
        private void CreateRacePreviewCollection()
        {
            try
            {
                this.racePreviewCollection = new RacePreviewCollection();
                racePreview = racePreviewCollection.GetRacepPreviewCollection();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Create the state metadata collection
        private void CreateStateMetadataCollection()
        {
            try
            {
                // Setup the master race collection & bind to grid
                this.stateMetadataCollection = new StateMetadataCollection();
                this.stateMetadataCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                stateMetadata = this.stateMetadataCollection.GetStateMetadataCollection();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Create the graphics concepts collection
        //private void CreateGraphicsConceptsCollection()
        //{
        //    try
        //    {
        //        // Setup the master race collection & bind to grid
        //        this.graphicsConceptsCollection = new GraphicsConceptsCollection();
        //        this.graphicsConceptsCollection.GraphicsDBConnectionString = GraphicsDBConnectionString;
        //        graphicsConceptTypes = this.graphicsConceptsCollection.GetGraphicsConceptTypesCollection();

        //        for (Int16 i = 0; i < graphicsConceptTypes.Count; i++)
        //            cbGraphicConcept.Items.Add(graphicsConceptTypes[i].ConceptName);

        //        conceptID = graphicsConceptTypes[0].ConceptID;
        //        conceptName = graphicsConceptTypes[0].ConceptName;
        //        cbGraphicConcept.SelectedIndex = 0;
        //        cbGraphicConcept.Text = conceptName;

        //        // Setup the master race collection & bind to grid
        //        //this.graphicsConceptsCollection = new GraphicsConceptsCollection();
        //        //this.graphicsConceptsCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
        //        graphicsConcepts = this.graphicsConceptsCollection.GetGraphicsConceptsCollection();

        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error
        //        log.Error("frmMain Exception occurred while trying to create graphics concepts collection: " + ex.Message);
        //        //log.Debug("frmMain Exception occurred while trying to create graphics concepts collection", ex);
        //    }
        //}
        #endregion

        #region Stack (group) creation & management functions
        /// <summary>
        /// STACK CREATION & MANAGEMENT FUNCTIONS
        /// </summary>
        // Function create the initial playlist elements collection for editing
        private void CreateStackElementsCollections()
        {
            try
            {
                // Setup the master stack collection & bind to grid
                this.stackElementsCollection = new StackElementsCollection();
                //this.stackElementsCollection.MainDBConnectionString = GraphicsDBConnectionString;
                this.stackElementsCollection.MainDBConnectionString = StacksDBConnectionString;
                stackElements = this.stackElementsCollection.GetStackElementsCollection(-1);

                // Setup the stacks grid
                stackGrid.AutoGenerateColumns = false;
                var stackGridDataSource = new BindingSource(stackElements, null);
                stackGrid.DataSource = stackGridDataSource;

                // Setup the stack collection used for loading to MSE only
                this.activateStackElementsCollection = new StackElementsCollection();
                //this.activateStackElementsCollection.MainDBConnectionString = GraphicsDBConnectionString;
                this.activateStackElementsCollection.MainDBConnectionString = StacksDBConnectionString;
                activateStackElements = this.stackElementsCollection.GetStackElementsCollection(-1);

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }
        #endregion

        #region General dialogs
        /// <summary>
        /// GENERAL DIALOGS
        /// </summary>
        // Launch About box
        private void miAboutBox_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.frmAbout aboutBox = new Forms.frmAbout();

                // Show the dialog
                aboutBox.ShowDialog();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Handler from main menu to launch show selection dialog
        //private void miSelectDefaultShow_Click(object sender, EventArgs e)
        //{
        //    DialogResult dr = new DialogResult();
        //    string mseEndpoint = string.Empty;
        //    if (usingPrimaryMediaSequencer)
        //    {
        //        mseEndpoint = mseEndpoint1;
        //    }
        //    else
        //    {
        //        mseEndpoint = mseEndpoint2;
        //    }
        //    frmSelectShow selectShow = new frmSelectShow(mseEndpoint,GraphicsDBConnectionString);
        //    dr = selectShow.ShowDialog();
        //    if (dr == DialogResult.OK)
        //    {
        //        // Set the new show
        //        currentShowName = selectShow.selectedShow;
        //        lblCurrentShow.Text = currentShowName;

        //        // Write the new default show out to the config file
        //        Properties.Settings.Default.CurrentShowName = currentShowName;
        //        Properties.Settings.Default.Save();
        //    }
        //}
        #endregion

        #region Raceboard add to Stack operations 
        // <summary>
        /// STACK OPERATIONS
        /// </summary>
        
        // Handler for Add 1-Way race board button
        private void btnAddRace1Way_Click(object sender, EventArgs e)
        {
            Int16 seType = (short)StackElementTypes.Race_Board_1_Way;
            string seDescription = "Race Board (1-Way)";
            Int16 seDataType = (int)DataTypes.Race_Boards;
            if (tabIndex == 4)
                seDataType = (int)DataTypes.Side_Panel;

            if (insertNext == true)
            {
                AddRaceBoardToStack(seType, seDescription, seDataType);
            }
            else
            {
            }
        }

        // Handler for Add 2-Way race board button
        private void btnAddRace2Way_Click(object sender, EventArgs e)
        {
            // Call method to add 2-way board
            Add2WayBoard();
        }
        // Double-click on availble races grid defaults to 2-way board
        private void availableRacesGrid_DoubleClick(object sender, EventArgs e)
        {
            // Call method to add 2-way board
            Add2WayBoard();
        }
        // General method to add a 2-way race board
        private void Add2WayBoard()
        {
            Int16 seType = (short)StackElementTypes.Race_Board_2_Way;
            string seDescription = "Race Board (2-Way)";
            Int16 seDataType = (int)DataTypes.Race_Boards;
            if (tabIndex == 4)
                seDataType = (int)DataTypes.Side_Panel;
            AddRaceBoardToStack(seType, seDescription, seDataType);
        }

        // Handler for Add 3-Way race board button
        private void btnAddRace3Way_Click(object sender, EventArgs e)
        {
            Int16 seType = (short)StackElementTypes.Race_Board_3_Way;
            string seDescription = "Race Board (3-Way)";
            Int16 seDataType = (int)DataTypes.Race_Boards;
            if (tabIndex == 4)
                seDataType = (int)DataTypes.Side_Panel;

            AddRaceBoardToStack(seType, seDescription, seDataType);
        }

        private void btnAddRace4Way_Click(object sender, EventArgs e)
        {
            Int16 seType = (short)StackElementTypes.Race_Board_4_Way;
            string seDescription = "Race Board (4-Way)";
            Int16 seDataType = (int)DataTypes.Race_Boards;

            AddRaceBoardToStack(seType, seDescription, seDataType);
        }

        // Generic method to add a race board to a stack
        private void AddRaceBoardToStack(Int16 stackElementType, string stackElementDescription, Int16 stackElementDataType)
        {
            if (stackLocked == false || autoCalledRacesActive)
            {
                try
                {
                    
                    // Instantiate new stack element model
                    StackElementModel newStackElement = new StackElementModel();
                    AvailableRaceModel selectedRace = new AvailableRaceModel();
                    int currentRaceIndex = 0;

                    //Get the selected race list object
                    if (tabIndex == 0)
                    {
                       currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                       selectedRace = availableRacesCollection.GetRace(availableRaces, currentRaceIndex);
                    }
                    else if (tabIndex == 4)
                    {
                        currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex;
                        selectedRace = availableRacesCollection.GetRace(availableRacesSP, currentRaceIndex);
                    }

                    Int32 stackID = 0;
                    newStackElement.fkey_StackID = stackID;
                    newStackElement.Stack_Element_ID = stackElements.Count;
                    newStackElement.Stack_Element_Type = stackElementType;
                    newStackElement.Stack_Element_Data_Type = stackElementDataType;
                    newStackElement.Stack_Element_Description = stackElementDescription;

                    // Get the template ID for the specified element type & concept ID
                    //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, stackElementType);

                    newStackElement.Election_Type = selectedRace.Election_Type;
                    newStackElement.Office_Code = selectedRace.Race_Office;
                    newStackElement.State_Number = selectedRace.State_Number;
                    newStackElement.State_Mnemonic = selectedRace.State_Mnemonic;
                    newStackElement.State_Name = selectedRace.State_Name;
                    newStackElement.CD = selectedRace.CD;
                    newStackElement.County_Number = 0;
                    newStackElement.County_Name = "";
                    newStackElement.Listbox_Description = selectedRace.Race_Description;

                    string party = "Rep";
                    if (selectedRace.Party == "D")
                        party = "Dem";

                    if (electionMode == "Primary")
                        newStackElement.Listbox_Description = $"{selectedRace.Election_Type} {party}  {selectedRace.Race_Description}";
                    else
                        newStackElement.Listbox_Description = $"{selectedRace.Race_Description}";


                    // Specific to race boards
                    newStackElement.Race_ID = selectedRace.Race_ID;
                    newStackElement.Race_Office = selectedRace.Race_Office;
                    newStackElement.Race_CandidateID_1 = 0;
                    newStackElement.Race_CandidateID_2 = 0;
                    newStackElement.Race_CandidateID_3 = 0;
                    newStackElement.Race_CandidateID_4 = 0;
                    newStackElement.Race_PollClosingTime = selectedRace.Race_PollClosingTime;
                    newStackElement.Race_UseAPRaceCall = selectedRace.Race_UseAPRaceCall;

                    //Specific to exit polls - set to default values
                    newStackElement.VA_Data_ID = string.Empty;
                    newStackElement.VA_Title = string.Empty;
                    newStackElement.VA_Type = string.Empty;
                    newStackElement.VA_Map_Color = string.Empty;
                    newStackElement.VA_Map_ColorNum = 0;

                    // Add element
                    stackElementsCollection.AppendStackElement(newStackElement);
                    // Update stack entries count label
                    txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("Exception occurred while trying to add board to stack: " + ex.Message);
                    //log.Debug("Exception occurred while trying to add board to stack", ex);
                }
            }
        }

        private void AddNPVBoardToStack()
        {
            if (stackLocked == false || autoCalledRacesActive)
            {
                try
                {
                    // Instantiate new stack element model
                    StackElementModel newStackElement = new StackElementModel();

                    //Get the selected race list object
                    //int currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                    //AvailableRaceModel selectedRace = availableRacesCollection.GetRace(availableRaces, currentRaceIndex);

                    //Int32 stackID = 0;
                    //newStackElement.fkey_StackID = stackID;
                    //newStackElement.Stack_Element_ID = stackElements.Count;
                    //newStackElement.Stack_Element_Type = stackElementType;
                    //newStackElement.Stack_Element_Data_Type = stackElementDataType;
                    //newStackElement.Stack_Element_Description = stackElementDescription;

                    Int32 stackID = 0;
                    newStackElement.fkey_StackID = stackID;
                    newStackElement.Stack_Element_ID = stackElements.Count;
                    newStackElement.Stack_Element_Type = (int)StackElementTypes.National_Popular_Vote;
                    newStackElement.Stack_Element_Data_Type = (int)DataTypes.National_Popular_Vote;
                    newStackElement.Stack_Element_Description = "NPV";


                    // Get the template ID for the specified element type & concept ID
                    //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, 3);

                    //newStackElement.Election_Type = selectedRace.Election_Type;
                    //newStackElement.Office_Code = selectedRace.Race_Office;
                    //newStackElement.State_Number = selectedRace.State_Number;
                    //newStackElement.State_Mnemonic = selectedRace.State_Mnemonic;
                    //newStackElement.State_Name = selectedRace.State_Name;
                    //newStackElement.CD = selectedRace.CD;
                    //newStackElement.County_Number = 0;
                    //newStackElement.County_Name = "";
                    //newStackElement.Listbox_Description = selectedRace.Race_Description;

                    newStackElement.Election_Type = "G";
                    newStackElement.Office_Code = "P";
                    newStackElement.State_Number = 0;
                    newStackElement.State_Mnemonic = "US";
                    newStackElement.State_Name = "UNITED STATES";
                    newStackElement.CD = 0;
                    newStackElement.County_Number = 0;
                    newStackElement.County_Name = "";
                    newStackElement.Listbox_Description = "NATIONAL POPULAR VOTE";

                    //string party = "Rep";
                    //if (selectedRace.Party == "D")
                    //    party = "Dem";
                    string party = "";

                    //if (electionMode == "Primary")
                    //    newStackElement.Listbox_Description = $"{selectedRace.Election_Type} {party}  {selectedRace.Race_Description}";
                    //else
                    //    newStackElement.Listbox_Description = $"{selectedRace.Race_Description}";

                    
                    // Specific to race boards
                    //newStackElement.Race_ID = selectedRace.Race_ID;
                    //newStackElement.Race_Office = selectedRace.Race_Office;
                    //newStackElement.Race_PollClosingTime = selectedRace.Race_PollClosingTime;
                    //newStackElement.Race_UseAPRaceCall = selectedRace.Race_UseAPRaceCall;

                    newStackElement.Race_ID = 1;
                    newStackElement.Race_Office = "P";
                    newStackElement.Race_PollClosingTime = new DateTime(2020,11,02,21,00,00);
                    newStackElement.Race_UseAPRaceCall = false;

                    newStackElement.Race_CandidateID_1 = 0;
                    newStackElement.Race_CandidateID_2 = 0;
                    newStackElement.Race_CandidateID_3 = 0;
                    newStackElement.Race_CandidateID_4 = 0;

                    //Specific to exit polls - set to default values
                    newStackElement.VA_Data_ID = string.Empty;
                    newStackElement.VA_Title = string.Empty;
                    newStackElement.VA_Type = string.Empty;
                    newStackElement.VA_Map_Color = string.Empty;
                    newStackElement.VA_Map_ColorNum = 0;

                    // Add element
                    stackElementsCollection.AppendStackElement(newStackElement);
                    // Update stack entries count label
                    txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("Exception occurred while trying to add board to stack: " + ex.Message);
                    //log.Debug("Exception occurred while trying to add board to stack", ex);
                }
            }
        }

        private void AddCountyBoardToStack(Int16 stackElementType, string stackElementDescription, Int16 stackElementDataType, int countyID, string countyName)
        {
            if (stackLocked == false || autoCalledRacesActive)
            {
                try
                {
                    // Instantiate new stack element model
                    StackElementModel newStackElement = new StackElementModel();

                    //Get the selected race list object
                    
                    AvailableRaceModel selectedRace = new AvailableRaceModel();
                    int currentRaceIndex = 0;

                    //Get the selected race list object
                    if (tabIndex == 0)
                    {
                        currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                        selectedRace = availableRacesCollection.GetRace(availableRaces, currentRaceIndex);
                    }
                    else if (tabIndex == 4)
                    {
                        currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex;
                        selectedRace = availableRacesCollection.GetRace(availableRacesSP, currentRaceIndex);
                    }


                    Int32 stackID = 0;
                    newStackElement.fkey_StackID = stackID;
                    newStackElement.Stack_Element_ID = stackElements.Count;
                    newStackElement.Stack_Element_Type = stackElementType;
                    newStackElement.Stack_Element_Data_Type = stackElementDataType;
                    newStackElement.Stack_Element_Description = stackElementDescription;

                    // Get the template ID for the specified element type & concept ID
                    //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, stackElementType);

                    newStackElement.Election_Type = selectedRace.Election_Type;
                    newStackElement.Office_Code = selectedRace.Race_Office;
                    newStackElement.State_Number = selectedRace.State_Number;
                    newStackElement.State_Mnemonic = selectedRace.State_Mnemonic;
                    newStackElement.State_Name = selectedRace.State_Name;
                    newStackElement.CD = selectedRace.CD;
                    newStackElement.County_Number = countyID + 1;
                    newStackElement.County_Name = countyName;

                    string party = "Rep";
                    if (selectedRace.Party == "D")
                        party = "Dem";

                    if (electionMode == "Primary")
                        newStackElement.Listbox_Description = $"{selectedRace.Election_Type} {party}  {countyName} {selectedRace.Race_Description}";
                    else
                        newStackElement.Listbox_Description = $"{countyName} {selectedRace.Race_Description}";

                    // Specific to race boards
                    newStackElement.Race_ID = selectedRace.Race_ID;
                    newStackElement.Race_Office = selectedRace.Race_Office;
                    newStackElement.Race_CandidateID_1 = 0;
                    newStackElement.Race_CandidateID_2 = 0;
                    newStackElement.Race_CandidateID_3 = 0;
                    newStackElement.Race_CandidateID_4 = 0;
                    newStackElement.Race_PollClosingTime = selectedRace.Race_PollClosingTime;
                    newStackElement.Race_UseAPRaceCall = selectedRace.Race_UseAPRaceCall;

                    //Specific to exit polls - set to default values
                    newStackElement.VA_Data_ID = string.Empty;
                    newStackElement.VA_Title = string.Empty;
                    newStackElement.VA_Type = string.Empty;
                    newStackElement.VA_Map_Color = string.Empty;
                    newStackElement.VA_Map_ColorNum = 0;

                    // Add element
                    stackElementsCollection.AppendStackElement(newStackElement);
                    // Update stack entries count label
                    txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("Exception occurred while trying to add board to stack: " + ex.Message);
                    //log.Debug("Exception occurred while trying to add board to stack", ex);
                }
            }
        }



        private void btnAddAll_Click(object sender, EventArgs e)
        {
            if (stackLocked == false)
            {
                AddAll();
                
            }
        }

        public void AddAll()
        {
            Int16 seType = (short)StackElementTypes.Race_Board_2_Way;
            string seDescription = "Race Board (2-Way)";
            Int16 seDataType = (int)DataTypes.Race_Boards;

            Int16 i = 0;
            foreach (DataGridViewRow rowNum in availableRacesGrid.Rows)
            {
                availableRacesGrid.CurrentCell = availableRacesGrid.Rows[i].Cells[0];
                AddRaceBoardToStack(seType, seDescription, seDataType);
                i++;
            }
        }

        
        #endregion

        #region UI widget data validation methods
        /// <summary>
        /// UI widget data validation methods
        /// </summary>
        // Handler for key down event for Stack ID text box (to limit valid keys to number keys)
        private void txtStackID_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }

        // Handler for key press event for Stack ID text box
        private void txtStackID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event. 
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        // Method to handle function keys for race boards
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (tabIndex == 0 && rbPresident.Enabled)
                        rbPresident.Checked = true;
                    if (tabIndex == 4 && rbPresidentSP.Enabled)
                        rbPresidentSP.Checked = true;
                    break;
                case Keys.F2:
                    if (tabIndex == 0 && rbSenate.Enabled)
                        rbSenate.Checked = true;
                    if (tabIndex == 4 && rbSenateSP.Enabled)
                        rbSenateSP.Checked = true;
                    break;
                case Keys.F3:
                    if (tabIndex == 0 && rbHouse.Enabled)
                        rbHouse.Checked = true;
                    if (tabIndex == 4 && rbHouseSP.Enabled)
                        rbHouseSP.Checked = true;
                    break;
                case Keys.F4:
                    if (tabIndex == 0 && rbGovernor.Enabled)
                        rbGovernor.Checked = true;
                    if (tabIndex == 4 && rbGovernorSP.Enabled)
                        rbGovernorSP.Checked = true;
                    break;
                case Keys.F5:
                    if (tabIndex == 0 && rbShowAll.Enabled)
                        rbShowAll.Checked = true;
                    if (tabIndex == 4 && rbShowAllSP.Enabled)
                        rbShowAllSP.Checked = true;
                    break;
                case Keys.F6:
                    if (tabIndex == 0 && rbTCTC.Enabled)
                        rbTCTC.Checked = true;
                    if (tabIndex == 4 && rbTCTCSP.Enabled)
                        rbTCTCSP.Checked = true;
                    break;
                case Keys.F7:
                    if (tabIndex == 0 && rbJustCalled.Enabled)
                        rbJustCalled.Checked = true;
                    if (tabIndex == 4 && rbJustCalledSP.Enabled)
                        rbJustCalledSP.Checked = true;
                    break;
                case Keys.F8:
                    if (tabIndex == 0 && rbCalled.Enabled)
                        rbCalled.Checked = true;
                    if (tabIndex == 4 && rbCalledSP.Enabled)
                        rbCalledSP.Checked = true;
                    break;
                case Keys.F9:
                    if (tabIndex == 0 && rbAll.Enabled)
                        rbAll.Checked = true;
                    if (tabIndex == 4 && rbAllSP.Enabled)
                        rbAllSP.Checked = true;
                    break;
                case Keys.F10:
                    if (tabIndex == 0 && rbBattleground.Enabled)
                        rbBattleground.Checked = true;
                    if (tabIndex == 4 && rbBattlegroundSP.Enabled)
                        rbBattlegroundSP.Checked = true;
                    break;
                case Keys.F11:
                    if (tabIndex == 0 && rbPollClosing.Enabled)
                        rbPollClosing.Checked = true;
                    if (tabIndex == 4 && rbPollClosingSP.Enabled)
                        rbPollClosingSP.Checked = true;
                    break;
                case Keys.F12:
                    if (tabIndex == 0 && rbNone.Enabled)
                        rbNone.Checked = true;
                    if (tabIndex == 4 && rbNoneSP.Enabled)
                        rbNoneSP.Checked = true;
                    break;
                case Keys.A:
                    if (e.Control == true)
                        btnAddAll_Click(sender, e);
                    break;
                // Check for 1 -> 4 way boards (horse-raced)
                case Keys.D1:
                    if (e.Control == true)
                        btnAddRace1Way_Click(sender, e);
                    else if (e.Alt == true)
                        btnSelect1_Click(sender, e);
                    break;
                case Keys.D2:
                    if (e.Control == true)
                        btnAddRace2Way_Click(sender, e);
                    else if (e.Alt == true)
                        btnSelect2_Click(sender, e);
                    break;
                case Keys.D3:
                    if (e.Control == true)
                        btnAddRace3Way_Click(sender, e);
                    else if (e.Alt == true)
                        btnSelect3_Click(sender, e);
                    break;
                case Keys.D4:
                    if (e.Control == true)
                        btnAddRace4Way_Click(sender, e);
                    else if (e.Alt == true)
                        btnSelect4_Click(sender, e);
                    break;
                case Keys.D5:
                    if (e.Control == true)
                        btnAddRace5Way_Click(sender, e);
                    else if (e.Alt == true)
                        btnSelect5_Click(sender, e);
                    break;
                // Stack operations
                case Keys.R:
                    if (e.Control == true)
                        btnLoadStack_Click(sender, e);
                    break;
                case Keys.S:
                    //if (e.Control == true)
                    //btnSaveActivateStack_Click(sender, e);

                    bool alreadyOpen = false;
                    FormCollection fc = Application.OpenForms;
                    int num = fc.Count;
                    for (int i = num; i > 0; i--)
                    {
                        Form frm = new Form();
                        frm = fc[i - 1];
                        if (frm.Name == "FrmSaveStack")
                            alreadyOpen = true;
                    }

                    if (e.Control == true && !alreadyOpen)
                        btnSaveStack_Click(sender, e);
                    break;
                case Keys.D:
                    if (e.Control == true)
                        btnDeleteStackElement_Click(sender, e);
                    break;
                case Keys.C:
                    if (e.Control == true)
                        btnClearStack_Click(sender, e);
                    break;
                case Keys.Space:
                    if (e.Control == true)
                        btnTake_Click(sender, e);
                    break;
                case Keys.O:
                    if (e.Control == true)
                        btnSaveStack_Click(sender, e);
                    break;
                case Keys.L:
                    if (e.Control == true)
                        btnLock_Click(sender, e);
                    break;
                case Keys.U:
                    if (e.Control == true)
                        btnUnlock_Click(sender, e);
                    break;
                case Keys.Up:
                    ArrowUp();
                    break;
                case Keys.Down:
                    ArrowDn();
                    break;
                    //default:
                    //    rbShowAll.Checked = true;
                    //    break;
            }
        }

        // Method to handle key press events (letters a-z as shortcuts to states in available races list)
        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for alpha character
            if (((e.KeyChar >= 'a') && (e.KeyChar <= 'z')) || ((e.KeyChar >= 'A') && (e.KeyChar <= 'Z')))
            {
                // Check for available races
                if (availableRaces.Count > 0)
                {
                    Boolean foundMatch = false;
                    Int16 searchIndex = 0;

                    do
                    {
                        AvailableRaceModel availableRace = null;
                        availableRace = availableRaces[searchIndex];
                        String stateName = availableRace.State_Mnemonic.Trim();
                        if ((stateName[0] == e.KeyChar) || (stateName[0] == Char.ToUpper(e.KeyChar)))
                        {
                            foundMatch = true;

                            availableRacesGrid.FirstDisplayedScrollingRowIndex = searchIndex;
                            availableRacesGrid.Refresh();

                            availableRacesGrid.CurrentCell = availableRacesGrid.Rows[searchIndex].Cells[0];

                            availableRacesGrid.Rows[searchIndex].Selected = true;
                        }
                        searchIndex++;
                    }
                    while ((foundMatch == false) && (searchIndex < availableRaces.Count));
                }
            }
            else if (e.KeyChar == ' ')
                btnTake_Click(sender, e);

        }

        #endregion

        #region Stack manipulation methods
        /// <summary>
        /// Stack manipulation methods
        /// </summary>
        /// 
        // 10/08/2018 Handler for change to graphics concept; previously, was not being set when concept was changed via drop-down
        private void cbGraphicConcept_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            conceptID = graphicsConceptTypes[cbGraphicConcept.SelectedIndex].ConceptID;
            conceptName = graphicsConceptTypes[cbGraphicConcept.SelectedIndex].ConceptName;
        }

        // Handler for insert button
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (stackLocked == false)
            {
                if (stackGrid.CurrentCell.RowIndex < 0)
                {
                    insertPoint = 0;
                }
                else
                {
                    insertPoint = stackGrid.CurrentCell.RowIndex;
                }
                // Set flag
                insertNext = true;
            }
        }
        
        // Handler for Save Stack button
        private void btnSaveStack_Click(object sender, EventArgs e)
        {
            try
            {
                if (stackElements.Count > 0)
                {

                    DialogResult dr = new DialogResult();
                    //if (stackType == 50)
                    //stackType = 10;

                    FrmSaveStack saveStack = new FrmSaveStack(stackID, stackDescription, builderOnlyMode, stackType, MindyMode, stackTypeOffset, StacksDBConnectionString);

                    dr = saveStack.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        // Instantiate a new top-level stack metadata model
                        StackModel stackMetadata = new StackModel();

                        stackID = saveStack.StackId;
                        stackType = saveStack.stackType;
                        stackDescription = saveStack.StackDescription;
                        bool multiplay = saveStack.multiplayMode;
                        stackMetadata.ixStackID = stackID;
                        stackMetadata.StackName = stackDescription;
                        stackMetadata.StackType = (short)stackType;

                        log.Debug($"\n ***** Save start - StackID: {stackID}   Stack Type: {stackType}   Stack Description: {stackDescription}");

                        if (builderOnlyMode)
                        {
                            stackMetadata.ShowName = currentShowName;
                            stackMetadata.ConceptID = conceptID;
                            stackMetadata.ConceptName = conceptName;
                        }
                        else
                        {
                            stackMetadata.ShowName = "N/A";
                            stackMetadata.ConceptID = -1;
                            stackMetadata.ConceptName = "N/A";
                        }


                        StacksCollection stacksCollection = new StacksCollection();

                        /*
                        if (multiplay)
                        {
                            stacksCollection.MainDBConnectionString = GraphicsDBConnectionString;
                            stackElementsCollection.MainDBConnectionString = GraphicsDBConnectionString;
                        }
                        else
                        {
                            stacksCollection.MainDBConnectionString = stacksDB;
                            stackElementsCollection.MainDBConnectionString = stacksDB;
                        }
                        */
                        stacksCollection.MainDBConnectionString = stacksDB;
                        stackElementsCollection.MainDBConnectionString = stacksDB;

                        stackMetadata.Notes = "Not currently used";
                        stacksCollection.SaveStack(stackMetadata);

                        // Save out stack elements; specify stack ID, and set flag to delete existing elements before adding
                        //stackElementsCollection.MainDBConnectionString = stacksDB;
                        stackElementsCollection.SaveStackElementsCollection(stackMetadata.ixStackID, true);
                        

                        // Update stack entries count label & name label
                        txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                        txtStackName.Text = stackDescription;
                    }
                }

                // Set status strip
                toolStripStatusLabel.BackColor = System.Drawing.Color.SpringGreen;
                //toolStripStatusLabel.Text = "Status Logging Message: Stack successfully saved out to database";
                toolStripStatusLabel.Text = String.Format("Status Logging Message: Stack {0} saved out to database", stackID);


            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Handler for move stack element up in stack order
        private void btnStackElementUp_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (stackElementsCollection.CollectionCount > 0)
                {
                    int currentRowIndex = stackGrid.CurrentRow.Index;
                    stackElementsCollection.MoveStackElementUp((short)stackGrid.CurrentRow.Index);
                    if (stackGrid.CurrentRow.Index > 0)
                    {
                        stackGrid.Rows[currentRowIndex - 1].Selected = true;
                        stackGrid.CurrentCell = stackGrid[0, currentRowIndex - 1];
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Handler for move stack element down in stack order
        private void btnStackElementDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (stackElementsCollection.CollectionCount > 0)
                {
                    stackElementsCollection.MoveStackElementDown((short)stackGrid.CurrentRow.Index);
                    if (stackGrid.CurrentRow.Index < stackGrid.RowCount - 1)
                    {
                        stackGrid.Rows[stackGrid.CurrentRow.Index + 1].Selected = true;
                        stackGrid.CurrentCell = stackGrid[0, stackGrid.CurrentRow.Index + 1];
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Handler for Clear Stack button
        private void btnClearStack_Click(object sender, EventArgs e)
        {
            try
            {
                if (stackElements.Count > 0)
                {
                    DialogResult result1 = MessageBox.Show("Are you sure you want to clear all entries from the stack?", "Confirmation",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 != DialogResult.Yes)
                    {
                        return;
                    }

                    // Operator didn't dump out, so proceed
                    if (stackGrid.RowCount > 0)
                    {
                        //Clear the collection
                        stackElements.Clear();
                    }

                    // Clear out current stack settings
                    stackID = -1;
                    txtStackName.Text = "None Selected";

                    // Update stack entries count label
                    txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Handler for delete stack element button
        private void btnDeleteStackElement_Click(object sender, EventArgs e)
        {
            try
            {
                if (stackGrid.RowCount > 0)
                {
                    //Get the delete point
                    int currentStackIndex = stackGrid.CurrentCell.RowIndex;

                    //Delete the item from the collection
                    stackElements.RemoveAt(currentStackIndex);
                }

                // Update stack entries count label
                txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);

                // Clear out current settings if no entries left in stack
                if (stackElements.Count == 0)
                {
                    stackID = -1;
                    txtStackName.Text = "None Selected";
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // Handler for Load Stack button
        private void btnLoadStack_Click(object sender, EventArgs e)
        {
            LoadSelectedStack();
        }
        private void availableStacksGrid_DoubleClick(object sender, EventArgs e)
        {
            LoadSelectedStack();
        }
        // Method to load stack
        private void LoadSelectedStack()
        {
            try
            {
                //Refresh the list of available stacks
                Int32 stackIndex = 0;

                // Setup dialog to load stack
                DialogResult dr = new DialogResult();

                //if (stackType == 50)
                //stackType = 10;

                //frmLoadStack loadStack = new frmLoadStack(builderOnlyMode, stackType);
                //frmLoadStack loadStack = new frmLoadStack(builderOnlyMode, stackType, MindyMode, stackTypeOffset, GraphicsDBConnectionString, StacksDBConnectionString);
                frmLoadStack loadStack = new frmLoadStack(builderOnlyMode, stackType, MindyMode, stackTypeOffset, StacksDBConnectionString);

                loadStack.EnableShowControls = enableShowSelectControls;

                //RefreshStacksList();

                dr = loadStack.ShowDialog();

                List<int> deleteStacks = new List<int>();

                // Process result from dialog
                // Check for Load Stack operation
                if (dr == DialogResult.OK)
                {
                    // Set candidateID's
                    stackIndex = loadStack.StackIndex;
                    stackID = loadStack.StackID;
                    bool multiplay = loadStack.multiplayMode;
                    stackDescription = loadStack.StackDesc;
                    stacks = loadStack.stacks;
                    deleteStacks = loadStack.stacksSelected;

                    log.Debug($"\n ***** Load stack start:  StackId: {stackID}   Stack description: {stackDescription}");

                    // Clear the collection
                    stackElements.Clear();

                    // Get the stack ID and load the selected collection
                    //t currentStackIndex = availableStacksGrid.CurrentCell.RowIndex;
                    int currentStackIndex = stackIndex;
                    StacksCollection stacksCollection = new StacksCollection();

                    /*
                    if (multiplay)
                    {
                        stacksCollection.MainDBConnectionString = GraphicsDBConnectionString;
                        stackElementsCollection.MainDBConnectionString = GraphicsDBConnectionString;
                    }
                    else
                    {
                        stacksCollection.MainDBConnectionString = stacksDB;
                        stackElementsCollection.MainDBConnectionString = stacksDB;
                    }
                    */

                    stacksCollection.MainDBConnectionString = stacksDB;
                    stackElementsCollection.MainDBConnectionString = stacksDB;

                    StackModel selectedStack = stacksCollection.GetStackMetadata(stacks, currentStackIndex);

                    //log.Debug($"StackElemants  DBconn: {stackElementsCollection.MainDBConnectionString} ");

                    // Load the collection
                    stackElementsCollection.GetStackElementsCollection(selectedStack.ixStackID);
                    // Update stack entries count label
                    txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);

                    // 10/08/2018 Set graphics concept in drop-down
                    if (builderOnlyMode)
                        cbGraphicConcept.SelectedIndex = selectedStack.ConceptID - 1;

                    //txtStackName.Text = selectedStack.StackName + " [ID: " + Convert.ToString(selectedStack.ixStackID) + "]";
                    txtStackName.Text = selectedStack.StackName;

                    switch (tabIndex)
                    {
                        case 0:
                            stackNameRB = selectedStack.StackName;
                            break;
                        case 1:
                            stackNameVA = selectedStack.StackName;
                            break;
                        case 2:
                            stackNameBOP = selectedStack.StackName;
                            break;
                        case 3:
                            stackNameREF = selectedStack.StackName;
                            break;
                        case 4:
                            stackNameSP = selectedStack.StackName;
                            break;
                        case 5:
                            stackNameMAP = selectedStack.StackName;
                            break;
                    }


                }
                // Check for Delete Stack operation
                else if (dr == DialogResult.Ignore)
                {
                    Boolean okToGo = true;

                    stackIndex = loadStack.StackIndex;
                    stacks = loadStack.stacks;
                    bool multiplay = loadStack.multiplayMode;
                    if (stacks.Count > 0)
                    {
                        int currentStackIndex;

                        for (int i = 0; i < loadStack.stacksSelected.Count; i++)
                        {

                            StacksCollection stacksCollection = new StacksCollection();

                            /*
                            if (multiplay)
                            {
                                stacksCollection.MainDBConnectionString = GraphicsDBConnectionString;
                                stackElementsCollection.MainDBConnectionString = GraphicsDBConnectionString;
                            }
                            else
                            {
                                stacksCollection.MainDBConnectionString = stacksDB;
                                stackElementsCollection.MainDBConnectionString = stacksDB;
                            }
                            */

                            stacksCollection.MainDBConnectionString = stacksDB;
                            stackElementsCollection.MainDBConnectionString = stacksDB;


                            // Get the stack index from the dialog
                            //int currentStackIndex = stackIndex;
                            currentStackIndex = loadStack.stacksSelected[i];


                            // Get the metadata for the stack
                            StackModel selectedStack = stacksCollection.GetStackMetadata(stacks, currentStackIndex);

                            // Check if operator is trying the delete the currently loaded stack and prompt
                            if (selectedStack.StackName == stackDescription)
                            {
                                DialogResult result1 =
                                    MessageBox.Show(
                                        "The stack you are deleting is currently loaded. If you proceed, the stack will also be cleared. Are you sure you want to proceed?",
                                        "Confirmation",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result1 == DialogResult.Yes)
                                {
                                    // Clear the stack elements collection
                                    if (stackGrid.RowCount > 0)
                                    {
                                        //Clear the collection
                                        stackElements.Clear();
                                    }

                                    // Clear out current stack settings
                                    stackID = -1;
                                    txtStackName.Text = "None Selected";

                                    // Update stack entries count label
                                    txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                                }
                                else okToGo = false;
                            }

                            // Proceed as long as operater did not opt out
                            if (okToGo)
                            {
                                stacksCollection.DeleteStack(selectedStack.ixStackID);

                                // only required if stack is a multiplay stack
                                if (multiplay)
                                {

                                    //New thread
                                    Task.Run(() =>
                                    {

                                    // Delete from MSE
                                    string groupSelfLink = string.Empty;

                                    // Get playlists directory URI based on current show
                                    string showPlaylistsDirectoryURI = show.GetPlaylistDirectoryFromShow(topLevelShowsDirectoryURI + "/", currentShowName);

                                    // Check for a playlist (group) in the VDOM with the specified name & return the Alt link
                                    // Delete the group so it can be re-created
                                    string playlistDownLink = playlist.GetPlaylistDownLink(showPlaylistsDirectoryURI, currentPlaylistName);
                                        if (playlistDownLink != string.Empty)
                                        {
                                        // Get the self link to the specified group
                                        groupSelfLink = group.GetGroupSelfLink(playlistDownLink, selectedStack.StackName);

                                        // Delete the group if it exists
                                        if (groupSelfLink != string.Empty)
                                            {
                                                group.DeleteGroup(groupSelfLink);
                                            }
                                        }

                                    });


                                }
                            }
                        }
                    }
                }
                // Check for Activate Stack operation
                else if (dr == DialogResult.Yes)
                {
                    stackIndex = loadStack.StackIndex;
                    if (stacks.Count > 0)
                    {
                        stackIndex = loadStack.StackIndex;
                        Double stackID = loadStack.StackID;
                        String stackDescription = loadStack.StackDesc;

                        // Clear the collection
                        activateStackElements.Clear();

                        // Get the stack ID and load the selected collection
                        StackModel selectedActivateStack = stacksCollection.GetStackMetadata(stacks, stackIndex);

                        // Load the collection
                        activateStackElementsCollection.GetStackElementsCollection(selectedActivateStack.ixStackID);
                        activateStackElements = activateStackElementsCollection.stackElements;

                        // Activate the specified stack
                        ActivateStack(stackID, stackDescription, activateStackElementsCollection, activateStackElements);
                    }
                }
                //Refresh the list of available stacks
                RefreshStacksList();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred during stack load: " + ex.Message);
                //log.Debug("frmMain Exception occurred during stack load", ex);
            }
        }

        // Method for handing click on Save & Activate stack button
        private void btnSaveActivateStack_Click(object sender, EventArgs e)
        {
            try
            {

                if (builderOnlyMode)
                {
                    if (stackElements.Count == 0)
                    {
                        MessageBox.Show("There are no elements specified in the stack to save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    // Added for 2018 Mid-Terms - new concepts for 6-Way & 8-Way boards; need to check for correct number of boards
                    // Check for 6-Way boards
                    else if ((cbGraphicConcept.SelectedIndex == (short)GraphicsConcepts.Six_Way - 1) && (stackElements.Count != 6))
                    {
                        MessageBox.Show("There must be exactly six (6) elements in the stack in order to save it for this graphics concept. " +
                            "Either set the number of boards to six (6), or choose another graphics concept from the drop-down menu.",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Check for 8-Way boards
                    else if ((cbGraphicConcept.SelectedIndex == (short)GraphicsConcepts.Eight_Way - 1) && (stackElements.Count != 8))
                    {
                        MessageBox.Show("There must be exactly eight (8) elements in the stack in order to save it for this graphics concept " +
                            "Either set the number of boards to six (6), or choose another graphics concept from the drop-down menu.",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else if (stackElements.Count > 0)
                    {
                        // Only display dialog if checkbox for prompt for info is checked
                        if (cbPromptForInfo.Checked == true)
                        {
                            DialogResult dr = new DialogResult();
                            //FrmSaveStack saveStack = new FrmSaveStack(stackID, stackDescription, builderOnlyMode, stackType, MindyMode, stackTypeOffset, GraphicsDBConnectionString, StacksDBConnectionString);
                            FrmSaveStack saveStack = new FrmSaveStack(stackID, stackDescription, builderOnlyMode, stackType, MindyMode, stackTypeOffset, StacksDBConnectionString);
                            //FrmSaveStack saveStack = new FrmSaveStack();

                            saveStack.EnableShowControls = enableShowSelectControls;

                            dr = saveStack.ShowDialog();

                            // Will only get here if Prompt for Info checkbox is checked
                            if (dr == DialogResult.OK)
                            {

                                // Instantiate a new top-level stack metadata model
                                StackModel stackMetadata = new StackModel();

                                stackID = saveStack.StackId;
                                stackDescription = saveStack.StackDescription;
                                stackMetadata.ixStackID = stackID;
                                stackMetadata.StackName = stackDescription;

                                stackMetadata.StackType = 0;
                                stackMetadata.ShowName = currentShowName;
                                stackMetadata.ConceptID = conceptID;
                                stackMetadata.ConceptName = conceptName;
                                stackMetadata.Notes = "Not currently used";

                                // Save out the top level metadata for the stack
                                stacksCollection.SaveStack(stackMetadata);

                                // Save out stack elements; specify stack ID, and set flag to delete existing elements before adding
                                stackElementsCollection.SaveStackElementsCollection(stackMetadata.ixStackID, true);

                                // Update stack entries count label & name label
                                txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                                //txtStackName.Text = stackDescription + " [ID: " + Convert.ToString(stackID) + "]";
                                txtStackName.Text = stackDescription;

                                // Call method to save stack out to MSE
                                ActivateStack(stackID, stackDescription, stackElementsCollection, stackElements);
                            }
                        }
                        else if ((cbPromptForInfo.Checked == false) && (stackID > 0))
                        {
                            // Instantiate a new top-level stack metadata model
                            StackModel stackMetadata = new StackModel();

                            stackMetadata.ixStackID = stackID;
                            stackMetadata.StackName = stackDescription;

                            stackMetadata.StackType = 0;
                            stackMetadata.ShowName = currentShowName;
                            stackMetadata.ConceptID = conceptID;
                            stackMetadata.ConceptName = conceptName;
                            stackMetadata.Notes = "Not currently used";

                            // Save out the top level metadata for the stack
                            stacksCollection.SaveStack(stackMetadata);

                            // Save out stack elements; specify stack ID, and set flag to delete existing elements before adding
                            stackElementsCollection.SaveStackElementsCollection(stackMetadata.ixStackID, true);

                            // Update stack entries count label & name label
                            //txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                            //txtStackName.Text = stackDescription + " [ID: " + Convert.ToString(stackID) + "]";

                            // Call method to save stack out to MSE
                            ActivateStack(stackID, stackDescription, stackElementsCollection, stackElements);
                        }

                        // Set status strip
                        toolStripStatusLabel.BackColor = System.Drawing.Color.SpringGreen;
                        // toolStripStatusLabel.Text = "Status Logging Message: Stack saved out to database and activated";
                        toolStripStatusLabel.Text = String.Format("Status Logging Message: Stack \"{0}\" saved out to database and activated", stackDescription);
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Debug("Exception occurred", ex);
                log.Error("Exception occurred while trying to save and activate group: " + ex.Message);
            }
        }

        /// <summary>
        /// Mathod to activate the specified stack (can be working stack or selected stack)
        /// </summary>
        private void ActivateStack(Double stack_ID, string stack_Description, StackElementsCollection _stackElementsCollection, BindingList<StackElementModel> _stackElements)
        {
            try
            {
                // MSE OPERATION
                string groupSelfLink = string.Empty;

                // Get playlists directory URI based on current show
                string showPlaylistsDirectoryURI = show.GetPlaylistDirectoryFromShow(topLevelShowsDirectoryURI, currentShowName);

                // Iterate through the races in the stack to build the preview collection, then call methods to create group containing elements
                // Clear out the existing race preview collection
                racePreview.Clear();

                string raceBoardTypeDescription = string.Empty;
                Int16 candidatesToReturn = 0;
                Int16 dataType = 0;
                string BOPHeader = String.Empty;

                // Build the Race Preview collection - contains strings for each race in the group/stack
                // Iterate through each race in the stack to build the race preview command strings collection                    
                for (int i = 0; i < _stackElements.Count; ++i)
                {
                    switch (_stackElements[i].Stack_Element_Type)
                    {
                        case (Int16)StackElementTypes.Race_Board_All_Way:
                            raceBoardTypeDescription = "All Way Board";
                            candidatesToReturn = 0;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;
                        
                        case (Int16)StackElementTypes.Race_Board_1_Way:
                            raceBoardTypeDescription = "1-Way Board";
                            candidatesToReturn = 1;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Race_Board_1_Way_Select:
                            raceBoardTypeDescription = "1-Way Select Board";
                            candidatesToReturn = 1;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Race_Board_2_Way:
                            raceBoardTypeDescription = "2-Way Board";
                            candidatesToReturn = 2;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Race_Board_2_Way_Select:
                            raceBoardTypeDescription = "2-Way Select Board";
                            candidatesToReturn = 2;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Race_Board_3_Way:
                            raceBoardTypeDescription = "3-Way Board";
                            candidatesToReturn = 3;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Race_Board_3_Way_Select:
                            raceBoardTypeDescription = "3-Way Select Board";
                            candidatesToReturn = 3;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Race_Board_4_Way:
                            raceBoardTypeDescription = "4-Way Board";
                            candidatesToReturn = 4;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Race_Board_4_Way_Select:
                            raceBoardTypeDescription = "4-Way Select Board";
                            candidatesToReturn = 4;
                            dataType = (Int16)DataTypes.Race_Boards;
                            break;

                        case (Int16)StackElementTypes.Exit_Poll_Full_Screen:
                            raceBoardTypeDescription = "Voter_Analysis";
                            dataType = (Int16)DataTypes.Voter_Analysis;
                            break;

                        case (Int16)StackElementTypes.Balance_of_Power_House_Current:
                            raceBoardTypeDescription = "Balance of Power - House: Current";
                            BOPHeader = "HOUSE^CURRENT";
                            dataType = (Int16)DataTypes.Balance_of_Power;
                            break;

                        case (Int16)StackElementTypes.Balance_of_Power_Senate_Current:
                            raceBoardTypeDescription = "Balance of Power - Senate: Current";
                            BOPHeader = "SENATE^CURRENT";
                            dataType = (Int16)DataTypes.Balance_of_Power;
                            break;

                        case (Int16)StackElementTypes.Balance_of_Power_House_New:
                            raceBoardTypeDescription = "Balance of Power - House: New";
                            BOPHeader = "HOUSE^NEW";
                            dataType = (Int16)DataTypes.Balance_of_Power;
                            break;

                        case (Int16)StackElementTypes.Balance_of_Power_Senate_New:
                            raceBoardTypeDescription = "Balance of Power - Senate: New";
                            BOPHeader = "SENATE^NEW";
                            dataType = (Int16)DataTypes.Balance_of_Power;
                            break;

                        case (Int16)StackElementTypes.Balance_of_Power_House_Net_Gain:
                            raceBoardTypeDescription = "Balance of Power - House: Net Gain";
                            BOPHeader = "HOUSE^NET GAIN";
                            dataType = (Int16)DataTypes.Balance_of_Power;
                            break;

                        case (Int16)StackElementTypes.Balance_of_Power_Senate_Net_Gain:
                            raceBoardTypeDescription = "Balance of Power - Senate: Net Gain";
                            BOPHeader = "SENATE^NET GAIN";
                            dataType = (Int16)DataTypes.Balance_of_Power;
                            break;

                        case (Int16)StackElementTypes.Referendums:
                            raceBoardTypeDescription = "Referendums";
                            dataType = (Int16)DataTypes.Referendums;
                            break;

                        case (Int16)StackElementTypes.Voter_Analysis_Full_Screen:
                            raceBoardTypeDescription = "Voter Analysis";
                            dataType = (Int16)DataTypes.Voter_Analysis;
                            break;


                    }

                    // Instantiate and set the values of a race preview element
                    RacePreviewModel newRacePreviewElement = new RacePreviewModel();

                    switch (dataType)
                    {
                        case (Int16)DataTypes.Race_Boards:

                            // Request the race data for the element in the stack - updates raceData binding list
                            raceData = GetRaceData(electionMode, _stackElements[i].State_Number, _stackElements[i].Race_Office, _stackElements[i].CD, _stackElements[i].Election_Type, candidatesToReturn, false, 0, 0, 0, 0, 0);

                            // Check for data returned for race
                            if (raceData.Count > 0)
                            {
                                // Instantiate and set the values of a race preview element
                                //RacePreviewModel newRacePreviewElement = new RacePreviewModel();

                                // Set the name of the element for the group
                                newRacePreviewElement.Raceboard_Description = _stackElements[i].Listbox_Description + " - " + raceBoardTypeDescription;
                                //newRacePreviewElement.Raceboard_Description = raceBoardTypeDescription + ": " + stackElements[i].Listbox_Description;

                                // Set FIELD_TYPE value - stack ID plus stack index
                                newRacePreviewElement.Raceboard_Type_Field_Text = stack_ID.ToString() + "|" + i.ToString();

                                // Call method to assemble the race data into the required command string for the raceboards scene
                                newRacePreviewElement.Raceboard_Preview_Field_Text = GetRacePreviewString(_stackElements[i], candidatesToReturn);

                                // Append the preview element to the race preview collection
                                racePreviewCollection.AppendRacePreviewElement(newRacePreviewElement);
                            }
                            break;

                            
                        case (Int16)DataTypes.Voter_Analysis:

                            StackElementModel stElement = new StackElementModel();
                            stElement = _stackElements[i];

                            // Setup the Voter Analysis collection
                            var records = VoterAnalysisDataCollection.GetVoterAnalysisDataCollection(_stackElements[i].VA_Type, _stackElements[i].VA_Data_ID, ElectionsDBConnectionString,0);

                            // Check for data returned for race
                            if (records.Count > 0)
                            {

                                // Set the name of the element for the group
                                newRacePreviewElement.Raceboard_Description = raceBoardTypeDescription + ": " + _stackElements[i].Listbox_Description;

                                // Set FIELD_TYPE value - stack ID plus stack index
                                newRacePreviewElement.Raceboard_Type_Field_Text = stack_ID.ToString() + "|" + i.ToString();

                                // Call method to assemble the race data into the required command string for the raceboards scene
                                newRacePreviewElement.Raceboard_Preview_Field_Text = GetVoterAnalysisPreviewString(records, stElement);

                                // Append the preview element to the race preview collection
                                racePreviewCollection.AppendRacePreviewElement(newRacePreviewElement);
                            }
                            break;
                            

                        case (Int16)DataTypes.Balance_of_Power:


                            // Set the name of the element for the group
                            newRacePreviewElement.Raceboard_Description = raceBoardTypeDescription + ": " + _stackElements[i].Listbox_Description;

                            // Set FIELD_TYPE value - stack ID plus stack index
                            newRacePreviewElement.Raceboard_Type_Field_Text = stack_ID.ToString() + "|" + i;

                            // Call method to assemble the race data into the required command string for the raceboards scene
                            newRacePreviewElement.Raceboard_Preview_Field_Text = GetBOPPreviewString(_stackElements[i], BOPHeader);

                            // Append the preview element to the race preview collection
                            racePreviewCollection.AppendRacePreviewElement(newRacePreviewElement);

                            break;

                        case (Int16)DataTypes.Referendums:

                            // Setup the referendums collection
                            this.referendumsDataCollection = new ReferendumsDataCollection();
                            this.referendumsDataCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
                            referendumsData = referendumsDataCollection.GetReferendumsDataCollection(_stackElements[i].State_Number, _stackElements[i].Race_Office);

                            ReferendumDataModel refData = new ReferendumDataModel();

                            // Check for data returned for race
                            if (referendumsData.Count > 0)
                            {

                                refData = referendumsData[1];

                                // Set the name of the element for the group
                                newRacePreviewElement.Raceboard_Description = raceBoardTypeDescription + ": " + _stackElements[i].Listbox_Description;

                                // Set FIELD_TYPE value - stack ID plus stack index
                                newRacePreviewElement.Raceboard_Type_Field_Text = stack_ID.ToString() + "|" + i.ToString();

                                // Call method to assemble the race data into the required command string for the raceboards scene
                                newRacePreviewElement.Raceboard_Preview_Field_Text = GetReferendumPreviewString(refData);

                                // Append the preview element to the race preview collection
                                racePreviewCollection.AppendRacePreviewElement(newRacePreviewElement);
                            }
                            break;
                    }

                }

                // MSE OPERATION - SAVE OUT THE GROUP W/STACK ELEMENTS
                // Get playlists directory URI based on current show
                showPlaylistsDirectoryURI = show.GetPlaylistDirectoryFromShow(topLevelShowsDirectoryURI, currentShowName);

                // Log if the URI could not be resolved
                if (showPlaylistsDirectoryURI == string.Empty)
                {
                    log.Error("Could not resolve Show Playlist Directory URI");
                    //log.Debug("Could not resolve Show Playlist Directory URI");
                }

                // Get templates directory URI based on current show
                string showTemplatesDirectoryURI = show.GetTemplateCollectionFromShow(topLevelShowsDirectoryURI, currentShowName);

                // Log if the URI could not be resolved
                if (showTemplatesDirectoryURI == string.Empty)
                {
                    log.Error("Could not resolve Show Templates Directory URI");
                    //log.Debug("Could not resolve Show Templates Directory URI");
                }

                // Check for a playlist in the VDOM with the specified name & return the Alt link; if the playlist doesn't exist, create it first
                if (playlist.CheckIfPlaylistExists(showPlaylistsDirectoryURI, currentPlaylistName) == false)
                {
                    playlist.CreatePlaylist(showPlaylistsDirectoryURI, currentPlaylistName);
                }

                // Check for a playlist in the VDOM with the specified name & return the Down link
                // Delete the group so it can be re-created
                string playlistDownLink = playlist.GetPlaylistDownLink(showPlaylistsDirectoryURI, currentPlaylistName);
                if (playlistDownLink != string.Empty)
                {
                    // Get the self link to the specified group
                    groupSelfLink = group.GetGroupSelfLink(playlistDownLink, stack_Description);

                    // Delete the group if it exists
                    if (groupSelfLink != string.Empty)
                    {
                        group.DeleteGroup(groupSelfLink);
                    }

                    // Create the group
                    REST_RESPONSE restResponse = group.CreateGroup(playlistDownLink, stack_Description);

                    // Check for elements in collection and add to group
                    if (racePreview.Count > 0)
                    {
                        // Iterate through each element in the preview collection and add the element to the group
                        for (int i = 0; i < racePreview.Count; ++i)
                        {
                            // Get the element from the collection
                            RacePreviewModel racePreviewElement;
                            racePreviewElement = racePreview[i];

                            // Add the element to the group
                            //Get the info for the current race
                            StackElementModel selectedStackElement = _stackElementsCollection.GetStackElement(_stackElements, i);

                            //Set template ID
                            string templateID = selectedStackElement.Stack_Element_TemplateID;

                            //Set page number
                            string pageNumber = i.ToString();

                            //Gets the URI's for the given show
                            GET_URI getURI = new GET_URI();

                            //Get the show info
                            //Get the URI to the show elements collection
                            elementCollectionURIShow = show.GetElementCollectionFromShow(topLevelShowsDirectoryURI, currentShowName);

                            // Log if the URI could not be resolved
                            if (elementCollectionURIShow == string.Empty)
                            {
                                log.Error("Could not resolve Show Elements Collection URI");
                                //log.Debug("Could not resolve Show Elements Collection URI");
                            }


                            //Get the URI to the show templates collection
                            templateCollectionURIShow = show.GetTemplateCollectionFromShow(topLevelShowsDirectoryURI, currentShowName);

                            // Log if the URI could not be resolved
                            if (templateCollectionURIShow == string.Empty)
                            {
                                log.Error("Could not resolve Show Templates Collection URI");
                                //log.Debug("Could not resolve Show Templates Collection URI");
                            }

                            //Get the URI to the model for the specified template within the specified show
                            templateModel = template.GetTemplateElementModel(templateCollectionURIShow, templateID);

                            // Alert if template model not found
                            if (templateModel == null)
                            {
                                // Log error
                                log.Error("Could not resolve template model - template might not exist");
                                //log.Debug("Could not resolve template model - template might not exist");
                            }

                            //Get the URI to the currently-specified playlist                                
                            elementCollectionURIPlaylist = restResponse.downLink;

                            // Check for element collection URI for the specified playlist
                            if (elementCollectionURIPlaylist == null)
                            {
                                log.Error("Could not resolve URI for specified playlist");
                                //log.Debug("Could not resolve URI for specified playlist");
                            }


                            // Set the data values as name/value pairs
                            // Get the element from the collection
                            racePreviewElement = racePreview[i];

                            // Add the element to the group
                            // NOTE: Currently hard-wired for race boards - will need to be extended to support varying data types
                            Dictionary<string, string> nameValuePairs =
                                new Dictionary<string, string> { { TemplateFieldNames.RaceBoard_Template_Preview_Field, racePreviewElement.Raceboard_Preview_Field_Text },
                                                                         { TemplateFieldNames.RaceBoard_Template_Type_Field, stack_ID.ToString() + "|" + pageNumber } };

                            // Instance the element management class
                            MANAGE_ELEMENTS element = new MANAGE_ELEMENTS();

                            // Create the new element
                            //element.createNewElement(i.ToString() + ": " + racePreviewElement.Raceboard_Description, elementCollectionURIPlaylist, templateModel, nameValuePairs, defaultTrioChannel);
                        }
                    }
                }
                // Log if the URI could not be resolved
                else
                {
                    log.Error("Could not resolve Playlist Down link");
                    //log.Debug("Could not resolve Playlist Down link");
                }
            }

            catch (Exception ex)
            {
                //log.Debug("Exception occurred", ex);
                log.Error("Exception occurred while trying to save and activate group: " + ex.Message);
            }
        }

        private void stackGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //currentRaceIndex = stackGrid.CurrentCell.RowIndex - 1;
            clickIndex = stackGrid.CurrentCell.RowIndex;
            clickFlag = true;
        }

        public void ArrowUp()
        {
            //currentRaceIndex = stackGrid.CurrentCell.RowIndex - 1;
            clickIndex = stackGrid.CurrentCell.RowIndex;
            clickFlag = true;
        }
        public void ArrowDn()
        {
            //currentRaceIndex = stackGrid.CurrentCell.RowIndex - 1;
            clickIndex = stackGrid.CurrentCell.RowIndex;
            clickFlag = true;
        }

        #endregion

        #region Preview strings
        /// <summary>
        /// Race data acquisition methods
        /// </summary>
        // Method to get the race data for a specified race
        private BindingList<RaceDataModel> GetRaceData(string electionMode, Int16 stateNumber, string raceOffice, Int16 cd, string electionType, Int16 candidatesToReturn,
            bool candidateSelectEnable, int candidateId1, int candidateId2, int candidateId3, int candidateId4, int candidateId5)
        {
            // Setup the master race collection & bind to grid
            //this.raceDataCollection = new RaceDataCollection();
            //this.raceDataCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
            raceData = this.raceDataCollection.GetRaceDataCollection(electionMode, stateNumber, raceOffice, cd, electionType, candidatesToReturn,
            candidateSelectEnable, candidateId1, candidateId2, candidateId3, candidateId4, candidateId5);

            return raceData;
        }

        private BindingList<RaceDataModel> GetRaceDataCounty(string stCode, int cnty, string raceOffice, string eType) 
        {
            // Setup the master race collection & bind to grid
            //this.raceDataCollection = new RaceDataCollection();
            //this.raceDataCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
            raceData = this.raceDataCollection.GetRaceDataCollectionCounty(stCode, cnty, raceOffice, eType);
            
            return raceData;
        }

        private BindingList<RaceDataModel> GetRaceDataPrimary(Int16 stateNumber, string raceOffice, Int16 cd, string electionType, string party)
        {
            // Setup the master race collection & bind to grid
            //this.raceDataCollection = new RaceDataCollection();
            //this.raceDataCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
            raceData = this.raceDataCollection.GetRaceDataCollectionPrimary(stateNumber, raceOffice, cd, electionType, party);
            
            return raceData;
        }


        // Method to return metadata for a specific state
        private StateMetadataModel GetStateMetadata(Int16 stateID)
        {
            StateMetadataModel selectedStateMetadata = null;
            if (stateMetadata.Count > 0)
            {
                selectedStateMetadata = stateMetadataCollection.GetStateMetadata(stateMetadata, stateID);
            }
            return selectedStateMetadata;
        }

        // Method to get the RACE_PREVIEW string for a specified race - references raceData binding list
        private string GetRacePreviewString(StackElementModel stackElement, Int16 candidatesToReturn)
        {
            // FoxIDs for all candidates separated by | then
            // ~ Board Mode ~ State Name ^ House CD # or County Name ^ Precincts Reporting ^ Race Description ^ 0
            // Then for each candidate:
            // Name ^ Party ID ^ Incumbent Status ^ Vote Count ^ Percent ^ Winner Status ^ Gain Status ^ Headshot Path
            // Separated by |
            // Board Modes  0 - Normal, 1 - Race Called, 2 - Just Called, 3 - Too Close To Call, 4 - Runoff, 55 - Race To Watch

            // Init
            //string previewField = string.Empty;
            string previewField = "!"; // Bang

            try
            {
                if (raceData.Count >= candidatesToReturn)
                {
                    // Build string of Fox IDs
                    for (int i = 0; i < candidatesToReturn; ++i)
                    {
                        if (i != 0)
                        {
                            previewField = previewField + "|";
                        }
                        previewField = previewField + raceData[i].FoxID;
                    }

                    //Ex: USGOV001769|USGOV000540~0~WISCONSIN^^^DEMOCRATIC PRIMARY^0~HILLARY CRINTON^1^0^ ^ %^0^0^O:\\business\\ObjectStore\\2016\\Q1\\clinton_hillary_official.png|BERNIE SANDERS^1^0^ ^ %^0^0^O:\\business\\ObjectStore\\2015\\Q3\\Sanders_Bernie_IVT_Sen.png
                    // Add race metadata
                    previewField = previewField + "~";
                    previewField = previewField + (Int16)BoardModes.Race_Board_Normal;
                    previewField = previewField + "~";
                    previewField = previewField + stackElement.State_Name.ToUpper().Trim();
                    previewField = previewField + "^^^";
                    // Add race descriptor
                    //Dem primary
                    if (stackElement.Election_Type == "D")
                    {
                        previewField = previewField + "DEMOCRATIC PRIMARY";
                    }
                    //Rep primary
                    else if (stackElement.Election_Type == "R")
                    {
                        previewField = previewField + "REPUBLICAN PRIMARY";
                    }
                    //Dem caucuses
                    else if (stackElement.Election_Type == "E")
                    {
                        previewField = previewField + "DEMOCRATIC CAUCUSES";
                    }
                    //Rep caucuses
                    else if (stackElement.Election_Type == "S")
                    {
                        previewField = previewField + "DEMOCRATIC CAUCUSES";
                    }
                    // Not a primary or caucus event - build string based on office type
                    else
                    {
                        if (stackElement.Race_Office == "P")
                        {
                            previewField = previewField + "President";
                        }
                        else if (stackElement.Race_Office == "G")
                        {
                            previewField = previewField + "Governor";
                        }
                        else if (stackElement.Race_Office == "L")
                        {
                            previewField = previewField + "Lt. Governor";
                        }
                        else if ((stackElement.Race_Office == "S") | (stackElement.Race_Office == "S2"))
                        {
                            previewField = previewField + "Senate";
                        }
                        else if (stackElement.Race_Office == "H")
                        {
                            if (GetStateMetadata(stackElement.State_Number).IsAtLargeHouseState)
                            {
                                previewField = previewField + "House At Large";
                            }
                            else
                            {
                                previewField = previewField + "U.S. House CD " + stackElement.CD.ToString();
                            }
                        }
                    }
                    previewField = previewField + "^0~";

                    // Add candidate data - only include name and headshot (no data for preview)
                    for (int i = 0; i < candidatesToReturn; ++i)
                    {
                        if (i != 0)
                        {
                            previewField = previewField + "|";
                        }
                        previewField = previewField + raceData[i].CandidateLastName.Trim();
                        previewField = previewField + "^";
                        // Add party ID
                        if (raceData[i].CandidatePartyID.ToUpper() == "REP")
                        {
                            previewField = previewField + "0";
                        }
                        else if (raceData[i].CandidatePartyID.ToUpper() == "DEM")
                        {
                            previewField = previewField + "1";
                        }
                        // Modified 10/13/2016 to support Libertarian candidates
                        else if (raceData[i].CandidatePartyID.ToUpper() == "LIB")
                        {
                            previewField = previewField + "3";
                        }
                        else
                        {
                            previewField = previewField + "2";
                        }
                        // Add blank fields for data
                        previewField = previewField + "^0^ ^ ^0^0^";
                        // Add headshot path
                        previewField = previewField + raceData[i].HeadshotPathFNC.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }

            return previewField;
        }




        // Method to get the Referendum string 
        private string GetReferendumPreviewString(ReferendumDataModel referendumData)
        {

            // Init
            string previewField = "!"; // Bang

            try
            {

                // ex State|Proposition|Proposition Name|Proposition Description|Checkstate|Yes Votes|Yes Info|No Votes|No Info|
                previewField = previewField + referendumData.StateName + "|";
                previewField = previewField + referendumData.PropRefID + "|";
                previewField = previewField + referendumData.Description + "|";
                previewField = previewField + referendumData.Detailtext + "|";
                previewField = previewField + "0|"; //Checkstate
                previewField = previewField + " |"; //Yes_Num
                previewField = previewField + " |"; //Yes_Info
                previewField = previewField + " |"; //No_Num
                previewField = previewField + " |"; //Mo_Info

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }

            return previewField;
        }

        // Method to get the Exit Poll string 
        /*
        private string GetExitPollPreviewString(BindingList<ExitPollDataModel> exitPollData, StackElementModel stackElement)
        {


            Int32 numResp = exitPollData.Count;

            // Init
            string previewField = "!"; // Bang
            string Title = "EXIT POLL";
            if (stackElement.ExitPoll_ShortMxLabel[0] == 'N')
                Title = "ENTRANCE POLL";
            if (stackElement.Election_Type == "E" || stackElement.Election_Type == "S")
                Title = "ENTRANCE POLL";
            string plusMinus = String.Empty;
            if (stackElement.ExitPoll_ShortMxLabel[0] == 'S')
                plusMinus = "+";
            if (stackElement.ExitPoll_ShortMxLabel[0] == 'W')
                plusMinus = "-";
            string Question = exitPollData[0].Question;
            if (stackElement.Election_Type[0].ToString() == "M")
                Question = stackElement.ExitPoll_ShortMxLabel;

            try
            {
                // ex # of Repomses|Title|Question|State|Response 1^Response2[^Response3][^Response4]|+/- Percent1^+/- Percent2[^+/- Percent3][^+/- Percent4]|
                previewField = previewField + numResp + "|";
                previewField = previewField + Title + "|";
                previewField = previewField + Question + "|";
                previewField = previewField + stackElement.State_Mnemonic + "|";

                for (Int16 i = 0; i < numResp; i++)
                {
                    if (i > 0)
                        previewField = previewField + "^";
                    previewField = previewField + exitPollData[i].rowLabel;
                }

                previewField = previewField + "||";

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }

            return previewField;
        }
        */

        // Method to get the Exit Poll string 
        
        private string GetVoterAnalysisPreviewString(BindingList<VoterAnalysisDataModel> voterAnalysisData, StackElementModel stackElement)
        {


            Int32 numResp = voterAnalysisData.Count;

            // Init
            string previewField = "!"; // Bang
            string Title = "VOTER ANALYSIS";

            string Question = voterAnalysisData[0].Title;
            
            try
            {
                // ex # of Repomses|Title|Question|State|Response 1^Response2[^Response3][^Response4]|+/- Percent1^+/- Percent2[^+/- Percent3][^+/- Percent4]|
                previewField = previewField + numResp + "|";
                previewField = previewField + Title + "|";
                previewField = previewField + Question + "|";
                previewField = previewField + stackElement.State_Mnemonic + "|";

                for (Int16 i = 0; i < numResp; i++)
                {
                    if (i > 0)
                        previewField = previewField + "^";
                    previewField = previewField + voterAnalysisData[i].Response;
                }

                previewField = previewField + "||";

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("GetVoterAnalysisPreviewString Exception occurred: " + ex.Message);
                
            }

            return previewField;
        }
        

        #endregion

        #region Balance of power methods
        /// <summary>
        /// Methods to add selected Balance of Power to stack
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            AddBOP();
        }

        private void BOPdataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddBOP();
        }
        // Method to get the Balance of Power preview string
        private string GetBOPPreviewString(StackElementModel stackElement, string Header)
        {

            // Init
            string previewField = "!"; // Bang

            try
            {

                // ex HOUSE^CURRENT| ^ ^ | ^ ^ | ^ ^ |
                previewField = previewField + Header;
                previewField = previewField + "| ^ ^ | ^ ^ | ^ ^ |";

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }

            return previewField;
        }


        // Loads the BOPdataGridView
        private void LoadBOPDGV()
        {
            try
            {
                BOPtype bopType = new BOPtype();
                for (int i = 1; i <= 5; i++)
                //for (int i = 1; i <= 4; i++)
                {
                    switch (i)
                    {
                        case 1:
                            bopType.eType = (short)StackElementTypes.Balance_of_Power_House_Current;
                            bopType.branch = "HOUSE";
                            bopType.session = "CURRENT";
                            break;

                        case 2:
                            bopType.eType = (short)StackElementTypes.Balance_of_Power_Senate_Current;
                            bopType.branch = "SENATE";
                            bopType.session = "CURRENT";
                            break;

                        case 3:
                            bopType.eType = (short)StackElementTypes.Balance_of_Power_House_New;
                            bopType.branch = "HOUSE";
                            bopType.session = "NEW";
                            break;

                        case 4:
                            bopType.eType = (short)StackElementTypes.Balance_of_Power_Senate_New;
                            bopType.branch = "SENATE";
                            bopType.session = "NEW";
                            break;

                        case 5:
                            bopType.eType = (short)StackElementTypes.Balance_of_Power_House_Net_Gain;
                            bopType.branch = "NET GAIN";
                            bopType.session = "NEW";
                            break;

                        case 6:
                            bopType.eType = (short)StackElementTypes.Balance_of_Power_Senate_Net_Gain;
                            bopType.branch = "SENATE";
                            bopType.session = "NET GAIN";
                            break;
                    }

                    BOPdataGridView.Rows.Add(bopType.eType, bopType.branch, bopType.session);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        // General method to add Balance Of Power to the stack
        private void AddBOP()
        {
            try
            {
                // Instantiate new stack element model
                StackElementModel newStackElement = new StackElementModel();

                //Get the selected BOP grid object
                int currentBOPIndex = BOPdataGridView.CurrentCell.RowIndex;

                //Get the selected branch 
                string BOPoffice = (string)BOPdataGridView[1, currentBOPIndex].Value;
                BOPoffice = Convert.ToString(BOPoffice[0]);

                // Get the Stack Element Type
                Int16 eType = (short)BOPdataGridView[0, currentBOPIndex].Value;

                newStackElement.fkey_StackID = 0;
                newStackElement.Stack_Element_ID = stackElements.Count;
                newStackElement.Stack_Element_Type = eType;
                newStackElement.Stack_Element_Data_Type = (Int16)DataTypes.Balance_of_Power;
                newStackElement.Stack_Element_Description = "Balance Of Power";
                // Get the template ID for the specified element type
                //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, eType);

                newStackElement.Election_Type = "G";
                newStackElement.Office_Code = BOPoffice;
                newStackElement.State_Number = 0;
                newStackElement.State_Mnemonic = string.Empty;
                newStackElement.State_Name = string.Empty;
                newStackElement.CD = 0;
                newStackElement.County_Number = 0;
                newStackElement.County_Name = string.Empty;
                newStackElement.Listbox_Description = (string)BOPdataGridView[1, currentBOPIndex].Value + ": " + BOPdataGridView[2, currentBOPIndex].Value;

                // Specific to race boards
                newStackElement.Race_ID = 0;
                newStackElement.Race_Office = string.Empty;
                newStackElement.Race_CandidateID_1 = 0;
                newStackElement.Race_CandidateID_2 = 0;
                newStackElement.Race_CandidateID_3 = 0;
                newStackElement.Race_CandidateID_4 = 0;
                newStackElement.Race_PollClosingTime = Convert.ToDateTime("2016 11 08");
                newStackElement.Race_UseAPRaceCall = false;

                //Specific to exit polls - set to default values
                newStackElement.VA_Data_ID = string.Empty;
                newStackElement.VA_Title = string.Empty;
                newStackElement.VA_Type = string.Empty;
                newStackElement.VA_Map_Color = string.Empty;
                newStackElement.VA_Map_ColorNum = 0;

                // Add element
                stackElementsCollection.AppendStackElement(newStackElement);
                // Update stack entries count label
                txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }
        #endregion

        #region Voter Analysis Stack Methods
        /// <summary>
        /// Methods to add selected Exit Polls to stack
        /// </summary>
        private void btnAddExitPoll_Click(object sender, EventArgs e)
        {
            //AddExitPoll();
            AddVoterAnalysis();
        }
        /*
        private void availableExitPollsGrid_DoubleClick(object sender, EventArgs e)
        {
            AddExitPoll();
        }

        private void AddExitPoll()
        {
            try
            {
                // Instantiate new stack element model
                StackElementModel newStackElement = new StackElementModel();

                //Get the selected race list object
                //int currentPollIndex = availableExitPollsGrid.CurrentCell.RowIndex;
                int currentPollIndex = 0;

                ExitPollQuestionsModel selectedPoll = exitPollQuestionsCollection.GetExitPoll(exitPollQuestions, currentPollIndex);

                newStackElement.fkey_StackID = 0;
                newStackElement.Stack_Element_ID = stackElements.Count;
                newStackElement.Stack_Element_Type = (short)StackElementTypes.Exit_Poll_Full_Screen;
                //newStackElement.Stack_Element_Data_Type = (short)DataTypes.Exit_Polls;
                newStackElement.Stack_Element_Description = "Exit Poll";
                // Get the template ID for the specified element type
                newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, (short)StackElementTypes.Exit_Poll_Full_Screen);

                newStackElement.Election_Type = selectedPoll.electionType;
                newStackElement.Office_Code = selectedPoll.office;
                newStackElement.State_Number = selectedPoll.stateNum;
                newStackElement.State_Mnemonic = selectedPoll.state;
                newStackElement.State_Name = selectedPoll.stateName;
                newStackElement.CD = selectedPoll.CD;
                newStackElement.County_Number = 0;
                newStackElement.County_Name = "N/A";
                newStackElement.Listbox_Description = selectedPoll.listBoxDescription;

                // Specific to race boards
                newStackElement.Race_ID = 0;
                newStackElement.Race_RecordType = selectedPoll.questionType;
                newStackElement.Race_Office = selectedPoll.office;
                newStackElement.Race_District = selectedPoll.CD;
                newStackElement.Race_CandidateID_1 = 0;
                newStackElement.Race_CandidateID_2 = 0;
                newStackElement.Race_CandidateID_3 = 0;
                newStackElement.Race_CandidateID_4 = 0;
                newStackElement.Race_PollClosingTime = Convert.ToDateTime("11/8/2016");
                newStackElement.Race_UseAPRaceCall = false;

                //Specific to exit polls - set to default values
                newStackElement.ExitPoll_mxID = Convert.ToInt32(selectedPoll.mxID);
                newStackElement.ExitPoll_BoardID = 0;
                newStackElement.ExitPoll_ShortMxLabel = selectedPoll.shortMxLabel;
                newStackElement.ExitPoll_NumRows = selectedPoll.numRows;
                newStackElement.ExitPoll_xRow = selectedPoll.rowNum;
                newStackElement.ExitPoll_BaseQuestion = selectedPoll.baseQuestion;
                newStackElement.ExitPoll_RowQuestion = selectedPoll.rowQuestion;
                newStackElement.ExitPoll_Subtitle = string.Empty;
                newStackElement.ExitPoll_Suffix = string.Empty;
                newStackElement.ExitPoll_HeaderText_1 = string.Empty;
                newStackElement.ExitPoll_HeaderText_2 = string.Empty;
                newStackElement.ExitPoll_SubsetName = selectedPoll.subsetName;
                newStackElement.ExitPoll_SubsetID = selectedPoll.subsetID;

                // Add element
                stackElementsCollection.AppendStackElement(newStackElement);
                // Update stack entries count label
                txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }
        */
        private void AddVoterAnalysis()
        {
            try
            {
                // Instantiate new stack element model
                StackElementModel newStackElement = new StackElementModel();

                //Get the selected race list object
                int currentPollIndex;
                //VoterAnalysisQuestionsModel selectedPoll = new VoterAnalysisQuestionsModel();
                StateMetadataModel st = new StateMetadataModel();

                newStackElement.fkey_StackID = 0;
                newStackElement.Stack_Element_ID = stackElements.Count;

                if (tcVoterAnalysis.SelectedIndex == 0)
                {
                    VoterAnalysisQuestionsModelNew selectedPoll = new VoterAnalysisQuestionsModelNew();
                    currentPollIndex = dgvVoterAnalysis.CurrentCell.RowIndex;
                    selectedPoll = VA_Qdata_FS[currentPollIndex];
                    newStackElement.Stack_Element_Type = (short)StackElementTypes.Voter_Analysis_Full_Screen;
                    //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, (short)StackElementTypes.Voter_Analysis_Full_Screen);
                    newStackElement.Office_Code = selectedPoll.ofc;
                    newStackElement.State_Mnemonic = selectedPoll.state;
                    newStackElement.Listbox_Description = $"{selectedPoll.state}-{selectedPoll.ofc}-{selectedPoll.r_type}-{selectedPoll.header}";
                    newStackElement.State_Name = selectedPoll.state.ToUpper();
                    newStackElement.VA_Data_ID = selectedPoll.VA_Data_Id;
                    newStackElement.VA_Type = selectedPoll.r_type;

                }
                else if (tcVoterAnalysis.SelectedIndex == 1)
                {
                    VoterAnalysisQuestionsModelNew selectedPoll = new VoterAnalysisQuestionsModelNew();
                    currentPollIndex = dgvVoterAnalysisTicker.CurrentCell.RowIndex;
                    selectedPoll = VA_Qdata_Tkr[currentPollIndex];
                    newStackElement.Stack_Element_Type = (short)StackElementTypes.Voter_Analysis_Ticker;
                    //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, (short)StackElementTypes.Voter_Analysis_Ticker);
                    newStackElement.Office_Code = selectedPoll.ofc;
                    newStackElement.State_Mnemonic = selectedPoll.state;
                    newStackElement.Listbox_Description = $"{selectedPoll.state}-{selectedPoll.ofc}-{selectedPoll.r_type}-{selectedPoll.header}";
                    newStackElement.State_Name = selectedPoll.state.ToUpper();
                    newStackElement.VA_Data_ID = selectedPoll.VA_Data_Id;
                    newStackElement.VA_Type = selectedPoll.r_type;

                }
                else if (tcVoterAnalysis.SelectedIndex == 2)
                {
                    VoterAnalysisManualQuestionsModelNew selectedPoll = new VoterAnalysisManualQuestionsModelNew();
                    currentPollIndex = dgvVAManual.CurrentCell.RowIndex;
                    selectedPoll = VA_Qdata_Man[currentPollIndex];
                    newStackElement.Stack_Element_Type = (short)StackElementTypes.Voter_Analysis_Manual;
                    //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, (short)StackElementTypes.Voter_Analysis_Manual);
                    newStackElement.State_Mnemonic = selectedPoll.st;
                    newStackElement.Listbox_Description = $"{selectedPoll.st}-{selectedPoll.r_type}-{selectedPoll.question}";
                    newStackElement.State_Name = selectedPoll.state.ToUpper();
                    newStackElement.VA_Data_ID = selectedPoll.VA_Data_Id;
                    newStackElement.VA_Type = selectedPoll.r_type;

                }

                newStackElement.Stack_Element_Data_Type = (short)DataTypes.Voter_Analysis;
                newStackElement.Stack_Element_Description = "Voter Analysis";
                // Get the template ID for the specified element type

                newStackElement.Election_Type = "G";
                //newStackElement.State_Number = (short)selectedPoll.stateId;
                
                newStackElement.CD = 0;
                newStackElement.County_Number = 0;
                newStackElement.County_Name = "N/A";
                
                string str = newStackElement.Listbox_Description;
                if (str.Length  > 100)
                    newStackElement.Listbox_Description = str.Substring(0, 98);


                // Specific to race boards
                newStackElement.Race_ID = 0;
                newStackElement.Race_Office = "";
                newStackElement.Race_CandidateID_1 = 0;
                newStackElement.Race_CandidateID_2 = 0;
                newStackElement.Race_CandidateID_3 = 0;
                newStackElement.Race_CandidateID_4 = 0;
                newStackElement.Race_PollClosingTime = Convert.ToDateTime("11/8/2016");
                newStackElement.Race_UseAPRaceCall = false;

                //Specific to exit polls - set to default values
                //newStackElement.VA_Data_ID = selectedPoll.VA_Data_Id;
                //newStackElement.VA_Title = selectedPoll.preface;
                //newStackElement.VA_Type = selectedPoll.r_type;
                newStackElement.VA_Map_Color = string.Empty;
                newStackElement.VA_Map_ColorNum = 0;

                // Add element
                stackElementsCollection.AppendStackElement(newStackElement);
                // Update stack entries count label
                txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }

        private void AddVoterAnalysisMap()
        {
            try
            {
                // Instantiate new stack element model
                StackElementModel newStackElement = new StackElementModel();

                //Get the selected race list object
                int currentPollIndex = dgvVoterAnalysisMap.CurrentCell.RowIndex;

                VoterAnalysisMapQuestionsModel selectedPoll = new VoterAnalysisMapQuestionsModel();
                StateMetadataModel st = new StateMetadataModel();

                newStackElement.fkey_StackID = 0;
                newStackElement.Stack_Element_ID = stackElements.Count;

                newStackElement.Stack_Element_Type = (short)StackElementTypes.Voter_Analysis_Maps;
                selectedPoll = VA_Qdata_Map[currentPollIndex];
                //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, (short)StackElementTypes.Voter_Analysis_Maps);
                newStackElement.Stack_Element_TemplateID = string.Empty;

                newStackElement.Stack_Element_Data_Type = (short)DataTypes.Voter_Analysis_Maps;
                newStackElement.Stack_Element_Description = "Voter Analysis Maps";
                // Get the template ID for the specified element type

                newStackElement.Election_Type = "G";
                newStackElement.Office_Code = " ";
                newStackElement.State_Number = 0;
                newStackElement.State_Mnemonic = "US";

                st = GetStateMetadata(0);
                List<MapMetaDataModelNew> mmData = getMapMetaData(selectedPoll.VA_Data_Id);

                int selectedColorNum = mmData[0].colorIndex;
                string selectedColor = mmData[0].color;

                newStackElement.State_Name = st.State_Name;
                newStackElement.CD = 0;
                newStackElement.County_Number = 0;
                newStackElement.County_Name = "N/A";
                newStackElement.Listbox_Description = $"{selectedPoll.answer} - {selectedPoll.r_type} - {selectedColor}";

                // Specific to race boards
                newStackElement.Race_ID = 0;
                newStackElement.Race_Office = string.Empty;
                newStackElement.Race_CandidateID_1 = 0;
                newStackElement.Race_CandidateID_2 = 0;
                newStackElement.Race_CandidateID_3 = 0;
                newStackElement.Race_CandidateID_4 = 0;
                newStackElement.Race_PollClosingTime = Convert.ToDateTime("11/3/2020");
                newStackElement.Race_UseAPRaceCall = false;

                //Specific to Voter Analysis
                newStackElement.VA_Data_ID = selectedPoll.VA_Data_Id;
                newStackElement.VA_Title = string.Empty;
                newStackElement.VA_Type = selectedPoll.r_type;
                newStackElement.VA_Map_Color = selectedColor;
                newStackElement.VA_Map_ColorNum = selectedColorNum;

                
                // Add element
                stackElementsCollection.AppendStackElement(newStackElement);
                // Update stack entries count label
                txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);


            }
            catch (Exception ex)
            {
                // Log error
                log.Error("Add Map Error: " + ex.Message);
                
            }
        }
        //private void AddVoterAnalysisMap()
        //{
        //    try
        //    {
        //        // Instantiate new stack element model
        //        StackElementModel newStackElement = new StackElementModel();

        //        //Get the selected race list object
        //        int currentPollIndex = dgvVoterAnalysisMap.CurrentCell.RowIndex;


        //        DialogResult dr = new DialogResult();
        //        frmColorSelect cs = new frmColorSelect();

        //        dr = cs.ShowDialog();

        //        if (dr == DialogResult.OK)
        //        {
        //            int selectedColorNum = cs.selectedColorNum + 1;
        //            string selectedColor = cs.selectedColor;

        //            VoterAnalysisMapQuestionsModel selectedPoll = new VoterAnalysisMapQuestionsModel();
        //            StateMetadataModel st = new StateMetadataModel();

        //            newStackElement.fkey_StackID = 0;
        //            newStackElement.Stack_Element_ID = stackElements.Count;

        //            newStackElement.Stack_Element_Type = (short)StackElementTypes.Voter_Analysis_Maps;
        //            selectedPoll = VA_Qdata_Map[currentPollIndex];
        //            //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, (short)StackElementTypes.Voter_Analysis_Maps);
        //            newStackElement.Stack_Element_TemplateID = string.Empty;

        //            newStackElement.Stack_Element_Data_Type = (short)DataTypes.Voter_Analysis_Maps;
        //            newStackElement.Stack_Element_Description = "Voter Analysis Maps";
        //            // Get the template ID for the specified element type

        //            newStackElement.Election_Type = "G";
        //            newStackElement.Office_Code = " ";
        //            newStackElement.State_Number = 0;
        //            newStackElement.State_Mnemonic = "US";

        //            st = GetStateMetadata(0);

        //            newStackElement.State_Name = st.State_Name;
        //            newStackElement.CD = 0;
        //            newStackElement.County_Number = 0;
        //            newStackElement.County_Name = "N/A";
        //            newStackElement.Listbox_Description = $"{selectedPoll.answer} - {selectedPoll.r_type} - {selectedColor}";

        //            // Specific to race boards
        //            newStackElement.Race_ID = 0;
        //            newStackElement.Race_Office = string.Empty;
        //            newStackElement.Race_CandidateID_1 = 0;
        //            newStackElement.Race_CandidateID_2 = 0;
        //            newStackElement.Race_CandidateID_3 = 0;
        //            newStackElement.Race_CandidateID_4 = 0;
        //            newStackElement.Race_PollClosingTime = Convert.ToDateTime("11/8/2016");
        //            newStackElement.Race_UseAPRaceCall = false;

        //            //Specific to Voter Analysis
        //            newStackElement.VA_Data_ID = selectedPoll.VA_Data_Id;
        //            newStackElement.VA_Title = string.Empty;
        //            newStackElement.VA_Type = selectedPoll.r_type;
        //            newStackElement.VA_Map_Color = selectedColor;
        //            newStackElement.VA_Map_ColorNum = selectedColorNum;

        //            // Add element
        //            stackElementsCollection.AppendStackElement(newStackElement);
        //            // Update stack entries count label
        //            txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error
        //        log.Error("Add Map Error: " + ex.Message);

        //    }
        //}

        #endregion

        #region Exit Poll Filters
        /// <summary>
        /// Exit Poll Questions radio button handlers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbEPMan_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (rbEPMan.Checked == true)
                rbEPMan.BackColor = Color.Gold;
            else
                rbEPMan.BackColor = gbExitPolls.BackColor;
            manualEPQuestions = rbEPMan.Checked;
            RefreshExitPollQuestions();
            */
        }

        private void rbEPAuto_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (rbEPAuto.Checked == true)
                rbEPAuto.BackColor = Color.Gold;
            else
                rbEPAuto.BackColor = gbExitPolls.BackColor;
            manualEPQuestions = rbEPMan.Checked;
            RefreshExitPollQuestions();
            */
        }


        #endregion 

        #region Referendums
        private void button1_Click(object sender, EventArgs e)
        {
            Addreferendum();
        }

        private void ReferendumsGrid_DoubleClick(object sender, EventArgs e)
        {
            Addreferendum();
        }

        // Method to add a referendum
        private void Addreferendum()
        {
            try
            {
                // Instantiate new stack element model
                StackElementModel newStackElement = new StackElementModel();

                //Get the selected race list object
                int currentReferendumIndex = ReferendumsGrid.CurrentCell.RowIndex;
                ReferendumModel selectedReferendum = referendumsCollection.GetReferendum(referendums, currentReferendumIndex);

                newStackElement.fkey_StackID = 0;
                newStackElement.Stack_Element_ID = stackElements.Count;
                newStackElement.Stack_Element_Type = (short)StackElementTypes.Referendums;
                newStackElement.Stack_Element_Data_Type = (short)DataTypes.Referendums;
                newStackElement.Stack_Element_Description = "Referendums";
                // Get the template ID for the specified element type
                //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, (short)StackElementTypes.Referendums);
                //093016
                //mseStackElementTypeCollection.GetMSEStackElementType(mseStackElementTypes, (short)StackElementTypes.Referendums).Element_Type_Template_ID;

                newStackElement.Election_Type = selectedReferendum.race_ElectionType;
                newStackElement.Office_Code = selectedReferendum.race_OfficeCode;
                newStackElement.State_Number = selectedReferendum.state_ID;
                newStackElement.State_Mnemonic = selectedReferendum.state_Abbv;
                newStackElement.State_Name = selectedReferendum.state_Name;
                newStackElement.CD = 0;
                newStackElement.County_Number = 0;
                newStackElement.County_Name = "N/A";
                newStackElement.Listbox_Description = selectedReferendum.race_Description;

                // Specific to race boards
                newStackElement.Race_ID = 0;
                newStackElement.Race_Office = selectedReferendum.race_OfficeCode;
                newStackElement.Race_CandidateID_1 = 0;
                newStackElement.Race_CandidateID_2 = 0;
                newStackElement.Race_CandidateID_3 = 0;
                newStackElement.Race_CandidateID_4 = 0;
                newStackElement.Race_PollClosingTime = Convert.ToDateTime("11/8/2016");
                newStackElement.Race_UseAPRaceCall = false;

                //Specific to Voter Analysis - set to default values
                newStackElement.VA_Data_ID = string.Empty;
                newStackElement.VA_Title = string.Empty;
                newStackElement.VA_Type = string.Empty;
                newStackElement.VA_Map_Color = string.Empty;
                newStackElement.VA_Map_ColorNum = 0;


                // Add element
                stackElementsCollection.AppendStackElement(newStackElement);
                // Update stack entries count label
                txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmMain Exception occurred: " + ex.Message);
                //log.Debug("frmMain Exception occurred", ex);
            }
        }


        #endregion

        #region Race Filters
        /// <summary>
        /// Contains all the methods for loading the available races list with the filters applied
        /// </summary>

        // load the available races list with Presidential races only

        public Int16 callStatus;
        public string ofcID = "A";
        public Boolean battlegroundOnly = false;
        public Int16 specialFilters;

        public Int16 callStatusSP;
        public string ofcIDSP = "A";
        public Boolean battlegroundOnlySP = false;
        public Int16 specialFiltersSP;


        private void rbPresident_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPresident.Checked == true)
                rbPresident.BackColor = Color.Gold;
            else
                rbPresident.BackColor = gbROF.BackColor;
            ofcID = "P";
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        // load the available races list with all races
        private void rbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShowAll.Checked == true)
                rbShowAll.BackColor = Color.Gold;
            else
                rbShowAll.BackColor = gbROF.BackColor;
            ofcID = "A";
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        // load the available races list with Senate races only
        private void rbSenate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSenate.Checked == true)
                rbSenate.BackColor = Color.Gold;
            else
                rbSenate.BackColor = gbROF.BackColor;
            ofcID = "S";
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        // load the available races list with House races only
        private void rbHouse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHouse.Checked == true)
                rbHouse.BackColor = Color.Gold;
            else
                rbHouse.BackColor = gbROF.BackColor;
            ofcID = "H";
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        // load the available races list with Governor races only
        private void rbGovernor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGovernor.Checked == true)
                rbGovernor.BackColor = Color.Gold;
            else
                rbGovernor.BackColor = gbROF.BackColor;
            ofcID = "G";
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }
        private void rbTCTC_Click(object sender, EventArgs e)
        {
            callStatus = (Int16)BoardModes.Race_Board_To_Close_To_Call;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        private void rbJustCalled_CheckedChanged(object sender, EventArgs e)
        {
            if (rbJustCalled.Checked == true)
                rbJustCalled.BackColor = Color.Gold;
            else
                rbJustCalled.BackColor = gbRCF.BackColor;
            callStatus = (Int16)BoardModes.Race_Board_Just_Called;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        private void rbCalled_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCalled.Checked == true)
                rbCalled.BackColor = Color.Gold;
            else
                rbCalled.BackColor = gbRCF.BackColor;
            callStatus = (Int16)BoardModes.Race_Board_Race_Called;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAll.Checked == true)
                rbAll.BackColor = Color.Gold;
            else
                rbAll.BackColor = gbRCF.BackColor;
            callStatus = (Int16)BoardModes.Race_Board_Normal;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        private void rbTCTC_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTCTC.Checked == true)
                rbTCTC.BackColor = Color.Gold;
            else
                rbTCTC.BackColor = gbRCF.BackColor;
            callStatus = (Int16)BoardModes.Race_Board_To_Close_To_Call;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        private void rbBattleground_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBattleground.Checked == true)
                rbBattleground.BackColor = Color.Gold;
            else
                rbBattleground.BackColor = gbSpF.BackColor;
            battlegroundOnly = rbBattleground.Checked;
            specialFilters = (short)SpecialCaseFilterModes.Battleground_States_Only;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        private void rbPollClosing_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPollClosing.Checked == true)
                rbPollClosing.BackColor = Color.Gold;
            else
                rbPollClosing.BackColor = gbSpF.BackColor;
            specialFilters = (short)SpecialCaseFilterModes.Next_Poll_Closing_States_Only;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNone.Checked == true)
                rbNone.BackColor = Color.Gold;
            else
                rbNone.BackColor = gbSpF.BackColor;
            battlegroundOnly = false;
            specialFilters = (short)SpecialCaseFilterModes.None;
            RefreshAvailableRacesListFiltered(ofcID, callStatus, specialFilters, stateMetadata, isPrimary);

        }

        private void rbPresidentSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPresidentSP.Checked == true)
                rbPresidentSP.BackColor = Color.Gold;
            else
                rbPresidentSP.BackColor = gbROFSP.BackColor;
            ofcIDSP = "P";
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);
        }

        private void rbSenateSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSenateSP.Checked == true)
                rbSenateSP.BackColor = Color.Gold;
            else
                rbSenateSP.BackColor = gbROFSP.BackColor;
            ofcIDSP = "S";
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);
        }

        private void rbHouseSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHouseSP.Checked == true)
                rbHouseSP.BackColor = Color.Gold;
            else
                rbHouseSP.BackColor = gbROFSP.BackColor;
            ofcIDSP = "H";
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);
        }

        private void rbGovernorSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGovernorSP.Checked == true)
                rbGovernorSP.BackColor = Color.Gold;
            else
                rbGovernorSP.BackColor = gbROFSP.BackColor;
            ofcIDSP = "G";
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);
        }

        private void rbShowAllSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShowAllSP.Checked == true)
                rbShowAllSP.BackColor = Color.Gold;
            else
                rbShowAllSP.BackColor = gbROFSP.BackColor;
            ofcIDSP = "A";
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);

        }

        private void rbTCTCSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTCTCSP.Checked == true)
                rbTCTCSP.BackColor = Color.Gold;
            else
                rbTCTCSP.BackColor = gbRCFSP.BackColor;
            callStatusSP = (Int16)BoardModes.Race_Board_To_Close_To_Call;
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);
        }

        private void rbJustCalledSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbJustCalledSP.Checked == true)
                rbJustCalledSP.BackColor = Color.Gold;
            else
                rbJustCalledSP.BackColor = gbRCFSP.BackColor;
            callStatusSP = (Int16)BoardModes.Race_Board_Just_Called;
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);

        }

        private void rbCalledSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCalledSP.Checked == true)
                rbCalledSP.BackColor = Color.Gold;
            else
                rbCalledSP.BackColor = gbRCFSP.BackColor;
            callStatusSP = (Int16)BoardModes.Race_Board_Race_Called;
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);

        }

        private void rbAllSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllSP.Checked == true)
                rbAllSP.BackColor = Color.Gold;
            else
                rbAllSP.BackColor = gbRCFSP.BackColor;
            callStatusSP = (Int16)BoardModes.Race_Board_Normal;
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);
        }

        private void rbBattlegroundSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBattlegroundSP.Checked == true)
                rbBattlegroundSP.BackColor = Color.Gold;
            else
                rbBattlegroundSP.BackColor = gbSpFSP.BackColor;
            battlegroundOnlySP = rbBattleground.Checked;
            specialFiltersSP = (short)SpecialCaseFilterModes.Battleground_States_Only;
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);

        }

        private void rbPollClosingSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPollClosingSP.Checked == true)
                rbPollClosingSP.BackColor = Color.Gold;
            else
                rbPollClosingSP.BackColor = gbSpF.BackColor;
            specialFiltersSP = (short)SpecialCaseFilterModes.Next_Poll_Closing_States_Only;
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);

        }

        private void rbNoneSP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoneSP.Checked == true)
                rbNoneSP.BackColor = Color.Gold;
            else
                rbNoneSP.BackColor = gbSpFSP.BackColor;
            battlegroundOnlySP = false;
            specialFiltersSP = (short)SpecialCaseFilterModes.None;
            RefreshAvailableRacesListFilteredSP(ofcIDSP, callStatusSP, specialFiltersSP, stateMetadata, isPrimary);

        }

        #endregion

        #region Add select boards
        // Add 1-way select board
        private void btnSelect1_Click(object sender, EventArgs e)
        {
            if (stackLocked == false)
            {
                try
                {
                    Int32 selectedCandidate1 = 0;
                    Int32 selectedCandidate2 = 0;
                    Int32 selectedCandidate3 = 0;
                    Int32 selectedCandidate4 = 0;
                    Int32 selectedCandidate5 = 0;
                    string cand1Name = string.Empty;
                    string cand2Name = string.Empty;
                    string cand3Name = string.Empty;
                    string cand4Name = string.Empty;
                    string cand5Name = string.Empty;
                    Int16 numCand = 1;

                    //Get the selected race list object
                    int currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                    BindingList<AvailableRaceModel> raceList = availableRaces;
                    if (tabIndex == 4)
                    {
                        currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex; 
                        raceList = availableRacesSP;
                    }
                    AvailableRaceModel selectedRace = availableRacesCollection.GetRace(raceList, currentRaceIndex);

                    string eType = selectedRace.Election_Type;
                    string ofc = selectedRace.Race_Office;
                    Int16 st = selectedRace.State_Number;
                    string des = selectedRace.Race_Description;
                    Int16 cd = selectedRace.CD;
                    string party = selectedRace.Party;

                    DialogResult dr = new DialogResult();
                    FrmCandidateSelect selectCand = new FrmCandidateSelect(numCand, st, ofc, eType, des, cd, electionMode, party);
                    dr = selectCand.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        // Set candidateID's

                        selectedCandidate1 = selectCand.Cand1;
                        cand1Name = selectCand.CandName1;
                        //selectedCandidate2 = selectCand.Cand2;
                        //cand2Name = selectCand.CandName2;
                        //selectedCandidate3 = selectCand.Cand3;
                        //cand3Name = selectCand.CandName3;
                        //selectedCandidate4 = selectCand.Cand4;
                        //cand4Name = selectCand.CandName4;
                        AddSelectRaceBoardToStack(numCand, selectedCandidate1, selectedCandidate2, selectedCandidate3, selectedCandidate4, selectedCandidate5, cand1Name, cand2Name, cand3Name, cand4Name, cand5Name);
                    }

                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("frmMain Exception occurred: " + ex.Message);
                    //log.Debug("frmMain Exception occurred", ex);
                }
            }
        }

        // Add 2-way select board
        private void btnSelect2_Click(object sender, EventArgs e)
        {
            if (stackLocked == false)
            {
                try
                {
                    Int32 selectedCandidate1 = 0;
                    Int32 selectedCandidate2 = 0;
                    Int32 selectedCandidate3 = 0;
                    Int32 selectedCandidate4 = 0;
                    Int32 selectedCandidate5 = 0;
                    string cand1Name = string.Empty;
                    string cand2Name = string.Empty;
                    string cand3Name = string.Empty;
                    string cand4Name = string.Empty;
                    string cand5Name = string.Empty;
                    Int16 numCand = 2;

                    //Get the selected race list object
                    int currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                    BindingList<AvailableRaceModel> raceList = availableRaces;
                    if (tabIndex == 4)
                    {
                        currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex;
                        raceList = availableRacesSP;
                    }
                    AvailableRaceModel selectedRace = availableRacesCollection.GetRace(raceList, currentRaceIndex);

                    string eType = selectedRace.Election_Type;
                    string ofc = selectedRace.Race_Office;
                    Int16 st = selectedRace.State_Number;
                    Int16 cd = selectedRace.CD;
                    string des = selectedRace.Race_Description;
                    string party = selectedRace.Party;

                    DialogResult dr = new DialogResult();
                    FrmCandidateSelect selectCand = new FrmCandidateSelect(numCand, st, ofc, eType, des, cd, electionMode, party);
                    dr = selectCand.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        // Set candidateID's

                        selectedCandidate1 = selectCand.Cand1;
                        cand1Name = selectCand.CandName1;
                        selectedCandidate2 = selectCand.Cand2;
                        cand2Name = selectCand.CandName2;
                        //selectedCandidate3 = selectCand.Cand3;
                        //cand3Name = selectCand.CandName3;
                        //selectedCandidate4 = selectCand.Cand4;
                        //cand4Name = selectCand.CandName4;
                        AddSelectRaceBoardToStack(numCand, selectedCandidate1, selectedCandidate2, selectedCandidate3, selectedCandidate4, selectedCandidate5, cand1Name, cand2Name, cand3Name, cand4Name, cand5Name);

                    }

                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("frmMain Exception occurred: " + ex.Message);
                    //log.Debug("frmMain Exception occurred", ex);
                }
            }

        }

        // Add 3-way select board
        private void btnSelect3_Click(object sender, EventArgs e)
        {
            if (stackLocked == false)
            {
                try
                {
                    Int32 selectedCandidate1 = 0;
                    Int32 selectedCandidate2 = 0;
                    Int32 selectedCandidate3 = 0;
                    Int32 selectedCandidate4 = 0;
                    Int32 selectedCandidate5 = 0;
                    string cand1Name = string.Empty;
                    string cand2Name = string.Empty;
                    string cand3Name = string.Empty;
                    string cand4Name = string.Empty;
                    string cand5Name = string.Empty;
                    Int16 numCand = 3;

                    //Get the selected race list object
                    int currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                    BindingList<AvailableRaceModel> raceList = availableRaces;
                    if (tabIndex == 4)
                    {
                        currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex;
                        raceList = availableRacesSP;
                    }
                    AvailableRaceModel selectedRace = availableRacesCollection.GetRace(raceList, currentRaceIndex);

                    string eType = selectedRace.Election_Type;
                    string ofc = selectedRace.Race_Office;
                    Int16 st = selectedRace.State_Number;
                    string des = selectedRace.Race_Description;
                    Int16 cd = selectedRace.CD;
                    string party = selectedRace.Party;

                    DialogResult dr = new DialogResult();
                    FrmCandidateSelect selectCand = new FrmCandidateSelect(numCand, st, ofc, eType, des, cd, electionMode, party);
                    dr = selectCand.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        // Set candidateID's

                        selectedCandidate1 = selectCand.Cand1;
                        cand1Name = selectCand.CandName1;
                        selectedCandidate2 = selectCand.Cand2;
                        cand2Name = selectCand.CandName2;
                        selectedCandidate3 = selectCand.Cand3;
                        cand3Name = selectCand.CandName3;
                        //selectedCandidate4 = selectCand.Cand4;
                        //cand4Name = selectCand.CandName4;
                        AddSelectRaceBoardToStack(numCand, selectedCandidate1, selectedCandidate2, selectedCandidate3, selectedCandidate4, selectedCandidate5, cand1Name, cand2Name, cand3Name, cand4Name, cand5Name);

                    }

                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("frmMain Exception occurred: " + ex.Message);
                    //log.Debug("frmMain Exception occurred", ex);
                }
            }
        }

        // Add 4-way select board
        private void btnSelect4_Click(object sender, EventArgs e)
        {
            if (stackLocked == false)
            {
                try
                {
                    Int32 selectedCandidate1 = 0;
                    Int32 selectedCandidate2 = 0;
                    Int32 selectedCandidate3 = 0;
                    Int32 selectedCandidate4 = 0;
                    Int32 selectedCandidate5 = 0;
                    string cand1Name = string.Empty;
                    string cand2Name = string.Empty;
                    string cand3Name = string.Empty;
                    string cand4Name = string.Empty;
                    string cand5Name = string.Empty;
                    Int16 numCand = 4;

                    //Get the selected race list object
                    int currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                    BindingList<AvailableRaceModel> raceList = availableRaces;
                    if (tabIndex == 4)
                    {
                        currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex;
                        raceList = availableRacesSP;
                    }
                    AvailableRaceModel selectedRace = availableRacesCollection.GetRace(raceList, currentRaceIndex);

                    string eType = selectedRace.Election_Type;
                    string ofc = selectedRace.Race_Office;
                    Int16 st = selectedRace.State_Number;
                    string des = selectedRace.Race_Description;
                    Int16 cd = selectedRace.CD;
                    string party = selectedRace.Party;

                    DialogResult dr = new DialogResult();
                    FrmCandidateSelect selectCand = new FrmCandidateSelect(numCand, st, ofc, eType, des, cd, electionMode, party);

                    // Only process if required number of candidates in race
                    if (selectCand.candidatesFound)
                    {
                        dr = selectCand.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            // Set candidateID's

                            selectedCandidate1 = selectCand.Cand1;
                            cand1Name = selectCand.CandName1;
                            selectedCandidate2 = selectCand.Cand2;
                            cand2Name = selectCand.CandName2;
                            selectedCandidate3 = selectCand.Cand3;
                            cand3Name = selectCand.CandName3;
                            selectedCandidate4 = selectCand.Cand4;
                            cand4Name = selectCand.CandName4;
                            AddSelectRaceBoardToStack(numCand, selectedCandidate1, selectedCandidate2, selectedCandidate3, selectedCandidate4, selectedCandidate5, cand1Name, cand2Name, cand3Name, cand4Name, cand5Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("frmMain Exception occurred: " + ex.Message);
                    //log.Debug("frmMain Exception occurred", ex);
                }
            }

        }

        // Generic method to add a candidate select race board to the stack
        private void AddSelectRaceBoardToStack(Int16 numCand, Int32 cID1, Int32 cID2, Int32 cID3, Int32 cID4, Int32 cID5, string cand1Name, string cand2Name, string cand3Name, string cand4Name, string cand5Name)
        {

            if (stackLocked == false)
            {
                //Get the selected race list object
                int currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                AvailableRaceModel selectedRace = availableRacesCollection.GetRace(availableRaces, currentRaceIndex);

                try
                {
                    string nameList = "(";

                    for (Int16 i = 1; i <= numCand; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                nameList = nameList + cand1Name;
                                break;
                            case 2:
                                nameList = nameList + ", " + cand2Name;
                                break;
                            case 3:
                                nameList = nameList + ", " + cand3Name;
                                break;
                            case 4:
                                nameList = nameList + ", " + cand4Name;
                                break;
                            case 5:
                                nameList = nameList + ", " + cand5Name;
                                break;
                        }
                    }
                    nameList = nameList + ")";

                    // Calculate element type based on number of candidates
                    int eType = numCand * 2;
                    if (numCand == 5)
                        eType = (int)StackElementTypes.Race_Board_5_Way_Select;

                    string eDesc = "Race Board (" + numCand + "-Way Select)";

                    // Instantiate new stack element model
                    StackElementModel newStackElement = new StackElementModel();

                    newStackElement.fkey_StackID = 0;
                    newStackElement.Stack_Element_ID = stackElements.Count;
                    newStackElement.Stack_Element_Type = (short)eType;
                    newStackElement.Stack_Element_Data_Type = (short)DataTypes.Race_Boards;
                    newStackElement.Stack_Element_Description = eDesc;

                    // Get the template ID for the specified element type
                    //newStackElement.Stack_Element_TemplateID = GetTemplate(conceptID, newStackElement.Stack_Element_Type);

                    newStackElement.Election_Type = selectedRace.Election_Type;
                    newStackElement.Office_Code = selectedRace.Race_Office;
                    newStackElement.State_Number = selectedRace.State_Number;
                    newStackElement.State_Mnemonic = selectedRace.State_Mnemonic;
                    newStackElement.State_Name = selectedRace.State_Name;
                    newStackElement.CD = selectedRace.CD;
                    newStackElement.County_Number = 0;
                    newStackElement.County_Name = "N/A";
                    newStackElement.Listbox_Description = selectedRace.Race_Description + "  " + nameList;

                    // Specific to race boards
                    newStackElement.Race_ID = selectedRace.Race_ID;
                    newStackElement.Race_Office = selectedRace.Race_Office;
                    newStackElement.Race_CandidateID_1 = cID1;
                    newStackElement.Race_CandidateID_2 = cID2;
                    newStackElement.Race_CandidateID_3 = cID3;
                    newStackElement.Race_CandidateID_4 = cID4;
                    newStackElement.Race_CandidateID_5 = cID5;
                    newStackElement.Race_PollClosingTime = selectedRace.Race_PollClosingTime;
                    newStackElement.Race_UseAPRaceCall = selectedRace.Race_UseAPRaceCall;

                    //Specific to Voter Analysis - set to default values
                    newStackElement.VA_Data_ID = string.Empty;
                    newStackElement.VA_Title = string.Empty;
                    newStackElement.VA_Type = string.Empty;
                    newStackElement.VA_Map_Color = string.Empty;
                    newStackElement.VA_Map_ColorNum = 0;

                    // Add element
                    stackElementsCollection.AppendStackElement(newStackElement);
                    // Update stack entries count label
                    txtStackEntriesCount.Text = Convert.ToString(stackElements.Count);
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("frmMain Exception occurred: " + ex.Message);
                    //log.Debug("frmMain Exception occurred", ex);
                }
            }
        }
        #endregion

        #region Methods for handling graphics concepts
        // Handler for Graphics Concept dropdown selector change
        private void cbGraphicConcept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (graphicsConceptTypes.Count > 0)
            {
                // Set data members for specifying graphics concept
                conceptID = (short)(cbGraphicConcept.SelectedIndex + 1);
                conceptName = graphicsConceptTypes[cbGraphicConcept.SelectedIndex].ConceptName;

                // Re-assign templates to elements in grid
                if (stackElements.Count > 0)
                {
                    for (int i = 0; i < stackElements.Count; i++)
                    {
                        stackElements[i].Stack_Element_TemplateID = GetTemplate(conceptID,
                            stackElements[i].Stack_Element_Type);
                    }
                }
                // For focus to the grid to get it to repaint
                stackGrid.Refresh();
            }
        }

        // Look up Template Name from conceptID and BoardType - used when saving out various graphic element types
        private string GetTemplate(Int16 tempConceptID, Int16 tempElementType)
        {
            string Template = string.Empty;
            for (short i = 0; i < graphicsConcepts.Count; i++)
            {
                if ((tempConceptID == graphicsConcepts[i].ConceptID) & (tempElementType == (short)graphicsConcepts[i].ElementTypeCode))
                {
                    Template = graphicsConcepts[i].TemplateName;
                }
            }
            return Template;
        }

        // Method to color any rows in grid red where the template specified is not the default template for the
        // data type and cannot be changed.
        private void stackGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (GetStackGridHighlightEnableFlag((int)e.RowIndex))
            {
                stackGrid.Rows[e.RowIndex].Cells["TemplateID"].Style.BackColor = Color.Red;

            }
            else
            {
                stackGrid.Rows[e.RowIndex].Cells["TemplateID"].Style.BackColor = Color.White;
            }
        }

        // Method to determine if the stack grid entry template column should be highlighted to indicate
        // that the graphics concept for that entry cannot be changed to the overall concept selected.
        private Boolean GetStackGridHighlightEnableFlag(int rowIndex)
        {
            Boolean highlightEnable = false;

            // Call method to re-assign templates based on graphics concept selected
            if ((stackElements.Count > 0) && (graphicsConcepts.Count > 0))
            {
                // Loop through graphics concepts
                for (int j = 0; j < graphicsConcepts.Count; j++)
                {
                    if ((graphicsConcepts[j].ElementTypeCode == stackElements[rowIndex].Stack_Element_Type) &&
                       (graphicsConcepts[j].ConceptID == conceptID))
                    {
                        if ((graphicsConcepts[j].AllowConceptChange == false) &&
                            (graphicsConcepts[j].IsBaseConcept == false))
                        {
                            highlightEnable = true;
                        }
                        else
                        {
                            highlightEnable = false;
                        }
                    }
                }
            }
            return highlightEnable;
        }

        #endregion

        #region Methods for switching between media sequencers
        // Go to primary media sequencer
        private void usePrimaryMediaSequencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (usePrimaryMediaSequencerToolStripMenuItem.Checked == false)
            //{
            //    if (mseEndpoint2_Enable)
            //    {
            //        usingPrimaryMediaSequencer = false;
            //        topLevelShowsDirectoryURI = Properties.Settings.Default.MSEEndpoint2 + Properties.Settings.Default.TopLevelShowsDirectory;
            //        masterPlaylistsDirectoryURI = Properties.Settings.Default.MSEEndpoint2 + Properties.Settings.Default.MasterPlaylistsDirectory;

            //        lblMediaSequencer.Text = "USING BACKUP MEDIA SEQUENCER: " + mseEndpoint2;
            //        lblMediaSequencer.BackColor = System.Drawing.Color.Yellow;
            //        usePrimaryMediaSequencerToolStripMenuItem.Checked = false;
            //        useBackupMediaSequencerToolStripMenuItem.Checked = true;
            //    }
            //}
            //else
            //{
            //    if (mseEndpoint1_Enable)
            //    {
            //        usingPrimaryMediaSequencer = true;
            //        //topLevelShowsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.TopLevelShowsDirectory;
            //        //masterPlaylistsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.MasterPlaylistsDirectory;

            //        lblMediaSequencer.Text = "USING PRIMARY MEDIA SEQUENCER: " + mseEndpoint1;
            //        lblMediaSequencer.BackColor = System.Drawing.Color.White;
            //        usePrimaryMediaSequencerToolStripMenuItem.Checked = true;
            //        useBackupMediaSequencerToolStripMenuItem.Checked = false;
            //    }
            //}
        }

        // Go to backup media sequencer
        private void useBackupMediaSequencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (useBackupMediaSequencerToolStripMenuItem.Checked == false)
            //{
            //    if (mseEndpoint1_Enable)
            //    {
            //        usingPrimaryMediaSequencer = true;
            //        //topLevelShowsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.TopLevelShowsDirectory;
            //        //masterPlaylistsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.MasterPlaylistsDirectory;

            //        lblMediaSequencer.Text = "USING PRIMARY MEDIA SEQUENCER: " + mseEndpoint1;
            //        lblMediaSequencer.BackColor = System.Drawing.Color.White;
            //        usePrimaryMediaSequencerToolStripMenuItem.Checked = true;
            //        useBackupMediaSequencerToolStripMenuItem.Checked = false;
            //    }

            //}
            //else
            //{
            //    if (mseEndpoint2_Enable)
            //    {
            //        usingPrimaryMediaSequencer = false;
            //        topLevelShowsDirectoryURI = Properties.Settings.Default.MSEEndpoint2 + Properties.Settings.Default.TopLevelShowsDirectory;
            //        masterPlaylistsDirectoryURI = Properties.Settings.Default.MSEEndpoint2 + Properties.Settings.Default.MasterPlaylistsDirectory;

            //        lblMediaSequencer.Text = "USING BACKUP MEDIA SEQUENCER: " + mseEndpoint2;
            //        lblMediaSequencer.BackColor = System.Drawing.Color.Yellow;
            //        usePrimaryMediaSequencerToolStripMenuItem.Checked = false;
            //        useBackupMediaSequencerToolStripMenuItem.Checked = true;
            //    }
            //}
        }
        #endregion

        #region Methods for taking data to air
        private void btnTake_Click(object sender, EventArgs e)
        {
            TakeNext();
        }

        public void TakeNext()
        {
            if (stackLocked)
            {
                if (stackGrid.Rows.Count > 0)
                {
                    if (clickFlag)
                    {
                        currentRaceIndex = clickIndex;
                        stackGrid.CurrentCell = stackGrid.Rows[currentRaceIndex].Cells[0];
                        clickFlag = false;
                    }
                    else
                    {
                        if (currentRaceIndex < stackGrid.RowCount - 1)
                            currentRaceIndex++;
                        stackGrid.CurrentCell = stackGrid.Rows[currentRaceIndex].Cells[0];
                    }
                    TakeCurrent();

                }
                stackGrid.Focus();

            }
        }

        public void TakeCurrent()
        {
            if (stackLocked)
            {
                if (stackGrid.Rows.Count > 0)
                {

                    currentRaceIndex = stackGrid.CurrentCell.RowIndex;
                    Int16 stackElementDataType = (Int16)stackElements[currentRaceIndex].Stack_Element_Data_Type;
                    SetOutput(stackElementDataType);

                    takeCnt++;

                    //int index = dataModeSelect.SelectedIndex;
                    int index = tabIndex;
                    if (takeCnt == 1 && index == 1)
                    {
                        SendCmdToViz("TRIGGER_ACTION", "TAKEIN", index);
                        takeIn = true;
                    }


                    switch (stackElementDataType)
                    {
                        case (short)DataTypes.Race_Boards:
                            TakeRaceBoards();
                            break;

                        case (short)DataTypes.Voter_Analysis:
                            TakeVoterAnalysis();
                            break;

                        case (short)DataTypes.Balance_of_Power:
                            TakeBOP();
                            break;

                        case (short)DataTypes.Referendums:
                            TakeReferendums();
                            break;

                        case (short)DataTypes.Side_Panel:
                            TakeRaceBoards();
                            break;

                        case (short)DataTypes.Voter_Analysis_Maps:
                            TakeVoterAnalysisMaps();
                            break;

                        case (short)DataTypes.National_Popular_Vote:
                            TakeRaceBoards();
                            break;

                    }
                    lastIndex = currentRaceIndex;
                }
            }
        }

        public void TakeLast()
        {
            if (stackLocked)
            {
                if (stackGrid.Rows.Count > 0)
                {

                    Int16 stackElementDataType = (Int16)stackElements[lastIndex].Stack_Element_Data_Type;
                    SetOutput(stackElementDataType);

                    currentRaceIndex = lastIndex;

                    switch (stackElementDataType)
                    {
                        case (short)DataTypes.Race_Boards:
                            TakeRaceBoards();
                            break;

                        case (short)DataTypes.Voter_Analysis:
                            TakeVoterAnalysis();
                            break;

                        case (short)DataTypes.Balance_of_Power:
                            TakeBOP();
                            break;

                        case (short)DataTypes.Referendums:
                            TakeReferendums();
                            break;

                        case (short)DataTypes.Side_Panel:
                            TakeRaceBoards();
                            break;

                        case (short)DataTypes.National_Popular_Vote:
                            TakeRaceBoards();
                            break;


                    }
                }
            }
        }

        public void LoadScene(string sceneName, int EngineNo)
        {
            string cmd = $"0 RENDERER*MAIN_LAYER SET_OBJECT SCENE*{sceneName}{term}";
            byte[] bCmd = Encoding.UTF8.GetBytes(cmd);
            vizClientSockets[EngineNo - 1].Send(bCmd);
            lastSceneLoaded[EngineNo - 1] = sceneName;
            listBox2.Items.Add($"Engine {EngineNo}: {cmd}");
        }


        public void InitialLoadScene(string sceneName, int EngineNo)
        {
            string cmd = $"0 SCENE*{sceneName} LOAD{term}";
            byte[] bCmd = Encoding.UTF8.GetBytes(cmd);
            vizClientSockets[EngineNo - 1].Send(bCmd);
            //lastSceneLoaded[EngineNo - 1] = sceneName;
            listBox2.Items.Add($"Engine {EngineNo}: {cmd}");
        }

        public void InitScene(string sceneName, int EngineNo)
        {
            string cmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}INIT{quot} 1{term}";
            byte[] bCmd = Encoding.UTF8.GetBytes(cmd);
            vizClientSockets[EngineNo - 1].Send(bCmd);
            //lastSceneLoaded[EngineNo - 1] = sceneName;
            listBox2.Items.Add($"Engine {EngineNo}: {cmd}");
        }

        private void SendCmdToViz(string cmd, string cmdData, int index)
        {

            string vizCmd = "";

            //int index = dataModeSelect.SelectedIndex;
            //int index = dataType;

            try
            {

                for (int i = 0; i < tabConfig[index].TabOutput.Count; i++)
                {
                    string sceneName = tabConfig[index].TabOutput[i].sceneName;
                    int engine = tabConfig[index].TabOutput[i].engine;

                    // load scene if last scene loaded on this viz is not = sceneName
                    if (lastSceneLoaded[engine - 1] != sceneName)
                        LoadScene(sceneName, engine);


                    vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}{cmd}{quot} {cmdData}{term}";


                    if (vizEngines[engine - 1].enable)
                    {
                        byte[] bCmd = Encoding.UTF8.GetBytes(vizCmd);
                        if (vizEngines[engine - 1].enable)
                            vizClientSockets[engine - 1].Send(bCmd);
                    }
                }
            }
            catch (Exception ex)
            {
                listBox2.Items.Add($"Send Cmd Error: {ex}");

            }
            listBox2.Items.Add(vizCmd);
            listBox2.SelectedIndex = listBox2.Items.Count - 1;

        }
        private void SendToViz(string cmd, int dataType)
            {
            try
            {

                string vizCmd = "";

                //int index = dataModeSelect.SelectedIndex;
                int index = dataType;

                for (int i = 0; i < tabConfig[index].TabOutput.Count; i++)
                {
                    string sceneName = tabConfig[index].TabOutput[i].sceneName;
                    int engine = tabConfig[index].TabOutput[i].engine;

                    // load scene if last scene loaded on this viz is not = sceneName
                    if (lastSceneLoaded[engine - 1] != sceneName)
                        LoadScene(sceneName, engine);


                    if (index == 0)
                        vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}CANDIDATE_DATA{quot} {cmd}{term}";

                    if (index == 1)
                        vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}POLL_DATA{quot} {cmd}{term}";

                    if (index == 2)
                        vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}BOP_DATA{quot} {cmd}{term}";

                    if (index == 3)
                        vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}REFERENDUM_DATA{quot} {cmd}{term}";

                    if (index == 4)
                        vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}CANDIDATE_DATA{quot} {cmd}{term}";
                    
                    if (index == 5)
                        vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}CANDIDATE_DATA{quot} {cmd}{term}";

                    if (index == 6)
                        vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}CANDIDATE_DATA{quot} {cmd}{term}";

                    if (vizEngines[engine - 1].enable)
                    {
                        byte[] bCmd = Encoding.UTF8.GetBytes(vizCmd);
                        if (vizEngines[engine - 1].enable)
                            vizClientSockets[engine - 1].Send(bCmd);
                        listBox2.Items.Add(vizCmd);
                        listBox2.SelectedIndex = listBox2.Items.Count - 1;
                        log.Debug($"Eng:{engine}: {vizCmd}");

                    }
                }

            }
            catch (Exception ex)
            {
                listBox2.Items.Add($"Send Cmd Error: {ex}");
                // Log error
                log.Error($"Send Cmd Error: {ex}");

            }

        }

        private void SendToViz2(string cmd, int dataType)
        {
            try
            {

                string vizCmd = "";

                //int index = dataModeSelect.SelectedIndex;
                int index = dataType;

                for (int i = 0; i < tabConfig[index].TabOutput.Count; i++)
                {
                    string sceneName = tabConfig[index].TabOutput[i].sceneName;
                    int engine = tabConfig[index].TabOutput[i].engine;

                    // load scene if last scene loaded on this viz is not = sceneName
                    if (lastSceneLoaded[engine - 1] != sceneName)
                        LoadScene(sceneName, engine);


                    vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {cmd}";

                    if (vizEngines[engine - 1].enable)
                    {
                        byte[] bCmd = Encoding.UTF8.GetBytes(vizCmd);
                        if (vizEngines[engine - 1].enable)
                            vizClientSockets[engine - 1].Send(bCmd);
                        listBox2.Items.Add(vizCmd);
                        listBox2.SelectedIndex = listBox2.Items.Count - 1;
                        log.Debug($"Eng:{engine}: {vizCmd}");

                    }
                }

            }
            catch (Exception ex)
            {
                listBox2.Items.Add($"Send Cmd2 Error: {ex}");
                // Log error
                log.Error($"Send Cmd2 Error: {ex}");
                
            }

        }
        private void LiveUpdateTimer_Tick(object sender, EventArgs e)
        {
            TakeLast();
        }


        private void stackGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TakeCurrent();
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            Unlock();
        }

        public void Unlock()
        {
            LiveUpdateTimer.Enabled = false;
            panel2.BackColor = Color.Navy;
            stackLocked = false;
            dataModeSelect.Enabled = true;

            btnDeleteStackElement.Enabled = true;
            btnClearStack.Enabled = true;
            btnLoadStack.Enabled = true;
            //btnSaveStack.Enabled = true;
            btnStackElementUp.Enabled = true;
            btnStackElementDown.Enabled = true;

            //if (dataModeSelect.SelectedIndex == 1 && tcVoterAnalysis.SelectedIndex == 1 && VictoriaMode == false)
            if (tabIndex == 1 && tcVoterAnalysis.SelectedIndex == 1 && VictoriaMode == false)
                btnSaveStack.Enabled = false;
            else
                btnSaveStack.Enabled = true;

            cbAutoCalledRaces.Checked = false;
            LoopTimer.Enabled = false;
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            Lock();
        }

        public void Lock()
        {
            if (stackGrid.Rows.Count > 0 || autoCalledRacesActive)
            {
                stackLocked = true;
                dataModeSelect.Enabled = false;

                btnDeleteStackElement.Enabled = false;
                btnClearStack.Enabled = false;
                btnLoadStack.Enabled = false;
                btnSaveStack.Enabled = false;
                btnStackElementUp.Enabled = false;
                btnStackElementDown.Enabled = false;

                currentRaceIndex = -1;
                if (stackGrid.Rows.Count > 0)
                    stackGrid.CurrentCell = stackGrid.Rows[0].Cells[0];
                panel2.BackColor = Color.Lime;

                //int index = dataModeSelect.SelectedIndex;
                int index = tabIndex;

                for (int i = 0; i < tabConfig[index].TabOutput.Count; i++)
                {
                    string scenename = tabConfig[index].TabOutput[i].sceneName;
                    int engine = tabConfig[index].TabOutput[i].engine;
                    if (vizEngines.Count > 0)
                    {
                        if (vizEngines[engine - 1].enable)
                        {
                            LoadScene(scenename, engine);
                            InitScene(scenename, engine);

                        }
                    }
                }


                if (index == 1 && takeIn == true)
                {
                    SendCmdToViz("TRIGGER_ACTION", "TAKEOUT", index);
                    takeIn = false;
                }

                takeCnt = 0;

                // if Looping checked start
                if (cbLooping.Checked)
                {
                    LoopTimer.Enabled = true;
                    if (stackLocked && stackGrid.Rows.Count > 0)
                    {
                        TakeNext();
                        
                    }
                }
                else
                {
                    LoopTimer.Enabled = false;
                }
                //pnlStack.Focus();
                stackGrid.Focus();

            }

            /*
            else
            {
                if (autoCalledRacesActive)
                {
                    panel2.BackColor = Color.Lime;
                    LoopTimer.Enabled = true;
                }
                
            }
            */
        }

        public static DataTable GetDBData(string cmdStr, string dbConnection)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(dbConnection))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = cmdStr;
                            //cmd.Parameters.Add("@StackID", SqlDbType.Float).Value = stackID;
                            sqlDataAdapter.SelectCommand = cmd;
                            sqlDataAdapter.SelectCommand.Connection = connection;
                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                            // Fill the datatable from adapter
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("GetDBData Exception occurred: " + ex.Message);
                ////log.Debug("GetDBData Exception occurred", ex);
            }

            return dataTable;
        }

        public void SetOutput(int index)
        {
            //int index = dataModeSelect.SelectedIndex;
            gbEngines.Text = $"Engines used for {tabConfig[index].tabName}";

            for (int i = 0; i < vizEngines.Count; i++)
            {
                if (vizEngines[i].enable)
                {
                    if (i == 0)
                    {
                        pbEng1.Visible = tabConfig[index].outputEngine[i];
                    }

                    if (i == 1)
                    {
                        pbEng2.Visible = tabConfig[index].outputEngine[i];
                    }
                    if (i == 2)
                    {
                        pbEng3.Visible = tabConfig[index].outputEngine[i];
                    }
                    if (i == 3)
                    {
                        pbEng4.Visible = tabConfig[index].outputEngine[i];
                    }

                }

                lblScene1.Text = $"Scene: ";
                lblScene2.Text = $"Scene: ";
                lblScene3.Text = $"Scene: ";
                lblScene4.Text = $"Scene: ";

                for (int j = 0; j < tabConfig[index].TabOutput.Count; j++)
                {
                    //if (tabConfig[index].TabOutput[j].engine == j + 1)
                    {
                        int n = tabConfig[index].TabOutput[j].engine;
                        if (n == 1)
                            lblScene1.Text = $"Scene: {tabConfig[index].TabOutput[j].sceneCode}";
                        if (n == 2)
                            lblScene2.Text = $"Scene: {tabConfig[index].TabOutput[j].sceneCode}";
                        if (n == 3)
                            lblScene3.Text = $"Scene: {tabConfig[index].TabOutput[j].sceneCode}";
                        if (n == 4)
                            lblScene4.Text = $"Scene: {tabConfig[index].TabOutput[j].sceneCode}";
                    }
                }
            }
            lblScenes.Text = $"Scenes: {tabConfig[index].engineSceneDef}";

        }




        private void cbLooping_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLooping.Checked)
            {
                if (stackLocked && stackGrid.Rows.Count > 0)
                {
                    TakeNext();
                    LoopTimer.Enabled = true;
                }
            }
            else
            {
                LoopTimer.Enabled = false;
            }
        }

        private void LoopTimer_Tick(object sender, EventArgs e)
        {
            if (stackGrid.RowCount == 0 && autoCalledRacesActive)
            {
                acrIndx++;
                if (acrIndx >= autoOfc.Count)
                    acrIndx = 0;

                ofcID = autoOfc[acrIndx];
                stackElements.Clear();
                RefreshAvailableRacesListFiltered(ofcID, 1, 0, stateMetadata, isPrimary);
                AddAll();
            }



            if (currentRaceIndex >= stackGrid.RowCount - 1)
            {
                if (autoCalledRacesActive)
                {
                    acrIndx++;
                    if (acrIndx >= autoOfc.Count)
                        acrIndx = 0;

                    ofcID = autoOfc[acrIndx];
                    stackElements.Clear();
                    RefreshAvailableRacesListFiltered(ofcID, 1, 0, stateMetadata, isPrimary);
                    AddAll();

                }

                if (stackGrid.RowCount > 0)
                {
                    currentRaceIndex = 0;
                    stackGrid.CurrentCell = stackGrid.Rows[currentRaceIndex].Cells[0];
                    TakeCurrent();
                }

            }
            else
                TakeNext();
        }

        #endregion

        #region Raceboards Data processing
        public void TakeRaceBoards()
        {
            BindingList<RaceDataModel> rd = new BindingList<RaceDataModel>();

            //Get the selected race list object
            //currentRaceIndex = stackGrid.CurrentCell.RowIndex;
            short stateNumber = stackElements[currentRaceIndex].State_Number;
            string stCode = stackElements[currentRaceIndex].State_Mnemonic;
            int cnty = stackElements[currentRaceIndex].County_Number;
            string cntyName = stackElements[currentRaceIndex].County_Name;
            short cd = stackElements[currentRaceIndex].CD;
            string raceOffice = stackElements[currentRaceIndex].Office_Code;
            string electionType = stackElements[currentRaceIndex].Election_Type;
            int candidatesToReturn = (int)stackElements[currentRaceIndex].Stack_Element_Type;
            int dataType = (int)stackElements[currentRaceIndex].Stack_Element_Data_Type;

            //if (dataModeSelect.SelectedIndex == 4)
            if (tabIndex == 4)
                dataType = 4;
            bool candidateSelectEnable;

            switch ((int)stackElements[currentRaceIndex].Stack_Element_Type)
            {
                case 0:
                    candidatesToReturn = 0;
                    break;
                case 1:
                    candidatesToReturn = 1;
                    break;
                case 2:
                    candidatesToReturn = 1;
                    break;
                case 3:
                    candidatesToReturn = 2;
                    break;
                case 4:
                    candidatesToReturn = 2;
                    break;
                case 5:
                    candidatesToReturn = 3;
                    break;
                case 6:
                    candidatesToReturn = 3;
                    break;
                case 7:
                    candidatesToReturn = 4;
                    break;
                case 8:
                    candidatesToReturn = 4;
                    break;
                case 23:
                    candidatesToReturn = 5;
                    break;
                case 24:
                    candidatesToReturn = 5;
                    break;
                case 25:
                    candidatesToReturn = 2;
                    break;
            }

            //candidatesToReturn = (candidatesToReturn + 1) / 2;
            if ((int)stackElements[currentRaceIndex].Stack_Element_Type % 2 == 0 && (int)stackElements[currentRaceIndex].Stack_Element_Type != 0)
                candidateSelectEnable = true;
            else
                candidateSelectEnable = false;

            int cand1 = stackElements[currentRaceIndex].Race_CandidateID_1;
            int cand2 = stackElements[currentRaceIndex].Race_CandidateID_2;
            int cand3 = stackElements[currentRaceIndex].Race_CandidateID_3;
            int cand4 = stackElements[currentRaceIndex].Race_CandidateID_4;
            int cand5 = stackElements[currentRaceIndex].Race_CandidateID_5;

            if (stackElements[currentRaceIndex].County_Number > 0)
                rd = GetRaceDataCounty(stCode, cnty, raceOffice, electionType);
            else if (electionMode == "Primary")
            {
                string party = "D";
                if (electionType == "R" || electionType == "S")
                    party = "R";
                rd = GetRaceDataPrimary(stateNumber, raceOffice, cd, electionType, party);
            }
            else
                rd = GetRaceData(electionMode, stateNumber, raceOffice, cd, electionType, (short)candidatesToReturn, candidateSelectEnable, cand1, cand2, cand3, cand4, cand5);

            if (rd.Count > 0)
            {
                StateMetadataModel st = GetStateMetadata(stateNumber);
                string stateName = st.State_Name;
                string dataStr = $"{stateName} {rd[0].OfficeName}";


                if (candidatesToReturn > rd.Count || candidatesToReturn == 0)
                    candidatesToReturn = rd.Count;

                for (int i = 0; i < candidatesToReturn; i++)
                {
                    if (candidateSelectEnable)
                    {
                        int candID;
                        switch (i)
                        {
                            case 0:
                                candID = cand1;
                                break;
                            case 1:
                                candID = cand2;
                                break;
                            case 2:
                                candID = cand3;
                                break;
                            case 3:
                                candID = cand4;
                                break;
                            case 4:
                                candID = cand5;
                                break;
                            default:
                                candID = cand1;
                                break;

                        }
                        for (int n = 0; n < rd.Count; n++)
                        {
                            if (rd[n].CandidateID == candID)
                                dataStr += $" - {rd[n].CandidateLastName} {rd[n].CandidateVoteCount}";
                        }


                    }
                    else
                        dataStr += $" - {rd[i].CandidateLastName} {rd[i].CandidateVoteCount}";
                }

                listBox1.Items.Add(dataStr);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;

                if (unreal && tabIndex == 0)
                {
                    SendErizosData(rd, candidatesToReturn, candidateSelectEnable);
                }
                else
                {
                    string outStr = GetRaceBoardMapkeyStr(rd, candidatesToReturn, candidateSelectEnable);
                    SendToViz(outStr, dataType);
                }
            
            }
            else
            {
                LiveUpdateTimer.Enabled = false;
                DialogResult dr =  MessageBox.Show("Error! No candidates to show.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LiveUpdateTimer.Enabled = false;
            LiveUpdateTimer.Enabled = true;

        }



        public void SendErizosData(BindingList<RaceDataModel> raceData, int numCand, bool CandSelect)
        {
            
            //“raceMode” – 0 (not called), 1(race called), 2 (just called), 3(too close to call), 4 (runoff), 5 (race to watch)
            // "evdel" delegates for primaries electoral votes for general elect president 0 otherwise

            string mapKeyStr = "";

            // Gets either simulated or actual time based on flag
            DateTime timeNow = TimeFunctions.GetTime();
            DateTime pollClosingTime = raceData[0].RacePollClosingTime;

            RaceBoardModel raceBoardData = new RaceBoardModel();

            short stateNumber = stackElements[currentRaceIndex].State_Number;
            StateMetadataModel st = GetStateMetadata(stateNumber);
            raceBoardData.state = st.State_Name;

            if (raceData[0].cntyName.Length > 0 && raceData[0].cntyName != "ALL")
                raceBoardData.cd = raceData[0].cntyName;
            else if (raceData[0].CD == 0)
                raceBoardData.cd = string.Empty;
            else
                //raceBoardData.cd = $"HOUSE CD {raceData[0].CD.ToString()}";
                raceBoardData.cd = $"CD {raceData[0].CD.ToString()}";

            TimeSpan fifteenMinutes = new TimeSpan(0, 15, 0);
            bool candidateCalledWinner = false;
            DateTime raceCallTime = raceData[0].RaceWinnerCallTime;
            bool raceCalled = false;

            // get office strings
            raceBoardData.office = GetOfficeStr(raceData[0]);
            //raceBoardData.office = "CAUCUSES";


            if (raceData[0].Office != "H")
            {

                if (ApplicationSettingsFlagsCollection.UseExpectedVoteIn)
                {
                    if (raceData[0].PercentExpectedVote > 0 && raceData[0].PercentExpectedVote < 1)
                        raceBoardData.pctsReporting = "<1";
                    else
                        raceBoardData.pctsReporting = raceData[0].PercentExpectedVote.ToString("F0");
                }
                else
                {
                    
                    float temp = (float)(raceData[0].PrecinctsReporting * 100.0 / raceData[0].TotalPrecincts);
                    if (temp > 0f && temp < 1f)
                        raceBoardData.pctsReporting = "<1";
                    else
                        raceBoardData.pctsReporting = temp.ToString("F0");
                }
            }
            else
            {
                float temp = (float)(raceData[0].PrecinctsReporting * 100.0 / raceData[0].TotalPrecincts);
                if (temp > 0f && temp < 1f)
                    raceBoardData.pctsReporting = "<1";
                else
                    raceBoardData.pctsReporting = temp.ToString("F0");
            }

            if (numCand == 0)
                numCand = raceData.Count;

            int cand1 = stackElements[currentRaceIndex].Race_CandidateID_1;
            int cand2 = stackElements[currentRaceIndex].Race_CandidateID_2;
            int cand3 = stackElements[currentRaceIndex].Race_CandidateID_3;
            int cand4 = stackElements[currentRaceIndex].Race_CandidateID_4;
            int cand5 = stackElements[currentRaceIndex].Race_CandidateID_5;
            int candIndx = 0;

            Erizos_API.RaceboardsPayload.Raceboards1Way raceboards1way = new Erizos_API.RaceboardsPayload.Raceboards1Way();
            raceboards1way.Candidate.value = 2;
            raceboards1way.CheckMark.value = false;

            for (int i = 0; i < numCand; i++)
            {
                candidateData_RB candidate = new candidateData_RB();

                if (CandSelect)
                {
                    int candID;
                    switch (i)
                    {
                        case 0:
                            candID = cand1;
                            break;
                        case 1:
                            candID = cand2;
                            break;
                        case 2:
                            candID = cand3;
                            break;
                        case 3:
                            candID = cand4;
                            break;
                        case 4:
                            candID = cand5;
                            break;
                        default:
                            candID = cand1;
                            break;

                    }

                    for (int n = 0; n < raceData.Count; n++)
                    {
                        if (raceData[n].CandidateID == candID)
                        {
                            candIndx = n;
                            
                        }
                    }
                }
                else
                    candIndx = i;

                if (raceData[candIndx].CandidatePartyID == "Dem")
                    raceboards1way.Candidate.value = 1;

                else if (raceData[candIndx].CandidatePartyID == "Rep")
                    raceboards1way.Candidate.value = 0;

                //mapKeyStr += raceData[candIndx].FoxID;
                //if (i < numCand - 1)
                //    mapKeyStr += "^";

                candidate.lastName = raceData[candIndx].CandidateLastName;
                candidate.firstName = raceData[candIndx].CandidateFirstName;

                if (Network == "FNC")
                {
                    if (raceData[candIndx].UseHeadshotFNC)
                    {
                        candidate.headshot = raceData[candIndx].HeadshotPathFNC;
                    }
                }
                else if (Network == "FBN")
                {

                    // Use FNC Headshot even for FBN for 2018 Midterms
                    if (raceData[candIndx].UseHeadshotFNC)
                    {
                        candidate.headshot = raceData[candIndx].HeadshotPathFNC;
                    }

                }
                else if (Network == "FBC")
                {

                    // Use FNC Headshot even for FBN for 2018 Midterms
                    if (raceData[candIndx].UseHeadshotFNC)
                    {
                        candidate.headshot = raceData[candIndx].HeadshotPathFNC;
                    }

                }
                // filename only, no path, no extension
                candidate.headshot = Path.GetFileNameWithoutExtension(candidate.headshot);

                // Format vote count as string with commas
                if (raceData[candIndx].CandidateVoteCount > 0)
                    //candidate.votes = string.Format("{0:n0}", raceData[i].CandidateVoteCount);
                    candidate.votes = raceData[candIndx].CandidateVoteCount.ToString();
                else
                    candidate.votes = " ";

                // get candidate percent str
                candidate.percent = GetCandPercent(raceData[candIndx]);
                // Get scene Party Id number from party str
                candidate.party = GetCandParty(raceData[candIndx]);

                // Set candidate incumbent flag
                candidate.incumbent = raceData[candIndx].IsIncumbentFlag == "Y" ? "1" : "0";
                var winnerCandidateId = 0;
                
                
                if ((timeNow >= pollClosingTime || PollClosinglockout == false) && raceData[candIndx].cntyName == "ALL")
                {
                    //Check for AP race call
                    if (raceData[candIndx].RaceUseAPRaceCall)
                    {
                        //Check for AP called winner
                        if (raceData[candIndx].cStat.ToUpper() == "W")
                        {
                            raceCalled = true;
                            candidateCalledWinner = true;
                            var raceCallTimeStr = raceData[candIndx].estTS;
                            winnerCandidateId = raceData[candIndx].CandidateID;
                        }
                        else
                        {
                            candidateCalledWinner = false;
                        }
                    }
                    //Check for Fox race call
                    else
                    {
                        winnerCandidateId = raceData[candIndx].RaceWinnerCandidateID;
                        if (raceData[candIndx].RaceWinnerCalled)
                        {
                            raceCalled = true;

                            // If the race was not called by AP, use the Race_WinnerCallTime from the DB
                            raceCallTime = raceData[candIndx].RaceWinnerCallTime;
                            candidateCalledWinner = (winnerCandidateId == raceData[candIndx].RaceWinnerCandidateID);
                        }
                        else
                        {
                            raceCalled = false;
                            candidateCalledWinner = false;
                        }
                    }
                }
                else
                {
                    candidateCalledWinner = false;
                }

                if (winnerCandidateId == raceData[candIndx].CandidateID)
                {
                    candidate.winner = "1";
                    candidate.gain = GetGainFlag(raceData[candIndx]);


                    if (raceData[candIndx].CandidatePartyID == "Dem")
                        raceboards1way.Candidate.value = 1;

                    else if (raceData[candIndx].CandidatePartyID == "Rep")
                        raceboards1way.Candidate.value = 0;

                    raceboards1way.CheckMark.value = true;

                }
                else
                {
                    candidate.winner = "0";
                    candidate.gain = "0";

                }
                raceBoardData.candData.Add(candidate);

            }

            // Set board mode 
            if (raceCalled)
            {

                // if race is called before the polls are closed time then use poll closing time as the race call time
                if (raceCallTime < pollClosingTime)
                    raceCallTime = pollClosingTime;

                // if race is called within 15 minutes 
                if ((timeNow - raceCallTime) < fifteenMinutes)
                    // then Just Called
                    raceBoardData.mode = (int)BoardModes.Race_Board_Just_Called;
                else
                    // Race Called
                    raceBoardData.mode = (int)BoardModes.Race_Board_Race_Called;
            }
            else if (raceData[0].RaceTooCloseToCall)
            {
                // RaceTooCloseToCall flag is set
                raceBoardData.mode = (int)BoardModes.Race_Board_To_Close_To_Call;
            }
            else
            {
                // Not called and not TCTC
                raceBoardData.mode = (int)BoardModes.Race_Board_Normal;
            }

            if (electionMode == "Primary")
            {
                if (raceData[0].eType == "D" || raceData[0].eType == "E")
                    raceBoardData.evdel = raceData[0].DemDelegatesAvailable;
                else if (raceData[0].eType == "R" || raceData[0].eType == "S")
                    raceBoardData.evdel = raceData[0].RepDelegatesAvailable;
            }
            else if (electionMode != "Primary" && raceData[0].Office == "P")
            {
                raceBoardData.evdel = raceData[0].ElectoralVotesAvailable;
            }
            else
                raceBoardData.evdel = 0;


            //“raceMode” – 0 (not called), 1(race called), 2 (just called), 3(too close to call), 4 (runoff), 5 (race to watch)
            // "evdel" delegates for primaries electoral votes for general elect president 0 otherwise

            //string raceblock = $"~state={raceBoardData.state};race={raceBoardData.cd};precincts={raceBoardData.pctsReporting};office={raceBoardData.office};racemode={raceBoardData.mode};evdel={raceBoardData.evdel}~";

            //mapKeyStr += raceblock;

            Erizos_API.RaceboardsPayload.Raceboards2Way raceboards = new Erizos_API.RaceboardsPayload.Raceboards2Way();

            raceboards.StateData.value = raceData[0].StateAbbv;
            raceboards.ElectoralData.value = raceData[0].ElectoralVotesAvailable;
            //raceboards.InPercent.value = raceData[0].PrecinctsReporting;
            //raceboards.InPercent.value = Convert.ToDouble(raceBoardData.pctsReporting);
            raceboards.InPercent.value = raceBoardData.pctsReporting;
            raceboards.RaceStatus.value = raceBoardData.mode;
            string hv = "F";
            if (raceData[0].TotalVoteCount > 0)
            {
                raceboards.HasVotes.value = true;
                hv = "T";
            }
            else
                raceboards.HasVotes.value = false;

            raceboards.TotalVotes.value = raceData[0].TotalVoteCount;

            string outstr = $"{unrealEng}: State={raceData[0].StateAbbv};EV={raceData[0].ElectoralVotesAvailable};InPercent{raceBoardData.pctsReporting};RaceStatus{raceBoardData.mode};";
            outstr += $"HasVotes={hv};";

            for (int i = 0; i < numCand; i++)
            {
                
                bool winner = false;
                string win = "F";
                if (raceBoardData.candData[i].winner == "1")
                {
                    winner = true;
                    win = "T";
                }

                int votes = 0;
                double percent = 0;
                if (raceBoardData.candData[i].votes != " ")
                {
                    votes = Convert.ToInt32(raceBoardData.candData[i].votes);
                    percent = Convert.ToDouble(raceBoardData.candData[i].percent);
                }
                

                if (raceBoardData.candData[i].party == "1")
                {
                    raceboards.DemsWinner.value = winner;
                    raceboards.DemsVotes.value = votes;
                    raceboards.DemsPercent.value = percent;
                    outstr += $"DemsWinner={win};DemVotes={votes};DemsPercent={percent};";
                    
                }
                else if (raceBoardData.candData[i].party == "0")
                {
                    raceboards.RepsWinner.value = winner;
                    raceboards.RepsVotes.value = votes;
                    raceboards.RepsPercent.value = percent;
                    outstr += $"RepsWinner={win};RepsVotes={votes};RepsPercent={percent};";

                }

            }
            raceboards.Difference.value = Math.Abs(raceboards.DemsVotes.value - raceboards.RepsVotes.value);
            outstr += $"Diff={raceboards.Difference.value};";

            raceboards1way.StateData.value = raceData[0].StateAbbv;
            raceboards1way.Logo.value = true;
            string check = "T";
            if (raceboards1way.CheckMark.value == false)
                check = "F";


            string outstr1 = $"{unrealEng}: State={raceData[0].StateAbbv};Logo=T;Candidate={raceboards1way.Candidate.value};Check={check};";

            Erizos_API.RaceboardsPayload.unrealEngine = unrealEng;
            if (numCand == 1)
            {
                Erizos_API.RaceboardsPayload.SendPayload(raceboards1way);
                listBox2.Items.Add(outstr1);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                log.Info(outstr1);

            }
            else
            {
                Erizos_API.RaceboardsPayload.SendPayload(raceboards);
                listBox2.Items.Add(outstr);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                log.Info(outstr);

            }

        }


        public string GetRaceBoardMapkeyStr(BindingList<RaceDataModel> raceData, int numCand, bool CandSelect)
        {
            //Example of a 2 way raceboard with the Dem candidate winning and adding a gain

            //USGOV99991 ^ USGOV99992 ~state = New York; race = CD02; precincts = 10; office = house; racemode = 1 ; evdel = 32 ~
            //name = candidate1; party = 0; incum = 0; vote = 3000; percent = 23.4; check = 0; gain = 0; imagePath = George_Bush | 
            //name = candidate2; party = 1; incum = 0; vote = 5000; percent = 33.4; check = 1; gain = 1; imagePath = barack_obama

            //“raceMode” – 0 (not called), 1(race called), 2 (just called), 3(too close to call), 4 (runoff), 5 (race to watch)
            // "evdel" delegates for primaries electoral votes for general elect president 0 otherwise

            string mapKeyStr = "";

            // Gets either simulated or actual time based on flag
            DateTime timeNow = TimeFunctions.GetTime();
            DateTime pollClosingTime = raceData[0].RacePollClosingTime;

            RaceBoardModel raceBoardData = new RaceBoardModel();

            short stateNumber = stackElements[currentRaceIndex].State_Number;
            StateMetadataModel st = GetStateMetadata(stateNumber);
            //raceBoardData.state = raceData[0].StateName.Trim();
            raceBoardData.state = st.State_Name;

            if (raceData[0].cntyName.Length > 0 && raceData[0].cntyName != "ALL")
                raceBoardData.cd = raceData[0].cntyName;
            else if (raceData[0].CD == 0)
                raceBoardData.cd = string.Empty;
            else
                //raceBoardData.cd = $"HOUSE CD {raceData[0].CD.ToString()}";
                raceBoardData.cd = $"CD {raceData[0].CD.ToString()}";

            TimeSpan fifteenMinutes = new TimeSpan(0, 15, 0);
            bool candidateCalledWinner = false;
            DateTime raceCallTime = raceData[0].RaceWinnerCallTime;
            bool raceCalled = false;

            // get office strings
            raceBoardData.office = GetOfficeStr(raceData[0]);
            //raceBoardData.office = "CAUCUSES";


            if (raceData[0].Office != "H")
            {
                
                if (ApplicationSettingsFlagsCollection.UseExpectedVoteIn)
                {
                    if (raceData[0].PercentExpectedVote > 0 && raceData[0].PercentExpectedVote < 1)
                        raceBoardData.pctsReporting = "<1";
                    else
                        raceBoardData.pctsReporting = raceData[0].PercentExpectedVote.ToString("F0");
                }
                else
                {
                    //int temp = (int)(raceData[0].PrecinctsReporting * 100.0 / raceData[0].TotalPrecincts);
                    //if (temp > 0 && temp < 1)
                    //    raceBoardData.pctsReporting = "<1";
                    //else
                    //    raceBoardData.pctsReporting = temp.ToString();


                    float temp = (float)(raceData[0].PrecinctsReporting * 100.0 / raceData[0].TotalPrecincts);
                    if (temp > 0f && temp < 1f)
                        raceBoardData.pctsReporting = "<1";
                    else
                        raceBoardData.pctsReporting = temp.ToString("F0");
                }
            }
            else
            {
                //int temp = (int)(raceData[0].PrecinctsReporting * 100.0 / raceData[0].TotalPrecincts);
                //if (temp > 0 && temp < 1)
                //    raceBoardData.pctsReporting = "<1";
                //else
                //    raceBoardData.pctsReporting = temp.ToString();

                float temp = (float)(raceData[0].PrecinctsReporting * 100.0 / raceData[0].TotalPrecincts);
                if (temp > 0f && temp < 1f)
                    raceBoardData.pctsReporting = "<1";
                else
                    raceBoardData.pctsReporting = temp.ToString("F0");
            }

            if (numCand == 0)
                numCand = raceData.Count;

            int cand1 = stackElements[currentRaceIndex].Race_CandidateID_1;
            int cand2 = stackElements[currentRaceIndex].Race_CandidateID_2;
            int cand3 = stackElements[currentRaceIndex].Race_CandidateID_3;
            int cand4 = stackElements[currentRaceIndex].Race_CandidateID_4;
            int cand5 = stackElements[currentRaceIndex].Race_CandidateID_5;
            int candIndx = 0;

            for (int i = 0; i < numCand; i++)
            {
                candidateData_RB candidate = new candidateData_RB();

                if (CandSelect)
                {
                    int candID;
                    switch (i)
                    {
                        case 0:
                            candID = cand1;
                            break;
                        case 1:
                            candID = cand2;
                            break;
                        case 2:
                            candID = cand3;
                            break;
                        case 3:
                            candID = cand4;
                            break;
                        case 4:
                            candID = cand5;
                            break;
                        default:
                            candID = cand1;
                            break;

                    }

                    for (int n = 0; n < raceData.Count; n++)
                    {
                        if (raceData[n].CandidateID == candID)
                            candIndx = n; 
                    }
                }
                else
                    candIndx = i;

                mapKeyStr += raceData[candIndx].FoxID;
                if (i < numCand - 1)
                    mapKeyStr += "^";

                candidate.lastName = raceData[candIndx].CandidateLastName;
                candidate.firstName = raceData[candIndx].CandidateFirstName;

                if (Network == "FNC")
                {
                    if (raceData[candIndx].UseHeadshotFNC)
                    {
                        candidate.headshot = raceData[candIndx].HeadshotPathFNC;
                    }
                }
                else if (Network == "FBN")
                {

                    // Use FNC Headshot even for FBN for 2018 Midterms
                    if (raceData[candIndx].UseHeadshotFNC)
                    {
                        candidate.headshot = raceData[candIndx].HeadshotPathFNC;
                    }

                    /*
                    if (raceData[i].UseHeadshotFBN)
                    {
                        candidate.headshot = raceData[i].HeadshotPathFBN;
                    }
                    */

                }
                else if (Network == "FBC")
                {

                    // Use FNC Headshot even for FBN for 2018 Midterms
                    if (raceData[candIndx].UseHeadshotFNC)
                    {
                        candidate.headshot = raceData[candIndx].HeadshotPathFNC;
                    }

                    /*
                    if (raceData[i].UseHeadshotFBN)
                    {
                        candidate.headshot = raceData[i].HeadshotPathFBN;
                    }
                    */

                }
                // filename only, no path, no extension
                candidate.headshot = Path.GetFileNameWithoutExtension(candidate.headshot);

                // Format vote count as string with commas
                if (raceData[candIndx].CandidateVoteCount > 0)
                    //candidate.votes = string.Format("{0:n0}", raceData[i].CandidateVoteCount);
                    candidate.votes = raceData[candIndx].CandidateVoteCount.ToString();
                else
                    candidate.votes = " ";

                // get candidate percent str
                candidate.percent = GetCandPercent(raceData[candIndx]);
                // Get scene Party Id number from party str
                candidate.party = GetCandParty(raceData[candIndx]);

                // Set candidate incumbent flag
                candidate.incumbent = raceData[candIndx].IsIncumbentFlag == "Y" ? "1" : "0";
                var winnerCandidateId = 0;


                if ((timeNow >= pollClosingTime || PollClosinglockout == false) && raceData[candIndx].cntyName == "ALL")
                {
                    //Check for AP race call
                    if (raceData[candIndx].RaceUseAPRaceCall)
                    {
                        //Check for AP called winner
                        if (raceData[candIndx].cStat.ToUpper() == "W")
                        {
                            raceCalled = true;
                            candidateCalledWinner = true;
                            var raceCallTimeStr = raceData[candIndx].estTS;
                            winnerCandidateId = raceData[candIndx].CandidateID;

                            raceCallTime = GetApRaceCallDateTime(raceCallTimeStr);
                        }
                        else
                        {
                            candidateCalledWinner = false;
                        }
                    }
                    //Check for Fox race call
                    else
                    {
                        winnerCandidateId = raceData[candIndx].RaceWinnerCandidateID;
                        if (raceData[candIndx].RaceWinnerCalled)
                        {
                            raceCalled = true;

                            // If the race was not called by AP, use the Race_WinnerCallTime from the DB
                            raceCallTime = raceData[candIndx].RaceWinnerCallTime;
                            candidateCalledWinner = (winnerCandidateId == raceData[candIndx].RaceWinnerCandidateID);
                        }
                        else
                        {
                            raceCalled = false;
                            candidateCalledWinner = false;
                        }
                    }
                }
                else
                {
                    candidateCalledWinner = false;
                }

                if (winnerCandidateId == raceData[candIndx].CandidateID)
                {
                    candidate.winner = "1";
                    candidate.gain = GetGainFlag(raceData[candIndx]);
                }
                else
                {
                    candidate.winner = "0";
                    candidate.gain = "0";

                }
                raceBoardData.candData.Add(candidate);

            }

            // Set board mode 
            if (raceCalled)
            {

                // if race is called before the polls are closed time then use poll closing time as the race call time
                if (raceCallTime < pollClosingTime)
                    raceCallTime = pollClosingTime;

                // if race is called within 15 minutes 
                if ((timeNow - raceCallTime) < fifteenMinutes)
                    // then Just Called
                    raceBoardData.mode = (int)BoardModes.Race_Board_Just_Called;
                else
                    // Race Called
                    raceBoardData.mode = (int)BoardModes.Race_Board_Race_Called;
            }
            else if (raceData[0].RaceTooCloseToCall)
            {
                // RaceTooCloseToCall flag is set
                raceBoardData.mode = (int)BoardModes.Race_Board_To_Close_To_Call;
            }
            else if (raceData[0].RaceIsRunoff)
            {
                // RaceIsRunoff flag is set
                raceBoardData.mode = (int)BoardModes.Race_Board_Runoff;
            }
            else
            {
                // Not called and not TCTC
                raceBoardData.mode = (int)BoardModes.Race_Board_Normal;
            }

            if (electionMode == "Primary")
            {
                if (raceData[0].eType == "D" || raceData[0].eType == "E")
                    raceBoardData.evdel = raceData[0].DemDelegatesAvailable;
                else if (raceData[0].eType == "R" || raceData[0].eType == "S")
                    raceBoardData.evdel = raceData[0].RepDelegatesAvailable;
            }
            else if (electionMode != "Primary" && raceData[0].Office == "P")
            {
                raceBoardData.evdel = raceData[0].ElectoralVotesAvailable;
            }
            else
                raceBoardData.evdel = 0;




            //USGOV99991 ^ USGOV99992 ~state = New York; race = CD02; precincts = 10; office = house; racemode = 1 ; evdel = 32 ~
            //name = candidate1; party = 0; incum = 0; vote = 3000; percent = 23.4; check = 0; gain = 0; imagePath = George_Bush | 
            //name = candidate2; party = 1; incum = 0; vote = 5000; percent = 33.4; check = 1; gain = 1; imagePath = barack_obama

            //“raceMode” – 0 (not called), 1(race called), 2 (just called), 3(too close to call), 4 (runoff), 5 (race to watch)
            // "evdel" delegates for primaries electoral votes for general elect president 0 otherwise


            //USGOV99991^ USGOV99992 ~ state=New York; race=CD02;precincts=10 ; office=house; racemode=1 ; evdel=32 ~ name=candidate1; party=0; incum=0; vote=3000; percent=23.4 ; check=0; gain=0; imagePath= George_Bush |name=candidate2; party=1; incum=0; vote=5000; percent=33.4 ; check=1; gain=1; imagePath= barack_obama

            string raceblock = $"~state={raceBoardData.state};race={raceBoardData.cd};precincts={raceBoardData.pctsReporting};office={raceBoardData.office};racemode={raceBoardData.mode};evdel={raceBoardData.evdel}~";

            //string raceblock = $"~state={raceBoardData.state};race=;precincts={raceBoardData.pctsReporting};office={raceBoardData.office};racemode={raceBoardData.mode};evdel={raceBoardData.evdel}~";

            mapKeyStr += raceblock;
            
            for (int i = 0; i < numCand; i++)
            {
                if (UseCandidateFirstName)
                    mapKeyStr += $"name={raceBoardData.candData[i].firstName} {raceBoardData.candData[i].lastName};party={raceBoardData.candData[i].party};incum={raceBoardData.candData[i].incumbent};";
                else
                    mapKeyStr += $"name={raceBoardData.candData[i].lastName};party={raceBoardData.candData[i].party};incum={raceBoardData.candData[i].incumbent};";
                mapKeyStr += $"vote={raceBoardData.candData[i].votes};percent={raceBoardData.candData[i].percent};check={raceBoardData.candData[i].winner};gain={raceBoardData.candData[i].gain};";
                mapKeyStr += $"imagePath={raceBoardData.candData[i].headshot}";
                if (i < numCand - 1)
                    mapKeyStr += "^";

            }
            return mapKeyStr;

        }

        private string GetCandPercent(RaceDataModel rd)
        {
            string pct = "";
            double pctVote;
            Boolean ShowTenths = ApplicationSettingsFlagsCollection.ShowTenthsofPercent;

            if (rd.CandidateVoteCount > 0)
            {
                pctVote = (rd.CandidateVoteCount * 100.0) / rd.TotalVoteCount;

                if (pctVote < 1.0)
                {
                    pct = "<1";
                }
                else if (ShowTenths)
                    // showing tenths
                    pct = string.Format("{0:0.0}", pctVote);
                else
                    // show no decimal 
                    pct = string.Format("{0:0}", Math.Round(pctVote, MidpointRounding.AwayFromZero));

            }
            else
                pct = " ";

            return pct;
        }
        private string GetCandParty(RaceDataModel rd)
        {
            string party = " ";
            if (rd.CandidatePartyID == "Rep")
                party = "0";
            else if (rd.CandidatePartyID == "Dem")
                party = "1";
            //else if (rd.CandidatePartyID == "Lib")
                //party = "3";
            else
                party = "2";

            return party;

        }
        private string GetOfficeStr(RaceDataModel rd)
        {

            string raceOfficeStr = " ";
            // Add race descriptor
            // 05/03/2018 Modified to support non-presidential primary
            if (isNonPresidentialPrimary)
            {
                // 05/02/2018 Added support for non-presidential primary - concatenate state + office
                if (rd.Office == "G")
                {
                    raceOfficeStr = "GOVERNOR";
                }
                else if (rd.Office == "L")
                {
                    raceOfficeStr = "LT. GOVERNOR";
                }
                else if ((rd.Office == "S") | (rd.Office == "S2"))
                {
                    raceOfficeStr = "SENATE";
                }
                else if (rd.Office == "H")
                {
                    if (rd.IsAtLargeHouseState)
                    {
                        raceOfficeStr = "HOUSE AT LARGE";
                    }
                    else
                    {
                        //raceOfficeStr = "U.S. House CD " + RaceDistrict.ToString();
                        //raceOfficeStr = "HOUSE CD " + rd.CD.ToString();
                        //raceOfficeStr = "HOUSE CD ";
                        raceOfficeStr = "HOUSE";
                    }
                }
            }
            else
            {
                if (tabIndex == 4)
                {
                    //Dem primary
                    if (rd.eType == "D")
                        raceOfficeStr = "PRIMARY";
                    //Rep primary
                    else if (rd.eType == "R")
                        raceOfficeStr = "PRIMARY";
                    //Dem caucuses
                    else if (rd.eType == "E")
                        raceOfficeStr = "CAUCUSES";
                    //Rep caucuses
                    else if (rd.eType == "S")
                        raceOfficeStr = "CAUCUSES";
                    // Not a primary or caucus event - build string based on office type
                    else
                    {
                        if (rd.Office == "P")
                            raceOfficeStr = "PRESIDENT";
                        else if (rd.Office == "G")
                            raceOfficeStr = "GOVERNOR";
                        else if (rd.Office == "L")
                            raceOfficeStr = "LT. GOVERNOR";
                        else if ((rd.Office == "S") | (rd.Office == "S2"))
                            raceOfficeStr = "SENATE";
                        else if (rd.Office == "H")
                        {
                            if (rd.IsAtLargeHouseState)
                                raceOfficeStr = "HOUSE AT LARGE";
                            else
                            {
                                //MD Modified 03/05/2018 to support 2018 primaries
                                //raceOfficeStr = "U.S. House CD " + RaceDistrict.ToString();
                                //raceOfficeStr = "HOUSE CD " + rd.CD.ToString();
                                //raceOfficeStr = "HOUSE CD ";
                                raceOfficeStr = "HOUSE";
                            }
                        }
                    }
                }
                else
                {
                    //Dem primary
                    if (rd.eType == "D")
                        raceOfficeStr = "DEMOCRATIC PRIMARY";
                    //Rep primary
                    else if (rd.eType == "R")
                        raceOfficeStr = "REPUBLICAN PRIMARY";
                    //Dem caucuses
                    else if (rd.eType == "E")
                        raceOfficeStr = "DEMOCRATIC CAUCUSES";
                    //Rep caucuses
                    else if (rd.eType == "S")
                        raceOfficeStr = "REPUBLICAN CAUCUSES";
                    // Not a primary or caucus event - build string based on office type
                    else
                    {
                        if (rd.Office == "P")
                            raceOfficeStr = "PRESIDENT";
                        else if (rd.Office == "G")
                            raceOfficeStr = "GOVERNOR";
                        else if (rd.Office == "L")
                            raceOfficeStr = "LT. GOVERNOR";
                        else if ((rd.Office == "S") | (rd.Office == "S2"))
                            raceOfficeStr = "SENATE";
                        else if (rd.Office == "H")
                        {
                            if (rd.IsAtLargeHouseState)
                                raceOfficeStr = "HOUSE AT LARGE";
                            else
                            {
                                //MD Modified 03/05/2018 to support 2018 primaries
                                //raceOfficeStr = "U.S. House CD " + RaceDistrict.ToString();
                                //raceOfficeStr = "HOUSE CD " + rd.CD.ToString();
                                raceOfficeStr = "HOUSE";
                            }
                        }
                    }
                }
            }
            return raceOfficeStr;
        }

        private string GetGainFlag(RaceDataModel rd)
        {
            bool gainFlag = false;
            // Do gain flag
            if (!rd.RaceIgnoreGain)
            {
                if ((rd.InIncumbentPartyFlag == "N") && ((rd.Office.Trim() == "H") || (rd.Office.Trim() == "S") || (rd.Office.Trim() == "S2")))
                {
                    gainFlag = true;
                }
                else
                {
                    gainFlag = false;
                }
            }
            else
            {
                gainFlag = false;
            }
            if (gainFlag)
                return "1";
            else
                return "0";
        }
        /// <summary>
        /// Method to convert the AP race call time from a string to a datetime value
        /// </summary>
        public static DateTime GetApRaceCallDateTime(String dateTimeStr)
        {
            DateTime apRaceCallDateTimeStr = new DateTime();

            try
            {
                // Convert formatted string to date time
                apRaceCallDateTimeStr = DateTime.ParseExact(dateTimeStr, "yyyyMMdd HHmmtt", null);
            }
            catch (Exception ex)
            {
                log.Error("SimulatedDateTimeAccess Exception occurred while trying to convert string to DateTime value: " + ex.Message);
                //log.Debug("SimulatedDateTimeAccess Exception occurred while trying to convert string to DateTime value", ex);
            }
            return apRaceCallDateTimeStr;
        }
        #endregion

        #region Balance Of Power Data Processing
        public void TakeBOP()
        {
            currentRaceIndex = stackGrid.CurrentCell.RowIndex;
            int seType = (int)stackElements[currentRaceIndex].Stack_Element_Type;
            int seDataType = (int)stackElements[currentRaceIndex].Stack_Element_Data_Type;

            string ofc = "H";
            string office = "HOUSE";

            if ((int)stackElements[currentRaceIndex].Stack_Element_Type % 2 == 0)
            {
                ofc = "H";
                office = "HOUSE";

            }
            else
            {
                ofc = "S";
                office = "SENATE";

            }

            DateTime currentTime = TimeFunctions.GetTime();
            string currTime = currentTime.ToString();

            int BOPtion = (((int)stackElements[currentRaceIndex].Stack_Element_Type - 10) / 2);

            DataTable dt = new DataTable();
            BOPDataAccess bop = new BOPDataAccess();
            bop.ElectionsDBConnectionString = ElectionsDBConnectionString;

            int curNew = BOPtion;
            //if (BOPtion == 2)
            //    curNew = 1;

            if (BOPtion == 0)
            {

                dt = bop.GetBOPDataCurrent(ofc, currentTime);

                BOPDataModel BOPData = new BOPDataModel();
                DataRow row = dt.Rows[0];

                BOPData.Total = Convert.ToInt16(row["TOTAL_COUNT"]);

                BOPData.DemCurrent = Convert.ToInt16(row["DEM_BASELINE_COUNT"]);
                BOPData.RepCurrent = Convert.ToInt16(row["GOP_BASELINE_COUNT"]);
                BOPData.IndCurrent = Convert.ToInt16(row["IND_BASELINE_COUNT"]);

                BOPData.Branch = office;

                BOPData.Session = "CURRENT";
                BOPData.DemDelta = 0;
                BOPData.RepDelta = 0;
                BOPData.IndDelta = 0;
                BOPData.Control = GetBoPControlNumber(ofc);


                string outStr = GetBOPMapKeyStr(BOPData, ofc, BOPtion);
                SendToViz(outStr, seDataType);
            }
            else if (BOPtion == 1)
            {

                dt = bop.GetBOPDataNewGain(ofc, currentTime);

                BOPDataModel BOPData = new BOPDataModel();
                DataRow row = dt.Rows[0];

                BOPData.Total = Convert.ToInt16(row["TOTAL_COUNT"]);


                BOPData.DemNew = Convert.ToInt16(row["DEM_COUNT"]);
                BOPData.RepNew = Convert.ToInt16(row["GOP_COUNT"]);
                BOPData.IndNew = Convert.ToInt16(row["IND_COUNT"]);

                BOPData.DemGain = Convert.ToInt16(row["DEM_GAIN"]);
                BOPData.RepGain = Convert.ToInt16(row["GOP_GAIN"]);
                BOPData.IndGain = Convert.ToInt16(row["IND_GAIN"]);
                BOPData.Branch = office;

                BOPData.Session = "NEW";
                BOPData.Control = GetBoPControlNumber(ofc);


                string outStr = GetBOPMapKeyStr(BOPData, ofc, BOPtion);
                SendToViz(outStr, seDataType);
            }
            else
            {
                BOPGainModel BOPGain = new BOPGainModel();
                DataRow row;
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        ofc = "H";
                        dt = bop.GetBOPDataNewGain(ofc, currentTime);

                        row = dt.Rows[0];
                        BOPGain.HouseDem = Convert.ToInt16(row["DEM_COUNT"]);
                        BOPGain.HouseRep = Convert.ToInt16(row["GOP_COUNT"]);
                        BOPGain.HouseInd = Convert.ToInt16(row["IND_COUNT"]);

                        int HouseCtrl = 0;
                        if (BOPGain.HouseDem >= 218)
                            HouseCtrl = 1;
                        else if (BOPGain.HouseRep >= 218)
                            HouseCtrl = 2;
                    }
                    else
                    {
                        ofc = "S";
                        dt = bop.GetBOPDataNewGain(ofc, currentTime);

                        row = dt.Rows[0];
                        BOPGain.SenateDem = Convert.ToInt16(row["DEM_COUNT"]);
                        BOPGain.SenateRep = Convert.ToInt16(row["GOP_COUNT"]);
                        BOPGain.SenateInd = Convert.ToInt16(row["IND_COUNT"]);
                        
                        int SenateCtrl = 0;
                        if (BOPGain.SenateDem >= 51)
                            SenateCtrl = 1;
                        else if (BOPGain.SenateRep >= 51)
                            SenateCtrl = 2;
                    }
                }

                //sqlGetBoPControlNumber 

                
                // Get gain numbers for senate
                dt = bop.GetBOPDataNewGain("S", currentTime);

                row = dt.Rows[0];

                BOPGain.SenateDemGain = Convert.ToInt16(row["DEM_GAIN"]);
                BOPGain.SenateRepGain = Convert.ToInt16(row["GOP_GAIN"]);
                BOPGain.SenateIndGain = Convert.ToInt16(row["IND_GAIN"]);
                BOPGain.SenateCtrl = GetBoPControlNumber("S");

                // Get gain numbers for house
                dt = bop.GetBOPDataNewGain("H", currentTime);

                row = dt.Rows[0];

                BOPGain.HouseDemGain = Convert.ToInt16(row["DEM_GAIN"]);
                BOPGain.HouseRepGain = Convert.ToInt16(row["GOP_GAIN"]);
                BOPGain.HouseIndGain = Convert.ToInt16(row["IND_GAIN"]);
                BOPGain.HouseCtrl = GetBoPControlNumber("H");

                // SEND MAIN_SCENE*MAP SET_STRING_ELEMENT "BOP_DATA" NETGAIN ^ NEW ~HouseRep = 182 | HouseDem = 237 | HouseInd = 0 | HouseRepChange = -10 |
                //HouseDemChange = 10 | HouseIndChange = 0 | HouseControl = REPUBLICANS NEED + 17 | SenRep = 36 | SenDem = 46 | SenInd = 2 | SenateRepChange = -7 |
                //SenateDemChange = 7 | SenateIndChange = 0 | SenateControl = DEMOCRATS NEED + 4


                string outStr = GetBOPGainMapKeyStr(BOPGain);
                SendToViz(outStr, seDataType);

            }
            LiveUpdateTimer.Enabled = false;
            LiveUpdateTimer.Enabled = true;

        }

        public string GetBOPMapKeyStr(BOPDataModel BOPData, string ofc, int BOPtion)
        {
            //Variables – 
            //SENATE or HOUSE
            //CURRENT / NEW

            //BOP_DATA = SENATE ^ CURRENT~RepNum = 10 | RepNetChange = 10 | DemNum = 20 | DemNetChange = 20 | IndNum = 1 | IndNetChange = 1

            string MapKeyStr;
            string branchStr = "|SenateControl=";
            if (BOPData.Branch == "HOUSE")
                branchStr = "|HouseControl=";

            MapKeyStr = $"{BOPData.Branch}^{BOPData.Session}~";
            if (BOPtion == 0)
                MapKeyStr += $"RepNum={BOPData.RepCurrent}|RepNetChange={BOPData.RepDelta}|DemNum={BOPData.DemCurrent}|DemNetChange={BOPData.DemDelta}|IndNum={BOPData.IndCurrent}|IndNetChange={BOPData.IndDelta}";
            else
            {
                //MapKeyStr += $"RepNum={BOPData.RepNew}|RepNetChange={BOPData.RepGain}|DemNum={BOPData.DemNew}|DemNetChange={BOPData.DemGain}|IndNum={BOPData.IndNew}|IndNetChange={BOPData.IndGain}{branchStr}{ BOPData.Control}";
                //MapKeyStr += $"{ branchStr}{ BOPData.Control}";
                MapKeyStr += $"RepNum={BOPData.RepNew}|RepNetChange={BOPData.RepGain}|DemNum={BOPData.DemNew}|DemNetChange={BOPData.DemGain}|IndNum={BOPData.IndNew}|IndNetChange={BOPData.IndGain}";
                MapKeyStr += $"{branchStr}{BOPData.Control}";
            }
            return MapKeyStr;

        }

        public string GetBOPGainMapKeyStr(BOPGainModel BOPData)
        {

            //(for net gain)
            //BOP_DATA = NET_GAIN~HouseNums | SenNums

            //BOP_DATA = NETGAIN^NEW~House Rep Delta|House Dem Delta|House Ind Delta|Senate Rep Delta|Senate Dem Delta|Senate Ind Delta|
            string MapKeyStr;

            // BOP_DATA = NET_GAIN~party^HouseNum|party^SenNum

            // SEND MAIN_SCENE*MAP SET_STRING_ELEMENT "BOP_DATA" NETGAIN ^ NEW ~HouseRep = 182 | HouseDem = 237 | HouseInd = 0 | HouseRepChange = -10 |
            //HouseDemChange = 10 | HouseIndChange = 0 | HouseControl = REPUBLICANS NEED + 17 | SenRep = 36 | SenDem = 46 | SenInd = 2 | SenateRepChange = -7 |
            //SenateDemChange = 7 | SenateIndChange = 0 | SenateControl = DEMOCRATS NEED + 4


            MapKeyStr = $"NETGAIN^NEW~";
            MapKeyStr += $"HouseRep={BOPData.HouseRep}|HouseDem={BOPData.HouseDem}|HouseInd={BOPData.HouseInd}|";
            MapKeyStr += $"HouseRepChange={BOPData.HouseRepGain}|HouseDemChange={BOPData.HouseDemGain}|HouseIndChange={BOPData.HouseIndGain}|HouseControl={BOPData.HouseCtrl}|";
            MapKeyStr += $"SenateRep={BOPData.SenateRep}|SenateDem={BOPData.SenateDem}|SenateInd={BOPData.SenateInd}|";
            MapKeyStr += $"SenateRepChange={BOPData.SenateRepGain}|SenateDemChange={BOPData.SenateDemGain}|SenateIndChange={BOPData.SenateIndGain}|SenateControl={BOPData.SenateCtrl}";


            return MapKeyStr;

        }

        public string GetBoPControlNumber(string ofc)
        {
            DataTable dataTable = new DataTable();
            string BoPCntrl = "";

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(ElectionsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = SQLCommands.sqlGetBoPControlNumber;
                            cmd.Parameters.Add("@OfficeCode", SqlDbType.Text).Value = ofc;

                            sqlDataAdapter.SelectCommand = cmd;
                            sqlDataAdapter.SelectCommand.Connection = connection;
                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                            // Fill the datatable from adapter
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }

                DataRow row;

                if (dataTable.Rows.Count > 0)
                {
                    row = dataTable.Rows[0];
                    BoPCntrl = row["ControlString"].ToString() ?? "";
                }
                
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("AvailableRaceAccess Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return BoPCntrl;
        }



        #endregion

        #region Referendums Data Processing
        public void TakeReferendums()
        {
            //Get the selected race list object
            currentRaceIndex = stackGrid.CurrentCell.RowIndex;
            short stateNumber = stackElements[currentRaceIndex].State_Number;
            string raceOffice = stackElements[currentRaceIndex].Office_Code;
            int seDataType = (int)stackElements[currentRaceIndex].Stack_Element_Data_Type;

            referendumsDataCollection = new ReferendumsDataCollection();
            referendumsDataCollection.ElectionsDBConnectionString = ElectionsDBConnectionString;
            referendumsData = referendumsDataCollection.GetReferendumsDataCollection(stateNumber, raceOffice);

            if (referendumsData[0].TotalVotes > 0)
            {

                referendumsData[0].VotePct = (Int32)referendumsData[0].VoteCount * 100 / referendumsData[0].TotalVotes;
                referendumsData[1].VotePct = (Int32)referendumsData[1].VoteCount * 100 / referendumsData[1].TotalVotes;
            }
            else
            {
                referendumsData[0].VotePct = 0;
                referendumsData[1].VotePct = 0;

            }

            string outStr = "";

            if (referendumsData.Count >= 2)
                outStr = GetReferendumsMapKeyStr(referendumsData);

            SendToViz(outStr, seDataType);


        }

        public string GetReferendumsMapKeyStr(BindingList<ReferendumDataModel> refData)
        {
            string MapKeyStr = $"{refData[0].StateName}|{refData[0].PropRefID}|{refData[0].Description}|{refData[0].Detailtext}|";


            if (refData[0].WinnerCalled)
            {
                MapKeyStr += $"{(refData[0].WinnerCandidateID - 1)}|";
            }
            else
                MapKeyStr += $"0|";


            /*
            if (refData[0].WinnerCalled)
            {
                MapKeyStr += $"1|";
            }
            else if (refData[1].WinnerCalled)
            {
                MapKeyStr += $"2|";
            }
            else
                MapKeyStr += $"0|";

            */

            MapKeyStr += $"{refData[0].VotePct}| |{refData[1].VotePct}| |";

            return MapKeyStr;
        }
        #endregion

        #region Voter Analysis Data Processing
        public void TakeVoterAnalysis()
        {
            //Get the selected race list object
            currentRaceIndex = stackGrid.CurrentCell.RowIndex;
            string VA_Data_Id = stackElements[currentRaceIndex].VA_Data_ID;
            string r_type = stackElements[currentRaceIndex].VA_Type;
            short stateNumber = stackElements[currentRaceIndex].State_Number;
            string raceOffice = stackElements[currentRaceIndex].Office_Code;
            int seDataType = (int)stackElements[currentRaceIndex].Stack_Element_Data_Type;
            int ft = tcVoterAnalysis.SelectedIndex;

            string outStr = "";

            if (r_type == "M")
            {
                var voterAnalysisManualData = VoterAnalysisDataCollection.GetVoterAnalysisManualDataCollection(VA_Data_Id, ElectionsDBConnectionString);
                if (voterAnalysisManualData.Count >= 2)
                    outStr = GetVoterAnalysisManualMapKeyStr(voterAnalysisManualData);
                else
                    MessageBox.Show("No data to show.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                var voterAnalysisData = VoterAnalysisDataCollection.GetVoterAnalysisDataCollection(r_type, VA_Data_Id, ElectionsDBConnectionString, ft);
                if (voterAnalysisData.Count >= 2)
                    outStr = GetVoterAnalysisMapKeyStr(voterAnalysisData);
                else
                    MessageBox.Show("No data to show.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



            SendToViz(outStr, seDataType);


        }

        public List<MapMetaDataModelNew> getMapMetaData(string pk)
        {
            DataTable dt = new DataTable();
            string cmd = $"SELECT * FROM FE_VoterAnalysis_Map_Defs_New Where VA_Data_ID = '{pk}'";
            dt = GetDBData(cmd, ElectionsDBConnectionString);

            int numBands = dt.Rows.Count;

            List<MapMetaDataModelNew> mmData = new List<MapMetaDataModelNew>();

            foreach (DataRow row in dt.Rows)
            {
                MapMetaDataModelNew mmd = new MapMetaDataModelNew();

                mmd.VA_Data_Id = row["VA_Data_Id"].ToString();
                mmd.color = row["Color"].ToString();
                mmd.colorIndex = Convert.ToInt32(row["ColorIndex"]);
                mmd.colorValue = Convert.ToInt32(row["ColorValue"]);
                mmd.numColorBands = Convert.ToInt32(row["NumBands"]);
                mmd.bandLo = Convert.ToInt32(row["BandLo"]);
                mmd.bandHi = Convert.ToInt32(row["BandHi"]);
                mmd.bandLabel = row["BandLbl"].ToString();
                mmd.Title = row["Title"].ToString();

                mmData.Add(mmd);
            }

            return mmData;
        }

        public string getMapColor(string pk)
        {
            DataTable dt = new DataTable();
            string cmd = $"SELECT * FROM FE_VoterAnalysis_Map_Defs_New Where VA_Data_ID = '{pk}'";
            dt = GetDBData(cmd, ElectionsDBConnectionString);

            int numBands = dt.Rows.Count;

            List<MapMetaDataModelNew> mmData = new List<MapMetaDataModelNew>();

            foreach (DataRow row in dt.Rows)
            {
                MapMetaDataModelNew mmd = new MapMetaDataModelNew();

                mmd.VA_Data_Id = row["VA_Data_Id"].ToString();
                mmd.color = row["Color"].ToString();
                mmd.colorIndex = Convert.ToInt32(row["ColorIndex"]);
                mmd.colorValue = Convert.ToInt32(row["ColorValue"]);
                mmd.numColorBands = Convert.ToInt32(row["NumBands"]);
                mmd.bandLo = Convert.ToInt32(row["BandLo"]);
                mmd.bandHi = Convert.ToInt32(row["BandHi"]);
                mmd.bandLabel = row["BandLbl"].ToString();
                mmd.Title = row["Title"].ToString();

                mmData.Add(mmd);
            }
            string color = mmData[0].color;

            return color;
        }

        public void TakeVoterAnalysisMaps()
        {
            //Get the selected race list object
            currentRaceIndex = stackGrid.CurrentCell.RowIndex;
            string VA_Data_Id = stackElements[currentRaceIndex].VA_Data_ID;
            string r_type = stackElements[currentRaceIndex].VA_Type;
            string color = stackElements[currentRaceIndex].VA_Map_Color;
            int colorNum = stackElements[currentRaceIndex].VA_Map_ColorNum;
            int seDataType = (int)stackElements[currentRaceIndex].Stack_Element_Data_Type;


            //List<VoterAnalysisMapLegendDefModel> mapLegend = new List<VoterAnalysisMapLegendDefModel>();
            //mapLegend = GetLegend(VA_Data_Id);

            List<MapMetaDataModelNew> mapLegend = new List<MapMetaDataModelNew>();
            mapLegend = getMapMetaData(VA_Data_Id);


            List<VoterAnalysisMapDataModel> mapdata = new List<VoterAnalysisMapDataModel>();
            //mapdata = GetVoterAnalysisMapData(VA_Data_Id, colorNum);
            mapdata = GetVoterAnalysisMapData(VA_Data_Id, mapLegend);
            int mapID = mapdata[0].MapId;



            //SqlConnection connection1 = new SqlConnection(Properties.Settings.Default.ElectionsDBConnectionString);
            SqlConnection connection1 = new SqlConnection(ElectionsDBConnectionString);
            connection1.Open();

            //SqlCommand cmd1 = new SqlCommand($"getFE_VoterAnalysis_MapData_MissingStates {quot}{VA_Data_Id}{quot}", connection1);
            SqlCommand cmd1 = new SqlCommand($"getFE_VoterAnalysis_MapData_MissingStates_New {quot}{VA_Data_Id}{quot}", connection1);
            SqlDataReader sqlData = cmd1.ExecuteReader();

            bool missingStateFlag = false;
            List<string> missingStateList = new List<string>();
            while (sqlData.Read())
            {
                missingStateList.Add(sqlData[0].ToString());
                missingStateFlag = true;
            }
            sqlData.Close();
            

            // Final assembly of command strings with map-keys


            string outStr = "";
            //vizCmd = $"SEND SCENE*{sceneName}*MAP SET_STRING_ELEMENT {quot}BOP_DATA{quot} {cmd}{term}";

            outStr = GetVoterAnalysisMapDataMapKeyStr(mapdata,missingStateList);
            //string mapDataCommand = $"SEND MAIN_SCENE*MAP SET_STRING_ELEMENT {quot}VOTER_DATA{quot} {outStr}";
            string mapDataCommand = $"{quot}VOTER_DATA{quot} {outStr}{term}";
            SendToViz2(mapDataCommand, seDataType);

            outStr = GetVoterAnalysisMapLegendMapKeyStr(mapLegend, colorNum, missingStateFlag);
            //string legendDataCommand = $"SEND MAIN_SCENE*MAP SET_STRING_ELEMENT {quot}VOTER_INFO{quot} {outStr}";
            string legendDataCommand = $"{quot}VOTER_INFO{quot} {outStr}{term}";
            SendToViz2(legendDataCommand, seDataType);

            

            //SqlCommand cmd2 = new SqlCommand($"SELECT answer, [percent] FROM FE_VoterAnalysisData_Map WHERE VA_Data_Id = '{VA_Data_Id}' AND state = 'US'", connection1);
            SqlCommand cmd2 = new SqlCommand($"SELECT answer, [percent] FROM FE_VoterAnalysisData_Map_New WHERE VA_Data_Id = '{VA_Data_Id}' AND state = 'US'", connection1);
            cmd2.CommandType = CommandType.Text;

            SqlDataReader sqlData1 = cmd2.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(sqlData1);

            DataRow row;
            row = dt1.Rows[0];

            string Title = row["answer"].ToString().Trim();
            int nat = Convert.ToInt32(row["percent"]);


            outStr = $"{nat}%";
            //string nationalDataCommand = $"SEND MAIN_SCENE*MAP SET_STRING_ELEMENT {quot}VOTER_BOX{quot} {outStr}";
            string nationalDataCommand = $"{quot}VOTER_BOX{quot} {outStr}{term}";
            SendToViz2(nationalDataCommand, seDataType);


            //outStr = $"{mapLegend[0].Title}";
            outStr = $"{Title}";
            //string titleCommand = $"SEND MAIN_SCENE*MAP SET_STRING_ELEMENT {quot}VOTER_TITLE{quot} {outStr}";
            string titleCommand = $"{quot}VOTER_TITLE{quot} {outStr}{term}";
            SendToViz2(titleCommand, seDataType);

            connection1.Close();


        }

        //public List<VoterAnalysisMapDataModel> GetVoterAnalysisMapData(string VA_Data_Id, int colorSet, List<VoterAnalysisMapLegendDefModel> mapLegend)
        public List<VoterAnalysisMapDataModel> GetVoterAnalysisMapData(string VA_Data_Id, List<MapMetaDataModelNew> mapLegend)
        {


            SqlConnection connection1 = new SqlConnection(ElectionsDBConnectionString);
            connection1.Open();


            SqlCommand cmd2 = new SqlCommand();
            //cmd2 = new SqlCommand($"SELECT state, [percent] FROM FE_VoterAnalysisData_Map WHERE VA_Data_Id = '{VA_Data_Id}' AND state != 'US'", connection1);
            cmd2 = new SqlCommand($"SELECT state, [percent], breakpoint FROM FE_VoterAnalysisData_Map_New WHERE VA_Data_Id = '{VA_Data_Id}' AND state != 'US'", connection1);
            cmd2.CommandType = CommandType.Text;

            SqlDataReader sqlData2 = cmd2.ExecuteReader();
            
            DataTable dt2 = new DataTable();
            
            dt2.Load(sqlData2);
            
            List<VoterAnalysisMapDataModel> mapdata = new List<VoterAnalysisMapDataModel>();
            DataRow row;

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                VoterAnalysisMapDataModel md = new VoterAnalysisMapDataModel();
                
                row = dt2.Rows[i];
                md.State = row["state"].ToString().Trim();
                md.Percent = Convert.ToInt32(row["percent"]);

                //md.RowNumber = GetRowNumber(md.Percent, bandHi, bandLo);
                //md.RowNumber = GetRowNumber(md.Percent, mapLegend);
                md.RowNumber = Convert.ToInt32(row["breakpoint"]);

                //md.RowLabel = $"{bandLo[md.RowNumber]}% - {bandHi[md.RowNumber]}%";
                //md.RowLabel = $"{mapLegend[md.RowNumber].bandLo}% - {mapLegend[md.RowNumber].bandHi}%";

                md.MapId = 0;

                //md.Color = GetColor(colorSet, md.RowNumber);
                md.Color = mapLegend[md.RowNumber].colorValue;
                md.RowLabel = mapLegend[md.RowNumber].bandLabel;

                int pos;
                switch (md.RowNumber + 1)
                {
                    case 1:
                        pos = -5;
                        break;

                    case 2:
                        pos = -2;
                        break;

                    case 3:
                        pos = 2;
                        break;

                    case 4:
                        pos = 5;
                        break;

                    default:
                        pos = 0;
                        break;
                            
                }

                md.Position = pos;

                mapdata.Add(md);

            }
            connection1.Close();

            return mapdata;
        }

        public int GetRowNumber2(int percent, int[] bandHi, int[] bandLo)
        {
            for (int i = 0; i < 4; i++)
            {
                if (percent >= bandLo[i] && percent <= bandHi[i])
                    return i;
            }
            return -1;
        }
        public int GetRowNumber(int percent, List<VoterAnalysisMapLegendDefModel> mapLegend)
        {
            for (int i = 0; i < mapLegend.Count; i++)
            {
                if (percent >= mapLegend[i].bandLo && percent <= mapLegend[i].bandHi)
                    return i;
            }
            return -1;
        }


        public int GetColor(int colorSet, int RowNum)
        {
            SqlConnection connection1 = new SqlConnection(ElectionsDBConnectionString);
            connection1.Open();
            SqlCommand cmd1 = new SqlCommand($"SELECT ColorValue FROM FE_VoterAnalysis_Map_Color_Sets WHERE ColorSetID = '{colorSet}' AND ColorIndex = '{RowNum + 1}'", connection1);
            cmd1.CommandType = CommandType.Text;

            SqlDataReader sqlData1 = cmd1.ExecuteReader();

            DataTable dt1 = new DataTable();
            dt1.Load(sqlData1);
            DataRow row;
            row = dt1.Rows[0];
            int color = Convert.ToInt32(row["ColorValue"]);
            connection1.Close();

            return color;

            
        }

        public List<VoterAnalysisMapLegendDefModel> GetLegend(string VA_Data_Id)
        {
            SqlConnection connection1 = new SqlConnection(ElectionsDBConnectionString);
            connection1.Open();

            SqlCommand cmd1 = new SqlCommand($"SELECT answer, [percent] FROM FE_VoterAnalysisData_Map WHERE VA_Data_Id = '{VA_Data_Id}' AND state != 'US' ORDER BY [percent] ASC", connection1);
            cmd1.CommandType = CommandType.Text;

            SqlDataReader sqlData1 = cmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(sqlData1);

            List<double> mapPercent = new List<double>();
            DataRow row;
            row = dt1.Rows[0];

            string Title = row["answer"].ToString().Trim();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                double md;

                row = dt1.Rows[i];
                md = Convert.ToDouble(row["percent"]);

                mapPercent.Add(md);
            }

            StatFunctions sf = new StatFunctions();
            int n = mapPercent.Count;
            double max = sf.Max(mapPercent);
            double min = sf.Min(mapPercent);
            double sdev = sf.StandardDeviation(mapPercent);
            double mean = sf.Mean(mapPercent);
            double median = sf.Median(mapPercent);
            double sdevHi1 = median + sdev;
            double sdevLo1 = median - sdev;
            double sdevHi2 = median + 2 * sdev;
            double sdevLo2 = median - 2 * sdev;
            
            double range;
            double band = 0;
            int iBand;
            int iband1;
            int mid = Convert.ToInt32((max + min) / 2);
            int numBands = 4;
            int[] bandLo = new int[4];
            int[] bandHi = new int[4];
            int bandRef;


            List<VoterAnalysisMapLegendDefModel> mapLegend = new List<VoterAnalysisMapLegendDefModel>();

            //int mapOption = 2;
            int mapOption = Convert.ToInt32(numericUpDown1.Value);

            if (mapOption == 1)
            {

                numBands = 4;
                range = (max - min) / 4.0;
                band = Math.Truncate(range) + 1;
                iBand = Convert.ToInt32(band);
                iband1 = iBand + 1;

                bandLo[2] = Convert.ToInt32(mid);
                bandHi[2] = Convert.ToInt32(mid + band);

                bandLo[3] = bandHi[2] + 1;
                bandHi[3] = bandLo[3] + Convert.ToInt32(band);

                bandHi[1] = bandLo[2] - 1;
                bandLo[1] = bandHi[1] - Convert.ToInt32(band);

                bandHi[0] = bandLo[1] - 1;
                bandLo[0] = bandHi[0] - Convert.ToInt32(band);
                
            }


            //option2 
            if (mapOption == 2)
            {


                double mmm = max - min;

                range = (max - median) / 2.0;

                if ((median - min) / 2.0 > range)
                    range = (median - min) / 2.0;

                band = Math.Truncate(range) + 1;
                iBand = Convert.ToInt32(band);
                iband1 = iBand + 1;
                bandRef = Convert.ToInt32(median);

                // Check if data in all bands
                // i.e. check if 1st band < min or 4th band > max
                // then make 3 bands
                if ((bandRef - band - 2) < min || (bandRef + band + 1) > max)
                    numBands = 3;
                else
                    numBands = 4;


                if (numBands == 4)
                {
                    bandLo[2] = bandRef;
                    bandHi[2] = Convert.ToInt32(median + band);

                    bandLo[3] = bandHi[2] + 1;
                    bandHi[3] = bandLo[3] + Convert.ToInt32(band);

                    bandHi[1] = bandLo[2] - 1;
                    bandLo[1] = bandHi[1] - Convert.ToInt32(band);

                    bandHi[0] = bandLo[1] - 1;
                    bandLo[0] = bandHi[0] - Convert.ToInt32(band);
                }

                // skip first band
                if (numBands == 3 && (bandRef - band - 2) < min)
                {
                    bandLo[1] = bandRef;
                    bandHi[1] = Convert.ToInt32(median + band);

                    bandHi[0] = bandLo[1] - 1;
                    bandLo[0] = bandHi[0] - Convert.ToInt32(band);

                    bandLo[2] = bandHi[1] + 1;
                    bandHi[2] = bandLo[2] + Convert.ToInt32(band);
                }

                // skip top band
                if (numBands == 3 && (bandRef + band + 1) > max)
                {
                    bandLo[2] = bandRef;
                    bandHi[2] = Convert.ToInt32(median + band);

                    bandHi[1] = bandLo[2] - 1;
                    bandLo[1] = bandHi[1] - Convert.ToInt32(band);

                    bandHi[0] = bandLo[1] - 1;
                    bandLo[0] = bandHi[0] - Convert.ToInt32(band);
                }

            }

            if (mapOption == 3)
            {

                if (sdev > 3.5)
                    numBands = 4;
                else
                    numBands = 3;

                range = (max - min) / Convert.ToDouble(numBands);
                band = Math.Truncate(range) + 1;
                iBand = Convert.ToInt32(band);
                iband1 = iBand + 1;

                if (numBands == 4)
                {
                    bandLo[2] = Convert.ToInt32(mid);
                    bandHi[2] = Convert.ToInt32(mid + band);

                    bandLo[3] = bandHi[2] + 1;
                    bandHi[3] = bandLo[3] + Convert.ToInt32(band);

                    bandHi[1] = bandLo[2] - 1;
                    bandLo[1] = bandHi[1] - Convert.ToInt32(band);

                    bandHi[0] = bandLo[1] - 1;
                    bandLo[0] = bandHi[0] - Convert.ToInt32(band);
                }

                if (numBands == 3)
                {
                    bandLo[1] = Convert.ToInt32(mid) - (iBand / 2);
                    bandHi[1] = Convert.ToInt32(mid + band);

                    bandHi[0] = bandLo[1] - 1;
                    bandLo[0] = bandHi[0] - Convert.ToInt32(band);

                    bandLo[2] = bandHi[1] + 1;
                    bandHi[2] = bandLo[2] + Convert.ToInt32(band);
                }
                
            }

            for (int i = 0; i < numBands; i++)
            {
                VoterAnalysisMapLegendDefModel ml = new VoterAnalysisMapLegendDefModel();
                ml.bandHi = bandHi[i];
                ml.bandLo = bandLo[i];
                ml.legend = $"{bandLo[i]}% - {bandHi[i]}%";
                mapLegend.Add(ml);
            }

            sqlData1.Close();
            connection1.Close();

            log.Debug(" ");

            string logStr = $"Map: {VA_Data_Id} - {Title}";
            log.Debug(logStr);

            logStr = $"Std Dev: {sdev} -  Max : {max} - Min: {min} - Mean: {mean} - Median: {median} - Band {band}";
            log.Debug(logStr);

            logStr = $"{numBands} Bands : ";
            for (int i = 0; i < numBands; i++)
            {
                logStr += $"{bandLo[i]}% - {bandHi[i]}%, ";
            }


            log.Debug(logStr);

            return mapLegend;
        }


        public List<VoterAnalysisMapLegendModel> GetVoterAnalysisMapLegendData(int mapID)
        {
            // Get data for newly selected map - will want to do this just in time
            SqlConnection connection1 = new SqlConnection(ElectionsDBConnectionString);
            connection1.Open();
            SqlCommand cmd1 = new SqlCommand($"getFE_VoterAnalysis_MapLegend {mapID}", connection1);
            cmd1.CommandType = CommandType.Text;

            SqlDataReader sqlData1 = cmd1.ExecuteReader();

            DataTable dt1 = new DataTable();
            
            dt1.Load(sqlData1);
            
            List<VoterAnalysisMapLegendModel> mapdata = new List<VoterAnalysisMapLegendModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                VoterAnalysisMapLegendModel md = new VoterAnalysisMapLegendModel();
                DataRow row;

                row = dt1.Rows[i];
                md.Title = row["Title"].ToString().Trim();
                md.RowNumber = Convert.ToInt32(row["RowNumber"]);
                md.RowColor = Convert.ToInt32(row["RowColor"]);
                md.RowLabel = row["RowLabel"].ToString().Trim();
                
                mapdata.Add(md);

            }

            connection1.Close();

            return mapdata;
        }

        public string GetVoterAnalysisMapDataMapKeyStr(List<VoterAnalysisMapDataModel> mapData, List<string> missingStateList)
        {

            string outStr = string.Empty;
            
            // Build the base map data string
            if (mapData.Count > 0)
            {
                for (int i = 0; i < mapData.Count; i++)
                {
                    // Append delimiter
                    if (i > 0)
                    {
                        outStr += "|";
                    }
                    //Build data string
                    outStr += $"{mapData[i].State}^{mapData[i].Color}^{mapData[i].Position}";

                }
            }
            // Add the missing states so they get the neutral color
            if (missingStateList.Count > 0)
            {
                for (int i = 0; i < missingStateList.Count; i++)
                {
                    //Build data string - for all missing states set color and extrusion to 0 for default
                    outStr += $"|{missingStateList[i]}^0^0";
                }
            }

            return outStr;

        }
        public string GetVoterAnalysisMapLegendMapKeyStr(List<MapMetaDataModelNew> mapLegend, int colorSet, bool missingStateFlag)
        {
            string outStr = string.Empty;
            
            // Legend data string
            if (mapLegend.Count > 0)
            {
                for (int i = 0; i < mapLegend.Count; i++)
                {
                    // Append delimiter
                    if (i > 0)
                    {
                        outStr += "|";
                    }
                    // Build data for string
                    // Note that legend is numbered from top to bottom but values are assigned from bottom to top
                    //outStr += $"{mapLegend[mapLegend.Count - i - 1].RowNumber}^{mapLegend[i].RowColor}^{mapLegend[i].RowLabel}";
                    //outStr += $"{mapLegend.Count - i}^{GetColor(colorSet, i)}^{mapLegend[i].legend}";
                    outStr += $"{mapLegend.Count - i}^{mapLegend[i].colorValue}^{mapLegend[i].bandLabel}";

                }
                //// Send blanks for unused legend rows
                //if (mapLegend.Count == 4)
                //{
                //    outStr += "|5^0^NO STATE RACE";
                //}
                //else if (mapLegend.Count == 3)
                //{
                //    outStr += "|4^0^NO STATE RACE|5^^";
                //}
                
                // Send blanks for unused legend rows
                if (mapLegend.Count == 4 && missingStateFlag)
                {
                    if (missingStateFlag)
                        outStr += "|5^0^NO DATA";
                    else
                        outStr += "|5^^";
                }
                else if (mapLegend.Count == 3 && missingStateFlag)
                {
                    if (missingStateFlag)
                        outStr += "|4^0^NO DATA|5^^";
                    else
                        outStr += "|4^^|5^^";
                }
            }
            return outStr;

        }

        public string GetVoterAnalysisMapKeyStr(BindingList<VoterAnalysisDataModel> VA_Data)
        {

            //SEND SCENE* SceneName *MAP SET_STRING_ELEMENT "POLL_DATA" 
            // NumResponses|Title|Question|Issue_State|

            //Response1^Resonse2^Response3... | Percent1^Percent2^Percent3...

            string MapKeyStr = "";

            MapKeyStr = $"{VA_Data.Count}| |{VA_Data[0].Title}|{VA_Data[0].State}|";

            for (int i = 0; i < VA_Data.Count; i++)
            {
                if (i > 0)
                    MapKeyStr += "^";

                MapKeyStr += $"{VA_Data[i].Response}";

            }

            MapKeyStr += "|";

            for (int i = 0; i < VA_Data.Count; i++)
            {
                if (i > 0)
                    MapKeyStr += "^";

                MapKeyStr += $"{VA_Data[i].percent}%";
                //MapKeyStr += $"{VA_Data[i].percent}";

            }

            //MapKeyStr += $"|{VA_Data[0].asOf}|";
            MapKeyStr += $"| |";

            return MapKeyStr;
        }

        public string GetVoterAnalysisManualMapKeyStr(BindingList<VoterAnalysisManualDataModel> VA_Data)
        {

            //SEND SCENE* SceneName *MAP SET_STRING_ELEMENT "POLL_DATA" 
            // NumResponses|Title|Question|Issue_State|

            //Response1^Resonse2^Response3... | Percent1^Percent2^Percent3...

            string MapKeyStr = "";

            MapKeyStr = $"{VA_Data.Count}| |{VA_Data[0].Title}|{VA_Data[0].State}|";

            for (int i = 0; i < VA_Data.Count; i++)
            {
                if (i > 0)
                    MapKeyStr += "^";

                MapKeyStr += $"{VA_Data[i].Response}";

            }

            MapKeyStr += "|";

            for (int i = 0; i < VA_Data.Count; i++)
            {
                if (i > 0)
                    MapKeyStr += "^";

                MapKeyStr += $"{VA_Data[i].Percent}";

            }

            //MapKeyStr += $"|{VA_Data[0].asOf}|";
            MapKeyStr += $"| |";

            return MapKeyStr;
        }
        #endregion

        #region Voter Analysis grid methods
        private void tcVoterAnalysis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (builderOnlyMode == false)
            {

                //stackType = (short)(10 * (dataModeSelect.SelectedIndex + 1));
                stackType = (short)(10 * (tabIndex + 1));
                
                //if (dataModeSelect.SelectedIndex == 1)
                if (tabIndex == 1)
                {
                    if (tcVoterAnalysis.SelectedIndex == 1)
                        stackType += tcVoterAnalysis.SelectedIndex;
                }
            }
            else
                stackType = 0;

            stackType += stackTypeOffset;

            if (tcVoterAnalysis.SelectedIndex == 1 && VictoriaMode == false)
                btnSaveStack.Enabled = false;
            else
                btnSaveStack.Enabled = true;

            if (tcVoterAnalysis.SelectedIndex == 2)
            {
                GetVoterAnalysisManualGridData();
                dgvVoterAnalysis.Visible = false;
                dgvVAManual.Visible = true;
            }
            else
            {
                GetVoterAnalysisGridData();
                dgvVoterAnalysis.Visible = true;
                dgvVAManual.Visible = false;
            }
        }

        //public void GetVoterAnalysisGridData()
        //{
        //    int cnt = 0;
        //    if (tcVoterAnalysis.SelectedIndex == 0)
        //    {
        //        VA_Qdata_FS = GetVoterAnalysisQuestionsData(tcVoterAnalysis.SelectedIndex);
        //        dgvVoterAnalysis.DataSource = VA_Qdata_FS;
        //        cnt = VA_Qdata_FS.Count;
        //    }

        //    if (tcVoterAnalysis.SelectedIndex == 1)
        //    {
        //        VA_Qdata_Tkr = GetVoterAnalysisQuestionsData(tcVoterAnalysis.SelectedIndex);
        //        dgvVoterAnalysis.DataSource = VA_Qdata_Tkr;
        //        cnt = VA_Qdata_Tkr.Count;
        //    }

        //    dgvVoterAnalysis.Columns[0].Width = 35;
        //    dgvVoterAnalysis.Columns[1].Width = 32;
        //    dgvVoterAnalysis.Columns[2].Width = 80;
        //    dgvVoterAnalysis.Columns[3].Width = 60;
        //    dgvVoterAnalysis.Columns[4].Width = 30;
        //    dgvVoterAnalysis.Columns[5].Width = 150;
        //    dgvVoterAnalysis.Columns[6].Width = 150;

        //    dgvVoterAnalysis.Columns[0].HeaderText = "st";
        //    dgvVoterAnalysis.Columns[1].HeaderText = "ofc";
        //    dgvVoterAnalysis.Columns[2].HeaderText = "qcode";
        //    dgvVoterAnalysis.Columns[3].HeaderText = "filter";
        //    dgvVoterAnalysis.Columns[4].HeaderText = "qa";
        //    dgvVoterAnalysis.Columns[5].HeaderText = "question";
        //    dgvVoterAnalysis.Columns[6].HeaderText = "answer";

        //    lblVAcnt.Text = $"Voter Analysis Questions: {cnt}";

        //}

        public void GetVoterAnalysisGridData()
        {
            int cnt = 0;
            if (tcVoterAnalysis.SelectedIndex == 0)
            {
                VA_Qdata_FS = GetVoterAnalysisQuestionsDataNew(tcVoterAnalysis.SelectedIndex);
                dgvVoterAnalysis.DataSource = VA_Qdata_FS;
                cnt = VA_Qdata_FS.Count;

                dgvVoterAnalysis.Columns[0].Width = 35;
                dgvVoterAnalysis.Columns[1].Width = 32;
                dgvVoterAnalysis.Columns[2].Width = 30;
                dgvVoterAnalysis.Columns[3].Width = 140;
                dgvVoterAnalysis.Columns[4].Width = 80;
                dgvVoterAnalysis.Columns[5].Width = 350;

                dgvVoterAnalysis.Columns[0].HeaderText = "st";
                dgvVoterAnalysis.Columns[1].HeaderText = "ofc";
                dgvVoterAnalysis.Columns[2].HeaderText = "qa";
                dgvVoterAnalysis.Columns[3].HeaderText = "qcode";
                dgvVoterAnalysis.Columns[4].HeaderText = "filter";
                dgvVoterAnalysis.Columns[5].HeaderText = "question";

            }

            if (tcVoterAnalysis.SelectedIndex == 1)
            {
                VA_Qdata_Tkr = GetVoterAnalysisQuestionsDataNew(tcVoterAnalysis.SelectedIndex);
                dgvVoterAnalysisTicker.DataSource = VA_Qdata_Tkr;
                cnt = VA_Qdata_Tkr.Count;

                dgvVoterAnalysisTicker.Columns[0].Width = 35;
                dgvVoterAnalysisTicker.Columns[1].Width = 32;
                dgvVoterAnalysisTicker.Columns[2].Width = 30;
                dgvVoterAnalysisTicker.Columns[3].Width = 140;
                dgvVoterAnalysisTicker.Columns[4].Width = 80;
                dgvVoterAnalysisTicker.Columns[5].Width = 350;

                dgvVoterAnalysisTicker.Columns[0].HeaderText = "st";
                dgvVoterAnalysisTicker.Columns[1].HeaderText = "ofc";
                dgvVoterAnalysisTicker.Columns[2].HeaderText = "qa";
                dgvVoterAnalysisTicker.Columns[3].HeaderText = "qcode";
                dgvVoterAnalysisTicker.Columns[4].HeaderText = "filter";
                dgvVoterAnalysisTicker.Columns[5].HeaderText = "question";

            }


            lblVAcnt.Text = $"Voter Analysis Questions: {cnt}";

        }
        public void GetVoterAnalysisManualGridData()
        {
            int cnt = 0;
            VA_Qdata_Man = GetVoterAnalysisQuestionsManualDataNew();
            dgvVAManual.DataSource = VA_Qdata_Man;
            cnt = VA_Qdata_Man.Count;

            dgvVAManual.Columns[0].Width = 35;
            dgvVAManual.Columns[1].Width = 32;
            dgvVAManual.Columns[2].Width = 100;
            dgvVAManual.Columns[3].Width = 100;
            dgvVAManual.Columns[4].Width = 350;
            dgvVAManual.Columns[5].Width = 80;

            dgvVAManual.Columns[0].HeaderText = "st";
            dgvVAManual.Columns[1].HeaderText = "qa";
            dgvVAManual.Columns[2].HeaderText = "state";
            dgvVAManual.Columns[3].HeaderText = "race";
            dgvVAManual.Columns[4].HeaderText = "question";
            //dgvVoterAnalysis.Columns[5].HeaderText = "question";

            lblVAcnt.Text = $"Voter Analysis Questions: {cnt}";

        }


        public void GetVoterAnalysisMapGridData()
        {
            int cnt = 0;

            VA_Qdata_Map = GetVoterAnalysisMapQuestionsData();
            dgvVoterAnalysisMap.DataSource = VA_Qdata_Map;
            cnt = VA_Qdata_Map.Count;

            dgvVoterAnalysisMap.Columns[0].Width = 80;
            //dgvVoterAnalysisMap.Columns[1].Width = 820;
            dgvVoterAnalysisMap.Columns[1].Width = 400;
            dgvVoterAnalysisMap.Columns[2].Width = 300;
            dgvVoterAnalysisMap.Columns[3].Width = 120;
            dgvVoterAnalysisMap.Columns[4].Width = 350;
            dgvVoterAnalysisMap.Columns[5].Width = 30;

            dgvVoterAnalysisMap.Columns[0].HeaderText = "Color";
            dgvVoterAnalysisMap.Columns[1].HeaderText = "Answer";
            dgvVoterAnalysisMap.Columns[2].HeaderText = "Question";
            dgvVoterAnalysisMap.Columns[3].HeaderText = "Qcode";
            dgvVoterAnalysisMap.Columns[4].HeaderText = "VA_Data_Id";
            dgvVoterAnalysisMap.Columns[5].HeaderText = "M";
            
            lblMapCnt.Text = $"Voter Analysis Maps: {cnt}";

        }


        public List<VoterAnalysisQuestionsModel> GetVoterAnalysisQuestionsData(int type)
        {
            string cmd = "";
            if (type == 0)
                cmd = SQLCommands.sqlGetVoterAnalysisQuestions_FullScreen;
            else
                cmd = SQLCommands.sqlGetVoterAnalysisQuestions_Ticker;

            DataTable dt = GetDBData(cmd, ElectionsDBConnectionString);
            List<VoterAnalysisQuestionsModel> VA_Qdata = new List<VoterAnalysisQuestionsModel>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                VoterAnalysisQuestionsModel VA_Data = new VoterAnalysisQuestionsModel();

                VA_Data.VA_Data_Id = row["VA_Data_Id"].ToString().Trim();
                VA_Data.race_id = row["race_Id"].ToString().Trim();
                VA_Data.state = row["st"].ToString().Trim();
                VA_Data.qcode = row["qcode"].ToString().Trim();
                VA_Data.filter = row["filter"].ToString().Trim();
                VA_Data.r_type = row["r_type"].ToString().Trim();
                VA_Data.preface = row["preface"].ToString().Trim();
                VA_Data.question = row["Question"].ToString().Trim();
                VA_Data.answer = row["Answer"].ToString().Trim();


                string race = VA_Data.race_id;

                if (race != "US-all")
                {

                    // parse the header info
                    string[] strSeparator = new string[] { "-" };
                    string[] raceStrings;

                    // this takes the header and splits it into key-value pairs
                    raceStrings = race.Split(strSeparator, StringSplitOptions.None);

                    string raceID = raceStrings[2];
                    string stateAbbv = raceStrings[0];
                    string ofc = raceStrings[1];
                    VA_Data.ofc = ofc;

                }
                else
                    VA_Data.ofc = "N";

                VA_Qdata.Add(VA_Data);

            }

            return VA_Qdata;

        }


        public List<VoterAnalysisQuestionsModelNew> GetVoterAnalysisQuestionsDataNew(int type)
        {
            string cmd = "";
            if (type == 0)
                cmd = SQLCommands.sqlGetVoterAnalysisQuestions_FullScreen;
            else
                cmd = SQLCommands.sqlGetVoterAnalysisQuestions_Ticker;

            DataTable dt = GetDBData(cmd, ElectionsDBConnectionString);
            List<VoterAnalysisQuestionsModelNew> VA_Qdata = new List<VoterAnalysisQuestionsModelNew>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                VoterAnalysisQuestionsModelNew VA_Data = new VoterAnalysisQuestionsModelNew();

                //VA_Data.race_id = row["race_Id"].ToString().Trim();
                VA_Data.state = row["st"].ToString().Trim();
                VA_Data.qcode = row["qcode"].ToString().Trim();
                VA_Data.filter = row["filter"].ToString().Trim();
                VA_Data.r_type = row["r_type"].ToString().Trim();
                //VA_Data.preface = row["preface"].ToString().Trim();
                VA_Data.header = row["header"].ToString().Trim();
                VA_Data.VA_Data_Id = row["VA_Data_Id"].ToString().Trim();
                //VA_Data.answer = row["Answer"].ToString().Trim();


                string race = row["race_Id"].ToString().Trim();

                if (race != "US-all")
                {

                    // parse the header info
                    string[] strSeparator = new string[] { "-" };
                    string[] raceStrings;

                    // this takes the header and splits it into key-value pairs
                    raceStrings = race.Split(strSeparator, StringSplitOptions.None);

                    string raceID = raceStrings[2];
                    string stateAbbv = raceStrings[0];
                    string ofc = raceStrings[1];
                    VA_Data.ofc = ofc;

                }
                else
                    VA_Data.ofc = "N";

                VA_Qdata.Add(VA_Data);

            }

            return VA_Qdata;

        }

        public List<VoterAnalysisManualQuestionsModelNew> GetVoterAnalysisQuestionsManualDataNew()
        {
            string cmd = SQLCommands.sqlGetVoterAnalysisQuestions_Manual;
            DataTable dt = GetDBData(cmd, ElectionsDBConnectionString);
            List<VoterAnalysisManualQuestionsModelNew> VA_Qdata = new List<VoterAnalysisManualQuestionsModelNew>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                VoterAnalysisManualQuestionsModelNew VA_Data = new VoterAnalysisManualQuestionsModelNew();

                VA_Data.st = row["st"].ToString().Trim();
                VA_Data.r_type = row["r_type"].ToString().Trim();
                VA_Data.question = row["question"].ToString().Trim();
                VA_Data.state = row["State"].ToString().Trim();
                VA_Data.race = row["race"].ToString().Trim();
                VA_Data.VA_Data_Id = row["VA_Data_Id"].ToString().Trim();
                
                VA_Qdata.Add(VA_Data);
            }
            return VA_Qdata;
        }


        public List<VoterAnalysisMapQuestionsModel> GetVoterAnalysisMapQuestionsData()
        {
            string cmd = SQLCommands.sqlGetVoterAnalysisQuestions_Map_New;

            DataTable dt = GetDBData(cmd, ElectionsDBConnectionString);
            List<VoterAnalysisMapQuestionsModel> VA_MapQdata = new List<VoterAnalysisMapQuestionsModel>();
                        
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                VoterAnalysisMapQuestionsModel VA_Data = new VoterAnalysisMapQuestionsModel();
                string VA_Data_Id = row[3].ToString().Trim();

                List<MapMetaDataModelNew> mmData = getMapMetaData(VA_Data_Id);
                string color = mmData[0].color;

                VA_Data.color = color;
                VA_Data.question = row[0].ToString().Trim();
                VA_Data.answer = row[1].ToString().Trim();
                VA_Data.filter = row[2].ToString().Trim();
                VA_Data.VA_Data_Id = row[3].ToString().Trim();
                VA_Data.r_type = row[4].ToString().Trim();
                
                VA_MapQdata.Add(VA_Data);

            }

            return VA_MapQdata;
        }

        private void dgvVoterAnalysis_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddVoterAnalysis();
        }

        
        private void dataModeSelect_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = dataModeSelect.TabPages[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, Font, paddedBounds, page.ForeColor);
        }

        private void dgvVoterAnalysisMap_DoubleClick(object sender, EventArgs e)
        {
            AddVoterAnalysisMap();
        }

        private void cbAutoCalledRaces_CheckedChanged(object sender, EventArgs e)
        {
            autoCalledRacesActive = cbAutoCalledRaces.Checked;
            if (autoCalledRacesActive)
            {
                cbLooping.Checked = true;

                acrIndx = 0;

                ofcID = autoOfc[acrIndx];
                RefreshAvailableRacesListFiltered(ofcID, 1, 0, stateMetadata, isPrimary);
                stackElements.Clear();
                AddAll();
            }
        }

        private void dgvVoterAnalysis_DoubleClick(object sender, EventArgs e)
        {
            AddVoterAnalysis();
        }

        private void btnAddMap_Click(object sender, EventArgs e)
        {
            AddVoterAnalysisMap();
        }

        #endregion

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnSelectCounties_Click(object sender, EventArgs e)
        {
            try
            {
                // Instantiate new stack element model
                StackElementModel newStackElement = new StackElementModel();

                Int16 seType = (short)StackElementTypes.Race_Board_2_Way;
                string seDescription = "Race Board (2-Way)";
                Int16 seDataType = (int)DataTypes.Race_Boards;

                if (isPrimary)
                {
                    seType = (short)StackElementTypes.Race_Board_All_Way;
                    seDescription = "Race Board (All-Way)";
                    seDataType = (int)DataTypes.Race_Boards;
                }

                AvailableRaceModel selectedRace = new AvailableRaceModel();
                int currentRaceIndex = 0;

                //Get the selected race list object
                if (tabIndex == 0)
                {
                    currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                    selectedRace = availableRacesCollection.GetRace(availableRaces, currentRaceIndex);
                }
                else if (tabIndex == 4)
                {
                    currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex;
                    selectedRace = availableRacesCollection.GetRace(availableRacesSP, currentRaceIndex);
                }

                
                DataTable dt = new DataTable();
                dt = GetListOfCounties(selectedRace.State_Number, selectedRace.Race_Office, selectedRace.Election_Type);

                List<string> countyNames = new List<string>();
                List<int> countyIndicies = new List<int>();

                foreach (DataRow row in dt.Rows)
                {
                    string State_Mnemonic = row["StateAbbv"].ToString() ?? "";
                    int cntyID = Convert.ToInt16(row["cnty"] ?? 0);
                    string cntyName = row["cntyName"].ToString() ?? "";
                    countyNames.Add(cntyName);
                }


                // Setup dialog to load stack
                DialogResult dr = new DialogResult();

                frmCountySelect countySelect = new frmCountySelect(countyNames);

                dr = countySelect.ShowDialog();
                countyNames.Clear();
                // Process result from dialog
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < countySelect.countiesChecked.Count; i++)
                    {
                        countyNames.Add(countySelect.countiesChecked[i]);
                        countyIndicies.Add(countySelect.indChecked[i]);
                        AddCountyBoardToStack(seType, seDescription, seDataType, countySelect.indChecked[i], countySelect.countiesChecked[i]);
                    }
                    
                }


            }
            catch (Exception ex)
            {
                log.Error($"SelectCounties error: {ex.Message}");
            }
        }

        public DataTable GetListOfCounties(int StateId, string ofc, string elecType)
        {

            DataTable dataTable = new DataTable();

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(ElectionsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {

                            cmd.CommandText = SQLCommands.sqlGetCountiesByState;
                            cmd.Parameters.Add("@State_Number", SqlDbType.Int).Value = StateId;
                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = ofc;
                            cmd.Parameters.Add("@Election_Type", SqlDbType.Text).Value = elecType;
                            sqlDataAdapter.SelectCommand = cmd;
                            sqlDataAdapter.SelectCommand.Connection = connection;
                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                            // Fill the datatable from adapter
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("GetListOfCounties Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return dataTable;
        }

        private void btnAllSP_Click(object sender, EventArgs e)
        {
            Int16 seType = (short)StackElementTypes.Race_Board_All_Way;
            string seDescription = "Race Board (All)";
            Int16 seDataType = (int)DataTypes.Race_Boards;

            AddRaceBoardToStack(seType, seDescription, seDataType);
        }

        private void btnAllCand_Click(object sender, EventArgs e)
        {
            Int16 seType = (short)StackElementTypes.Race_Board_All_Way;
            string seDescription = "Race Board (All)";
            Int16 seDataType = (int)DataTypes.Race_Boards;

            AddRaceBoardToStack(seType, seDescription, seDataType);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void RefreshFlagsTimer_Tick(object sender, EventArgs e)
        {
            RefreshApplicationFlags();
        }

        private void dgvVoterAnalysisTicker_DoubleClick(object sender, EventArgs e)
        {
            AddVoterAnalysis();
        }

        private void dgvVAManual_DoubleClick(object sender, EventArgs e)
        {
            AddVoterAnalysis();
        }

        private void availableRacesGridSP_DoubleClick(object sender, EventArgs e)
        {
            if (isPrimary)
                btnAllSP_Click(sender, e);
            else
                btnAddRace2Way_Click(sender, e);

        }

        private void VAQRefreshTimer_Tick(object sender, EventArgs e)
        {
            lblRefresh.Visible = true;
            if (tabIndex == 1)
            {
                GetVoterAnalysisGridData();
                GetVoterAnalysisManualGridData();
                
            }
            if (tabIndex == 5)
                GetVoterAnalysisMapGridData();
            RefreshLblTimer.Enabled = true;
        }

        private void RefreshLblTimer_Tick(object sender, EventArgs e)
        {
            lblRefresh.Visible = false;
            RefreshLblTimer.Enabled = false;
        }

        private void btnAddRace5Way_Click(object sender, EventArgs e)
        {
                Int16 seType = (short)StackElementTypes.Race_Board_5_Way;
                string seDescription = "Race Board (5-Way)";
                Int16 seDataType = (int)DataTypes.Race_Boards;

                AddRaceBoardToStack(seType, seDescription, seDataType);
            
        }

        private void btnSelect5_Click(object sender, EventArgs e)
        {
            if (stackLocked == false)
            {
                try
                {
                    Int32 selectedCandidate1 = 0;
                    Int32 selectedCandidate2 = 0;
                    Int32 selectedCandidate3 = 0;
                    Int32 selectedCandidate4 = 0;
                    Int32 selectedCandidate5 = 0;
                    string cand1Name = string.Empty;
                    string cand2Name = string.Empty;
                    string cand3Name = string.Empty;
                    string cand4Name = string.Empty;
                    string cand5Name = string.Empty;
                    Int16 numCand = 5;

                    //Get the selected race list object
                    int currentRaceIndex = availableRacesGrid.CurrentCell.RowIndex;
                    BindingList<AvailableRaceModel> raceList = availableRaces;
                    if (tabIndex == 4)
                    {
                        currentRaceIndex = availableRacesGridSP.CurrentCell.RowIndex;
                        raceList = availableRacesSP;
                    }
                    AvailableRaceModel selectedRace = availableRacesCollection.GetRace(raceList, currentRaceIndex);

                    string eType = selectedRace.Election_Type;
                    string ofc = selectedRace.Race_Office;
                    Int16 st = selectedRace.State_Number;
                    string des = selectedRace.Race_Description;
                    Int16 cd = selectedRace.CD;
                    string party = selectedRace.Party;

                    DialogResult dr = new DialogResult();
                    FrmCandidateSelect selectCand = new FrmCandidateSelect(numCand, st, ofc, eType, des, cd, electionMode, party);

                    // Only process if required number of candidates in race
                    if (selectCand.candidatesFound)
                    {
                        dr = selectCand.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            // Set candidateID's

                            selectedCandidate1 = selectCand.Cand1;
                            cand1Name = selectCand.CandName1;
                            selectedCandidate2 = selectCand.Cand2;
                            cand2Name = selectCand.CandName2;
                            selectedCandidate3 = selectCand.Cand3;
                            cand3Name = selectCand.CandName3;
                            selectedCandidate4 = selectCand.Cand4;
                            cand4Name = selectCand.CandName4;
                            selectedCandidate5 = selectCand.Cand5;
                            cand5Name = selectCand.CandName5;
                            AddSelectRaceBoardToStack(numCand, selectedCandidate1, selectedCandidate2, selectedCandidate3, selectedCandidate4, selectedCandidate5, cand1Name, cand2Name, cand3Name, cand4Name, cand5Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("frmMain Exception occurred: " + ex.Message);
                    //log.Debug("frmMain Exception occurred", ex);
                }
            }

        }

    }

}
