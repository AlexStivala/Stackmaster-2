using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Xml;
using System.ComponentModel;

namespace MSEInterface
{
    using Constants;
    using DataModel;

    public class MANAGE_SHOWS
    {

        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";
        internal static readonly XNamespace App = "http://www.w3.org/2007/app";

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// Get a list of shows in the network directory; populate list of objects with title, self/alt links
        /// </summary>
        public BindingList<ShowObject> GetListOfShows(string showsDirectoryURI)
        {

            var showList = new BindingList<ShowObject>();

            try
            {
                XElement showDoc;

                GET_URI getURI = new GET_URI();

                //Get all the entries
                var showNames = getURI.SendGETRequest(showsDirectoryURI).Descendants(Atom + "entry");

                if (showNames != null)
                {

                    foreach (XElement show in showNames)
                    {
                        string title = show.Element(Atom + "title").Value;

                        string selfLink = string.Empty;
                        showDoc = show.Descendants(Atom + "link")
                            .Where(x => (string) x.Attribute("rel") == "self")
                            .FirstOrDefault();
                        if (showDoc != null)
                        {
                            selfLink = showDoc.Attribute("href").Value;
                        }

                        string alternateLink = string.Empty;
                        showDoc = show.Descendants(Atom + "link")
                            .Where(x => (string) x.Attribute("rel") == "alternate")
                            .FirstOrDefault();
                        if (showDoc != null)
                        {
                            alternateLink = showDoc.Attribute("href").Value;
                        }

                        string relatedLink = string.Empty;
                        showDoc = show.Descendants(Atom + "link")
                            .Where(x => (string) x.Attribute("rel") == "related")
                            .FirstOrDefault();
                        if (showDoc != null)
                        {
                            relatedLink = showDoc.Attribute("href").Value;
                        }

                        ShowObject showObject = new ShowObject();
                        showObject.title = title;
                        showObject.selfLink = selfLink;
                        showObject.alternateLink = alternateLink;
                        showObject.relatedLink = relatedLink;

                        showList.Add(showObject);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_SHOWS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_SHOWS Exception occurred", ex);
            }

            return showList;
        }


        /// <summary>
        /// Gets the URI to a list of all elements in the specified show
        /// </summary>
        public string GetElementCollectionFromShow(string showDirectoryURI, string showName)
        {
            string elementCollectionURI = string.Empty;

            try
            {

                string showURI = string.Empty;
                XElement showDoc;

                GET_URI getURI = new GET_URI();

                //Get all the entries
                var showNames = getURI.SendGETRequest(showDirectoryURI).Descendants(Atom + "entry");

                // Find the URI for the matching show
                foreach (XElement name in showNames)
                {

                    //Gets node that has the title of the show that was passed
                    if (name.Element(Atom + "title").Value == showName)
                    {
                        showDoc = name.Descendants(Atom + "link")
                          .Where(x => (string)x.Attribute("rel") == "alternate")
                          .FirstOrDefault();

                        //Gets the uri for the show
                        showURI = showDoc.Attribute("href").Value;
                    }
                }

                if (showURI != null)
                {
                    // Call to get the URI for the element collection for the specified show
                    elementCollectionURI = ParseShowResponseForElementCollection(showURI);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_SHOWS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_SHOWS Exception occurred", ex);
            }

            return elementCollectionURI;
        }

        /// <summary>
        /// Parses the xml returned by a show request to get the link to the elements collection
        /// </summary>
        private string ParseShowResponseForElementCollection(string URI)
        {
            string link = string.Empty;

            try
            {
                GET_URI getURI = new GET_URI();

                var collectionURL = getURI.SendGETRequest(URI).Descendants(Atom + "entry")
                    .Where(x => (string)x.Element(Atom + "category")
                    .Attribute("term") == "element_collection")
                    .FirstOrDefault();

                var collectionURI = collectionURL.Descendants(Atom + "link").FirstOrDefault();

                link = collectionURI.Attribute("href").Value;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_SHOWS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_SHOWS Exception occurred", ex);
            }

            return link;
        }


        /// <summary>
        /// PLAYLIST DIRECTORY
        /// Gets the URI to the playlists directory for the show
        /// </summary>
        public string GetPlaylistDirectoryFromShow(string showsDirectoryURI, string showName)
        {
            string playlistDirectoryURI = string.Empty;

            try
            {
                string showURI = string.Empty;
                XElement showDoc;

                GET_URI getURI = new GET_URI();

                //Get all the entries
                var showNames = getURI.SendGETRequest(showsDirectoryURI).Descendants(Atom + "entry");

                // Find the URI for the matching show
                foreach (XElement name in showNames)
                {

                    //Gets node that has the title of the show that was passed
                    if (name.Element(Atom + "title").Value == showName)
                    {
                        showDoc = name.Descendants(Atom + "link")
                          .Where(x => (string)x.Attribute("rel") == "alternate")
                          .FirstOrDefault();

                        //Gets the uri for the show
                        showURI = showDoc.Attribute("href").Value;
                    }
                }

                if (showURI != null)
                {
                    // Call to get the URI for the playlist directory for the specified show
                    playlistDirectoryURI = ParseShowResponseForPlaylistDirectory(showURI);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_SHOWS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_SHOWS Exception occurred", ex);
            }

            return playlistDirectoryURI;
        }

        /// <summary>
        /// Parses the xml returned by a show request to get the link to the playlists directory
        /// </summary>
        private string ParseShowResponseForPlaylistDirectory(string URI)
        {
            string link = string.Empty;

            try
            {
                GET_URI getURI = new GET_URI();


                var collectionURL = getURI.SendGETRequest(URI).Descendants(Atom + "entry")
                    .Where(x => (string)x.Element(Atom + "category")
                    .Attribute("term") == "directory")
                    .FirstOrDefault();

                var collectionURI = collectionURL.Descendants(Atom + "link").FirstOrDefault();

                link = collectionURI.Attribute("href").Value;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_SHOWS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_SHOWS Exception occurred", ex);
            }

            return link;
        }

        /// <summary>
        /// TEMPLATE COLLECTION
        /// Gets the URI to a list of all templates in the specified show
        /// </summary>
        public string GetTemplateCollectionFromShow(string showsDirectoryURI, string showName)
        {
            string templateCollectionURI = string.Empty;

            try
            {
                string showURI = string.Empty;
                XElement showDoc;

                GET_URI getURI = new GET_URI();

                //Get all the entries
                var showNames = getURI.SendGETRequest(showsDirectoryURI).Descendants(Atom + "entry");

                // Find the URI for the matching show
                foreach (XElement name in showNames)
                {

                    //Gets node that has the title of the show that was passed
                    if (name.Element(Atom + "title").Value == showName)
                    {
                        showDoc = name.Descendants(Atom + "link")
                          .Where(x => (string)x.Attribute("rel") == "alternate")
                          .FirstOrDefault();

                        //Gets the uri for the show
                        showURI = showDoc.Attribute("href").Value;
                    }
                }

                if (showURI != null)
                {
                    // Call to get the URI for the templates collection for the specified show
                    templateCollectionURI = ParseShowResponseForTemplateCollection(showURI);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_SHOWS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_SHOWS Exception occurred", ex);
            }

            return templateCollectionURI;
        }


        /// <summary>
        /// Parses the xml returned by a show request to get the link to the template collection
        /// </summary>
        private string ParseShowResponseForTemplateCollection(string showURI)
        {
            string link = string.Empty;

            try
            {
                GET_URI getURI = new GET_URI();

                //Get the templates collection URI for the specified show
                var collectionURL = getURI.SendGETRequest(showURI).Descendants(Atom + "entry")
                .Where(x => (string)x.Element(Atom + "category")
                .Attribute("term") == "templates")
                .FirstOrDefault();

                //Get the URI to the templates collection link element
                var collectionURI = collectionURL.Descendants(Atom + "link").FirstOrDefault();

                //Get the link to the templates collection
                link = collectionURI.Attribute("href").Value;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_SHOWS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_SHOWS Exception occurred", ex);
            }

            return link;
        }
    }
}

