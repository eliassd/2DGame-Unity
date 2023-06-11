using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaPlataforma : MonoBehaviour
{

    public GameObject player;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            player.transform.parent = collision.gameObject.transform;
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            player.transform.parent = null;
        }
    }
}
