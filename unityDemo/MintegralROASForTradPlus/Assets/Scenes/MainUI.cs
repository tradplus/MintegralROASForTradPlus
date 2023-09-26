using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using System;

public class MainUI : MonoBehaviour
{
#if UNITY_ANDROID
    string adUnitId = "788E1FCB278B0D7E97282231154458B7";
    string appId = "6640E7E3BDAC951B8F28D4C8C50E50B5";
    string mtgAppId = "144002";
    string mtgAppKey = "7c22942b749fe6a6e361b675e96b3ee9";
#else
    string adUnitId = "580EA72031574148726E8BD5C2EB43D6";
    string appId = "DB6D75719E469134132D4490CA557A3E";
    string mtgAppId = "150180";
    string mtgAppKey = "7c22942b749fe6a6e361b675e96b3ee9";
#endif
    string infoStr = "";
    // Start is called before the first frame update
    void Start()
    {
        ////初始化MTGSDK
        MBridgeSDKManager.initialize(mtgAppId, mtgAppKey);
        //初始化TPSDK
        TradplusAds.Instance().InitSDK(appId);
        TradplusAds.Instance().AddGlobalAdImpression(OnGlobalAdImpression);
    }

    void OnGlobalAdImpression(Dictionary<string, object> adInfo)
    {
        Debug.Log("OnGlobalAdImpression ------ adInfo:" + Json.Serialize(adInfo));
        //收到展示回调后将数据传给 MTGROAS
        string attributionPlatformName = MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_APPSFLYER;
        //"userid"替换为归因平台UID
        MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(attributionPlatformName, "userid");
        mBridgeRevenueParamsEntity.SetTradPlusAdInfo(adInfo);
        MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        float height = (Screen.height - 140) / 8 - 20;
        GUI.skin.button.fixedHeight = height;
        GUI.skin.button.fontSize = (int)(height / 3);
        GUI.skin.label.fontSize = (int)(height / 3);
        GUI.skin.label.fixedHeight = height;

        var rect = new Rect(20, 20, Screen.width - 40, Screen.height);
        rect.y += Screen.safeArea.y;
        GUILayout.BeginArea(rect);
        GUILayout.Space(20);
        if (GUILayout.Button("加载"))
        {
            infoStr = "开始加载";
            TPInterstitialExtra extra = new TPInterstitialExtra();
            TradplusInterstitial.Instance().LoadInterstitialAd(adUnitId, extra);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("isReady"))
        {
            bool isReady = TradplusInterstitial.Instance().InterstitialAdReady(adUnitId);
            infoStr = "isReady: "+ isReady;
        }
        GUILayout.Space(20);
        if (GUILayout.Button("展示"))
        {
            infoStr = "";
            TradplusInterstitial.Instance().ShowInterstitialAd(adUnitId);
        }
        GUILayout.Space(20);
        GUILayout.Label(infoStr);
        GUILayout.EndArea();
    }

    //回调设置
    private void OnEnable()
    {
        //常用
        TradplusInterstitial.Instance().OnInterstitialLoaded += OnlLoaded;
        TradplusInterstitial.Instance().OnInterstitialLoadFailed += OnLoadFailed;
        TradplusInterstitial.Instance().OnInterstitialImpression += OnImpression;
        TradplusInterstitial.Instance().OnInterstitialShowFailed += OnShowFailed;
        TradplusInterstitial.Instance().OnInterstitialClicked += OnClicked;
        TradplusInterstitial.Instance().OnInterstitialClosed += OnClosed;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusInterstitial.Instance().OnInterstitialStartLoad += OnStartLoad;
        TradplusInterstitial.Instance().OnInterstitialBiddingStart += OnBiddingStart;
        TradplusInterstitial.Instance().OnInterstitialBiddingEnd += OnBiddingEnd;
        TradplusInterstitial.Instance().OnInterstitialIsLoading += OnAdIsLoading;

        TradplusInterstitial.Instance().OnInterstitialOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoaded += OnOneLayerLoaded;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayStart += OnVideoPlayStart;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayEnd += OnVideoPlayEnd;
        TradplusInterstitial.Instance().OnInterstitialAllLoaded += OnAllLoaded;

#if UNITY_ANDROID

        TradplusInterstitial.Instance().OnDownloadStart += OnDownloadStart;
        TradplusInterstitial.Instance().OnDownloadUpdate += OnDownloadUpdate;
        TradplusInterstitial.Instance().OnDownloadFinish += OnDownloadFinish;
        TradplusInterstitial.Instance().OnDownloadFailed += OnDownloadFailed;
        TradplusInterstitial.Instance().OnDownloadPause += OnDownloadPause;
        TradplusInterstitial.Instance().OnInstalled += OnInstallled;
#endif
    }

    private void OnDestroy()
    {
        TradplusInterstitial.Instance().OnInterstitialLoaded -= OnlLoaded;
        TradplusInterstitial.Instance().OnInterstitialLoadFailed -= OnLoadFailed;
        TradplusInterstitial.Instance().OnInterstitialImpression -= OnImpression;
        TradplusInterstitial.Instance().OnInterstitialShowFailed -= OnShowFailed;
        TradplusInterstitial.Instance().OnInterstitialClicked -= OnClicked;
        TradplusInterstitial.Instance().OnInterstitialClosed -= OnClosed;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoadFailed -= OnOneLayerLoadFailed;

        TradplusInterstitial.Instance().OnInterstitialStartLoad -= OnStartLoad;
        TradplusInterstitial.Instance().OnInterstitialBiddingStart -= OnBiddingStart;
        TradplusInterstitial.Instance().OnInterstitialBiddingEnd -= OnBiddingEnd;
        TradplusInterstitial.Instance().OnInterstitialIsLoading -= OnAdIsLoading;

        TradplusInterstitial.Instance().OnInterstitialOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoaded -= OnOneLayerLoaded;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayStart -= OnVideoPlayStart;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayEnd -= OnVideoPlayEnd;
        TradplusInterstitial.Instance().OnInterstitialAllLoaded -= OnAllLoaded;

#if UNITY_ANDROID

        TradplusInterstitial.Instance().OnDownloadStart -= OnDownloadStart;
        TradplusInterstitial.Instance().OnDownloadUpdate -= OnDownloadUpdate;
        TradplusInterstitial.Instance().OnDownloadFinish -= OnDownloadFinish;
        TradplusInterstitial.Instance().OnDownloadFailed -= OnDownloadFailed;
        TradplusInterstitial.Instance().OnDownloadPause -= OnDownloadPause;
        TradplusInterstitial.Instance().OnInstalled -= OnInstallled;
#endif

    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        //Debug.Log("InterstitialUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        // Debug.Log("InterstitialUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        //Debug.Log("InterstitialUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        // Debug.Log("InterstitialUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        //Debug.Log("InterstitialUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        // Debug.Log("InterstitialUI OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        //Debug.Log("InterstitialUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        //Debug.Log("InterstitialUI OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        //Debug.Log("InterstitialUI OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnAdIsLoading(string adunit)
    {
        //Debug.Log("InterstitialUI OnAdIsLoading ------ adunit:" + adunit );
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        // Debug.Log("InterstitialUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        // Debug.Log("InterstitialUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        // Debug.Log("InterstitialUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
    {
        // Debug.Log("InterstitialUI OnVideoPlayStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
    {
        //Debug.Log("InterstitialUI OnVideoPlayEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        //Debug.Log("InterstitialUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnDownloadStart(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        // Debug.Log("InterstitialUI OnDownloadStart ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadUpdate(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
    {
        //Debug.Log("InterstitialUI OnDownloadUpdate ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "; progress:" + progress);
    }

    void OnDownloadPause(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        // Debug.Log("InterstitialUI OnDownloadPause ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFinish(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        // Debug.Log("InterstitialUI OnDownloadFinish ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFailed(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        //Debug.Log("InterstitialUI OnDownloadFailed ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnInstallled(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        //   Debug.Log("InterstitialUI OnInstallled ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }
}