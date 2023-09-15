using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 2.0f; // Duration of the explosion effect

    void Start()
    {
        // Automatically destroy the explosion after a certain duration
        Destroy(gameObject, duration);
    }
}
