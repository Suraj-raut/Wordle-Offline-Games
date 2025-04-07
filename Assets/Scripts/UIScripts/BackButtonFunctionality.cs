using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonFunctionality : MonoBehaviour
{
 
     public void OnClickBackButton()
    {
        AudioManager.Instance.PlayButtonClickSound();
        if(DatabaseManager.Instance.isOnlineModeActive){  MediationAdsManager.Instance.DestroyBanner(); }
        DatabaseManager.Instance.OnlineWordsList.Clear();
        DatabaseManager.Instance.isOnlineModeActive = false;
        SceneManager.LoadScene("StartScene");
       
    } 

    public void ExitGame()
    {
         AudioManager.Instance.PlayButtonClickSound();
         Application.Quit();
    }
}
