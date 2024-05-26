using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText; 

    //Don't do this like that. it just works for this example but it's not safe
    void Update()
    {
        highScoreText.text = "HighScore: "+ScoreManager.instance.HighScore.ToString();
        currentScoreText.text = "CurrentScore: "+ScoreManager.instance.CurrentScore.ToString();
    }
}
