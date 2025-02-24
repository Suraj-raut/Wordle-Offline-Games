using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
   public InitializeAds initializeads;
   public InterstitialAds interstitialAds;
   public RewardedInterstitialAds rewardedAds;
   public BannerAds bannerAds;

    public static AdsManager Instance {get; private set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }

       Debug.unityLogger.logEnabled = true; 
    } 

    public void LoadAdsInitially()
    {
        bannerAds.LoadBannerAd();
        interstitialAds.LoadInterstitialAds();
        rewardedAds.LoadRewardedAds();
        Debug.Log("Loading of ads is finished..");

    }

}
