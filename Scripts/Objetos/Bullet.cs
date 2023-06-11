using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Bullet : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] float bulletSpeed;
    //[SerializeField] float bulletDamage = 2f;
    public float lifeTime = 0.2f;
    public Rigidbody2D rb;



    void Start()
    {
        StartCoroutine(DestroyBullet());
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }

    /*void OnTriggerEnter2D(Collider2D collision)
    {

        BasicEnemy enemy = collision.GetComponent<BasicEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }*/

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

