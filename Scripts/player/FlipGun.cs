using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGun : MonoBehaviour
{
    public GameObject gun;
    private bool isFacingRight = true;


    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(180f, 0f, 0f);
    }
}

