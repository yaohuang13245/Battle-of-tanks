using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string levelToLoad = "Stage1"; // Set the level name to "Stage1".

    // Attach this method to the button's OnClick event in the Inspector.
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad); // Load the specified level.
    }
}