using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        // Quit the game (works in standalone builds)
        Application.Quit();

        // Note: In the Unity Editor, Application.Quit() won't work in Play Mode.
        // It will only work when you build and run the game as a standalone application.
    }
}