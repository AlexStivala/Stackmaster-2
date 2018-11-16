using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DataInterface.DataAccess;
using DataInterface.DataModel;
using DataInterface.Enums;
using LogicLayer.Collections;
using MSEInterface;
using MSEInterface.DataModel;

namespace GUILayer.Forms
{
    public partial class frmSelectShow : Form
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        private string GraphicsDBConnectionString { get; set; }
        // Define the binding list object for the list of available shows
        private BindingList<ShowObject> showNames;
        // The sublist ID to be returned 
        public string selectedShow { get; set; }
        #endregion

        public frmSelectShow(string mediaSequencerEndPoint, string DBconn)
        {
            try
            {
                InitializeComponent();

                //Gets the URI's for available shows
                MANAGE_SHOWS getShowList = new MANAGE_SHOWS();

                //Read in values from the config file
                //GraphicsDBConnectionString = Properties.Settings.Default.GraphicsDBConnectionString;
                GraphicsDBConnectionString = DBconn;

                showNames = getShowList.GetListOfShows(mediaSequencerEndPoint + Properties.Settings.Default.TopLevelShowsDirectory);

                // Enable handling of function keys
                KeyPreview = true;
                this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);

                // Setup the available stacks grid
                availableShowsGrid.AutoGenerateColumns = false;
                var availableShowsGridDataSource = new BindingSource(showNames, null);
                availableShowsGrid.DataSource = availableShowsGridDataSource;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmSelectShow Exception occurred: " + ex.Message);
                //log.Debug("frmSelectShow Exception occurred", ex);
            }
        }

        // Handler for select show
        private void btnSelectShow_Click(object sender, EventArgs e)
        {
            SelectShow();
        }
        // Handler for double-click on grid => select show
        private void availableShowsGrid_DoubleClick(object sender, EventArgs e)
        {
            SelectShow();
        }

        // Method to set parameters for show and return
        private void SelectShow()
        {
            selectedShow = string.Empty;

            try
            {
                // Set the sublist ID
                if (showNames.Count > 0)
                {
                    //Get the playlist ID from the grid
                    int currentShowIndex = availableShowsGrid.CurrentCell.RowIndex;
                    string ShowName = showNames.ElementAt(currentShowIndex).title.ToString();

                    DialogResult result1 = MessageBox.Show("Are you sure you want to switch to the new show: " + ShowName + "?", "Confirmation",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 != DialogResult.Yes)
                    {
                        return;
                    }

                    // Set new show name
                    selectedShow = ShowName;
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("frmSelectShow Exception occurred: " + ex.Message);
                //log.Debug("frmSelectShow Exception occurred", ex);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Method to handle function keys for race boards
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    if (e.Control == true)
                        btnSelectShow_Click(sender, e);
                    break;
            }
        }

    }
}
