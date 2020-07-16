using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using DataInterface.DataModel;
using GUILayer.Properties;
using log4net;
using LogicLayer.Collections;
// Required for implementing logging to status bar

namespace GUILayer.Forms
{
    public partial class FrmCandidateSelect : Form
    {
        #region Logger instantiation - uses reflection to get module name

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        private readonly string _eType;
        private readonly short Jcde = 0;
        private readonly int _numCand;
        private readonly string _ofc;
        private readonly string _em;
        private readonly string _p;

        private readonly short _st;

        public Boolean candidatesFound = false;

        public FrmCandidateSelect(short numCandidates, short stNum, string office, string electionType,
            string raceDesription, short cd, string ElectionMode, string party)
        {
            InitializeComponent();

            _numCand = numCandidates;
            _st = stNum;
            _ofc = office;
            _eType = electionType;
            Jcde = cd;
            _em = ElectionMode;
            _p = party;

            Width = numCandidates*340 + 20;
            dgvCand1.Left = 20;
            dgvCand2.Left = 20;
            dgvCand3.Left = 20;
            dgvCand4.Left = 20;
            dgvCand5.Left = 20;
            dgvCand1.Top = 70;
            dgvCand2.Top = 70;
            dgvCand3.Top = 70;
            dgvCand4.Top = 70;
            dgvCand5.Top = 70;

            var half = (numCandidates*340 + 20)/2;

            switch (numCandidates)
            {
                case 1:
                    dgvCand1.Left = 15;
                    dgvCand1.Visible = true;
                    dgvCand2.Visible = false;
                    dgvCand3.Visible = false;
                    dgvCand4.Visible = false;
                    dgvCand5.Visible = false;
                    break;
                case 2:
                    dgvCand1.Left = 15;
                    dgvCand2.Left = 355;
                    dgvCand1.Visible = true;
                    dgvCand2.Visible = true;
                    dgvCand3.Visible = false;
                    dgvCand4.Visible = false;
                    dgvCand5.Visible = false;
                    break;
                case 3:
                    dgvCand1.Left = 15;
                    dgvCand2.Left = 355;
                    dgvCand3.Left = 695;
                    dgvCand1.Visible = true;
                    dgvCand2.Visible = true;
                    dgvCand3.Visible = true;
                    dgvCand4.Visible = false;
                    dgvCand5.Visible = false;
                    break;
                case 4:
                    dgvCand1.Left = 15;
                    dgvCand2.Left = 355;
                    dgvCand3.Left = 695;
                    dgvCand4.Left = 1035;
                    dgvCand1.Visible = true;
                    dgvCand2.Visible = true;
                    dgvCand3.Visible = true;
                    dgvCand4.Visible = true;
                    dgvCand5.Visible = false;
                    break;
                case 5:
                    dgvCand1.Left = 15;
                    dgvCand2.Left = 355;
                    dgvCand3.Left = 695;
                    dgvCand4.Left = 1035;
                    dgvCand5.Left = 1375;
                    dgvCand1.Visible = true;
                    dgvCand2.Visible = true;
                    dgvCand3.Visible = true;
                    dgvCand4.Visible = true;
                    dgvCand5.Visible = true;
                    break;
                default:
                    dgvCand1.Left = 15;
                    dgvCand2.Left = 355;
                    dgvCand1.Visible = true;
                    dgvCand2.Visible = true;
                    dgvCand3.Visible = false;
                    dgvCand4.Visible = false;
                    break;
            }

            btnOK.Left = half - 120;
            btnCancel.Left = half + 10;

            label2.Text = numCandidates + @" Way Select Board for: " + raceDesription;
            GetCandidatesForRace();
        }

        private void GetCandidatesForRace()
        {
            try
            {
                _raceDataCollection = new RaceDataCollection {ElectionsDBConnectionString = _electionsDbConnectionString};
                // Specify state ID = -1 => Don't query database for candidate data until requesting actual race data
                //_raceData = _raceDataCollection.GetRaceDataCollection(_st, _ofc, Jcde, _eType, 0);
                
                if (_em == "Primary")
                    _raceData = _raceDataCollection.GetRaceDataCollectionPrimary(_st, _ofc, Jcde, _eType, _p);
                else
                    _raceData = _raceDataCollection.GetRaceDataCollection(_em, _st, _ofc, Jcde, _eType, 0, false, 0, 0, 0, 0, 0);

                // Setup the available races grid
                dgvCand1.AutoGenerateColumns = false;
                var dgvCand1DataSource = new BindingSource(_raceData, null);
                dgvCand1.DataSource = dgvCand1DataSource;

                dgvCand2.AutoGenerateColumns = false;
                var dgvCand2DataSource = new BindingSource(_raceData, null);
                dgvCand2.DataSource = dgvCand2DataSource;

                dgvCand3.AutoGenerateColumns = false;
                var dgvCand3DataSource = new BindingSource(_raceData, null);
                dgvCand3.DataSource = dgvCand3DataSource;

                dgvCand4.AutoGenerateColumns = false;
                var dgvCand4DataSource = new BindingSource(_raceData, null);
                dgvCand4.DataSource = dgvCand4DataSource;

                dgvCand5.AutoGenerateColumns = false;
                var dgvCand5DataSource = new BindingSource(_raceData, null);
                dgvCand5.DataSource = dgvCand5DataSource;
                
                dgvCand1.Rows[0].Selected = true;
                _candIndex = 0;
                Cand1 = Convert.ToInt32(dgvCand1[2, _candIndex].Value);
                CandName1 = (string) dgvCand1[0, _candIndex].Value;

                dgvCand2.Rows[0].Selected = true;
                //_candIndex = dgvCand2.CurrentCell.RowIndex;
                Cand2 = Convert.ToInt32(dgvCand2[2, _candIndex].Value);
                CandName2 = (string) dgvCand2[0, _candIndex].Value;

                dgvCand3.Rows[0].Selected = true;
                //_candIndex = dgvCand3.CurrentCell.RowIndex;
                Cand3 = Convert.ToInt32(dgvCand3[2, _candIndex].Value);
                CandName3 = (string) dgvCand3[0, _candIndex].Value;

                dgvCand4.Rows[0].Selected = true;
                //_candIndex = dgvCand4.CurrentCell.RowIndex;
                Cand4 = Convert.ToInt32(dgvCand4[2, _candIndex].Value);
                CandName4 = (string) dgvCand4[0, _candIndex].Value;

                dgvCand5.Rows[0].Selected = true;
                //_candIndex = dgvCand4.CurrentCell.RowIndex;
                Cand5 = Convert.ToInt32(dgvCand5[2, _candIndex].Value);
                CandName5 = (string)dgvCand5[0, _candIndex].Value;

                label1.Text = @"Number of candidates in race: " + _raceData.Count;
                if (_raceData.Count < _numCand)
                {
                    candidatesFound = false;

                    MessageBox.Show(
                        @"You cannot add a " + label2.Text + @" because there are only " + _raceData.Count +
                        @" candidates in race.", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Abort;
                    Close();
                }
                else
                {
                    candidatesFound = true;
                }
            }
            catch (Exception ex)
            {
                // Log error
                Log.Error("frmCandidateSelect Exception occurred: " + ex.Message);
                //Log.Debug("frmCandidateSelect Exception occurred", ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
//            this.Close();
        }

        private void dgvCand1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand1.CurrentCell.RowIndex;
            Cand1 = Convert.ToInt32(dgvCand1[2, _candIndex].Value);
            CandName1 = (string) dgvCand1[0, _candIndex].Value;
        }

        private void frmCandidateSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void dgvCand1_Click(object sender, EventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand1.CurrentCell.RowIndex;
            Cand1 = Convert.ToInt32(dgvCand1[2, _candIndex].Value);
            CandName1 = (string) dgvCand1[0, _candIndex].Value;
        }

        private void dgvCand2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand2.CurrentCell.RowIndex;
            Cand2 = Convert.ToInt32(dgvCand2[2, _candIndex].Value);
            CandName2 = (string) dgvCand2[0, _candIndex].Value;
        }

        private void dgvCand2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand2.CurrentCell.RowIndex;
            Cand2 = Convert.ToInt32(dgvCand2[2, _candIndex].Value);
            CandName2 = (string) dgvCand2[0, _candIndex].Value;
        }

        private void dgvCand3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand3.CurrentCell.RowIndex;
            Cand3 = Convert.ToInt32(dgvCand3[2, _candIndex].Value);
            CandName3 = (string) dgvCand3[0, _candIndex].Value;
        }

        private void dgvCand3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand3.CurrentCell.RowIndex;
            Cand3 = Convert.ToInt32(dgvCand3[2, _candIndex].Value);
            CandName3 = (string) dgvCand3[0, _candIndex].Value;
        }

        private void dgvCand4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand4.CurrentCell.RowIndex;
            Cand4 = Convert.ToInt32(dgvCand4[2, _candIndex].Value);
            CandName4 = (string) dgvCand4[0, _candIndex].Value;
        }

        private void dgvCand4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand4.CurrentCell.RowIndex;
            Cand4 = Convert.ToInt32(dgvCand4[2, _candIndex].Value);
            CandName4 = (string) dgvCand4[0, _candIndex].Value;
        }

        #region Collection & binding list definitions

        // Define the collection used for storing candidate data for a specific race
        private RaceDataCollection _raceDataCollection;
        private BindingList<RaceDataModel> _raceData;

        // Read in database connection strings
        //private string _graphicsDbConnectionString = Settings.Default.GraphicsDBConnectionString;
        private readonly string _electionsDbConnectionString = frmMain.ElectionsDBConnectionString;

        public int Cand1 { get; set; }
        public string CandName1 { get; set; }
        public int Cand2 { get; set; }
        public string CandName2 { get; set; }
        public int Cand3 { get; set; }
        public string CandName3 { get; set; }
        public int Cand4 { get; set; }
        public string CandName4 { get; set; }
        public int Cand5 { get; set; }
        public string CandName5 { get; set; }
        private int _candIndex;

        #endregion

        private void dgvCand5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand5.CurrentCell.RowIndex;
            Cand5 = Convert.ToInt32(dgvCand5[2, _candIndex].Value);
            CandName5 = (string)dgvCand5[0, _candIndex].Value;
        }

        private void dgvCand5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get the selected race list object
            _candIndex = dgvCand5.CurrentCell.RowIndex;
            Cand5 = Convert.ToInt32(dgvCand5[2, _candIndex].Value);
            CandName5 = (string)dgvCand5[0, _candIndex].Value;
        }
    }
}