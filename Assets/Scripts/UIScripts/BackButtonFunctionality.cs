using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonFunctionality : MonoBehaviour
{
 
     public void OnClickBackButton()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        DatabaseManager.Instance.OnlineWordsList.Clear();
        SceneManager.LoadScene("StartScene");
    } 

    public void ExitGame()
    {
         AudioManager.Instance.PlayPopupOpenSound();
         Application.Quit();
    }
}
