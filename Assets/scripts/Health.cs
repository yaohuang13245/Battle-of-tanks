using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int actualHealth;
    int currentHealth;

    public GameObject explosionPrefab;
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
        if (gameObject.CompareTag("Player"))
        {
            GPM.SpawnPlayer(); //Spawn Player
        }
        else
        {
            if (gameObject.CompareTag("Small")) MasterTracker.smallTanksDestroyed++;
            else if (gameObject.CompareTag("Fast")) MasterTracker.fastTanksDestroyed++;
            else if (gameObject.CompareTag("Big")) MasterTracker.bigTanksDestroyed++;
            else if (gameObject.CompareTag("Armored")) MasterTracker.armoredTanksDestroyed++;
        }
        Destroy(gameObject);
    }
}