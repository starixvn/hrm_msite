// -----------------------------------------------------------------------
// <copyright file="MailSettingViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Criteria.MailService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;
    using MailDeliver.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MailFunctionViewModel
    {
        public int FunctionId { get; set; }

        [Required]
        [Display(Name = "Setting", ResourceType = typeof(ViewResource))]
        public int SettingId { get; set; }

        [Required]
        [Display(Name = "Config", ResourceType = typeof(ViewResource))]
        public int ConfigId { get; set; }

        [Required]
        [Display(Name = "Function_Name", ResourceType = typeof(ViewResource))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "InputLengthValidationMsg",
            ErrorMessageResourceType = typeof(ViewResource))]
        [RegularExpression(@"^[\w\s]{1,}$",
            ErrorMessageResourceName = "InvalidNameValidationMsg",
            ErrorMessageResourceType = typeof(ViewResource))]
        public string FunctionName { get; set; }

        public MailFunctionViewModel()
        {
        }

        public MailFunctionViewModel(MailFunction mailFunction)
        {
            this.ConfigId = mailFunction.Config.AppID;
            this.SettingId = mailFunction.Setting.SettingID;
            this.FunctionId = mailFunction.FuncID;
            this.FunctionName = mailFunction.FuncName;
        }

        public MailFunction GetMailFunction()
        {
            MailFunction mailFunction = new MailFunction
            {
                Config = new MailConfig
                {
                    AppID = this.ConfigId
                },
                Setting = new MailSetting
                {
                    SettingID = this.SettingId
                },
                FuncName = this.FunctionName,
                FuncID = this.FunctionId
            };

            return mailFunction;
        }
    }
}
