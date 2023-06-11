using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAuto : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;
    [SerializeField] private bool canFire = true;
    private float timer;
    [SerializeField] private float rateOfFire;
    public Animator shotAnimation;
    [SerializeField] AudioSource shotSound;

    void Update()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > rateOfFire)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && canFire)
        {
            canFire = false;
            shotSound.Play();
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            shotAnimation.SetTrigger("Shoot");
        }
    }
}
