using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterface.DataModel;
using DataInterface.DataAccess;
using System.Data.SqlClient;
using System.ComponentModel;

namespace LogicLayer.Collections
{
    /// <summary>
    /// Collection used to hold command strings for generating race preview pages for an MSE group
    /// </summary>
    public class RacePreviewCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<RacePreviewModel> racePreviewElements;

        private int collectionCount { get; set; }
        public int CollectionCount
        {
            get { return this.collectionCount; }
            set { this.collectionCount = value; }
        }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public RacePreviewCollection()
        {
            // Create list
            racePreviewElements = new BindingList<RacePreviewModel>();
        }

        /// <summary>
        /// Get the list of race preview elements
        /// </summary>
        public BindingList<RacePreviewModel> GetRacepPreviewCollection()
        {
            return this.racePreviewElements;
        }

        /// <summary>
        /// Append an element to the race preview elements collection
        /// </summary>
        public void AppendRacePreviewElement(RacePreviewModel racePreviewElement)
        {
            try
            {
                racePreviewElements.Add(racePreviewElement);
                this.collectionCount = racePreviewElements.Count;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("RacePreviewCollection Exception occurred: " + ex.Message);
                //log.Debug("RacePreviewCollection Exception occurred", ex);
            }
        }

        /// <summary>
        /// Insert an element into the race preview elements collection at the specified location
        /// </summary>
        public void AppendRacePreviewElement(Int16 insertPoint, RacePreviewModel racePreviewElement)
        {
            try
            {
                racePreviewElements.Insert(insertPoint, racePreviewElement);
                this.collectionCount = racePreviewElements.Count;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("RacePreviewCollection Exception occurred: " + ex.Message);
                //log.Debug("RacePreviewCollection Exception occurred", ex);
            }
        }

        /// <summary>
        /// Get the specified stack element from the collection
        /// </summary>
        public RacePreviewModel GetRacePreviewElement(BindingList<RacePreviewModel> RacePreviewElements, int itemIndex)
        {
            RacePreviewModel racePreviewElement = null;

            try
            {
                racePreviewElement = RacePreviewElements[itemIndex];
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("RacePreviewCollection Exception occurred: " + ex.Message);
                //log.Debug("RacePreviewCollection Exception occurred", ex);
            }

            return racePreviewElement;
        }
        #endregion
    }
}
