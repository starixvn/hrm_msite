using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Collections.Specialized;
using System.Configuration.Provider;
/*
 * Developer: Viet Hoang
 */
namespace IBCMaintenance.Configuration
{    
    public class SiteMapXML
    {
        private List<SiteMapNode> Nodes;

        #region "urls"
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]        
        #endregion

        public SiteMapXML()
        {
            Nodes = new List<SiteMapNode>();
        }        

        public SiteMapXML(System.Web.SiteMapNode rootNode) : this()
        {
            Add(rootNode);            
        }

        private void Add(System.Web.SiteMapNode rootNode)
        {
            Nodes.Add(rootNode);

            if (rootNode.HasChildNodes)
            {
                foreach (System.Web.SiteMapNode node in rootNode.ChildNodes)
                {
                    if (node.IsAccessibleToUser(System.Web.HttpContext.Current)) Add(node);
                }
            }
        }

        public SiteMapNode FindNodeByURL(string _url)
        {
            SiteMapNode result = null;
            result = Nodes.Find(
                 delegate(SiteMapNode _node)
                 {                     
                     return _node.Url == _url;                     
                 }
             );
            return result;
        }

        public SiteMapNode FindNodeByID(string _id)
        {            
            SiteMapNode result = null;
            result = Nodes.Find(
                 delegate(SiteMapNode _node)
                 {
                     return _node.Title == _id;
                     
                 }
             );
            return result;
        }                
    }

    public class ModuleURL
    {
        private const string siteMapCache = "acc2.sitemapCache";
        
        public ModuleURL()
        {
            
        }

        private static SiteMapXML GetNodes()
        {
            SiteMapXML nodes = new SiteMapXML();
            nodes = (SiteMapXML)CacheFactory.GetCacheManager().GetData(siteMapCache);
            if (nodes == null)
            {
                nodes = new SiteMapXML(SiteMap.RootNode);
                CacheFactory.GetCacheManager().Add(siteMapCache, nodes, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromHours(2)));
            }
            return nodes;
        }

        public static SiteMapNode GetNodeByID(string id)
        {
            return GetNodes().FindNodeByID(id);
        }

        public static SiteMapNode GetNodeByURL(string id)
        {
            return GetNodes().FindNodeByURL(id);
        }
    }
}
