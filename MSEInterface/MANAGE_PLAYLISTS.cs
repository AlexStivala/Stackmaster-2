using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Xml;

namespace MSEInterface
{
    using Constants;
    using DataModel;

    public class MANAGE_PLAYLISTS
    {

        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";
        internal static readonly XNamespace App = "http://www.w3.org/2007/app";

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// LIST OF PLAYLISTS FOR SHOW
        /// Get a list of playlists for the specified show playlist directory URI
        /// </summary>
        public List<PlaylistObject> GetListOfShowPlaylists(string playlistDirectoryURI)
        {

            var playlistList = new List<PlaylistObject>();

            try
            {
                XElement playlistDoc;

                GET_URI getURI = new GET_URI();

                // Get all the entries
                var playlistNames = getURI.SendGETRequest(playlistDirectoryURI).Descendants(Atom + "entry");

                foreach (XElement playlist in playlistNames)
                {
                    string title = playlist.Element(Atom + "title").Value;

                    string selfLink = string.Empty;
                    playlistDoc = playlist.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "self")
                        .FirstOrDefault();
                    if (playlistDoc != null)
                    {
                        selfLink = playlistDoc.Attribute("href").Value;
                    }

                    string alternateLink = string.Empty;
                    playlistDoc = playlist.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "alternate")
                        .FirstOrDefault();
                    if (playlistDoc != null)
                    {
                        alternateLink = playlistDoc.Attribute("href").Value;
                    }

                    PlaylistObject playlistObject = new PlaylistObject();
                    playlistObject.title = title;
                    playlistObject.selfLink = selfLink;
                    playlistObject.alternateLink = alternateLink;

                    playlistList.Add(playlistObject);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }
            return playlistList;
        }


        /// <summary>
        /// Check to see if a playlist with the specified name already exists in the VDOM
        /// </summary>
        public string CheckForPlaylistExists(string playlistDirectoryURI, string playlistName)
        {
            string playlistURI = string.Empty;

            try
            {
                XElement playlistDoc;

                GET_URI getURI = new GET_URI();

                //Get all the playlist entries for the specified show
                var playlistNames = getURI.SendGETRequest(playlistDirectoryURI).Descendants(Atom + "entry");

                if (playlistNames != null)
                {
                    // Walk through each playlist and check for match by title
                    foreach (XElement playlist in playlistNames)
                    {
                        string title = playlist.Element(Atom + "title").Value;

                        // If title matches, get the self-link to the playlist
                        if (title == playlistName)
                        {
                            string selfLink = string.Empty;
                            playlistDoc = playlist.Descendants(Atom + "link")
                                .Where(x => (string)x.Attribute("rel") == "self")
                                .FirstOrDefault();
                            if (playlistDoc != null)
                            {
                                selfLink = playlistDoc.Attribute("href").Value;
                                // Set return value
                                playlistURI = selfLink;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }
            return playlistURI;
        }

        /// <summary>
        /// Get the Alt link for the specified playlist
        /// </summary>
        public string GetPlaylistAltLink(string playlistDirectoryURI, string playlistName)
        {
            string playlistAltLink = string.Empty;

            try
            {
                XElement playlistDoc;

                GET_URI getURI = new GET_URI();

                //Get all the playlist entries for the specified show
                var playlistNames = getURI.SendGETRequest(playlistDirectoryURI).Descendants(Atom + "entry");

                if (playlistNames != null)
                {
                    // Walk through each playlist and check for match by title
                    foreach (XElement playlist in playlistNames)
                    {
                        string title = playlist.Element(Atom + "title").Value;

                        // If title matches, get the Alt-link to the playlist
                        if (title == playlistName)
                        {
                            string altLink = string.Empty;
                            playlistDoc = playlist.Descendants(Atom + "link")
                                .Where(x => (string)x.Attribute("rel") == "alternate")
                                .FirstOrDefault();
                            if (playlistDoc != null)
                            {
                                altLink = playlistDoc.Attribute("href").Value;
                                // Set return value
                                playlistAltLink = altLink;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }
            return playlistAltLink;
        }

        /// <summary>
        /// Get the Down link for the specified playlist - used for grouping
        /// </summary>
        public string GetPlaylistDownLink(string playlistDirectoryURI, string playlistName)
        {
            string playlistDownLink = string.Empty;

            try
            {
                XElement playlistDoc;

                GET_URI getURI = new GET_URI();

                //Get all the playlist entries for the specified show
                var playlistNames = getURI.SendGETRequest(playlistDirectoryURI).Descendants(Atom + "entry");

                if (playlistNames != null)
                {
                    // Walk through each playlist and check for match by title
                    foreach (XElement playlist in playlistNames)
                    {
                        string title = playlist.Element(Atom + "title").Value;

                        // If title matches, get the Alt-link to the playlist
                        if (title == playlistName)
                        {
                            string downLink = string.Empty;
                            playlistDoc = playlist.Descendants(Atom + "link")
                                .Where(x => (string)x.Attribute("rel") == "down")
                                .FirstOrDefault();
                            if (playlistDoc != null)
                            {
                                downLink = playlistDoc.Attribute("href").Value;
                                // Set return value
                                playlistDownLink = downLink;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }
            return playlistDownLink;
        }


        /// <summary>
        /// Check for the existence of a playlist
        /// </summary>
        public Boolean CheckIfPlaylistExists(string playlistDirectoryURI, string playlistName)
        {
            Boolean playlistExists = false;

            try
            {
                GET_URI getURI = new GET_URI();

                //Get all the playlist entries for the specified show
                var playlistNames = getURI.SendGETRequest(playlistDirectoryURI).Descendants(Atom + "entry");

                if (playlistNames != null)
                {
                    // Walk through each playlist and check for match by title
                    foreach (XElement playlist in playlistNames)
                    {
                        string title = playlist.Element(Atom + "title").Value;

                        // If title matches, get the Alt-link to the playlist
                        if (title == playlistName)
                        {
                            playlistExists = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }

            return playlistExists;
        }

        /// <summary>
        /// Create a new playlist
        /// </summary>
        public REST_RESPONSE CreatePlaylist(string playlistsDirectoryURI, string playlistName)
        {

            //Save the results in a new object
            REST_RESPONSE restResponse = new REST_RESPONSE();

            byte[] bdata = new byte[0];

            try
            {
                NameValueCollection headers = new NameValueCollection();

                //Playlist name goes into the slug attribute
                headers.Add("slug", playlistName);

                REST_CLIENT client = new REST_CLIENT(playlistsDirectoryURI, headers, HttpVerb.POST, bdata, ContentTypes.Playlist);

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
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }

            return restResponse;
        }

        /// <summary>
        /// Delete the specified playlist
        /// </summary>
        public void DeletePlaylist(string deletePlaylistURI)
        {
            try
            {
                REST_CLIENT client = new REST_CLIENT(deletePlaylistURI, HttpVerb.DELETE);
                client.MakeRequest();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }
        }

        /// <summary>
        /// Gets a URI to the list of elements in a playlist
        /// </summary>
        public string GetElementCollectionFromPlaylist(string playlistAltURI)
        {
            string collectionURI = string.Empty;

            try
            {
                GET_URI getURI = new GET_URI();

                var playlist = getURI.SendGETRequest(playlistAltURI).Descendants(Atom + "link")
                     .Where(x => (string)x.Attribute("rel") == "self")
                     .FirstOrDefault();

                collectionURI = playlist.Attribute("href").Value;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PLAYLISTS Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PLAYLISTS Exception occurred", ex);
            }          

            return collectionURI;
        }

    }
}
