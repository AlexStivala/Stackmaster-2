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

    public class MANAGE_PROFILES
    {

        internal static readonly XNamespace Viz = "http://www.vizrt.com/types";
        internal static readonly XNamespace VizAtom = "http://www.vizrt.com/atom";
        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// Get the list of profiles and action URIs stored in the MSE
        /// </summary>
        public List<ProfileObject> GetListOfProfiles(string ProfileCollectionURI)
        {

            var profileList = new List<ProfileObject>();

            try
            {
                XElement profileDoc;

                GET_URI getURI = new GET_URI();

                //Get all the entries
                var profileCollection = getURI.SendGETRequest(ProfileCollectionURI).Descendants(Atom + "entry");

                foreach (XElement profile in profileCollection)
                {
                    string title = profile.Element(Atom + "title").Value;

                    //Get action links
                    string readLink = string.Empty;
                    profileDoc = profile.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "read")
                        .FirstOrDefault();
                    if (profileDoc != null)
                    {
                        readLink = profileDoc.Attribute("href").Value;
                    }

                    string cueLink = string.Empty;
                    profileDoc = profile.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "cue")
                        .FirstOrDefault();
                    if (profileDoc != null)
                    {
                        cueLink = profileDoc.Attribute("href").Value;
                    }

                    string takeLink = string.Empty;
                    profileDoc = profile.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "take")
                        .FirstOrDefault();
                    if (profileDoc != null)
                    {
                        takeLink = profileDoc.Attribute("href").Value;
                    }

                    string takeOutLink = string.Empty;
                    profileDoc = profile.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "out")
                        .FirstOrDefault();
                    if (profileDoc != null)
                    {
                        takeOutLink = profileDoc.Attribute("href").Value;
                    }

                    string updateLink = string.Empty;
                    profileDoc = profile.Descendants(Atom + "link")
                        .Where(x => (string)x.Attribute("rel") == "update")
                        .FirstOrDefault();
                    if (profileDoc != null)
                    {
                        updateLink = profileDoc.Attribute("href").Value;
                    }

                    ProfileObject profileObject = new ProfileObject();
                    profileObject.title = title;
                    profileObject.readLink = readLink;
                    profileObject.cueLink = cueLink;
                    profileObject.takeLink = takeLink;
                    profileObject.takeOutLink = takeOutLink;
                    profileObject.updateLink = updateLink;

                    profileList.Add(profileObject);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("MANAGE_PROFILES Exception occurred: " + ex.Message);
                //log.Debug("MANAGE_PROFILES Exception occurred", ex);
            }

            return profileList;
        }


    }
}
