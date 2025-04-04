using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonFunctionality : MonoBehaviour
{
 
     public void OnClickBackButton()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        DatabaseManager.Instance.isOnlineModeActive = false;
        DatabaseManager.Instance.OnlineWordsList.Clear();
        SceneManager.LoadScene("StartScene");
       if(DatabaseManager.Instance.isOnlineModeActive){  MediationAdsManager.Instance.DestroyBanner(); }
    } 

    public void ExitGame()
    {
         AudioManager.Instance.PlayPopupOpenSound();
         Application.Quit();
    }
}
