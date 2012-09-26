using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DNAide.Data;
using System.Collections;
using IBCMaintenance.Configuration;
using System.Data;
using System.Text;
namespace Fanex.Maintenance.Report.Core.Report
{
    public class UserOnline
    {
        public static DataTable GetTotelOnlineUser(int top)
        {
            StoreName sp = new StoreName();
            Hashtable htb = new Hashtable();
            htb.Add("top", top);
            DbResult dbr = null;
            dbr = DbHelper.Execute(sp.OnlineUser, htb, DatabaseType.ON_USER);
            return dbr.ResultSet.Tables[0];
        }
    }
    public partial class GenerateHTML
    {
        public static string GenOnlineUser(int show)
        {            
            DataTable dtb = DataHandler.UserOnline.GetTotelOnlineUser(show);
            StringBuilder _s = new StringBuilder();
            foreach (DataRow i in dtb.Rows)
            {
                _s.Append("<tr class=\"row1h\">");
                _s.Append("<td>" + i["logdate"].ToString() + "</td>");
                _s.Append("<td>" + i["totalAgent"].ToString() + "</td>");
                _s.Append("<td>" + i["super"].ToString() + "</td>");
                _s.Append("<td>" + i["master"].ToString() + "</td>");
                _s.Append("<td>" + i["agent"].ToString() + "</td>");
                _s.Append("<td>" + i["accounting"].ToString() + "</td>");
                _s.Append("<td>" + i["affiliate"].ToString() + "</td>");
                _s.Append("</tr>");
            }
            return _s.ToString();
        }
        private static DataTable GenDataTable(DataTable dtb,int RowNumber)
        {
            string _datetime = dtb.Rows[0]["logdate"].ToString();
            for (int i = 0; i < RowNumber; i++)
            {                
                string _date = _datetime.Substring(0, _datetime.IndexOf(" ")).Trim();
                string _hour = _datetime.Substring(_datetime.IndexOf(" "), _datetime.Length - _datetime.IndexOf(" ")).Trim();
                string[] Arr = _hour.Split(':');
                DataRow dr = dtb.NewRow();
                if (Convert.ToInt32(Arr[1]) == 60 )
                {
                    int myhour = Convert.ToInt32(Arr[0]) + AppSetting.Minute;
                    int myminute = Convert.ToInt32(Arr[1]) / 60;
                    _datetime = _date + " " + (myhour < 10 ? "0" + myhour.ToString() : myhour.ToString()) + ":" + (myminute < 10 ? "0" + myminute.ToString() : myminute.ToString());                    
                }
                else
                {
                    int _minute = Convert.ToInt32(Arr[1]) + AppSetting.Minute;

                    _datetime = _date + " " + Arr[0] + ":" + (_minute < 10 ? "0" + _minute.ToString() : _minute.ToString());
                }
                dr[0] = _datetime;            
                dr[1] = "-1";
                dr[2] = "-1";
                dr[3] = "-1";
                dr[4] = "-1";
                dr[5] = "-1";
                dr[6] = "-1";
                dtb.Rows.Add(dr);   
            }
            return dtb;
        }
        public static string GenDataChart(int show)
        {
            string data = "";
            DataTable dtb = DataHandler.UserOnline.GetTotelOnlineUser(show);
            if (dtb.Rows.Count > 0)
            {                
                if (show > dtb.Rows.Count)
                {
                    int addRow = show - dtb.Rows.Count;
                    dtb = GenDataTable(dtb, addRow);
                }
                DataRow[] drs = dtb.Select(null, "logdate desc");

                foreach (DataRow dr in drs)
                {

                    data += ChangeHourToMinute(dr["logdate"].ToString()) + ";" + dr["super"] + ";" + dr["master"] + ";" + dr["agent"] + ";" + dr["accounting"] + ";" + dr["affiliate"] + "^";
                }
                data = data.Substring(0, data.Length - 1);
            }
            return data;
        }
        public static string ChangeHourToMinute(string _DateTime)
        {
            string ngay = _DateTime.Substring(0, _DateTime.IndexOf(" "));
            string hour = _DateTime.Substring(_DateTime.IndexOf(" "), _DateTime.Length - _DateTime.IndexOf(" "));
            string[] arrList = hour.Split(":".ToCharArray());            
            int minute = (Convert.ToInt32(arrList[0]) * 60) + Convert.ToInt32(arrList[1]);                    
            return minute.ToString();
        }
        public static string SplitDateToHour(string date)
        {
            return date = date.Substring(date.Length - 5, 5);
        }
        public static string ChangeMinuteToHour(int minute)
        {
            int hour = minute / 60;
            int min = minute % 60;
            return hour.ToString() + ":" + min.ToString();
        }
    }
}
