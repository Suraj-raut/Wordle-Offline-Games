using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeyboardInputManager : MonoBehaviour
{
    [SerializeField] GridLayoutGroup grid;

    public static event Action<string> OnWordComplete;

    private int tileCounter = 0;
    private int gridMaxLength = 30;

    private string[] guessedWordArray = new string[5];
    private string guessedWord;

    private int column = 0;
    private int rows = 0;

  //  private bool isLetterRemoved = false;
    private bool isWordSubmitted = false;

    public TextMeshProUGUI[] TilesText;

    public void DisplayLetterOnGridTile(string letter)
    {
     //  string letter = GameManager.Instance.DisplayLetter;
        if(rows < 6)
        {
            if(letter != null && (tileCounter < gridMaxLength) && (column < 5))
            {
                isWordSubmitted = false;
             //   GameObject tile = grid.transform.GetChild((rows * 5) + column).GetChild(0).gameObject;
               // TextMeshProUGUI tileText = tile.GetComponent<TextMeshProUGUI>();
               TextMeshProUGUI tileText =  TilesText[(rows * 5) + column];
                tileText.text = letter.ToString();
                guessedWordArray[column] = letter;
                tileCounter++;
                column++;
                
            }
        guessedWord = ConvertToString(guessedWordArray);
        OnWordComplete?.Invoke(guessedWord);
        
        }
       
    }

    public void RemoveLetterFromGridTile()
    { 
         AudioManager.Instance.PlayButtonClickSound();

        if(!isWordSubmitted)
        {
            if(tileCounter >= 1)
            {
                if(((tileCounter < 5) && rows == 0) ||
                    ((tileCounter - 1)>= (rows * 5)))
                {
                    try
                    {
                        // Code that might throw an exception
                        TextMeshProUGUI tileText = TilesText[tileCounter-1];
                        Debug.Log("Tile Counter:- " + tileCounter);
                                    
                        if(tileText.text != null)
                        {
                                tileText.text = null;
                                if(tileCounter > 0)tileCounter--;
                                if(column > 0)column--;
                                Array.Clear(guessedWordArray,column,1);
                                guessedWord = ConvertToString(guessedWordArray);
                                OnWordComplete?.Invoke(guessedWord);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Code to handle the exception (e.g., logging it, showing an error message)
                        Debug.LogError("An error occurred: " + ex.Message);
                    }
                }
            }

        }
        
    }

    private string ConvertToString(string[] array)
    {
        return string.Concat(array);
    }

    public string GetTheGuessedWord()
    {
        rows++;
        column = 0;
        Array.Clear(guessedWordArray,0,5);
         isWordSubmitted = true;
       //  Debug.Log("Rows After submit click: " + rows);
         return guessedWord;
    }

    public void ResetTheGame()
    {
        for(int i=0; i < gridMaxLength; i++)
        {
            GameObject tile = grid.transform.GetChild(i).gameObject;
            TextMeshProUGUI tileText = tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tileText.text = "";
            tile.GetComponent<Image>().color = new Color32(255,255,255,100);
        }
        tileCounter = 0;
        column = 0;
        rows = 0;
         Array.Clear(guessedWordArray,0,5);
    }

}
