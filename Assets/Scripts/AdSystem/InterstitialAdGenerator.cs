using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InterstitialAdGenerator : MonoBehaviour
{
    private IAdCreator creator;

    private InterstitialAd ad;

    public void Start(){
        string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #else
            adUnitId = "unexpected_platform";
        #endif

        this.ad = new InterstitialAd(adUnitId);

        ad.OnAdLoaded += HandleOnAdLoaded;
        ad.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        ad.OnAdOpening += HandleOnAdOpening;
        ad.OnAdClosed += HandleOnAdClosed;
        ad.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.ad.LoadAd(request);
    }

    public void Show(IAdCreator creator){
        this.creator = creator;
        if (ad.IsLoaded()) ad.Show();
    }


    public void HandleOnAdLoaded (object sender, EventArgs args){}

    public void HandleOnAdFailedToLoad (object sender, AdFailedToLoadEventArgs args){}

    public void HandleOnAdOpening (object sender, EventArgs args){}

    public void HandleOnAdClosed (object sender, EventArgs args){
        DestroyHandlers();
        Start();
        creator.IntAdClosed();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args){}

    void DestroyHandlers(){
        ad.OnAdLoaded -= HandleOnAdLoaded;
        ad.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        ad.OnAdOpening -= HandleOnAdOpening;
        ad.OnAdClosed -= HandleOnAdClosed;
        ad.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
    }

    void OnDestroy(){
        DestroyHandlers();
    }
}
