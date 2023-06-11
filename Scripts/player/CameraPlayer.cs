using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    public Transform target;
    [SerializeField] private float smoothSpeed = 0.5f;
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private float cHeight, cWidth;

    [SerializeField] private bool isFacingRight = true;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 startPosition = new Vector3(Mathf.Clamp(target.position.x, minX, maxX) + cWidth, Mathf.Clamp(target.position.y, minY, maxY) + cHeight, -1f);
        Vector3 SmoothPosition = Vector3.Lerp(transform.position, startPosition, smoothSpeed);
        transform.position = SmoothPosition;
    }


    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < target.position.x && isFacingRight)
        {
            FlipCamera();
        }
        else if (mousePos.x > target.position.x && !isFacingRight)
        {
            FlipCamera();
        }
    }


    void FlipCamera()
    {
        isFacingRight = !isFacingRight;
        cWidth *= -1;

    }

}

