using UnityEngine;
using UnityEngine.UI;

public class NoInternetPopupScript : MonoBehaviour
{
 
   [SerializeField] private Button tryAgain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tryAgain.onClick.AddListener(InternetChecker.Instance.OnClickTryAgain);
    }


}
