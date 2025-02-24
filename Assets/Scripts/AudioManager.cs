using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource popupOpenSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource losingSound;
    [SerializeField] private AudioSource sumbitButtonClicked;
    [SerializeField] private AudioSource BGSound;

    public static AudioManager Instance {get; private set;}

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
    }

    public void PlayButtonClickSound()
    {
        if(buttonClick != null) buttonClick.Play();
    }

    public void PlayPopupOpenSound()
    {
        if(popupOpenSound != null) popupOpenSound.Play();
    }

    public void PlayWinningSound()
    {
        if(winSound != null) winSound.Play();
    }

    public void PlayLosingSound()
    {
        if(losingSound != null) losingSound.Play();
    }

    public void PlaySubmitButtonClickedSound()
    {
        if(sumbitButtonClicked != null) sumbitButtonClicked.Play();
    }

    public void TurnOnBackgroundSound()
    {
       if(BGSound != null) BGSound.Play();
    }

    public void TurnOffBackgroundSound()
    {
        if(BGSound != null) BGSound.Stop();
    }

    public void TurnAllSoundsEffectsOn()
    {
        buttonClick.mute = false;
        popupOpenSound.mute = false;
        winSound.mute = false;
        losingSound.mute = false;
        sumbitButtonClicked.mute = false;

    }

    public void TurnAllSoundsEffectsOff()
    {
        buttonClick.mute = true;
        popupOpenSound.mute = true;
        winSound.mute = true;
        losingSound.mute = true;
        sumbitButtonClicked.mute = true;

    }

        //Called when Slider is moved
    public void changeVolume(float sliderValue)
    {
        BGSound.volume = sliderValue;
    }
}
