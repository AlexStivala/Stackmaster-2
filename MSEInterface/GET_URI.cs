using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace MSEInterface
{
    //Class to instantiate a REST_CLIENT and RES_RESPONSE, and request an XElement object for the specified URI
    public class GET_URI
    {

        internal static readonly XNamespace Atom = "http://www.w3.org/2005/Atom";
        internal static readonly XNamespace App = "http://www.w3.org/2007/app";

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// GENERIC METHOD TO SEND GET
        /// </summary>
        public XElement SendGETRequest(string URI)
        {

            REST_RESPONSE response = new REST_RESPONSE();
            REST_CLIENT client = new REST_CLIENT(URI);

            try
            {
                response = client.MakeRequest();
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("GET_URI Exception occurred while making Get URI request: " + ex.Message);
                //log.Debug("GET_URI Exception occurred while making Get URI request", ex);
            }

            XElement doc = null;

            // Don't process if response is null
            if ((response != null) & (response.xmlResponse != null))
            {
                try
                {
                    doc = XElement.Parse(response.xmlResponse);
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("GET_URI Exception occurred while trying to parse response: " + ex.Message);
                    //log.Debug("GET_URI Exception occurred while trying to parse response", ex);
                }
            }
            
            return doc;     
        }
    }
}
