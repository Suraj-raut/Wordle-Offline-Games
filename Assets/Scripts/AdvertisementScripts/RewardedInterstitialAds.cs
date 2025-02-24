using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class RewardedInterstitialAds : MonoBehaviour ,IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitID;
    [SerializeField] private string iosAdUnitID;

    private string adUnitId;
    public bool isRewarded = false;

    public event Action OnRewardedAdCompleted;

    private void Awake()
    {
        #if UNITY_IOS
            adUnitId = iosAdUnitID;
        #elif UNITY_ANDROID
            adUnitId = androidAdUnitID;
        #endif

    }

    public void LoadRewardedAds()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(adUnitId, this);
        LoadRewardedAds();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Rewarded Ad Loaded..");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
       Debug.Log("Rewarded Ad Failed to Load..");
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
        if(placementId == adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
             Debug.Log("Rewarded Ads Shown Successfully..Kindly Reward the player");
             isRewarded = true;
             OnRewardedAdCompleted?.Invoke();

        }
       
    }
}
