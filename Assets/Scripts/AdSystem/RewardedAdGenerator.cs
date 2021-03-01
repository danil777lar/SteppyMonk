using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class RewardedAdGenerator : MonoBehaviour
{
    private IAdCreator creator;

    private RewardBasedVideoAd ad;

    public void Start(){
        string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #else
            adUnitId = "unexpected_platform";
        #endif

        this.ad = RewardBasedVideoAd.Instance;

        ad.OnAdLoaded += HandleOnAdLoaded;
        ad.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        ad.OnAdOpening += HandleOnAdOpening;
        ad.OnAdStarted += HandleOnAdStarted;
        ad.OnAdClosed += HandleOnAdClosed;
        ad.OnAdRewarded += HandleOnAdRewarded;
        ad.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.ad.LoadAd(request, adUnitId);
    }

    public void Show(IAdCreator creator){
        this.creator = creator;
        if (ad.IsLoaded()) ad.Show();
    }


    public void HandleOnAdLoaded (object sender, EventArgs args){}

    public void HandleOnAdFailedToLoad (object sender, AdFailedToLoadEventArgs args){}

    public void HandleOnAdOpening (object sender, EventArgs args){}

    public void HandleOnAdStarted (object sender, EventArgs args){}

    public void HandleOnAdClosed (object sender, EventArgs args){
        DestroyHandlers();
        Start();
    }

    public void HandleOnAdRewarded (object sender, Reward args){
        creator.RewAdClosed();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args){}

    void DestroyHandlers(){
        ad.OnAdLoaded -= HandleOnAdLoaded;
        ad.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        ad.OnAdOpening -= HandleOnAdOpening;
        ad.OnAdStarted -= HandleOnAdStarted;
        ad.OnAdClosed -= HandleOnAdClosed;
        ad.OnAdRewarded -= HandleOnAdRewarded;
        ad.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
    }

    void OnDestroy(){
        DestroyHandlers();
    }
}
