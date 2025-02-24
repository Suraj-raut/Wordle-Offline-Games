using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject bgMusicOn;
    [SerializeField] GameObject bgMusicOff;
    [SerializeField] GameObject soundsOn;
    [SerializeField] GameObject soundsOff;

    [SerializeField] Slider volumeSlider;

    public void OpenSettingsPanel()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        this.transform.gameObject.SetActive(true);
    }

    public void PlayClosePopUpSound()
    {
        AudioManager.Instance.PlayButtonClickSound();
        this.transform.gameObject.SetActive(false);
        
    }

    public void TurnMusicOn()
    {
        AudioManager.Instance.PlayButtonClickSound();
        bgMusicOff.SetActive(false);
        bgMusicOn.SetActive(true);
        AudioManager.Instance.TurnOnBackgroundSound();

    }

    public void TurnMusicOFF()
    {
        AudioManager.Instance.PlayButtonClickSound();
        bgMusicOff.SetActive(true);
        bgMusicOn.SetActive(false);
        AudioManager.Instance.TurnOffBackgroundSound();

    }

    public void TurnSoundsOn()
    {
        AudioManager.Instance.PlayButtonClickSound();
        soundsOff.SetActive(false);
        soundsOn.SetActive(true);
        AudioManager.Instance.TurnAllSoundsEffectsOn();

    }

    public void TurnSoundsOFF()
    {
        AudioManager.Instance.PlayButtonClickSound();
        soundsOff.SetActive(true);
        soundsOn.SetActive(false);
        AudioManager.Instance.TurnAllSoundsEffectsOff();
    } 

    void OnEnable()
    {
        //Register Slider Events
        volumeSlider.onValueChanged.AddListener(delegate { AudioManager.Instance.changeVolume(volumeSlider.value); });
    }

    void OnDisable()
    {
        //Un-Register Slider Events
        volumeSlider.onValueChanged.RemoveAllListeners();
    }


}
