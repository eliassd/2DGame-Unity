using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;

    private float timer;
    private bool canShoot = true;
    [SerializeField] float rateOfFire;
    [SerializeField] int life = 8;

    [SerializeField] Vector2 lineOfSight;
    [SerializeField] float lineOfSightValue;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] Transform player;
    [SerializeField] AudioSource shotSound;

    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        timer += Time.deltaTime;

        if(timer > rateOfFire && canShoot == true) 
        {
            timer = 0;
            if (distanceFromPlayer <= lineOfSightValue)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        shotSound.Play();
        Instantiate(bullet, bulletTransform.position, Quaternion.identity);
    }

    private void TakeDamage()
    {
        life -= 1;
        if (life <= 0)
        {
            canShoot = false;
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerAtaque")
        {
            TakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
    }
}
