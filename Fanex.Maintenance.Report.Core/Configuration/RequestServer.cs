using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Web.Configuration;
using System.Net;
using System.IO;
namespace IBCMaintenance.Configuration
{
    public static class RequestServer
    {
        private static Hashtable GetSerVerIP(string site)
        {
            Hashtable htb = new Hashtable();
            System.Configuration.Configuration objConfig = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsetting = (AppSettingsSection)objConfig.GetSection("appSettings");
            if (objAppsetting != null)
            {
                foreach (string key in objAppsetting.Settings.AllKeys)
                {
                    if (site == SiteName._Agent)
                    {
                        if (key.IndexOf("Ag_Url") != -1)
                        {
                            htb.Add(key, objAppsetting.Settings[key].Value);
                        }
                    }
                    else if (site == SiteName._Accounting)
                    {
                        if (key.IndexOf("Acc_Url") != -1)
                        {
                            htb.Add(key, objAppsetting.Settings[key].Value);
                        }
                    }
                    else
                    {
                        if (key.IndexOf("Aff_Url") != -1)
                        {
                            htb.Add(key, objAppsetting.Settings[key].Value);
                        }
                    }                    

                }
            }
            return htb;

        }
        public static void CreateRequestServer(string url, string site, ref Hashtable Err)
        {
            Hashtable htb = GetSerVerIP(site);
            IDictionaryEnumerator en = htb.GetEnumerator();
            try
            {
                while (en.MoveNext())
                {
                    Uri uri = new Uri(en.Value + url);
                    if (uri.Scheme == Uri.UriSchemeHttp)
                    {
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                        request.Method = WebRequestMethods.Http.Post;
                        request.ContentLength = 0;
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string tmp = reader.ReadToEnd();
                        response.Close();
                        if (tmp != "Ok")
                        {
                            Err.Add(en.Value, tmp); 
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                Err.Add(en.Value, ex.Message);
            }
        }
    }
}
