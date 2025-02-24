using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour , IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitID;
    [SerializeField] private string iosAdUnitID;

    private string adUnitId;

    private void Awake()
    {
        #if UNITY_IOS
            adUnitId = iosAdUnitID;
        #elif UNITY_ANDROID
            adUnitId = androidAdUnitID;
        #endif

    }

    public void LoadInterstitialAds()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(adUnitId, this);
        LoadInterstitialAds();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial Ad Loaded..");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
       Debug.Log("Interstitial Ad Failed to Load..");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    } 

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Unity Insterstitial Ads Showed Successfully..");
    }
   
   
}
