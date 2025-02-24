using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardButtonController : MonoBehaviour
{
    [SerializeField] Image containerBorderImage;
    [SerializeField] Image containerFillImage;
    [SerializeField] Image containerIcon;
    [SerializeField] TextMeshProUGUI containerText;
    [SerializeField] TextMeshProUGUI containerActionText;
    public string keyLetter;

    private string clickedLetter = "";

    private void Start() {
        SetContainerBorderColor(ColorDataStore.GetKeyboardBorderColor());
        SetContainerFillColor(ColorDataStore.GetKeyboardFillColor());
        SetContainerTextColor(ColorDataStore.GetKeyboardTextColor());
        SetContainerActionTextColor(ColorDataStore.GetKeyboardActionTextColor());
    }

    public void SetContainerBorderColor(Color color) => containerBorderImage.color = color;
    public void SetContainerFillColor(Color color) => containerFillImage.color = color;
    public void SetContainerTextColor(Color color) => containerText.color = color;
    public void SetContainerActionTextColor(Color color) { 
        containerActionText.color = color;
        containerIcon.color = color;
    }

    public void AddLetter() {

        if(GameManager.Instance != null) {
            AudioManager.Instance.PlayButtonClickSound();
            GameManager.Instance.AddLetter(containerText.text);
            clickedLetter = containerText.text.ToString();
        } else {
            Debug.Log(containerText.text + " is pressed");
        }
    }
    public void DeleteLetter() { 
        if(GameManager.Instance != null) {
            AudioManager.Instance.PlayButtonClickSound();
            GameManager.Instance.DeleteLetter();
        } else {
            Debug.Log("Last char deleted");
        }
    }
    public void SubmitWord() {
        if(GameManager.Instance != null) {
            GameManager.Instance.SubmitWord();
        } else {
            Debug.Log("Submitted successfully!");
        }
    }

    public Color GetTheKeyColor()
    {
        return containerFillImage.color;
    } 


}