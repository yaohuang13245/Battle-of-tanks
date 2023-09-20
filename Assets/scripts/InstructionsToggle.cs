using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsToggle : MonoBehaviour
{
    public GameObject instructionCanvas; // Reference to your instruction canvas GameObject
    private bool isInstructionsVisible = false;

    public void ToggleInstructions()
    {
        isInstructionsVisible = !isInstructionsVisible;
        instructionCanvas.SetActive(isInstructionsVisible);
    }

    public void DisableInstructions()
    {
        isInstructionsVisible = false;
        instructionCanvas.SetActive(false);
    }
}
