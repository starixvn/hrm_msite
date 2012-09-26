using System.Text;

namespace Fanex.Maintenance.Report.Core.Report
{
    public class TemplateCredit_Template
    {
        protected static string s0 = "&nbsp;<table class=\"tblCreditdata\" cellpadding=\"2px\" cellspacing=\"0px\"><tr><td colspan=\"2\" class=\"tblHeader0\">CREDIT SITES</td><td class=\"tblHeader\">rpt_betting</td><td class=\"tblHeader\" >DW</td></tr>";
        public EmailTemplateCredit_Turnover_Block Turnover = new EmailTemplateCredit_Turnover_Block();
        public EmailTemplateCredit_CustomerWinLoss_Block CustomerWinLoss = new EmailTemplateCredit_CustomerWinLoss_Block();
        public EmailTemplateCredit_BetCount_Block BetCount = new EmailTemplateCredit_BetCount_Block();
        protected static string s1 = "</table>";
        public void ToString(ref StringBuilder bd)
        {
            bd.Append(s0);
            if (null != Turnover) Turnover.ToString(ref bd, "Turnover");
            if (null != CustomerWinLoss) CustomerWinLoss.ToString(ref bd, "CustomerWL");
            if (null != BetCount) BetCount.ToString(ref bd, "BetCount");
            bd.Append(s1);
        }
        public override string ToString()
        {
            StringBuilder bd = new StringBuilder();
            ToString(ref bd);
            return bd.ToString();
        }

    }

    public class EmailTemplateCredit_Turnover_Block : BlockCredit
    {
    }

    public class EmailTemplateCredit_CustomerWinLoss_Block : BlockCredit
    {
    }

    public class EmailTemplateCredit_BetCount_Block : BlockCredit
    {
    }
}
