using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    GameObject canonBall, fire;
    Projectile canon;
    [SerializeField]
    int speed;
    void Start()
    {
        canonBall = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        canon = canonBall.GetComponent<Projectile>();
        canon.speed = speed;
        fire = transform.GetChild(0).gameObject;
    }
    public void Fire()
    {
        if (canonBall.activeSelf == false)
        {
            canonBall.transform.position = transform.position;
            canonBall.transform.rotation = transform.rotation;
            StartCoroutine(ShowFire());
            canonBall.SetActive(true);
        }
    }
    IEnumerator ShowFire()
    {
        fire.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        fire.SetActive(false);
    }
    private void OnDestroy()
    {
        if (canonBall != null) canon.DestroyProjectile();
    }
}