using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour 
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

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

    }

    public void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions()
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError

        };

        Advertisement.Banner.Load(adUnitId, options);
    }

    public void OnBannerLoaded()
    {

    }
    public void OnBannerError(string message)
    {

    }

    public void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
 
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(adUnitId, options);
    }
 
    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
 
    public void OnBannerClicked() { }
    public void OnBannerShown() { }
    public void OnBannerHidden() { }
}
