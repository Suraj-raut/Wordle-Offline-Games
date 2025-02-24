using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayPanelScript : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel;

    public void OpenInstructionsPanel()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        if(howToPlayPanel != null) howToPlayPanel.SetActive(true);
    }

    public void PlayClosePopUpSound()
    {
        AudioManager.Instance.PlayButtonClickSound();
        this.transform.gameObject.SetActive(false);
        
    } 

}
