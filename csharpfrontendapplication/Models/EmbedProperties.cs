using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syncfusion.Server.EmbedApplication.Models
{
    public class EmbedProperties
    {
        //BoldBI server URL (ex: http://localhost:5000/bi, http://demo.boldbi.com/bi)
        public static string RootUrl = "http://localhost:59102/bi/designer";

        //For Bold BI Enterprise edition, it should be like `site/site1`. For Bold BI Cloud, it should be empty string.
        public static string SiteIdentifier = "http://localhost:59102/bi/site/site1";

        //Enter your BoldBI credentials here.
        public static string UserEmail = "rajkumar.nachiappan@syncfusion.com";
        
        // Get the embedSecret key from Bold BI, please check this link(https://help.syncfusion.com/bold-bi/on-premise/site-settings/embed-settings)
        public static string EmbedSecret = "3KeapmAM9suDtt57RCkIu7EywfKofitd";
    }
}