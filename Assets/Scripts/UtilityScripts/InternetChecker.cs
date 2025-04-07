using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class InternetChecker : MonoBehaviour
{
    public static InternetChecker Instance;
    private GameObject noInternetPopup;
    private bool isInternetAvailable = false;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object persistent
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CheckInternetOnlyInOnlineMode()
    {
      if(DatabaseManager.Instance.isOnlineModeActive) StartCoroutine(CheckConnectionRoutine());
    }

    IEnumerator CheckConnectionRoutine()
    {
        while (DatabaseManager.Instance.isOnlineModeActive)
        {
            CheckInternetConnection();
            yield return new WaitForSeconds(5f); // Check every 5 seconds
        }
    }

    public void CheckInternetConnection(Action<bool> callback = null)
    {
        if(noInternetPopup == null)
        {
            noInternetPopup = Instantiate (Resources.Load("NoInternet_PopUP") as GameObject);
            noInternetPopup.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
            noInternetPopup.SetActive(false);
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)  // Check Connected to Wifi or Mobile Network
        {
            Debug.Log("No Internet Connection");
            noInternetPopup.SetActive(true);
            isInternetAvailable = false;
        }
        else
        {
            Debug.Log("Internet is Available");
            // isInternetAvailable = true;
            // Check if connected to firebase
            FirebaseDatabase.DefaultInstance.GetReference(".info/connected").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && task.Result.Value != null && (bool)task.Result.Value)
                {
                    Debug.Log("Connected to Firebase");
                    noInternetPopup.SetActive(false);
                   // internetPopup.SetActive(false);
                    isInternetAvailable = true;
                }
                else
                {
                    Debug.Log("Disconnected from Firebase");
                     noInternetPopup.SetActive(true);
                     isInternetAvailable = false;
                 //   internetPopup.SetActive(true);
                 //   Time.timeScale = 0; // Pause the game
                }

                if(callback != null){ callback?.Invoke(isInternetAvailable); }
            });

           

        }
    }

    public void OnClickTryAgain()
    {
        if(noInternetPopup.activeInHierarchy)
        {
            AudioManager.Instance.PlayButtonClickSound();
            Debug.Log("OnClick Try Again..");
            CheckInternetOnlyInOnlineMode();
        }

    }


}
