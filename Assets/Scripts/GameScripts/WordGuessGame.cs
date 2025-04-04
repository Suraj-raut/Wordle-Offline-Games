using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WordGuessGame : MonoBehaviour
{
    [SerializeField] private WordListLoader wordListLoader;

    [SerializeField] private KeyboardInputManager _keyboardInputManager;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private ResultPanel resultPanel;
    [SerializeField] private ScoreManager _scoreManger;

    [SerializeField] private GameObject hintButton;

    [SerializeField] private SubmitButtonFunctionality submitButton;

   // public static event Action<string> OnWordSumbit;

    private string targetWord;
    private int attempts = 0;
    public KeyboardButtonController[] allKeys;
    private bool isPlayerWon = false;
    private int gamePlayedCount = 1;


    void Start()
    {
        wordListLoader = wordListLoader.GetComponent<WordListLoader>();
        // Ensure the word list is not empty
        GetTargetWord();
         //submitButton.onClick.AddListener(CheckGuess);

        if(DatabaseManager.Instance.isOnlineModeActive) 
        {
            MediationAdsManager.Instance.LoadAdsInitially();
            hintButton.SetActive(true);
        }
        else { hintButton.SetActive(false); }
    }
    

    private string GetRandomWordFromList()
    {
        int index = UnityEngine.Random.Range(0, wordListLoader.wordList.Count);
        return wordListLoader.wordList[index].ToLower();
    }


    public void GetTargetWord()
    {
        if (wordListLoader.wordList.Count > 0)
        {
            targetWord = GetRandomWordFromList();
            Debug.Log("Target Word: " + targetWord);
        }
        else
        {
             Debug.LogError("Word list is empty! Cannot select a target word.");
        }
    }

    private void OnEnable()
    {
        KeyboardInputManager.OnWordComplete += CheckGuessIsValid;
        MediationAdsManager.Instance.OnRewardedAdCompleted += RewardThePlayer;
        DatabaseManager.Instance.OnWordSearched += IsWordFoundInDatabase;
    }
    private void OnDisable()
    {
        KeyboardInputManager.OnWordComplete -= CheckGuessIsValid;
        MediationAdsManager.Instance.OnRewardedAdCompleted -= RewardThePlayer;
        DatabaseManager.Instance.OnWordSearched -= IsWordFoundInDatabase;
    }

    private void CheckGuessIsValid(string guess)
    {
        if (guess.Length != 5)
        {
            Debug.Log("Not a proper 5 letter word");
            submitButton.InCompleteWord();
            
        }
        else if(!wordListLoader.wordList.Contains(guess))
        {
          
            if(DatabaseManager.Instance.isOnlineModeActive)
            {
                Debug.Log("Game is in online mode--->>");

                DatabaseManager.Instance.WordPresentInDatabase(guess);

            }
            else
            {
                Debug.Log("Invalid word");
                submitButton.InvaildWord();
            }
        }
        else
        {
           Debug.Log("Valid word to Sumit");
           submitButton.ValidWord();
        }
    }

    private void IsWordFoundInDatabase()
    {
        bool _isWordPresent = DatabaseManager.Instance.isWordPresent;

         Debug.Log("Game is in online mode--->>>Is word present :-After" + _isWordPresent);

         if(_isWordPresent) { submitButton.ValidWord(); }
         else { submitButton.InvaildWord(); }

    }


   public void CheckGuess()
    {
        AudioManager.Instance.PlaySubmitButtonClickedSound();
        string guess = _keyboardInputManager.GetTheGuessedWord();

        for (int i = 0; i < guess.Length; i++)
        {
            if(attempts < 6)
            {
                //Color Tempcolor = tile.GetComponent<Image>().color;
                GameObject tile = grid.transform.GetChild((attempts * 5) + i).gameObject;
                //  TextMeshProUGUI tileText = tile.GetComponent<TextMeshProUGUI>();
                //  tileText.text = guess[i].ToString();

                if (targetWord[i] == guess[i])
                {
                    tile.GetComponent<Image>().color = Color.green;
                    ChangeKeyColor(guess[i].ToString(), Color.green);
                   
                }
                else if (targetWord.Contains(guess[i]))
                {
                    tile.GetComponent<Image>().color = Color.yellow;
                    ChangeKeyColor(guess[i].ToString(), Color.yellow);
                
                }
                else
                {
                    tile.GetComponent<Image>().color = Color.gray;
                    ChangeKeyColor(guess[i].ToString(), Color.gray);
                    
                }
                
            }
        }
        submitButton.InCompleteWord();

         attempts++;

        if (guess == targetWord)
        {
            Debug.Log("You won!");
            AudioManager.Instance.PlayWinningSound();
            isPlayerWon = true;
            _scoreManger.IncreseScore();
            _scoreManger.SaveTheHighScore();
            resultPanel.gameObject.SetActive(true);
            resultPanel.PlayerWon();
           if(DatabaseManager.Instance.isOnlineModeActive) { MediationAdsManager.Instance.DestroyBanner(); }
            // Handle win condition
        }
        else if (attempts == 6)
        {
            Debug.Log("Game over! The word was: " + targetWord);
            AudioManager.Instance.PlayLosingSound();
            isPlayerWon = false;
            _scoreManger.SaveTheHighScore();
            resultPanel.gameObject.SetActive(true);
            resultPanel.PlayerLose(targetWord);
           if(DatabaseManager.Instance.isOnlineModeActive) { MediationAdsManager.Instance.DestroyBanner(); }
            // Handle game over
        }
    }

    public void PlayAgain()
    {
        AudioManager.Instance.PlayButtonClickSound();
        gamePlayedCount++;
        resultPanel.gameObject.SetActive(false);
        if(!isPlayerWon) _scoreManger.StartTheGame();
        _keyboardInputManager.ResetTheGame();
        attempts = 0;
        ResetKeyboardKeysColor();
        submitButton.InCompleteWord();
        GetTargetWord();
       if(DatabaseManager.Instance.isOnlineModeActive)
       {
            MediationAdsManager.Instance.LoadBanner();
            Debug.Log("Game Played count :--" + gamePlayedCount);
            if(gamePlayedCount % 3 == 0)
            {
                Debug.Log("Game Played count :--Show interstitial" + gamePlayedCount);
                MediationAdsManager.Instance.ShowInterstitial();
            }
       }
    }

    public void ChangeKeyColor(string keyLetter, Color newColor)
    {       
        foreach (KeyboardButtonController key in allKeys)
        {
            if (key.keyLetter == keyLetter)
            {
                //Debug.Log("Color changed....");
                key.SetContainerFillColor(newColor);
                break; // Exit loop once the key is found and color is changed
            }
        }
    }

    private void ResetKeyboardKeysColor()
    {
         foreach (KeyboardButtonController key in allKeys)
        {
            key.SetContainerFillColor(ColorDataStore.GetKeyboardFillColor());
        }

         
    }

    public void OnClickHintButton()
    {
        AudioManager.Instance.PlayPopupOpenSound();
       if(DatabaseManager.Instance.isOnlineModeActive) {  MediationAdsManager.Instance.ShowRewarded(); }
        
    }

    private void RewardThePlayer()
    {
        bool isRewardGiven = false;

        if(MediationAdsManager.Instance.isRewarded)
        {
            if(!isRewardGiven)
            {
                foreach (KeyboardButtonController key in allKeys)
                {
                    for (int i = 0; i < targetWord.Length; i++)
                    {
                        if (targetWord.Contains(key.keyLetter) && !isRewardGiven)
                        {
                            Color color = key.GetTheKeyColor();
                            
                            if(color != Color.green && color != Color.yellow)
                            {
                                key.SetContainerFillColor(Color.yellow);
                                isRewardGiven = true;
                                break;
                            }
                        
                        }
                    }
                }
            }

        }

    }

}

