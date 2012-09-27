using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace IBCMaintenance.Configuration
{
    public class StoreName
    {

        string _OnlineUser, _OnlineUserDaily;
        string _GetKeySetting, _GetKeySettingByKey, _InsKeySetting, _UpdKeySetting, _DelKeySetting;
        string _GetFunctionUsage;
        string _GET_DepositDataToValidate_SumWL, _GET_DepositDataToValidate_SumWLOld, _GET_DepositDataToValidate_LicenseeWL, _GET_DepositDataToValidate_Betting, _GET_DepositDataToValidate_DW;
        string _Lrf_SelBalanceUpDay;
        string _GET_CreditDataToValidate_Betting, _GET_CreditDataToValidate_DW;
        //string _GetAllDomain,_GetAllDomainByID,_UpdateDomain,_DeleteDomain,_InsertDomain;
        //string _SelStatistics;
        public StoreName()
        {
            bool isLive = bool.Parse(ConfigurationManager.AppSettings["SP.Live"]);
            if (isLive)
            {
                _OnlineUser = "Is_SelTotalOnlineUser";
                _OnlineUserDaily = "Is_OnlineReportDaily";
                _GetKeySetting = "Is_GetKeySettings";
                _GetKeySettingByKey = "Is_GetKeySettingsByKey";
                _InsKeySetting = "Is_InsKeySetting";
                _UpdKeySetting = "Is_UpdKeySetting";
                _DelKeySetting = "Is_DelKeySetting";
                _GetFunctionUsage = "IS_GetFunctionUsage";


                _PartnerSelByPage = "Is_Odds_PartnerSelByPage";
                _PartnerGet = "Is_Odds_PartnerSel";
                _PartnerIns = "Is_Odds_PartnerIns";
                _PartnerUpd = "Is_Odds_PartnerUpdInfo";
                _PartnerDel = "Is_Odds_PartnerDel";
                _IPSettingSel = "Is_Odds_IPSettingSel";
                _IPSettingIns = "Is_Odds_IPSettingIns";
                _IPSettingUpd = "Is_Odds_IPSettingUpd";
                _IPSettingDel = "Is_Odds_IPSettingDel";

                _ProfileSel = "Is_Odds_ProfileSel";
                _ProfileSelByPage = "Is_Odds_ProfileSelByPage";
                _ProfileIns = "Is_Odds_ProfileIns";
                _ProfileUpd = "Is_Odds_ProfileUpd";
                _ProfileDel = "Is_Odds_ProfileDel";
                _SystemSel = "Is_Odds_SystemSel";
                _SystemIns = "Is_Odds_SystemIns";
                _SystemUpd = "Is_Odds_SystemUpd";
                _SystemDel = "Is_Odds_SystemDel";
                _SystemSelByPage = "Is_Odds_SystemSelByPage";
                _PartnerDomainSel = "Is_Odds_PartnerDomainSel";
                _PartnerDomainDel = "Is_Odds_DomainSettingDel";
                _PartnerDomainIns = "Is_Odds_DomainSettingIns";
                _RemainPartnerDomainSel = "Is_Odds_RemainPartnerDomainSel";
                _DomainBySystemSel = "Is_Odds_DomainBySystemSel";
                _DomainSel = "Is_Odds_DomainSel";
                _DomainSelByPage = "Is_Odds_DomainSelByPage";
                _DomainSettingSelByPage = "Is_Odds_DomainSettingSelByPage";
                _UpdateDomain = "Is_Odds_DomainUpd";
                _DeleteDomain = "Is_Odds_DomainDel";
                _InsertDomain = "Is_Odds_DomainIns";
                _StatisticSelByMonth = "Is_Odds_StatisticSelByMonth";
                _StatisticDetailSel = "Is_Odds_StatisticDetailSel";
            }
            else
            {
                string suffix = ConfigurationManager.AppSettings["SP.Suffix"];

                _OnlineUser = "Is_SelTotalOnlineUser" ;
                _OnlineUserDaily = "Is_OnlineReportDaily" ;
                _GetKeySetting = "Is_GetKeySettings" ;
                _GetKeySettingByKey = "Is_GetKeySettingsByKey" ;
                _InsKeySetting = "Is_InsKeySetting" ;
                _UpdKeySetting = "Is_UpdKeySetting" ;
                _DelKeySetting = "Is_DelKeySetting" ;
                _GetFunctionUsage = "IS_GetFunctionUsage" ;

                _PartnerSelByPage = "Is_Odds_PartnerSelByPage" + suffix;
                _PartnerGet = "Is_Odds_PartnerSel" + suffix;
                _PartnerIns = "Is_Odds_PartnerIns" + suffix;
                _PartnerUpd = "Is_Odds_PartnerUpdInfo" + suffix;
                _PartnerDel = "Is_Odds_PartnerDel" + suffix;
                _IPSettingSel = "Is_Odds_IPSettingSel" + suffix;
                _IPSettingIns = "Is_Odds_IPSettingIns" + suffix;
                _IPSettingUpd = "Is_Odds_IPSettingUpd" + suffix;
                _IPSettingDel = "Is_Odds_IPSettingDel" + suffix;

                _ProfileSel = "Is_Odds_ProfileSel" + suffix;
                _ProfileSelByPage = "Is_Odds_ProfileSelByPage" + suffix;
                _ProfileIns = "Is_Odds_ProfileIns" + suffix;
                _ProfileUpd = "Is_Odds_ProfileUpd" + suffix;
                _ProfileDel = "Is_Odds_ProfileDel" + suffix;
                _SystemSel = "Is_Odds_SystemSel" + suffix;
                _SystemIns = "Is_Odds_SystemIns" + suffix;
                _SystemUpd = "Is_Odds_SystemUpd" + suffix;
                _SystemDel = "Is_Odds_SystemDel" + suffix;
                _SystemSelByPage = "Is_Odds_SystemSelByPage" + suffix;
                _PartnerDomainSel = "Is_Odds_PartnerDomainSel" + suffix;
                _PartnerDomainDel = "Is_Odds_DomainSettingDel" + suffix;
                _PartnerDomainIns = "Is_Odds_DomainSettingIns" + suffix;
                _RemainPartnerDomainSel = "Is_Odds_RemainPartnerDomainSel" + suffix;
                _DomainBySystemSel = "Is_Odds_DomainBySystemSel" + suffix;
                _DomainSel = "Is_Odds_DomainSel" + suffix;
                _DomainSelByPage = "Is_Odds_DomainSelByPage" + suffix;
                _DomainSettingSelByPage = "Is_Odds_DomainSettingSelByPage" + suffix;
                _UpdateDomain = "Is_Odds_DomainUpd" + suffix;
                _DeleteDomain = "Is_Odds_DomainDel" + suffix;
                _InsertDomain = "Is_Odds_DomainIns" + suffix;
                _StatisticSelByMonth = "Is_Odds_StatisticSelByMonth" + suffix;
                _StatisticDetailSel = "Is_Odds_StatisticDetailSel" + suffix;
            }

            _GET_DepositDataToValidate_SumWL = "GET_DepositDataToValidate_SumWL";
            _GET_DepositDataToValidate_SumWLOld = "GET_DepositDataToValidate_SumWLOld";
            _GET_DepositDataToValidate_LicenseeWL = "GET_DepositDataToValidate_LicenseeWL";
            _GET_DepositDataToValidate_Betting = "GET_DepositDataToValidate_Betting";
            _GET_DepositDataToValidate_DW = "GET_DepositDataToValidate_DW";
            _Lrf_SelBalanceUpDay = "Lrf_SelBalanceUpDay";

            _GET_CreditDataToValidate_Betting = "GET_CreditDataToValidate_Betting";
            _GET_CreditDataToValidate_DW = "GET_CreditDataToValidate_DW";

        }
        public string OnlineUser
        {
            get
            {
                return _OnlineUser;
            }
        }
        public string OnlineUserDaily
        {
            get
            {
                return _OnlineUserDaily;
            }
        }
        public string GetKeySetting
        {
            get
            {
                return _GetKeySetting;
            }
        }
        public string GetKeySettingByKey
        {
            get
            {
                return _GetKeySettingByKey;
            }
        }
        public string InsKeySetting
        {
            get
            {
                return _InsKeySetting;
            }
        }
        public string UpdKeySetting
        {
            get
            {
                return _UpdKeySetting;
            }
        }
        public string DelKeySetting
        {
            get
            {
                return _DelKeySetting;
            }
        }
        public string GetFuctionUsage
        {
            get
            {
                return _GetFunctionUsage;
            }
        }

        public string GET_DepositDataToValidate_SumWL
        {
            get
            {
                return _GET_DepositDataToValidate_SumWL;
            }
        }

        public string GET_DepositDataToValidate_SumWLOld
        {
            get
            {
                return _GET_DepositDataToValidate_SumWLOld;
            }
        }


        public string GET_DepositDataToValidate_LicenseeWL
        {
            get
            {
                return _GET_DepositDataToValidate_LicenseeWL;
            }
        }

        public string GET_DepositDataToValidate_Betting
        {
            get
            {
                return _GET_DepositDataToValidate_Betting;
            }
        }

        public string GET_DepositDataToValidate_DW
        {
            get
            {
                return _GET_DepositDataToValidate_DW;
            }
        }

        public string Lrf_SelBalanceUpDay
        {
            get
            {
                return _Lrf_SelBalanceUpDay;
            }
        }

        public string GET_CreditDataToValidate_Betting
        {
            get
            {
                return _GET_CreditDataToValidate_Betting;
            }
        }

        public string GET_CreditDataToValidate_DW
        {
            get
            {
                return _GET_CreditDataToValidate_DW;
            }
        }

        #region Partner

        private String _PartnerSelByPage;
        public String PartnerSelByPage { get { return _PartnerSelByPage; } }

        private String _PartnerGet;
        public String PartnerGet { get { return _PartnerGet; } }

        private String _PartnerIns;
        public String PartnerIns { get { return _PartnerIns; } }

        private String _PartnerUpd;
        public String PartnerUpd { get { return _PartnerUpd; } }

        private String _PartnerDel;
        public String PartnerDel { get { return _PartnerDel; } }

        #endregion

        #region IPSetting

        private String _IPSettingSel;
        public String IPSettingSel { get { return _IPSettingSel; } }

        private String _IPSettingIns;
        public String IPSettingIns { get { return _IPSettingIns; } }

        private String _IPSettingUpd;
        public String IPSettingUpd { get { return _IPSettingUpd; } }

        private String _IPSettingDel;
        public String IPSettingDel { get { return _IPSettingDel; } }

        #endregion

        #region Profile

        private String _ProfileSel;
        public String ProfileSel { get { return _ProfileSel; } }

        private String _ProfileSelByPage;
        public String ProfileSelByPage { get { return _ProfileSelByPage; } }

        private String _ProfileIns;
        public String ProfileIns { get { return _ProfileIns; } }

        private String _ProfileUpd;
        public String ProfileUpd { get { return _ProfileUpd; } }

        private String _ProfileDel;
        public String ProfileDel { get { return _ProfileDel; } }
        #endregion

        #region System

        private String _SystemSel;
        public String SystemSel { get { return _SystemSel; } }

        private String _SystemIns;
        public String SystemIns { get { return _SystemIns; } }

        private String _SystemUpd;
        public String SystemUpd { get { return _SystemUpd; } }

        private String _SystemDel;
        public String SystemDel { get { return _SystemDel; } }

        private String _SystemSelByPage;
        public String SystemSelByPage { get { return _SystemSelByPage; } }

        #endregion

        #region DomainSetting

        private String _PartnerDomainSel;
        public String PartnerDomainSel { get { return _PartnerDomainSel; } }

        private String _PartnerDomainDel;
        public String PartnerDomainDel { get { return _PartnerDomainDel; } }

        private String _PartnerDomainIns;
        public String PartnerDomainIns { get { return _PartnerDomainIns; } }

        private String _RemainPartnerDomainSel;
        public String RemainPartnerDomainSel { get { return _RemainPartnerDomainSel; } }

        #endregion

        #region Domain
        
        
        private String _DomainBySystemSel;
        public String DomainBySystemSel { get { return _DomainBySystemSel; } }


        private String _DomainSel;
        public String DomainSel { get { return _DomainSel; } }

        private String _DomainSelByPage;
        public String DomainSelByPage { get { return _DomainSelByPage; } }

        private String _DomainSettingSelByPage;
        public String DomainSettingSelByPage { get { return _DomainSettingSelByPage; } }

        private String _UpdateDomain;
        public String UpdDomain { get { return _UpdateDomain; } }

        private String _DeleteDomain;
        public String DeleteDomain { get { return _DeleteDomain; } }

        private String _InsertDomain;
        public String InsertDomain { get { return _InsertDomain; } }

        private String _StatisticSelByMonth;
        public String StatisticSelByMonth { get { return _StatisticSelByMonth; } }


        private String _StatisticDetailSel;
        public String StatisticDetailSel { get { return _StatisticDetailSel; } }
        #endregion
    }
}
