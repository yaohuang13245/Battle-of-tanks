using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int actualHealth;
    int currentHealth;

    [SerializeField]
    private GameObject powerUpPrefab, projectileSteelPrefab;

    public GameObject explosionPrefab;

    // Add a flag to determine if the object is an enemy
    [SerializeField]
    private bool isEnemy = true;

    void Start()
    {
        SetHealth();
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            // Instantiate the tank explosion prefab at the tank's position
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destroy the explosion after 2 seconds
            Destroy(explosion, 2f);
            Death();
        }
    }

    public void SetHealth()
    {
        currentHealth = actualHealth;
    }

    public void SetInvincible()
    {
        currentHealth = 1000;
    }

    void Death()
    {
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();

        if (isEnemy)
        {
            // Generate a random number between 0 and 1
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= 0.5f)
            {
                // Instantiate the power-up prefab at the enemy's position with an offset
                Instantiate(powerUpPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }

        }

        if (gameObject.CompareTag("Player"))
        {
            GPM.SpawnPlayer(); //Spawn Player
        }
        else if (isEnemy)
        {
            // Track enemy destruction only
            if (gameObject.CompareTag("Small")) MasterTracker.smallTanksDestroyed++;
            else if (gameObject.CompareTag("Fast")) MasterTracker.fastTanksDestroyed++;
            else if (gameObject.CompareTag("Big")) MasterTracker.bigTanksDestroyed++;
            else if (gameObject.CompareTag("Armored")) MasterTracker.armoredTanksDestroyed++;
        }

        Destroy(gameObject);
    }
}
