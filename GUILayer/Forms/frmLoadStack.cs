using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataInterface.DataAccess;
using DataInterface.DataModel;
using DataInterface.Enums;
using System.Reflection;
using MSEInterface;

using LogicLayer.Collections;
using LogicLayer.GeneralDataProcessingFunctions;

namespace GUILayer.Forms
{
    public partial class frmLoadStack : Form
    {
        // Define the collection object for the list of available stacks
        private StacksCollection stacksCollection = new StacksCollection();
        //private StacksCollection stacksCollection;
        public BindingList<StackModel> stacks;
        
        // Read in database connection strings
        string GraphicsDBConnectionString = Properties.Settings.Default.GraphicsDBConnectionString;
        string ElectionsDBConnectionString = Properties.Settings.Default.ElectionsDBConnectionString;
        string StacksDBConnectionString = Properties.Settings.Default.StacksDBConnectionString;
        public int stackType = 0;

        public bool builderOnlyMode { get; set; }

        private Int32 stackIndex;
        public Int32 StackIndex { get { return stackIndex; } set { StackIndex = stackIndex; } }

        private Double stackID;
        public Double StackID { get { return stackID; } set { StackID = stackID; } }

        private string stackDesc;
        public string StackDesc { get { return stackDesc; } set { StackDesc = stackDesc; } }

        public Boolean EnableShowControls { get; set;}
        public bool multiplayMode = false;

        //string topLevelShowsDirectoryURI = Properties.Settings.Default.MSEEndpoint1 + Properties.Settings.Default.TopLevelShowsDirectory;
        //string currentShowName = Properties.Settings.Default.CurrentShowName;
        //string currentPlaylistName = Properties.Settings.Default.CurrentSelectedPlaylist;
        //string currentShowName = "MAIN_WALL";
        //string currentPlaylistName = "GRAPHICS";

        //public delegate void DelDeleteStack(frmLoadStack sender, Double stackID, String );

        //public event DelDeleteStack DeleteStack;

        public frmLoadStack(bool buildMode, int StackType)
        {
            InitializeComponent();

            builderOnlyMode = buildMode;
            stackType = StackType;
            if (stackType <= 10)
            {
                btnShowMultiplay.Visible = true;
            }

            
            if (builderOnlyMode)
            {
                this.stacksCollection.MainDBConnectionString = GraphicsDBConnectionString;
                btnActivateStack.Visible = true;
            }
            else
            {
                this.stacksCollection.MainDBConnectionString = StacksDBConnectionString;
                btnActivateStack.Visible = false;
            }

            // Enable handling of function keys
            KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);

            RefreshStacksList();

            if (EnableShowControls)
            {
                this.availableStacksGrid.Columns[1].Visible = true;
            }
            else
            {
                this.availableStacksGrid.Columns[1].Visible = false;
            }

            // Select first stack in list as default
            if (stacks.Count > 0)
            {
                stackIndex = 0;
                stackID = Convert.ToDouble(availableStacksGrid[0, stackIndex].Value);
                stackDesc = (string)availableStacksGrid[1, stackIndex].Value;
            }
        }

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
            //toolStripStatusLabel.BackColor = System.Drawing.Color.SpringGreen;
            //toolStripStatusLabel.Text = "Status Logging Message: Statusbar reset";
        }
        #endregion
        
        // Method to refresh the stacks list
        private void RefreshStacksList()
        {
            try
            {
                // Setup the available stacks collection
                //this.stacksCollection = new StacksCollection();
                //this.stacksCollection.MainDBConnectionString = GraphicsDBConnectionString;
                
                stacks = this.stacksCollection.GetStackCollection(stackType);

                // Setup the available stacks grid
                availableStacksGrid.AutoGenerateColumns = false;
                var availableStacksGridDataSource = new BindingSource(stacks, null);
                availableStacksGrid.DataSource = availableStacksGridDataSource;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("FrmLoadStack Exception occurred during stacks list refresh: " + ex.Message);
                log.Debug("FrmLoadStack Exception occurred during stacks list refresh", ex);
            }
        }

        // Handler for Load Stack button       
        private void btnLoadStack_Click(object sender, EventArgs e)
        {
            LoadSelectedStack();
        }
        // Handler for double-click in grid - loads selected stack
        private void availableStacksGrid_DoubleClick(object sender, EventArgs e)
        {
            LoadSelectedStack();
        }
        // Method to load stack
        private void LoadSelectedStack()
        {
            try
            {
                // Disable confirmation dialog for 2016 election
                //if ((stackIndex >= 0) && (availableStacksGrid.RowCount > 0))
                //{
                    //DialogResult result1 =
                    //    MessageBox.Show(
                    //        "Are you sure you want to clear any current contents of the stack and load the specified stack from the database?",
                    //        "Confirmation",
                    //        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result1 == DialogResult.Yes)
                    //{
                    //    this.DialogResult = DialogResult.OK;
                    //    this.Close();
                    //}
                    //else
                    //{
                    //    this.DialogResult = DialogResult.Cancel;
                    //    this.Close();
                    //}
                //}
                // No elements in stack, so don't prompt operator
                //else
                //{
                    this.DialogResult = DialogResult.OK;
                    this.Close();                   
                //}

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmLoadStacks Exception occurred: " + ex.Message);
                log.Debug("frmLoadStacks Exception occurred", ex);
            }
        }

        // Handler for Delete Stack button
        private void btnDeleteStack_Click(object sender, EventArgs e)
        {
            try
            {
                if (stacks.Count > 0)
                {
                    DialogResult result1 = MessageBox.Show("Are you sure you want to delete the selected stack from the database?", "Confirmation",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 != DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmLoadStacks Exception occurred: " + ex.Message);
                log.Debug("frmLoadStacks Exception occurred", ex);
            }
        
        }

        // Handlers to change grid selection based on operator GUI actions
        private void availableStacksGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected stack list object
            if (availableStacksGrid.CurrentCell != null)
            {
                stackIndex = availableStacksGrid.CurrentCell.RowIndex;
                stackID = Convert.ToDouble(availableStacksGrid[0, stackIndex].Value);
                stackDesc = (string)availableStacksGrid[2, stackIndex].Value;
            }
        }
        private void availableStacksGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected stack list object
            if (availableStacksGrid.CurrentCell != null)
            {
                stackIndex = availableStacksGrid.CurrentCell.RowIndex;
                stackID = Convert.ToDouble(availableStacksGrid[0, stackIndex].Value);
                stackDesc = (string)availableStacksGrid[2, stackIndex].Value;
            }          
        }
        private void availableStacksGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected stack list object
            if (availableStacksGrid.CurrentCell != null)
            {
                stackIndex = availableStacksGrid.CurrentCell.RowIndex;
                stackID = Convert.ToDouble(availableStacksGrid[0, stackIndex].Value);
                stackDesc = (string)availableStacksGrid[2, stackIndex].Value;
            }
        }
        private void availableStacksGrid_Scroll(object sender, ScrollEventArgs e)
        {
            //Get the selected stack list object
            if (availableStacksGrid.CurrentCell != null)
            {
                stackIndex = availableStacksGrid.CurrentCell.RowIndex;
                stackID = Convert.ToDouble(availableStacksGrid[0, stackIndex].Value);
                stackDesc = (string)availableStacksGrid[2, stackIndex].Value;
            }
        }
        private void availableStacksGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //Get the selected stack list object
            if (availableStacksGrid.CurrentCell != null)
            {
                stackIndex = availableStacksGrid.CurrentCell.RowIndex;
                stackID = Convert.ToDouble(availableStacksGrid[0, stackIndex].Value);
                stackDesc = (string)availableStacksGrid[2, stackIndex].Value;
            }
        }

        // Method to handle function keys
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Control == true)
                    {
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    break;
                case Keys.L:
                    if (e.Control == true)
                        btnLoadStack_Click(sender, e);
                    break;
                case Keys.C:
                    if (e.Control == true)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                    break;
                case Keys.D:
                    if (e.Control == true)
                    {
                        btnDeleteStack_Click(sender, e);
                    }
                    break;
            }
        }

        // Event Handler here to prevent grid from going to next row when Enter key is pressed
        private void availableStacksGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;

                // Default to Load Stack function on Enter key
                btnLoadStack_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancelStackLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnActivateStack_Click(object sender, EventArgs e)
        {

        }

        private void btnShowMultiplay_Click(object sender, EventArgs e)
        {
            multiplayMode = !multiplayMode;
            if (multiplayMode)
            {
                btnShowMultiplay.Text = "Show Raceboard Stacks(Ctrl - M)";
                this.stacksCollection.MainDBConnectionString = GraphicsDBConnectionString;
                stackType = 0;
            }
            else
            {
                btnShowMultiplay.Text = "Show Multiplay Stacks(Ctrl - M)";
                this.stacksCollection.MainDBConnectionString = StacksDBConnectionString;
                stackType = 10;
            }
            RefreshStacksList();
        }

    }
}
