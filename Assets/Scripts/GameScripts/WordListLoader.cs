using System.Collections.Generic;
using UnityEngine;

public class WordListLoader : MonoBehaviour
{
    public List<string> wordList = new List<string>();
    private bool isOnlineModeActive = false;

    void Awake()
    {
        isOnlineModeActive = DatabaseManager.Instance.isOnlineModeActive;
        LoadWordList();
    }

    void LoadWordList()
    {

        if(isOnlineModeActive)
        {
            wordList = DatabaseManager.Instance.OnlineWordsList;

        }
        else
        {
            TextAsset wordListText = Resources.Load<TextAsset>("WordsDB");
            string[] words = wordListText.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            wordList.AddRange(words);

            Debug.Log("Words loaded: " + wordList.Count);
        }
    }
}

