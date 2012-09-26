using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using DNAide.Data;
using IBCMaintenance.Configuration;
using System.Text;
namespace Fanex.Maintenance.Report.Core.Report
{
    public class UserOnlineDaily
    {
        public static DataTable GetDataOnlineUserDaily(DateTime MyDate)
        {
            StoreName sp = new StoreName();
            DataTable dtb = new DataTable();
            Hashtable htb = new Hashtable();
            htb.Add("date", MyDate);
            dtb = DbHelper.Execute(sp.OnlineUserDaily, htb, DatabaseType.ON_USER).ResultSet.Tables[0];
            return dtb;
        }
    }
    public partial class GenerateHTML
    {
        public static string GenOnlineUserDaily(DateTime MyDate)
        {
            DataTable dtb = UserOnlineDaily.GetDataOnlineUserDaily(MyDate);
            StringBuilder _s = new StringBuilder();
            foreach (DataRow dr in dtb.Rows)
            {
                _s.Append("<tr class=\"row1h\">");
                _s.Append("<td>" + dr["timerange"].ToString() + "</td>");
                _s.Append("<td>" + dr["localtimerange"].ToString() + "</td>");
                _s.Append("<td>" + dr["sag"].ToString() + "</td>");
                _s.Append("<td>" + dr["mag"].ToString() + "</td>");
                _s.Append("<td>" + dr["ag"].ToString() + "</td>");
                _s.Append("<td>" + dr["sag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["mag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["ag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["acc"].ToString() + "</td>");
                _s.Append("<td>" + dr["aff"].ToString() + "</td>");
                _s.Append("<td>" + dr["affadmin"].ToString() + "</td>");
                _s.Append("</tr>");
            }
            return _s.ToString();

        }
    }
}
