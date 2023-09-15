using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TallyScore : MonoBehaviour
{
    [SerializeField]
    Text hiScoreText, stageText, playerScoreText, smallTankScoreText, fastTankScoreText, bigTankScoreText, armoredTankScoreText, smallTanksDestroyed, fastTanksDestroyed, bigTanksDestroyed, armoredTanksDestroyed, totalTanksDestroyed;
    int smallTankScore, fastTankScore, bigTankScore, armoredTankScore;
    MasterTracker masterTracker;
    int smallTankPointsWorth, fastTankPointsWorth, bigTankPointsWorth, armoredTankPointsWorth;
    // Use this for initialization

    void UpdateHighScore()
    {
        int currentScore = MasterTracker.playerScore;
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0); // Get the saved high score (default is 0)

        if (currentScore > savedHighScore)
        {
            // If the current score is higher than the saved high score, update it
            PlayerPrefs.SetInt("HighScore", currentScore);
            savedHighScore = currentScore; // Update the saved high score
        }

        hiScoreText.text = savedHighScore.ToString(); // Update the hiScoreText UI element with the high score
    }

    void Start()
    {
        masterTracker = GameObject.Find("MasterTracker").GetComponent<MasterTracker>();
        smallTankPointsWorth = masterTracker.smallTankPointsWorth;
        fastTankPointsWorth = masterTracker.fastTankPointsWorth;
        bigTankPointsWorth = masterTracker.bigTankPointsWorth;
        armoredTankPointsWorth = masterTracker.armoredTankPointsWorth;
        stageText.text = "STAGE " + MasterTracker.stageNumber;
        playerScoreText.text = MasterTracker.playerScore.ToString();
        hiScoreText.text = playerScoreText.text;
        UpdateHighScore();
        StartCoroutine(UpdateTankPoints());
    }
    IEnumerator UpdateTankPoints()
    {
        for (int i = 0; i <= MasterTracker.smallTanksDestroyed; i++)
        {
            smallTankScore = smallTankPointsWorth * i;
            smallTankScoreText.text = smallTankScore.ToString();
            smallTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i <= MasterTracker.fastTanksDestroyed; i++)
        {
            fastTankScore = fastTankPointsWorth * i;
            fastTankScoreText.text = fastTankScore.ToString();
            fastTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i <= MasterTracker.bigTanksDestroyed; i++)
        {
            bigTankScore = bigTankPointsWorth * i;
            bigTankScoreText.text = bigTankScore.ToString();
            bigTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i <= MasterTracker.armoredTanksDestroyed; i++)
        {
            armoredTankScore = armoredTankPointsWorth * i;
            armoredTankScoreText.text = armoredTankScore.ToString();
            armoredTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        totalTanksDestroyed.text = (MasterTracker.smallTanksDestroyed + MasterTracker.fastTanksDestroyed + MasterTracker.bigTanksDestroyed + MasterTracker.armoredTanksDestroyed).ToString();
        MasterTracker.playerScore += (smallTankScore + fastTankScore + bigTankScore + armoredTankScore);
        yield return new WaitForSeconds(5f);
        if (MasterTracker.stageCleared)
        {
            ClearStatistics();
            SceneManager.LoadScene("Stage" + (MasterTracker.stageNumber + 1));
        }
        else
        {
            ClearStatistics();
            //Load GameOver Scene
        }
    }
    void ClearStatistics()
    {
        MasterTracker.smallTanksDestroyed = 0;
        MasterTracker.fastTanksDestroyed = 0;
        MasterTracker.bigTanksDestroyed = 0;
        MasterTracker.armoredTanksDestroyed = 0;
        MasterTracker.stageCleared = false;
    }
}