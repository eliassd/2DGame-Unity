using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    private bool moveDown = true;

    [SerializeField] float velocity;
    public Transform ponto1;
    public Transform ponto2;


    void Update()
    {
        if(transform.position.y > ponto1.position.y)
        {
            moveDown = true;
        }
        if(transform.position.y < ponto2.position.y)
        {
            moveDown = false;
        }


        if(moveDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - velocity * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
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
