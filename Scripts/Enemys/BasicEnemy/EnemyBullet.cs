using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    GameObject target;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLife = 2f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * bulletSpeed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, bulletLife);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
