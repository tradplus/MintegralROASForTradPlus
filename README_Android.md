# MintegralROASForTradPlus

#### Android

在收到TP展示回调后向Mintegral ROAS 设置方法如下：

```

 MBridgeSDK mIntegralSDK = MBridgeSDKFactory.getMBridgeSDK();
        Map<String, String> configurationMap;
        configurationMap = mIntegralSDK.getMBConfigurationMap(mAppId, mAppKey);
        mIntegralSDK.init(configurationMap, context, new SDKInitStatusListener() {
            @Override
            public void onInitSuccess() {

            }

            @Override
            public void onInitFail(String s) {
            }
        });

        TradPlusSdk.setGlobalImpressionListener(new GlobalImpressionManager.GlobalImpressionListener() {
            @Override
            public void onImpressionSuccess(TPAdInfo tpAdInfo) {

                MBridgeRevenueParamsEntityForCustom revenueParamsEntity = new MBridgeRevenueParamsEntityForCustom(attributtionPlatformName, attributionPlatformUserId);

                String adNetwork = tpAdInfo.adSourceName;
                double revenue = Double.valueOf(tpAdInfo.ecpm);
                String precision = tpAdInfo.ecpmPrecision;
                String adFormat = tpAdInfo.format;

                String pid = tpAdInfo.adSourceId;

                JSONObject networkInfoJsonObj = new JSONObject();
                try {
                    networkInfoJsonObj.put("placementId", pid);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                revenueParamsEntity.setNetworkInfo(networkInfoJsonObj);
                revenueParamsEntity.setNetworkName(adNetwork);

                revenueParamsEntity.setMediationName("TradPlus");
                revenueParamsEntity.setAdType(adFormat);
                revenueParamsEntity.setRevenue(revenue/1000 +"");
                revenueParamsEntity.setPrecision(precision);


                MBridgeRevenueManager.track(context, revenueParamsEntity);
            }
        });
