using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Collections.Specialized;
using System.Data;
using System.Threading.Tasks;
using DataInterface.DataModel;
using DataInterface.DataAccess;
using System.Data.SqlClient;
using System.ComponentModel;


namespace MSEInterface
{
    using Constants;
    using DataModel;

    public class MANAGE_GROUPS
    {

        internal static readonly XNamespace Viz = "http://www.vizrt.com/types";
        internal static readonly XNamespace VizAtom = "http://www.vizrt.com/atom";
        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion


        /// <summary>
        /// Add a new group
        /// </summary>
        public REST_RESPONSE CreateGroup(string playlistDownURI, string groupName)
        {
            //Save the results in a new object
            REST_RESPONSE restResponse = new REST_RESPONSE();

            try
            {
                string newGroup = string.Empty;

                newGroup = "<entry xmlns=\"http://www.w3.org/2005/Atom\">" +
                    "<title>" + groupName + "</title>" +
                    "<summary>" + groupName + "</summary>" +
                    "<category term=\"group\" scheme=\"http://www.vizrt.com/types\"/>" +
                    "</entry>";

                byte[] bdata = System.Text.Encoding.UTF8.GetBytes(newGroup);

                // Call method that does not use header - content type specified by client object
                REST_CLIENT client = new REST_CLIENT(playlistDownURI, HttpVerb.POST, bdata, ContentTypes.Group_Creation);

                var response = client.MakeRequest();

                XElement doc = XElement.Parse(response.xmlResponse);

                //Get the self link - used for deletion of playlist
                var selfURI = doc.Descendants(Atom + "link")
                    .Where(x => (string)x.Attribute("rel").Value == "self")
                    .FirstOrDefault();

                var selfLink = selfURI.Attribute("href").Value;

                //Get the down link - used for insertion of new elements into a group
                var downURI = doc.Descendants(Atom + "link")
                    .Where(x => (string)x.Attribute("rel").Value == "down")
                    .FirstOrDefault();

                var downLink = downURI.Attribute("href").Value;

                restResponse.selfLink = selfLink;
                restResponse.downLink = downLink;

            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_GROUPS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_GROUPS Exception occurred", ex);
            }

            return restResponse;
        }

        /// <summary>
        /// Check to see if a playlist with the specified name already exists in the VDOM
        /// </summary>
        public string GetGroupSelfLink(string playlistURI, string deleteGroupName)
        {
            string groupSelfLink = string.Empty;
            XElement groupDoc;

            GET_URI getURI = new GET_URI();

            try
            {
                //Get all the playlist/group entries for the specified show
                var groupNames = getURI.SendGETRequest(playlistURI).Descendants(Atom + "entry");

                if (groupNames != null)
                {
                    // Walk through each playlist and check for match by title
                    foreach (XElement group in groupNames)
                    {
                        string groupName = group.Element(Atom + "summary").Value;

                        // If the group name matches, get the self-link to the playlist
                        if (groupName == deleteGroupName)
                        {
                            string selfLink = string.Empty;
                            groupDoc = group.Descendants(Atom + "link")
                                .Where (x => (string)x.Attribute("rel") == "self") 
                                .Where (x => (string)x.Attribute("type") == "application/atom+xml;type=entry")
                                .FirstOrDefault();
                            if (groupDoc != null)
                            {
                                selfLink = groupDoc.Attribute("href").Value;
                                // Set return value
                                groupSelfLink = selfLink;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_GROUPS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_GROUPS Exception occurred", ex);
            }
            return groupSelfLink;
        }

        /// <summary>
        /// Method to delete the specified group
        /// </summary>
        public REST_RESPONSE DeleteGroup(string groupSelfLink)
        {
            REST_RESPONSE response = new REST_RESPONSE();

            try
            {
                byte[] bdata = new byte[0];

                //Post the new element to the MSE
                REST_CLIENT client = new REST_CLIENT(groupSelfLink, HttpVerb.DELETE);

                response = client.MakeRequest();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_GROUPS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_GROUPS Exception occurred", ex);
            }

            return response;
        }

        // NOTE: Methods below now obsolete
        /// <summary>
        /// Generate entire group containing elements
        /// Create a new playlist
        /// </summary>
        /*
        public REST_RESPONSE CreateGroup_Obsolete(string playlistURI, string vizChannel, string groupName, string templateURI, BindingList<RacePreviewModel> racePreviewElements)
        {
            //Save the results in a new object
            REST_RESPONSE restResponse = new REST_RESPONSE();

            try
            {
                // Build group header and element XML
                string newGroup = getGroupCreateXML(groupName);

                // Check for elements in collection
                if (racePreviewElements.Count > 0)
                {
                    // Iterate through each element in the preview collection and add the element to the group
                    for (int i = 0; i < racePreviewElements.Count; ++i)
                    {
                        // Get the element from the collection
                        RacePreviewModel racePreviewElement;
                        racePreviewElement = racePreviewElements[i];

                        // Add the element to the group
                        newGroup = newGroup + getRaceboardElementCreateXML(i.ToString() + ": " + racePreviewElement.Raceboard_Description,
                            vizChannel, templateURI, racePreviewElement.Raceboard_Preview_Field_Text, racePreviewElement.Raceboard_Type_Field_Text);
                    }
                }

                // Close out the group
                newGroup = newGroup + getGroupCloseXML();

                byte[] bdata = System.Text.Encoding.UTF8.GetBytes(newGroup);

                // Call method that does not use header - content type specified by client object
                REST_CLIENT client = new REST_CLIENT(playlistURI, HttpVerb.POST, bdata, ContentTypes.Group_Creation);

                var response = client.MakeRequest();

                XElement doc = XElement.Parse(response.xmlResponse);

                //Get the self link - used for deletion of playlist
                var selfURI = doc.Descendants(Atom + "link")
                    .Where(x => (string)x.Attribute("rel").Value == "self")
                    .FirstOrDefault();

                var selfLink = selfURI.Attribute("href").Value;

                //Get the alternate link - used for the element collection
                var altURI = doc.Descendants(Atom + "link")
                    .Where(x => (string)x.Attribute("rel").Value == "alternate")
                    .FirstOrDefault();

                var altLink = altURI.Attribute("href").Value;

                restResponse.selfLink = selfLink;
                restResponse.alternateLink = altLink;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_GROUPS Exception occurred: " + ex.Message);
                log.Debug("MANAGE_GROUPS Exception occurred", ex);
            }

            return restResponse;
        }
        */

        /// <summary>
        /// Methods for generating XML to create groups with elements
        /// </summary>
        /*
        public string getGroupCreateXML(string groupName)
        {
            string groupCreateXML = string.Empty;

            try
            {
                groupCreateXML =
                    "<atom:feed xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:vaext=\"http://www.vizrt.com/atom-ext\">" +
                    "<atom:title>" + groupName + "</atom:title>";
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_GROUPS Exception occurred: " + ex.Message);
                log.Debug("MANAGE_GROUPS Exception occurred", ex);
            }

            return groupCreateXML;
        }
        */


        /// <summary>
        /// Create general XML for race board element - obsolete approach from Viz
        /// </summary>
        /*
        public string getRaceboardElementCreateXML(string elementName, string vizChannel, string templateURI, string previewCommand, string typeCommand)
        {
            string elementCreateXML = string.Empty;

            try
            {
                elementCreateXML =
                    "<atom:entry xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:viz=\"http://www.vizrt.com/atom\">" +
                    "<atom:title>" + elementName + "</atom:title>" +
                    "<viz:channel>" + vizChannel + "</viz:channel>" +
                    "<viz:alternative_concept>graphic_concept</viz:alternative_concept>" +
                    "<atom:content type = \"application/vnd.vizrt.payload+xml\">" +
                    "<payload xmlns=\"http://www.vizrt.com/types\" model=\"" + templateURI + "\">" +
                    "<field name=\"" + TemplateFieldNames.RaceBoard_Template_Preview_Field + "\">" +
                    "<value>\"" + previewCommand + "\"</value>" +
                    "</field>" +
                    "<field name=\"" + TemplateFieldNames.RaceBoard_Template_Type_Field + "\">" +
                    "<value>\"" + typeCommand + "\"</value>" +
                    "</field>" +
                    "</payload>" +
                    "</atom:content>" +
                    "</atom:entry>";
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_GROUPS Exception occurred: " + ex.Message);
                log.Debug("MANAGE_GROUPS Exception occurred", ex);
            }

            return elementCreateXML;
        }
        */

        /// <summary>
        /// Create general XML for race board element - obsolete approach from Viz
        /// Generate XML to close a group definition
        /// </summary>
        /*
        public string getGroupCloseXML()
        {
            string groupCloseXML = string.Empty;
            groupCloseXML = "</atom:feed>";

            return groupCloseXML;
        }
        */
    }
}
