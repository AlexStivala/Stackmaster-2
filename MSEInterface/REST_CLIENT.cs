using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Specialized;
using System.Xml.Linq;

public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE
}

namespace MSEInterface
{
    /// <summary>
    /// Main REST client class
    /// </summary>
    public class REST_CLIENT
    {

        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public byte[] PostData { get; set; }
        public string PostType { get; set; }
        public NameValueCollection Headers { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private bool UseCredentials;
        private bool UseHeaders;

 
        //Overloaded constructors

        public REST_CLIENT(string endpoint)
        {
            this.EndPoint = endpoint;
            this.Method = HttpVerb.GET;
            this.ContentType = "text/xml";
            this.PostData = null;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.UseCredentials = false;
            this.UseHeaders = false;
        }

        public REST_CLIENT(string endpoint, HttpVerb method)
        {
            this.EndPoint = endpoint;
            this.Method = method;
            this.ContentType = "text/xml";
            this.PostData = null;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.UseCredentials = false;
            this.UseHeaders = false;
        }

        public REST_CLIENT(string endpoint, HttpVerb method, byte[] postData, string contentType)
        {
            this.EndPoint = endpoint;
            this.Method = method;
            this.ContentType = contentType;
            this.PostData = postData;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.UseCredentials = false;
            this.UseHeaders = false;
        }

        public REST_CLIENT(string endpoint, NameValueCollection headers, HttpVerb method, byte[] postData, string contentType)
        {
            this.EndPoint = endpoint;
            this.Method = method;
            this.ContentType = contentType;
            this.PostData = postData;
            this.Headers = headers;
            this.UseCredentials = false;
            this.UseHeaders = true;
            this.Username = string.Empty;
            this.Password = string.Empty;
        }

        //Make Request with no parameters
        public REST_RESPONSE MakeRequest()
        {
            return MakeRequest("");
        }

        /// <summary>
        /// Make Request with parameters to upload files
        /// </summary>
        public REST_RESPONSE MakeRequest(string parameters)
        {
          REST_RESPONSE restResponse = new REST_RESPONSE();

          var request = (HttpWebRequest)WebRequest.Create(EndPoint);

          request.Method = Method.ToString();
          request.ContentLength = 0;
          request.ContentType = ContentType;
        
          //Check to see if credentials are required
          if (this.UseCredentials)
          {
              request.Credentials = new NetworkCredential(this.Username, this.Password);
          }

          //Check to see if headers are required
          if (this.UseHeaders)
          {
              //Get the first key
              string headerKey = this.Headers.GetKey(0).ToString();
              //Get the value of the first key
              string headerValue = this.Headers[this.Headers.GetKey(0)];

              //Set the request header
              request.Headers.Add(headerKey, headerValue);
          }

          //POST or PUT Data
          if (PostData != null && ((Method == HttpVerb.POST) || (Method == HttpVerb.PUT)))
          {
              request.ContentLength = PostData.Length;

              using (var writeStream = request.GetRequestStream())
              {
                  writeStream.Write(PostData, 0, PostData.Length);
              }
          }

          //Make the Request
          try
          {       
              using (var response = (HttpWebResponse)request.GetResponse())
              {

                  // Displays each header and it's key associated with the response. 
                  for (int i = 0; i < response.Headers.Count; ++i)
                  {
                      //Console.WriteLine("\nHeader Name:{0}, Value :{1}", response.Headers.Keys[i], response.Headers[i]);

                      if (response.Headers.Keys[i] == "Location")
                      {
                          restResponse.headerLocation = response.Headers[i];
                      }
                  }

                  var responseValue = string.Empty;
                 
                  // grab the response
                  using (var responseStream = response.GetResponseStream())
                  {
                 
                      if (responseStream != null)
                     
                          using (var reader = new StreamReader(responseStream))
                          {
                            
                              responseValue = reader.ReadToEnd();

                          }
                  }

                  restResponse.xmlResponse = responseValue;

                  return restResponse;
              }

          }

          catch (Exception ex)
          {
              // Log error
              log.Error("REST_CLIENT Exception occurred: " + ex.Message);
              //log.Debug("REST_CLIENT Exception occurred", ex);
          }

          return null;
        }

  }
}

