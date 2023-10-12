using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    public const string firstOpen = "firstOpen";
    public const string stateSoundFX = "stateSoundFX";
    public const string stateMusic = "stateMusic";
    public const string stateVibration = "stateVibration";
    public const string language = "language";
    public const string rateUsSuccess = "rateUsSuccess";
    //public const string rateUsShowPlayGameCount = "rateUsShowPlayGameCount";
    public const string rateUsShowed_Turn = "rateUsShowed_{0}";
    public const string timeRequestAds = "timeRequestAds";
    public const string indexBGComplete = "indexBGComplete";
    public const string coinTotal = "coinTotal";
    public const string turnPlay = "turnPlay";
    public const string settedMKT = "settedMKT";
    public const string numberNameRecord = "numberNameRecord";
    public const string stringMonsterMaked = "stringMonsterMaked";
    public const string isShowStateScoreInAlbum = "isShowStateScoreInAlbum";
    public const string isUnlockLand_IdLand = "isUnlockLand_{0}";
    public const string isReviewedAlbum = "isReviewedAlbum";
    public const string isUnlockButtonAlbum = "isUnlockButtonAlbum";
    public const string bestScoreFirst = "bestScoreFirst";
    public const string bestScore = "bestScore";
    public const string isUnlockStageInLandAlbum_id = "isUnlockStageInLandAlbum_{0}";
    public const string seconDanceGetCoinStageInLandAlbum_id = "seconDanceGetCoinStageInLandAlbum_{0}";
    public const string isFullMonsterStageInLandAlbum_id = "isFullMonsterStageInLandAlbum_{0}";
    public const string isCompleteTutorialAlbum = "isCompleteTutorialAlbum";
    public const string isCompleteShowTutorialNoti1Album = "isCompleteShowTutorialNoti1Album";
    public const string isIAPRestored = "isIAPRestored";
    public const string isPackRemoveAdsOwer = "isPackRemoveAdsOwer";
    public const string isUnlockItemSmash_id = "isUnlockItemSmash_{0}";

    public const string timeOpenNotificationFirstApp = "timeOpenNotificationFirstApp";
    public const string checkPushNotificationFirst = "checkPushNotificationFirst";
    public const string firstClearNotification = "firstClearNotification";

    //public const string showRateFirst = "showRateFirst";//

    public const string removedAds = "removedAds";

    public const int numberPowerVibration = 15;

    public const string pushFBOpenFirst = "pushFBOpenFirst";
    //Firebase
    public const string fbFirstOpen = "FIRST_OPEN";
    public const string fbOpenAgain = "AGAIN_OPEN";
    public const string fbSCREEN_VIEW = "SCREEN_VIEW";
    public const string fbPLAY_TURN_XXX = "PLAY_TURN_{0}";
    public const string fbDONE_TURN_XXX = "DONE_TURN_{0}";
    public const string fbUSE_NO_USE_Position = "USE_NO_USE_{0}";
    public const string fbREPLAY_CLICK = "REPLAY_CLICK";
    public const string fbBUY_NAMEITEM = "BUY_{0}";
    public const string fbUSE_NAMEITEM = "USE_{0}";
    public const string fbMUSIC_ON = "MUSIC_ON";
    public const string fbMUSIC_OFF = "MUSIC_OFF";
    public const string fbSOUND_ON = "SOUND_ON";
    public const string fbSOUND_OFF = "SOUND_OFF";
    public const string fbHOME_CLICK = "HOME_CLICK";
    public const string fbSETTING_CLICK = "SETTING_CLICK";
    public const string fbVIBRATION_ON = "VIBRATION_ON";
    public const string fbVIBRATION_OFF = "VIBRATION_OFF";
    public const string fbREMOVEADS_CLICK = "REMOVEADS_CLICK";
    public const string fbREMOVEADS_SUCCESS = "REMOVEADS_SUCCESS";
    public const string fbREQUEST_ADS = "REQUEST_ADS";
    public const string fbREQUEST_ADS_SUCCESS = "REQUEST_ADS_SUCCESS";
    public const string fbADS_REWARD_CLICK_xxx = "ADS_REWARD_CLICK_{0}";//xxx: vi tri reward click: Skip
    public const string fbADS_REWARD_COMPLETED_xxx = "ADS_REWARD_COMPLETED_{0}";//xxx: vi tri reward click: Skip
    public const string fbADS_REWARD_COMPLETED = "ADS_REWARD_COMPLETED";
    public const string fbADS_REWARD_FAIL = "ADS_REWARD_FAIL";
    public const string fbSHOW_INTER_SUCCESS = "SHOW_INTER_SUCCESS";
    public const string fbSHOW_INTER_FAIL = "SHOW_INTER_FAIL";
    public const string fbSHOW_OPEN_ADS_SUCCESS = "SHOW_OPEN_ADS_SUCCESS";
    public const string fbSHOW_OPEN_ADS_FAIL = "SHOW_OPEN_ADS_FAIL";
    public const string fbSHARE_CLICK = "SHARE_CLICK";
    public const string fbSHARE_COMPLETED = "SHARE_COMPLETED";
    public const string fbWALLPAPER_NameBG = "WALLPAPER_{0}";
    public const string fbGIFT_CLICK = "GIFT_CLICK";
    public const string fbGIFT_SHOW_COMPLETED = "GIFT_SHOW_COMPLETED";
    public const string fbALBUM_CLICK = "ALBUM_CLICK";
    public const string fbSHOW_NATIVE_ADS_SUCCESS = "SHOW_NATIVE_ADS_SUCCESS";
    public const string fbNATIVE_ADS_CLICK = "NATIVE_ADS_CLICK";
    public const string fbCLICK_SMASH = "CLICK_SMASH";
    public const string fbCLICK_KEEP_PUNISH = "CLICK_KEEP_PUNISH";
    public const string fbCLICK_ULTIMATE = "CLICK_ULTIMATE";

    public const string fb_Position_OtherStage = "OtherStage";
    public const string fb_Position_Gold = "Gold";
    public const string fb_Position_EarnCoin = "EarnCoin";
    public const string fb_Position_Wallpaper = "Wallpaper";
    public const string fb_Position_Gift = "Gift";
    public const string fb_Position_Album = "Album";

    //
    public const string timeShowInterAds_Between_Inter_Inter = "timeShowInterAds_Between_Inter_Inter";
    public const string timeShowInterAds_Between_Reward_Inter = "timeShowInterAds_Between_Reward_Inter";
    public const string timeShowInterAds_Between_OpenAds_Inter = "timeShowInterAds_Between_OpenAds_Inter";
    public const string timeShowInterAds_Between_StartGame_Inter = "timeShowInterAds_Between_StartGame_Inter";
    public const string timeShowOpenAds_InterAds_OpenAds = "timeShowOpenAds_InterAds_OpenAds";
    public const string timeShowOpenAds_OpenAds_OpenAds = "timeShowOpenAds_OpenAds_OpenAds";

    //Firebase remote
    public const string turn_Start_Show_Inter_FBRemote = "Turn_Start_Show_Inter_FBRemote";
    public const string time_Delay_Show_Inter_Between_InterToInter_FBRemote = "time_Delay_Show_Inter_Between_InterToInter_FBRemote";//thời gian để hiển thị quảng cáo Inter lần tới sau inter vừa hoàn thành
    public const string time_Delay_Show_Inter_Between_RewardToInter_FBRemote = "time_Delay_Show_Inter_Between_RewardToInter_FBRemote";//thời gian để hiển thị quảng cáo lần tới sau Reward vừa hoàn thành; 0: bo qua
    public const string time_Delay_Show_Inter_Between_OpenAdsToInter_FBRemote = "time_Delay_Show_Inter_Between_OpenAdsToInter_FBRemote";//thời gian để hiển thị quảng cáo Inter lần tới sau OpenAds vừa hoàn thành; 0: bo qua
    public const string time_Delay_Show_Inter_Between_StartGameToInter_FBRemote = "time_Delay_Show_Inter_Between_StartGameToInter_FBRemote";//thời gian để hiển thị quảng cáo Inter lần tới sau OpenAds vừa hoàn thành; 0: bo qua
    public const string time_Delay_Show_OpenAds_Between_OpenAdsToOpenAds_FBRemote = "time_Delay_Show_OpenAds_Between_OpenAdsToOpenAds_FBRemote";//thời gian để hiển thị quảng cáo open ads lần tới sau khi đóng open ads vừa hoàn thành;
    public const string time_Delay_Show_OpenAds_Between_InterToOpenAds_FBRemote = "time_Delay_Show_OpenAds_Between_InterToOpenAds_FBRemote";//thời gian để hiển thị quảng cáo open ads lần tới sau khi đóng inter vừa hoàn thành;
    public const string verCodeAppCurrent = "VerCodeAppCurrent";
    public const string timeShowComplete_FBRemote = "TimeShowComplete";//Transition,Score,Complete
    public const string logicAdsTapEnywhere_FBRemote = "LogicAdsTapEnywhere";
    public const string timeDelayGiftDrop_FBRemote = "TimeDelayGiftDrop";
    public const string secondProcessingDanceCollectCoin_FBRemote = "secondProcessingDanceCollectCoin_FBRemote";
    public const string coinCollectAfterProcessedDance_FBRemote = "coinCollectAfterProcessedDance_FBRemote";
    public const string idHeadsHide_FBRemote = "idHeadsHide_FBRemote";//Chuoi id cần ẩn đi
    public const string idEyesHide_FBRemote = "idEyesHide_FBRemote";//Chuoi id cần ẩn đi
    public const string idMouthsHide_FBRemote = "idMouthsHide_FBRemote";//Chuoi id cần ẩn đi
    public const string idAccsHide_FBRemote = "idAccsHide_FBRemote";//Chuoi id cần ẩn đi
    public const string idBodysHide_FBRemote = "idBodysHide_FBRemote";//Chuoi id cần ẩn đi
    public const string isHideBanner_FBRemote = "isHideBanner_FBRemote";//0;1

    public const string newIdDefaultHide_FBRemote = "newIdDefaultHide_FBRemote";//Chuoi id cần ẩn đi
    public const string newIdHairsHide_FBRemote = "newIdHairsHide_FBRemote";//Chuoi id cần ẩn đi
    public const string newIdEyesHide_FBRemote = "newIdEyesHide_FBRemote";//Chuoi id cần ẩn đi
    public const string newIdNewDressHide_FBRemote = "newIdNewDressHide_FBRemote";//Chuoi id cần ẩn đi
    public const string newIdNewShoeHide_FBRemote = "newIdNewShowHide_FBRemote";//Chuoi id cần ẩn đi

    //Test
    //BannerView-Bieu ngu
    public const string bannerViewIdAndroidTest = "ca-app-pub-3940256099942544/6300978111";
    public const string bannerViewIdIOSTest = "ca-app-pub-3940256099942544/2934735716";

    //InterstitialAd - xen ke
    public const string interstitialAdIdAndroidTest = "ca-app-pub-3940256099942544/1033173712";
    public const string interstitialAdIdIOSTest = "ca-app-pub-3940256099942544/4411468910";

    //RewardedAd - xem video co tang thuong
    public const string rewardedAdIdAndroidTest = "ca-app-pub-3940256099942544/5224354917";
    public const string rewardedAdIdIOSTest = "ca-app-pub-3940256099942544/1712485313";

    public static string pathDataSave = Application.persistentDataPath + "/MonstersData.data";
}
