using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InternetChecker : MonoBehaviour
{
    public static InternetChecker Instance;
    private GameObject noInternetPopup;
    

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

    public void CheckInternetConnection()
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
        }
        else
        {
            Debug.Log("Internet is Available");

            // Check if connected to firebase
            FirebaseDatabase.DefaultInstance.GetReference(".info/connected").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && task.Result.Value != null && (bool)task.Result.Value)
                {
                    Debug.Log("Connected to Firebase");
                    noInternetPopup.SetActive(false);
                   // internetPopup.SetActive(false);
                }
                else
                {
                    Debug.Log("Disconnected from Firebase");
                     noInternetPopup.SetActive(true);
                 //   internetPopup.SetActive(true);
                 //   Time.timeScale = 0; // Pause the game
                }
            });


        }
    }

    public void OnClickTryAgain()
    {
        if(noInternetPopup.activeInHierarchy)
        {
            CheckInternetConnection();
        }

    }

}
