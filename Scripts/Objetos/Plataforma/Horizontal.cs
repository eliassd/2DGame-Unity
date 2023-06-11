using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Horizontal : MonoBehaviour
{
    private bool moveRight = true;

    [SerializeField] float velocity;
    public UnityEngine.Transform ponto1;
    public UnityEngine.Transform ponto2;


    void Update()
    {
        if (transform.position.x < ponto1.position.x)
        {
            moveRight = true;
        }
        if (transform.position.x > ponto2.position.x)
        {
            moveRight = false;
        }


        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - velocity * Time.deltaTime, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
