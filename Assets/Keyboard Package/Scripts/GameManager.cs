using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] KeyboardInputManager _keyboardInputManager;
   // public bool isRewarded = false;
  //  [SerializeField] TextMeshProUGUI textBox;
  //  [SerializeField] TextMeshProUGUI printBox;

    //public string DisplayLetter = "";

    private void Start()
    {
        Instance = this;
    //    printBox.text = "";
    //    textBox.text = "";
    }

    public void DeleteLetter()
    {
        _keyboardInputManager.RemoveLetterFromGridTile();
        // if(textBox.text.Length != 0) {
        //     textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
        // }
    }

    public void AddLetter(string letter)
    {
       // textBox.text = textBox.text + letter;
      //  DisplayLetter = letter;
     //   Debug.Log("AddLetter GameManger : " + letter);
        _keyboardInputManager.DisplayLetterOnGridTile(letter);
        
    }

    public void SubmitWord()
    {
        // printBox.text = textBox.text;
        // textBox.text = "";
      //  Debug.Log("Text submitted successfully!");
    }


}
