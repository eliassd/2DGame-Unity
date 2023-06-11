using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public Rigidbody2D rb;
    public float life = 4;
    public GameObject gunholder;
    public GameObject body;
    [SerializeField] AudioSource shotSound;
    [SerializeField] AudioSource cockingGun;
    public ChangeColors changeColor;


    [Header("Patrulha")]
    [SerializeField] float patrolSpeed;
    private float moveDirecion = 1f;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Transform patrolCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask patrolLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] private bool checkingGround = true;
    [SerializeField] private bool checkingWall;
    [SerializeField] private bool checkingPatrol;


    [Header("Combate")]
    [SerializeField] float combatSpeed;
    [SerializeField] Transform player;
    private bool playerDetected = false;
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] float lineOfSightValue;
    [SerializeField] Vector2 shootingDistance;
    [SerializeField] float shootingDistanceValue;
    [SerializeField] private float distanceFromPlayer;
    public GameObject Bullet;
    public GameObject BulletTransform;
    [SerializeField] float fireRate = 1f;
    private float nextFireTime;
    private bool canShoot = true;

    [Header("Efeitos")]
    [SerializeField] ParticleSystem deathEsplosion = default;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        changeColor = FindObjectOfType<ChangeColors>();
        canShoot = true;
    }


    void FixedUpdate()
    {

        playerDetected = Physics2D.OverlapBox(transform.position, lineOfSight, 0, playerLayer);
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (playerDetected == false)
        {
            changeColor.paintYellow();
            checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
            checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, wallLayer);
            checkingPatrol = Physics2D.OverlapCircle(patrolCheckPoint.position, circleRadius, patrolLayer);
            Patrulhando();
        }else if(playerDetected == true)
        {
            EmCombate();
            
        }

    }

    //Comportamento quando patrulhando;
    void Patrulhando()
    {
        
        //CheckForPlayer();
        if (/*checkingGround == false || */checkingWall || checkingPatrol)
        {
            if (facingRight)
            {
                Flip();
            }else if (!facingRight)
            {
                Flip();
            }
        }

        rb.velocity = new Vector2(patrolSpeed * moveDirecion, rb.velocity.y);

    }

    //Comportamento quando achar o player;
    void EmCombate()
    {

        //CheckForPlayer();
        changeColor.paintRed();
        flipTowardsPlayer();
        //distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSightValue && distanceFromPlayer > shootingDistanceValue)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, combatSpeed * Time.deltaTime);

        }
        else if (distanceFromPlayer <= shootingDistanceValue && nextFireTime < Time.time && canShoot == true)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            //spriteRenderer.color = Color.red;
            cockingGun.Play();
            Invoke("Shoot", 0.7f);
            //spriteRenderer.color = Color.yellow;
            /*shotSound.Play();
            Instantiate(Bullet, BulletTransform.transform.position, Quaternion.identity);*/
            nextFireTime = Time.time + fireRate;
            
        }
        
    }

    void Shoot()
    {
        shotSound.Play();
        Instantiate(Bullet, BulletTransform.transform.position, Quaternion.identity);
    }


    void Flip()
    {
        moveDirecion *= -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //muda para a direção do player em combate
    void flipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if(playerPosition<0 && facingRight)
        {
            Flip();
        }else if(playerPosition>0 && !facingRight)
        {
            Flip();
        }
    }


    private void TakeDamage()
    {
        life -= 1;
        if(life == 0)
        {
            canShoot = false;
            body.SetActive(false);
            gunholder.SetActive(false);
            deathEsplosion.Play();
            Invoke("Die", 0.5f);

        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerAtaque")
        {
            TakeDamage();
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerAtaque")
        {
            TakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(patrolCheckPoint.position, circleRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
        Gizmos.DrawWireCube(transform.position, shootingDistance);
    }
}
