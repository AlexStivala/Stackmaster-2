using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterface.DataModel;
using DataInterface.DataAccess;
using System.ComponentModel;
using System.Data;


namespace LogicLayer.Collections
{
    /// <summary>
    /// Class for operations related to the available races
    /// </summary>
    public class GraphicsConceptsCollection
    {
        #region Logger instantiation - uses reflection to get module name

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties and Members

        public BindingList<GraphicsConceptsModel> graphicsConcepts;
        public BindingList<GraphicsConceptsModel> graphicsConceptTypes;
        public string GraphicsDBConnectionString { get; set; }

        #endregion

        #region Public Methods

        // Constructor - instantiates list collection
        public GraphicsConceptsCollection()
        {
            // Create list
            graphicsConcepts = new BindingList<GraphicsConceptsModel>();
            graphicsConceptTypes = new BindingList<GraphicsConceptsModel>();
        }

        /// <summary>
        /// Get the Graphics Concepts list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<GraphicsConceptsModel> GetGraphicsConceptsCollection()
        {
            DataTable dataTable;

            // Clear out the current collection
            graphicsConcepts.Clear();

            try
            {
                GraphicsConceptsAccess graphicsConceptsAccess = new GraphicsConceptsAccess();
                graphicsConceptsAccess.GraphicsDBConnectionString = GraphicsDBConnectionString;
                dataTable = graphicsConceptsAccess.GetGraphicsConcepts();

                foreach (DataRow row in dataTable.Rows)
                {
                    var newgraphicsConcepts = new GraphicsConceptsModel()
                    {

                        //Specific to referendums
                        ConceptID = Convert.ToInt16(row["ConceptID"] ?? 0),
                        ConceptName = row["ConceptName"].ToString() ?? "",
                        ElementTypeCode = Convert.ToInt16(row["ElementTypeCode"] ?? 0),
                        ElementTypeDescription = row["ElementTypeDescription"].ToString() ?? "",
                        TemplateName = row["TemplateName"].ToString() ?? "",
                        TemplateDescription = row["TemplateDescription"].ToString() ?? "",
                        IsBaseConcept = Convert.ToBoolean(row["IsBaseConcept"] ?? 0),
                        AllowConceptChange = Convert.ToBoolean(row["AllowConceptChange"] ?? 0),
                    };

                    graphicsConcepts.Add(newgraphicsConcepts);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("GraphicsConceptsCollection Exception occurred: " + ex.Message);
                //log.Debug("GraphicsConceptsCollection Exception occurred", ex);
            }

            // Return 
            return graphicsConcepts;
        }

        /// <summary>
        /// Get the Graphics Concepts list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<GraphicsConceptsModel> GetGraphicsConceptTypesCollection()
        {
            DataTable dataTable;

            // Clear out the current collection
            graphicsConceptTypes.Clear();

            try
            {
                GraphicsConceptsAccess graphicsConceptsAccess = new GraphicsConceptsAccess();
                graphicsConceptsAccess.GraphicsDBConnectionString = GraphicsDBConnectionString;
                dataTable = graphicsConceptsAccess.GetGraphicsConceptTypes();

                foreach (DataRow row in dataTable.Rows)
                {
                    var newgraphicsConcepts = new GraphicsConceptsModel()
                    {

                        //Specific to graphics concepts
                        ConceptID = Convert.ToInt16(row["ConceptID"] ?? 0),
                        ConceptName = row["ConceptName"].ToString() ?? "",

                    };

                    graphicsConceptTypes.Add(newgraphicsConcepts);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("GraphicsConceptsCollection Exception occurred: " + ex.Message);
                //log.Debug("GraphicsConceptsCollection Exception occurred", ex);
            }

            // Return 
            return graphicsConceptTypes;
        }

        #endregion
    }
}
