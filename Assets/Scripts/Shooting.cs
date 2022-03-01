using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject prefab;
    public float emitForce = 20f;

    public float lastUseTime = -1000;
    public float cooldown = 0.1f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();        
        }
    }

    void Shoot()
    {
        if (lastUseTime + cooldown > Time.time)
            return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        rb2d.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        lastUseTime = Time.time;
    }


}
