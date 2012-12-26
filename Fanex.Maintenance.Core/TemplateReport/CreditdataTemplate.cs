// --------------------------------------------------------------------------
// <copyright file="CreditdataTemplate.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Core.TemplateReport
{
    using System.Text;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class TemplateCredit_Template
    {
        //protected static string s0 = "&nbsp;<table class=\"tblCreditdata\" cellpadding=\"2px\" cellspacing=\"0px\"><tr><td colspan=\"2\" class=\"tblHeader0\">CREDIT SITES</td><td class=\"tblHeader\">rpt_betting</td><td class=\"tblHeader\" >DW</td></tr>";
        public EmailTemplateCredit_Turnover_Block Turnover = new EmailTemplateCredit_Turnover_Block();

        public EmailTemplateCredit_CustomerWinLoss_Block CustomerWinLoss = new EmailTemplateCredit_CustomerWinLoss_Block();

        public EmailTemplateCredit_BetCount_Block BetCount = new EmailTemplateCredit_BetCount_Block();

        protected static string s1 = "</table>";

        public void ToString(ref StringBuilder bd)
        {
            bd.Append("<table class='tblCreditdata'>");
            bd.Append("<thead><tr>");
            bd.Append("<th colspan='2'>CREDIT SITES</th>");
            bd.Append("<th>rpt_betting</th>");
            bd.Append("<th>DW</th>");
            bd.Append("</tr></thead>");

            if (null != Turnover)
            {
                Turnover.ToString(ref bd, "Turnover");
            }

            if (null != CustomerWinLoss)
            {
                CustomerWinLoss.ToString(ref bd, "CustomerWL");
            }

            if (null != BetCount)
            {
                BetCount.ToString(ref bd, "BetCount");
            }

            bd.Append(s1);
        }

        public override string ToString()
        {
            StringBuilder bd = new StringBuilder();
            ToString(ref bd);
            return bd.ToString();
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class EmailTemplateCredit_Turnover_Block : BlockCredit
    {
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class EmailTemplateCredit_CustomerWinLoss_Block : BlockCredit
    {
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class EmailTemplateCredit_BetCount_Block : BlockCredit
    {
    }
}
