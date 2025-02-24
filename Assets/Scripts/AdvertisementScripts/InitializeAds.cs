using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour , IUnityAdsInitializationListener 
{

    [SerializeField] private string androidGameID;
    [SerializeField] private string iosGameID;

    [SerializeField] private bool isTesting;

    private string gameId;

    private void Awake()
    {
        #if UNITY_IOS
            gameId = iosGameID;
        #elif UNITY_ANDROID
            gameId = androidGameID;
        #elif UNITY_EDITOR
            gameId = androidGameID;
        #endif

        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTesting, this);
        }

    }

    public void OnInitializationComplete()
    {
       Debug.Log("Ads Initialization Completed..");
       AdsManager.Instance.LoadAdsInitially();
       Debug.unityLogger.logEnabled = true; 
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {   
        Debug.Log("Ads Initialization Failed..");
    }
}
