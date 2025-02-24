using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitButtonFunctionality : MonoBehaviour
{
    [SerializeField] Image containerBGImage;
    [SerializeField] TextMeshProUGUI actionText;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        actionText.text = "Submit";
        containerBGImage.color = new Color32(123,123,123,50);
    }

    public void InCompleteWord()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        actionText.text = "Submit";
        containerBGImage.color = new Color32(123,123,123,50);
    }

    public void InvaildWord()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        actionText.text = "Invalid Word";
        containerBGImage.color = new Color32(255,0,0,50);
    }

    public void ValidWord()
    {
        this.gameObject.GetComponent<Button>().interactable = true;
        actionText.text = "Submit";
        containerBGImage.color = new Color32(131,255,0,50);
    }
}
