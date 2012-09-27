using System;
using System.Collections;
using System.Data;
using System.Text;
using IBCMaintenance.Configuration;
namespace Fanex.Maintenance.Report.Core.Report
{
    using DNAide.Data;
    using DNAide.Utils;

    public class FunctionUsage
    {
        public static DbResult GetFunctionUsage(DateTime fromDate, DateTime toDate, string function, string subfunction)
        {
            StoreName sp = new StoreName();
            Hashtable htb = new Hashtable();
            htb.Add("fromdate", FormatHandler.ResetStartDate(fromDate));
            htb.Add("todate", FormatHandler.ResetEndDate(toDate));
            htb.Add("function", function);
            htb.Add("subfunction", subfunction);
            DbResult dbr = DbHelper.Execute(sp.GetFuctionUsage, htb, DatabaseType.DBLog);
            return dbr;
        }
    }
    public partial class GenerateHTML
    {
        public static string GenFunctionUsage(DateTime fromDate, DateTime toDate, string function, string subfunction)
        {
            DataTable dtb = DataHandler.FunctionUsage.GetFunctionUsage(fromDate, toDate, function, subfunction).ResultSet.Tables[0];
            StringBuilder _s = new StringBuilder();
            int total = 0;
            int super = 0;
            int master = 0;
            int agent = 0;
            int accounting = 0;
            int affiliate = 0;

            foreach (DataRow r in dtb.Rows)
            {
                total += Convert.ToInt32(r["totalagent"]);
                super += Convert.ToInt32(r["super"]);
                master += Convert.ToInt32(r["master"]);
                agent += Convert.ToInt32(r["agent"]);
                accounting += Convert.ToInt32(r["accounting"]);
                affiliate += Convert.ToInt32(r["affiliate"]);

                _s.Append("<tr class=\"row1h\">");
                _s.Append("<td>" + r["logdate"].ToString() + "</td>");
                _s.Append("<td align='left'>" + "<a href=\"#\" onclick=\"ShowData('" + r["function"].ToString() + "','');\">" + r["function"].ToString() + "</a></td>");
                _s.Append("<td align='left'>" + "<a href=\"#\" onclick=\"ShowData('','" + r["subfunction"].ToString() + "');\">" + r["subfunction"].ToString() + "</a></td>");
                _s.Append("<td>" + r["totalagent"].ToString() + "</td>");
                _s.Append("<td>" + r["super"].ToString() + "</td>");
                _s.Append("<td>" + r["master"].ToString() + "</td>");
                _s.Append("<td>" + r["agent"].ToString() + "</td>");
                _s.Append("<td>" + r["accounting"].ToString() + "</td>");
                _s.Append("<td>" + r["affiliate"].ToString() + "</td>");
                _s.Append("</tr>");

            }
            _s.Append("<tr align='center'>");
            _s.Append("<td colspan=\"3\" align='right'>Total</td>");
            _s.Append("<td>" + total.ToString() + "</td>");
            _s.Append("<td>" + super.ToString() + "</td>");
            _s.Append("<td>" + master.ToString() + "</td>");
            _s.Append("<td>" + agent.ToString() + "</td>");
            _s.Append("<td>" + accounting.ToString() + "</td>");
            _s.Append("<td>" + affiliate.ToString() + "</td>");
            _s.Append("</tr>");
            return _s.ToString();
        }
    }
}
