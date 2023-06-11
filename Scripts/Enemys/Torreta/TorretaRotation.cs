using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaRotation : MonoBehaviour
{
    public GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 playerPos = player.transform.position;

        Vector2 direction = new Vector2(
            playerPos.x - transform.position.x,
            playerPos.y - transform.position.y);

        transform.up = direction;
    }
}
