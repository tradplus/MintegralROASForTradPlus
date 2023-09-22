# MintegralROASForTradPlus

#### iOS 在收到TP展示回调后向Mintegral ROAS 设置方法如下：

```
#import <TradPlusAds/TradPlusAds.h>
#import <MTGSDK/MTGTrackAdRevenue.h>
#import<MTGSDK/MTGSDK.h>

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

