using UnityEngine;

public class SelectLanguage : MonoBehaviour
{


    public void OnClickCloseButton()
    {
        AudioManager.Instance.PlayButtonClickSound();
        DatabaseManager.Instance.isOnlineModeActive = false;
        this.transform.gameObject.SetActive(false);
    }

}
