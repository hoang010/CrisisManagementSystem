using CMS2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CMS2.ReportAndSocialMedia_Module
{
    public class SocialMediaJobs
    {
        //tweet the required message to the account
        public void tweetMessage(SocialMediaUpdates socialMediaUpdates)
        {
            try
            {
                //api usage
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://cmsntu.herokuapp.com/tweet");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"tweet\":" + "\"" + socialMediaUpdates.Description.ToString() + "\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}