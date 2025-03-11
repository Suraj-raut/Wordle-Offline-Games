using Firebase.Database;
using Firebase.Extensions;
using Firebase;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    private string databasePath = "English"; // Path to your restructured words node
    private string wordCountPath = "TotalWordsCount"; //Path to the word count
    private int numWordsToFetch = 100;
    private long totalWordCount = 3646;
    private DatabaseReference reference;

    public List<string> OnlineWordsList = new List<string>();

    public static DatabaseManager Instance {get; private set;}

    public bool isOnlineModeActive = false;

    public static event Action PlayOnSelectLanguage;
    public event Action OnWordSearched;

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

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;; // Get root reference
        Debug.Log("Total number of words-->Start : " + totalWordCount);

    }

    private void OnEnable()
    {
        PlayOnSelectLanguage += EnterOnlineGameMode;

    }

    private void OnDisable()
    {
        PlayOnSelectLanguage -= EnterOnlineGameMode;
 
    }

    private void EnterOnlineGameMode()
    {
        AudioManager.Instance.PlayPopupOpenSound();
        SceneManager.LoadScene("WordGuess");
    }

    public void OnSelectLanguage(string databaseNode)    
    {
        AudioManager.Instance.PlayPopupOpenSound();

        if(databaseNode != null) { databasePath = databaseNode; }  

         Debug.Log("GetTotalWordsCount: of language " + databaseNode);

        //Fetch the total number of words
        reference.Child(databaseNode).Child(wordCountPath).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("Total number of words--->From database : " + totalWordCount);
                HandleError(task);
                return;
            }

             totalWordCount = (long)task.Result.Value;
              Debug.Log("Total number of words-->After fetching : " + totalWordCount);

             //Generate a list of random unique indices to fetch words
             List<long> randomIndices = GenerateRandomUniqueIndices(totalWordCount, numWordsToFetch);

            //Now fetch the words based on random indices
             FetchWordsByIndices(randomIndices);
           

        });
           
           
    }

    private List<long> GenerateRandomUniqueIndices(long totalWords, int count)
    {
        List<long> indices = new List<long>();
        HashSet<long> uniqueIndices = new HashSet<long>(); //Ensures no duplicate indices

        System.Random random = new System.Random();
        while (indices.Count < count)
        {
            long index = (long)random.Next((int)totalWords);
            if (uniqueIndices.Add(index))
            {
                indices.Add(index);
              //  Debug.Log("Unique Index: " + index);
            }
        }
        return indices;
    }

    public int counter = 0;
    private void FetchWordsByIndices(List<long> indices)
    {
        
        counter = 0;
        foreach (long index in indices)
        {
            reference.Child(databasePath).Child(index.ToString()).GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted || task.IsCanceled)
                {
                    HandleError(task);
                    return;
                }
                else if (task.IsCompleted)
               {

                    string word = (string)task.Result.Value;
                    OnlineWordsList.Add(word);
                    counter++;
                    Debug.Log("Word added : " + word);

               }

 
                Debug.Log("Counter of words -->>" + counter);
                if(counter == indices.Count)
                {

                    EnterOnlineGameMode();

                }
             
            });


        }
    }     

    void HandleError(Task task)
    {
        Debug.LogError("Error fetching data: " + task.Exception);
    }

    public bool isWordPresent = false;

    public void WordPresentInDatabase(string wordToCheck)
    {
        Debug.Log("Searching in database" + wordToCheck);
         reference.Child(databasePath).OrderByValue().EqualTo(wordToCheck).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Task Completed...>>>");
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    isWordPresent = true;
                    Debug.Log("Word found: " + wordToCheck);
                    OnWordSearched?.Invoke();
                }
                else
                {
                    isWordPresent = false;
                    Debug.Log("Word NOT found.");
                    OnWordSearched?.Invoke();
                }
            }
        });
    }


}




