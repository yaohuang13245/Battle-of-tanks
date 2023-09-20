using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField]
    Text stageNumberText, gameOverText;
    [SerializeField]
    RectTransform canvas;
    GameObject[] spawnPoints, spawnPlayerPoints;
    bool stageStart = false;
    bool tankReserveEmpty = false;

    // Use this for initialization
    void Start()
    {
        stageNumberText.text = "STAGE " + MasterTracker.stageNumber.ToString();
        stageStart = true;
        StartCoroutine(StartStage());
        // Find the GameObject with the AudioSource component
        GameObject backgroundMusicObject = GameObject.Find("BackgroundMusic");

        // Check if the object exists and has an AudioSource component
        if (backgroundMusicObject != null)
        {
            // Get the AudioSource component
            AudioSource backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();

            // Start playing the background music
            if (backgroundMusic != null)
            {
                backgroundMusic.Play();
            }
            else
            {
                Debug.LogWarning("BackgroundMusic GameObject does not have an AudioSource component.");
            }
        }
        else
        {
            Debug.LogWarning("BackgroundMusic GameObject not found in the scene.");
        }
    }
    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(5);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        InvokeRepeating("SpawnEnemy", LevelManager.spawnRate, LevelManager.spawnRate);
        /*SpawnPlayer();*/
    }
    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(1, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), 1);
            yield return null;
        }
    }
    IEnumerator RevealTopStage()
    {
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < 1250)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        while (bottomCurtain.rectTransform.position.y > -400)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 40f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        MasterTracker.stageCleared = false;
        LevelCompleted();
    }
    public void SpawnEnemy()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        if (LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Animator anime = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            CancelInvoke();
            tankReserveEmpty = true;
        }
    }

    private void Update()
    {
        if (tankReserveEmpty && GameObject.FindWithTag("Small") == null && GameObject.FindWithTag("Fast") == null && GameObject.FindWithTag("Big") == null && GameObject.FindWithTag("Armored") == null)
        {
            MasterTracker.stageCleared = true;
            LevelCompleted();
        }
    }
    private void LevelCompleted()
    {
        tankReserveEmpty = false;
        SceneManager.LoadScene("Score");
    }

    public void SpawnPlayer()
    {
        spawnPlayerPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        if (MasterTracker.playerLives > 0)
        {
            if (!stageStart)
            {
                MasterTracker.playerLives--;
            }
            stageStart = false;
            Animator anime = spawnPlayerPoints[0].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            StartCoroutine(GameOver());
       }
    }
}