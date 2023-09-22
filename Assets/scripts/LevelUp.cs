using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField]
    private float speedIncrease = 2.0f; // The amount by which the player's speed will increase

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding GameObject is the player
        if (other.CompareTag("Player"))
        {
            // Get the Player script (you may need to adjust this depending on your player setup)
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                // Increase the player's speed
                player.IncreaseSpeed(speedIncrease);

                // Destroy the power-up after collection
                Destroy(gameObject);
            }
        }
    }
}
