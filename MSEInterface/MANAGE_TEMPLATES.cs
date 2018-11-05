using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace MSEInterface
{
    using Constants;
    using DataModel;

    public class MANAGE_TEMPLATES
    {
        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";
        internal static readonly XNamespace App = "http://www.w3.org/2007/app";

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// LIST OF TEMPLATES FOR A SPECIFIED SHOW
        /// Get a list of templates for the specified show; populate list of objects with title, self/alt links
        /// </summary>
        public List<TemplateObject> GetListOfShowTemplates(string templatesCollectionURI)
        {
            var templateList = new List<TemplateObject>();

            try
            {
                XElement templateDoc;

                GET_URI getURI = new GET_URI();

                //Get all the entries
                var templateNames = getURI.SendGETRequest(templatesCollectionURI).Descendants(Atom + "entry");

                foreach (XElement template in templateNames)
                {
                    string title = template.Element(Atom + "title").Value;

                    string selfLink = string.Empty;
                    templateDoc = template.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "self")
                        .FirstOrDefault();
                    if (templateDoc != null)
                    {
                        selfLink = templateDoc.Attribute("href").Value;
                    }

                    string alternateLink = string.Empty;
                    templateDoc = template.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "alternate")
                        .FirstOrDefault();
                    if (templateDoc != null)
                    {
                        alternateLink = templateDoc.Attribute("href").Value;
                    }

                    TemplateObject templateObject = new TemplateObject();
                    templateObject.title = title;
                    templateObject.selfLink = selfLink;
                    templateObject.alternateLink = alternateLink;

                    templateList.Add(templateObject);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_TEMPLATES Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_TEMPLATES Exception occurred", ex);
            }

            return templateList;
        }


        /// <summary>
        /// GET the template collection URI for the specified template
        /// </summary>
        public string GetTemplateElementModel(string showTemplatesURI, string templateID)
        {
            var model = (string)null;

            try
            {
                GET_URI getURI = new GET_URI();
                XElement templateDoc;

                //var templateCollectionDoc = getURI.SendGETRequest(showTemplatesURI).Descendants(Atom + "link")
                //     //.Where(x => (string)x.Attribute("rel") == "related")
                //     .Where(x => (string)x.Attribute("rel") == "alternate")
                //     .FirstOrDefault();

                var templateCollectionDoc = getURI.SendGETRequest(showTemplatesURI).Descendants(Atom + "entry");

                // Find the URI for the matching show
                foreach (XElement name in templateCollectionDoc)
                {

                    //Gets node that has the ID of the template that was passed
                    if (name.Element(Atom + "title").Value == templateID)
                    {
                        templateDoc = name.Descendants(Atom + "link")
                          .Where(x => (string)x.Attribute("rel") == "alternate")
                          .FirstOrDefault();

                        //Gets the uri for the template fields
                        model = templateDoc.Attribute("href").Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_TEMPLATES Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_TEMPLATES Exception occurred", ex);
            }

            return model;
        }

        /// <summary>
        /// Parse the template collection URI response to get the model URI for the given template
        /// </summary>
        private string ParseTemplateCollectionResponse(string templateURI)
        {
            string link = string.Empty;

            try
            {
                GET_URI getURI = new GET_URI();

                //Get the template URI for the specified template in the template collection
                var templateFeed = getURI.SendGETRequest(templateURI).Descendants(Atom + "entry")
                    //.Where(x => (string)x.Element(Atom + "title").Value == templateID)
                    //.Descendants(Atom + "link")
                    .Where(x => (string)x.Attribute("rel").Value == "alternate")
                    .FirstOrDefault();

                var collectionURI = templateFeed.Descendants(Atom + "link").FirstOrDefault();

                link = collectionURI.Attribute("href").Value;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_TEMPLATES Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_TEMPLATES Exception occurred", ex);
            }

            return link;
        }

        /// <summary>
        /// Check to see if a playlist with the specified name already exists in the VDOM
        /// </summary>
        public string GetTemplateAltLink(string templateDirectoryURI, string templateName)
        {
            string templateAltLink = "";

            try
            {
                XElement templateDoc;

                GET_URI getURI = new GET_URI();

                //Get all the playlist entries for the specified show
                var templateNames = getURI.SendGETRequest(templateDirectoryURI).Descendants(Atom + "entry");

                if (templateNames != null)
                {
                    // Walk through each playlist and check for match by title
                    foreach (XElement playlist in templateNames)
                    {
                        string title = playlist.Element(Atom + "title").Value;

                        // If title matches, get the self-link to the playlist
                        if (title == templateName)
                        {
                            string altLink = "";
                            templateDoc = playlist.Descendants(Atom + "link")
                                .Where(x => (string)x.Attribute("rel") == "alternate")
                                .FirstOrDefault();
                            if (templateDoc != null)
                            {
                                altLink = templateDoc.Attribute("href").Value;
                                // Set return value
                                templateAltLink = altLink;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_TEMPLATES Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_TEMPLATES Exception occurred", ex);
            }

            return templateAltLink;
        }
    }
}
