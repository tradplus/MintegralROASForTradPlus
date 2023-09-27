# MintegralROASForTradPlus

#### Unity 

1.项目中导入 mintegral_roas_for_tradplus.untiypage 

2.在 Mintegral 菜单中选择 TradPlus ，每个平台都需要选择一次 （可参考 settingRes下的截图）

3.在收到TP展示后调用Mintegral ROAS 设置方法（可参考 unityDemo）
 
```
void Start()
{
    //初始化MTGSDK
    MBridgeSDKManager.initialize(mtgAppId, mtgAppKey);
    //初始化TPSDK
    TradplusAds.Instance().InitSDK(appId);
    TradplusAds.Instance().AddGlobalAdImpression(OnGlobalAdImpression);
}

void OnGlobalAdImpression(Dictionary<string, object> adInfo)
{
    //收到展示回调后将数据传给 MTGROAS
    string attributionPlatformName = MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_APPSFLYER;
    //"userid"替换为归因平台UID
    MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(attributionPlatformName, "userid");
    mBridgeRevenueParamsEntity.SetTradPlusAdInfo(adInfo);
    MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
}

```
