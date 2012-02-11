using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace AsyncWebServer
{
    /// <summary>
    /// Summary description for AsyncWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AsyncWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public IAsyncResult BeginGetPage(String url, AsyncCallback callback, Object state)
        {
            var req = WebRequest.Create(url);
            return req.BeginGetResponse(callback, req);

        }

        [WebMethod]
        public String EndGetPage(IAsyncResult ar)
        {
            var req = (WebRequest)ar.AsyncState;
            var response = req.EndGetResponse(ar);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
