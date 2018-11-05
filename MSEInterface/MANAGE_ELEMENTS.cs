using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Collections.Specialized;

namespace MSEInterface
{

    using Constants;
    using DataModel;

    public class MANAGE_ELEMENTS
    {
        
        internal static readonly XNamespace Viz = "http://www.vizrt.com/types";
        internal static readonly XNamespace VizAtom = "http://www.vizrt.com/atom";
        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// Get a list of names for the pages/elements in the specified show; populate list of objects with title, self/alt links
        /// </summary>
        public List<ElementObject> GetListOfElements(string elementsCollectionURI)
        {
            var elementList = new List<ElementObject>();
            XElement elementDoc;

            GET_URI getURI = new GET_URI();

            try
            {
                //Get all the entries
                var elementNames = getURI.SendGETRequest(elementsCollectionURI).Descendants(Atom + "entry");

                foreach (XElement element in elementNames)
                {
                    string id = element.Element(Atom + "title").Value;

                    string selfLink = string.Empty;
                    elementDoc = element.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "self")
                        .FirstOrDefault();
                    if (elementDoc != null)
                    {
                        selfLink = elementDoc.Attribute("href").Value;
                    }

                    string alternateLink = string.Empty;
                    elementDoc = element.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "alternate")
                        .FirstOrDefault();
                    if (elementDoc != null)
                    {
                        alternateLink = elementDoc.Attribute("href").Value;
                    }

                    string relatedLink = string.Empty;
                    elementDoc = element.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "related")
                        .FirstOrDefault();
                    if (elementDoc != null)
                    {
                        relatedLink = elementDoc.Attribute("href").Value;
                    }

                    ElementObject elementObject = new ElementObject();
                    elementObject.id = id;
                    elementObject.selfLink = selfLink;
                    elementObject.alternateLink = alternateLink;

                    elementList.Add(elementObject);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_ELEMENTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_ELEMENTS Exception occurred", ex);
            }

            return elementList;
        }


        /// <summary>
        /// Creates a new element
        /// Specify Trio channel name when creating element, but not when updating
        /// </summary>
        public void createNewElement(string PageTitle, string elementCollectionURI, string elementModelURI, Dictionary<string, string> elementList, string trioChannelName)
        {
            try
            {
                // Create a VDF doc object
                string newVDF = CreateVDFDoc(elementModelURI, elementList);

                byte[] bdata = System.Text.Encoding.UTF8.GetBytes(newVDF);

                //Set the headers for the post request - header is the name of the page for the new element
                NameValueCollection headers = new NameValueCollection();
                headers.Add("slug", PageTitle);

                //Post the new element to the MSE
                REST_CLIENT client = new REST_CLIENT(elementCollectionURI, headers, HttpVerb.POST, bdata, ContentTypes.VDF_Document);

                REST_RESPONSE response = new REST_RESPONSE();
                response = client.MakeRequest();

                //Update the payload of the new element -- i.e. channel assignment, name
                //Dummy data for testing purposes - cannot be updated in current MSE version due to bug: Summary, Title, Author
                XElement updatedDoc = new XElement(updateElementPayload(response.headerLocation, PageTitle, PageTitle, "Election Stack Builder", trioChannelName));

                //Update the previously created element
                updateVizElement(response.headerLocation, updatedDoc);
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_ELEMENTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_ELEMENTS Exception occurred", ex);
            }
        }


        /// <summary>
        /// Create a VDF document
        /// </summary>
        private string CreateVDFDoc(string elementModelURI, Dictionary<string, string> fieldValue)
        {
            string vdf = "<payload xmlns=\"http://www.vizrt.com/types\" model=\"" + elementModelURI + "\">";
            try
            {
                foreach (var item in fieldValue)
                {
                    vdf += string.Format(
                        @"<field name=""{0}""><value>{1}</value></field>",
                        item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_ELEMENTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_ELEMENTS Exception occurred", ex);
            }
            vdf += "</payload>";

            return vdf;
        }

        /// <summary>
        /// Create version of updated existing xml document - NEEDS TO BE FLESHED OUT
        /// </summary>
        private XElement updateElementPayload(string URI, string summaryUpdate, string titleUpdate, string authorUpdate, string channelUpdate)
        {
            REST_CLIENT client = new REST_CLIENT(URI);
            REST_RESPONSE response = new REST_RESPONSE();

            XElement doc = null;

            try
            {
                response = client.MakeRequest();

                //Create an XElement from the REST response
                doc = XElement.Parse(response.xmlResponse);

                //Change the summary field from "unknown"
                var summary = doc.Descendants(Atom + "summary")
                   .FirstOrDefault();

                summary.Value = summaryUpdate;

                var title = doc.Descendants(Atom + "title")
                   .FirstOrDefault();

                title.Value = titleUpdate;

                var author = doc.Descendants(Atom + "author")
                    .Descendants(Atom + "name")
                   .FirstOrDefault();

                author.Value = authorUpdate;

                //SET THE ENGINE
                doc.Add(new XElement(VizAtom + "channel", channelUpdate));
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_ELEMENTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_ELEMENTS Exception occurred", ex);
            }

            return doc;
        }


        /// <summary>
        /// Sends a put request to POST an updated (via updateElementPayload) XML payload
        /// </summary>
        private void updateVizElement(string URI, XElement payload)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                payload.Save(ms);

                byte[] bdata = ms.ToArray();

                var client = new REST_CLIENT(URI, HttpVerb.PUT, bdata, "application/atom+xml;type=entry");

                client.MakeRequest();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_ELEMENTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_ELEMENTS Exception occurred", ex);
            }
        }


        /// <summary>
        /// Update an existing element based on the playlist, element and specified values
        /// Specify Trio channel name when creating element, but not when updating
        /// </summary>
        public void UpdateExistingElementField(string playlistAltURI, string playlistElementName, Dictionary<string, string> dataFieldList)
        {
            try
            {
                GET_URI request = new GET_URI();

                XElement doc = request.SendGETRequest(playlistAltURI + "elements/" + playlistElementName + "/");

                //Loop through each field to update
                foreach (KeyValuePair<string, string> field in dataFieldList)
                {

                    var name = doc.Descendants(Viz + "field")
                                   .Where(x => (string)x.Attribute("name") == field.Key.ToString())
                                   .Descendants(Viz + "value")
                                   .FirstOrDefault();

                    name.Value = field.Value.ToString();

                }

                //Create memory stream and serialize document
                MemoryStream ms = new MemoryStream();
                doc.Save(ms);

                byte[] bdata = ms.ToArray();

                //Setup the update to the element and PUT the updated data
                var client = new REST_CLIENT(playlistAltURI + "elements/" + playlistElementName + "/", HttpVerb.PUT, bdata, ContentTypes.VDF_Document);

                REST_RESPONSE response = new REST_RESPONSE();

                response = client.MakeRequest();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_ELEMENTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_ELEMENTS Exception occurred", ex);
            }
        }

    }
}
