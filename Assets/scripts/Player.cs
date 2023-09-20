using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    float h, v;
    Rigidbody2D rb2d;
    WeaponController wc;
    private AudioSource shootEffect; // Rename the variable to match your AudioSource

    void Start()
    {
        wc = GetComponentInChildren<WeaponController>();
        rb2d = GetComponent<Rigidbody2D>();

        // Get the AudioSource component by name "ShootEffect" from the same GameObject
        shootEffect = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (h != 0 && !isMoving) StartCoroutine(MoveHorizontal(h, rb2d));
        else if (v != 0 && !isMoving) StartCoroutine(MoveVertical(v, rb2d));
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wc.Fire();

            // Play the shoot sound effect using the "ShootEffect" AudioSource
            if (shootEffect != null)
            {
                shootEffect.Play();
            }
        }
    }
}
