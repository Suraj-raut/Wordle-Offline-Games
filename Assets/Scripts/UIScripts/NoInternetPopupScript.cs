using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoInternetPopupScript : MonoBehaviour
{
 
   [SerializeField] private Button tryAgain;
   [SerializeField] private GameObject closeButton;
   [SerializeField] private GameObject backToHome_btn;

   void Awake()
   {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            Debug.Log("The current scene is Start!");
            closeButton.SetActive(true);
            backToHome_btn.SetActive(false);
        }
        else
        {
            Debug.Log("The current scene is not Start.");
            closeButton.SetActive(false);
            backToHome_btn.SetActive(true);
        }

   }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        tryAgain.onClick.AddListener(InternetChecker.Instance.OnClickTryAgain);

    }

    public void OnClickCloseButton()
    {
        AudioManager.Instance.PlayButtonClickSound();
        DatabaseManager.Instance.isOnlineModeActive = false;
        this.transform.gameObject.SetActive(false);
    }

    public void BackToHomeScene()
    {
        AudioManager.Instance.PlayButtonClickSound();
        DatabaseManager.Instance.isOnlineModeActive = false;
        DatabaseManager.Instance.OnlineWordsList.Clear();
        SceneManager.LoadScene("StartScene");
       if(DatabaseManager.Instance.isOnlineModeActive){  MediationAdsManager.Instance.DestroyBanner(); }
    }


}
