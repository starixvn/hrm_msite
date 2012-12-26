// --------------------------------------------------------------------------
// <copyright file="MailConfigViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.MailService
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Core;
    using MailDeliver.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
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
