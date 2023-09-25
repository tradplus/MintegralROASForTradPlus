# MintegralROASForTradPlus

#### iOS 在收到TP展示回调后向Mintegral ROAS 设置方法如下：

```
#import <TradPlusAds/TradPlusAds.h>
#import <MTGSDK/MTGTrackAdRevenue.h>
#import <MTGSDK/MTGSDK.h>

//初始化MTGSDK
[[MTGSDK sharedInstance] setAppID:appid ApiKey:appkey];
//初始化TradPlus SDK 
[TradPlus initSDK:@"您的tradplus appid" completionBlock:^(NSError *error){}];
//设置全局展示回调   
[TradPlus sharedInstance].impressionDelegate = self;

#pragma mark - TradPlusAdImpressionDelegate
- (void)tradPlusAdImpression:(NSDictionary *)adInfo
{
    MTGTrackAdRevenueCustomModel *customModel = [[MTGTrackAdRevenueCustomModel alloc]init];
    
    //If the integration is appsflyer
    customModel.attributionPlatformName = MTGAttributionPlatformNameAppsFlyer;
    customModel.attributionUserID = [[AppsFlyerLib shared]getAppsFlyerUID];
    //If the integration is Adjust
   // customModel.attributionPlatformName = MTGAttributionPlatformNameAdjust;
   // customModel.attributionUserID = [Adjust adid];
    
    customModel.mediationUnitId = adInfo[@"adunit_id"];
    customModel.adType = adInfo[@"adType"];
    customModel.adNetworkName = adInfo[@"adsource_name"];
    customModel.precision = adInfo[@"ecpm_precision"];
    CGFloat revenue = [adInfo[@"ecpm"] floatValue]/1000.0;
    customModel.revenue = @(revenue);
    NSString *instanceId = [NSString stringWithFormat:@"%@%@",adInfo[@"request_id"],adInfo[@"adsource_placement_id"]];
    customModel.adNetworkUnitInfo = @{@"instanceId":instanceId};
     
    [MTGTrackAdRevenue trackAdRevenueWithAdRevenueModel:customModel];
}

```

#### Unity 
1.项目中导入.untiypage 
2.在 Mintegral 菜单中选择 TradPlus ，每个平台都需要选择一次 （可参考 settingRes下的截图）
3.在收到TP展示后调用Mintegral ROAS 设置方法（可参考 unityDemo）
 
```
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
    //收到展示回调后将数据传给 MTGROAS
    string attributionPlatformName = MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_APPSFLYER;
    //"userid"替换为归因平台UID
    MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(attributionPlatformName, "userid");
    mBridgeRevenueParamsEntity.SetTradPlusAdInfo(adInfo);
    MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
}

```
