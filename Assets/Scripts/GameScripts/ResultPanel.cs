using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPanel : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI resultText;
   [SerializeField] private GameObject answerPanel;
   [SerializeField] private TextMeshProUGUI targetWordText;
   [SerializeField] private TextMeshProUGUI buttonText;

   public void PlayerWon()
   {
      resultText.text = "Yeah! You did it.";
      answerPanel.SetActive(false);
      buttonText.text = "Next";      

   }

   public void PlayerLose(string _targetWord)
   {
      resultText.text = "Level Failed";
      answerPanel.SetActive(true);
      targetWordText.text = _targetWord.ToString();
      buttonText.text = "Play Again";

   } 
    
}
