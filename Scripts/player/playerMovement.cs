using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : Subject, IObserver
{
    public Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private BoxCollider2D box2d;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    [SerializeField] bool isGrounded;
    [SerializeField] int life = 3;
    public SpriteRenderer spriteRenderer;
    public GameObject gunholder;
    public Animator animator;
    public Animation anim;

    [Header("Variáveis de movimento")]
    private bool isFacingRight = true;
    private float move;


    [Header("Variaveis de pulo")]
    //private bool jump;
    //private bool canJump = true;
    //[SerializeField] private bool isJumping;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airControl;
    [SerializeField] float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;


    [Header("Variáveis de agachamento")]
    [SerializeField] private Vector2 standingCollider;
    [SerializeField] private Vector2 offSetStanding;
    [SerializeField] private Vector2 croucnhigCollider;
    [SerializeField] private Vector2 offSetCrouching;
    [SerializeField] private bool drawRaycast;
    [SerializeField] private bool blockedHead;
    [SerializeField] private bool isCrouching;
    [SerializeField] private float distanceFromHead = 0.35f;
    [SerializeField] private float crouchSpeed;


    [Header("Variáveis da esquiva")]
    [SerializeField] private float dashingVelocity;
    [SerializeField] private float dashingTime;
    [SerializeField] float dashCoolDown = 2;
    private Vector2 dirDash;
    private bool isDashing;
    [SerializeField] private bool canDash = true;
    private bool inputDashing;

    [Header("Efeitos")]
    [SerializeField] ParticleSystem deathEsplosion = default;

    [Header("Vitoria")]
    [SerializeField] Subject _FinalFase;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        standingCollider = box2d.size;
        offSetStanding = box2d.offset;
        croucnhigCollider = new Vector2(box2d.size.x, box2d.size.y / 2f);
        offSetCrouching = new Vector2(box2d.offset.x, box2d.offset.y / 2f);
        //anim["crouching"].wrapMode = WrapMode.ClampForever;
        
    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);

        //Andar
        if (isGrounded == true && isCrouching == false)
        {
            //movimento no chão de pé
            rb.velocity = new Vector2(move * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (isGrounded == true && isCrouching == true)
        {
            //movimento agachado
            rb.velocity = new Vector2(move * crouchSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else
        {
            //movimento no ar
            rb.velocity = new Vector2((move * moveSpeed * Time.fixedDeltaTime) / airControl, rb.velocity.y);
        }



        //agachar
        CheckHead();
        //Crouch();

        //Rolar
        inputDashing = Input.GetKey(KeyCode.LeftShift);
        if (inputDashing && canDash && isGrounded)
        {
            isDashing = true;
            canDash = false;
            dirDash = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            if (dirDash == Vector2.zero && isFacingRight)
            {
                dirDash = new Vector2(transform.localScale.x, 0f);
            }
            else if (dirDash == Vector2.zero && !isFacingRight)
            {
                dirDash = new Vector2(transform.localScale.x * -1, 0f);
            }

        }
        if (isDashing)
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
            rb.velocity = dirDash.normalized * dashingVelocity;
            StartCoroutine(StopDashing());
            
            StartCoroutine(DashingCooldown());
            return;
        }


    }

    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        //Flip
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }


        //Pulo
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            //isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //isJumping = true;
            jumpBufferCounter = 0;
        }

        if(Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        /*if (Input.GetKey(KeyCode.S))
        {
            Crouch();
        }
        else
        {
            isCrouching = false;
            box2d.size = standingCollider;
            box2d.offset = offSetStanding;
        }*/
    }


    //--------------------------------------------------------------------------------------------------------------//

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }


    //Fução para agaixar
    private void Crouch()
    {
        /*if (!isGrounded)
        {
            return;
        }*/

        //if (Input.GetKey(KeyCode.S))
       // {
            isCrouching = true;
            //animator.SetBool("crouch", isCrouching);
            box2d.size = croucnhigCollider;
            box2d.offset = offSetCrouching;
       // }
        //else
        //{
            /*if (!blockedHead)
            {
                isCrouching = false;
                box2d.size = standingCollider;
                box2d.offset = offSetStanding;
            }*/
        //}
    }

    void CheckHead()
    {
        Vector2 pos = transform.position;
        Vector2 offset = new Vector2(0f, box2d.size.y);

        //RaycastHit2D checkHead = Physics2D.Raycast(pos + offset, Vector2.up, distanceFromHead, groundLayer);
        RaycastHit2D checkHead = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0, Vector2.up, distanceFromHead, groundLayer);

        if (checkHead)
        {
            blockedHead = true;
        }
        else
        {
            blockedHead = false;
        }

        if (drawRaycast)
        {
            Color rayColor = checkHead ? Color.red : Color.green;
            Debug.DrawRay(pos + offset, Vector2.up * distanceFromHead, rayColor);
        }
    }

    //Função da esquiva
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }

    private IEnumerator DashingCooldown()
    {
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    private void TakeDamage(int damage)
    {
        life -= damage;
        notifyObserver(PlayerActions.SofreuDano);
        if (life <= 0)
        {
            spriteRenderer.color = Color.clear;
            gunholder.SetActive(false);
            deathEsplosion.Play();
            //notifyObserver(PlayerActions.Morreu);
            Invoke("Die", 0.5f);
        }
    }

    public void OnNotify(PlayerActions action)
    {
        if (action == PlayerActions.Vitoria)
        {
            gunholder.SetActive(false);
            Invoke("Die", 0.5f);

        }
    }

    private void Die()
    {
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(this);
        notifyObserver(PlayerActions.Morreu);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            TakeDamage(1);
        }else if(collision.gameObject.tag == "instaKill")
        {
            TakeDamage(10);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);
    }

    public int getLife()
    {
        return life;
    }

    private void OnEnable()
    {
        _FinalFase.addObserver(this);
    }

    private void OnDisable()
    {
        _FinalFase.removeObserver(this);
    }
}

