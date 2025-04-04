using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{     
    [SerializeField] GameObject howToPlayPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject online_offline_SelectionPanel;
    [SerializeField] GameObject selectLanguagePanel;

    
    public GameObject Eng_TickMark;
    public GameObject GER_TickMark;
    public GameObject FRE_TickMark;
    public GameObject SPA_TickMark;

    [SerializeField] Slider downloadingBar;
    [SerializeField] TextMeshProUGUI downloadingPercentage;
    bool isDownloading = false;

    void Awake()
    {
        downloadingBar.gameObject.SetActive(false);
        isDownloading = false;
        downloadingBar.value = 0;

    }

    void Update()
    {
        if(isDownloading) 
        {
            if(DatabaseManager.Instance.counter != 100)
            {
               if(DatabaseManager.Instance.counter != null) DownloadingSliderProgress();
            }
        }

    }

    public void OpenInstructionsPanel()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        if(howToPlayPanel != null) howToPlayPanel.SetActive(true);
    }  

    public void OpenSettingsPanel()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        if(settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void OnClickPlayButton()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        SceneManager.LoadScene("WordGuess");
    }  

    public void SampleTestMediationAds()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        SceneManager.LoadScene("LevelPlaySample");
    }

    public void TestMediationAds()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        SceneManager.LoadScene("MediationTestScene");
    }

    public void OnClickLanguageButton(string databasePath)
    {
        if(databasePath != null) 
        {
            DatabaseManager.Instance.OnSelectLanguage(databasePath);
            MakeTickMarkON(databasePath);
            isDownloading = true;
            
        }

    }

    
    private void MakeTickMarkON(string tickMarkName)
    {
        Eng_TickMark.SetActive(tickMarkName.Equals(Eng_TickMark.name));
        GER_TickMark.SetActive(tickMarkName.Equals(GER_TickMark.name));
        FRE_TickMark.SetActive(tickMarkName.Equals(FRE_TickMark.name));
        SPA_TickMark.SetActive(tickMarkName.Equals(SPA_TickMark.name));

    }

    private void DownloadingSliderProgress()
    {
        downloadingBar.gameObject.SetActive(true);
        Debug.Log("Counter For UI: --> " +  DatabaseManager.Instance.counter);
        downloadingBar.value = DatabaseManager.Instance.counter;
        downloadingPercentage.text = DatabaseManager.Instance.counter.ToString() + "%";
    }

    public void IsOnlineMode(bool isonline)
    {
        if(isonline != null)
        {
            DatabaseManager.Instance.isOnlineModeActive = isonline;
            InternetChecker.Instance.CheckInternetOnlyInOnlineMode();
            if(isonline)
            {
                AudioManager.Instance.PlayPopupOpenSound();
                selectLanguagePanel.SetActive(true);
            }
            else
            {
                OnClickPlayButton();
            }
        }

    }

    public void OpenOnlineOfflineSelectionPanel()
    {
         AudioManager.Instance.PlayButtonClickSound();
         online_offline_SelectionPanel.SetActive(true);
    }

}
