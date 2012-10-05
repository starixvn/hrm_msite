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
    public class MailConfigViewModel
    {
        public int AppId { get; set; }

        [Required]
        [Display(Name = "Setting", ResourceType = typeof(ViewResource))]
        public int SettingId { get; set; }

        [Required]
        [Display(Name = "Application_Name", ResourceType = typeof(ViewResource))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "InputLengthValidationMsg",
            ErrorMessageResourceType = typeof(ViewResource))]
        [RegularExpression(@"^[\w\s]{1,}$",
            ErrorMessageResourceName = "InvalidNameValidationMsg",
            ErrorMessageResourceType = typeof(ViewResource))]
        public string AppName { get; set; }
                
        [Display(Name = "Description", ResourceType = typeof(ViewResource))]
        public string Description { get; set; }

        public MailConfigViewModel()
        {
        }

        public MailConfigViewModel(MailConfig mailSetting)
        {
            this.AppName = mailSetting.AppName;
            this.Description = mailSetting.Description;
            this.SettingId = mailSetting.Setting.SettingID;
            this.AppId = mailSetting.AppID;
        }

        public MailConfig GetMailConfig()
        {
            MailConfig mailConfig = new MailConfig
            {
                AppID = this.AppId,
                AppName = this.AppName,
                Description = this.Description,
                Setting = new MailSetting
                {
                    SettingID = this.SettingId
                }
            };

            return mailConfig;
        }
    }
}
