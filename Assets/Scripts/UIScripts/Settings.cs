using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject bgMusicOn;
    [SerializeField] GameObject bgMusicOff;
    [SerializeField] GameObject soundsOn;
    [SerializeField] GameObject soundsOff;

    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        if(AudioManager.Instance.isMusicOn) { TurnMusicOn(); }
        else { TurnMusicOFF(); }

        if(AudioManager.Instance.isClickSoundsOn) { TurnSoundsOn(); }
        else{ TurnSoundsOFF(); }

       volumeSlider.value = AudioManager.Instance.volumeSliderValue;
        
    }

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
        AudioManager.Instance.isMusicOn = true;
        AudioManager.Instance.PlayButtonClickSound();
        bgMusicOff.SetActive(false);
        bgMusicOn.SetActive(true);
        AudioManager.Instance.TurnOnBackgroundSound();

    }

    public void TurnMusicOFF()
    {
        AudioManager.Instance.isMusicOn = false;
        AudioManager.Instance.PlayButtonClickSound();
        bgMusicOff.SetActive(true);
        bgMusicOn.SetActive(false);
        AudioManager.Instance.TurnOffBackgroundSound();

    }

    public void TurnSoundsOn()
    {
        AudioManager.Instance.isClickSoundsOn = true;
        AudioManager.Instance.PlayButtonClickSound();
        soundsOff.SetActive(false);
        soundsOn.SetActive(true);
        AudioManager.Instance.TurnAllSoundsEffectsOn();

    }

    public void TurnSoundsOFF()
    {
        AudioManager.Instance.isClickSoundsOn = false;
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
