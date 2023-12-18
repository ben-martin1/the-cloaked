using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    //movement
    private float vertical;
    private Vector3 direction;

    [SerializeField]
    private float speed;
    private float launch;
    private bool moving;
    private bool crouching;
    private bool runJump;
    public Transform cam;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public Animator animator;
    Vector3 moveDir;
    Vector3 launchMoveDir;
    public float launchHorizontal;
    public float launchVertical;

    public Camera raycastCam;

    //animation stuff
    public bool hasJumped;
   public bool hasDoubleJumped;
    public float jumpCountdown = 1f;

    //jumpstuff
    public Rigidbody rb;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public float jumpTimer;

    //effects
    public GameObject walkDust;
    public GameObject summonEffect;
    public GameObject noActive;
    public GameObject desummoned;

    //interact
    public Transform raycastCheck;
    public float raycastDistance;


    //summoning
    private playerSummon playerSummon;
    public GameObject activeSummon;/*
    public GameObject summon;
    public int summonCount;
    public List<GameObject> summonedCreatures;
    public int summonMax;
    public float summonRange;
    Vector3 spawnPoint;*/

    public Vector3 velocity;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerSummon = GetComponent<playerSummon>();
    }
    
    void FixedUpdate()
    {
        monitorDirection();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        animator.SetFloat("Speed", horizontal + vertical);
        animator.SetBool("isMoving", moving);
        animator.SetFloat("jumpCountdown", jumpCountdown);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", hasJumped);
        animator.SetBool("isDoubleJumping", hasDoubleJumped);

        monitorInput();
    }
    void monitorDirection()
    {
        if (direction.magnitude == 0f && isGrounded)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (direction.magnitude >= 0.1f)
        {
            moveCharacter();

        }
        else
        {
            moving = false;
            transform.rotation = transform.rotation;
        }
    }
    void monitorInput()
    {
        if (isGrounded)
        {
            jumpTimer += Time.deltaTime;
            jumpCountdown = 1f;
            runJump = false;
            launchHorizontal = 0f;
            launchVertical = 0f;
            hasDoubleJumped = false;
            launchMoveDir = Vector3.zero;
            if (jumpTimer > .1f)
            {
                hasJumped = false;
            }
        }
        if (Input.GetButton("Fire3"))
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpTimer = 0f;
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * gravity), rb.velocity.z);
            hasJumped = true;
            animator.SetBool("isJumping", hasJumped);
            animator.SetBool("isGrounded", false);
        }
        if (Input.GetButtonDown("Jump") && !hasDoubleJumped && !isGrounded)
        {
            hasDoubleJumped = true;
            animator.SetBool("isDoubleJumping", true);
            launch *= .5f;
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -1.8f * gravity), rb.velocity.z);
        }

        #region raycasts
        if (Input.GetButtonDown("Fire2"))
        {
            getSummon();
        }
        #endregion

        if ((Input.GetButtonDown("Fire1")) && playerSummon.hasAvailableSummons())
        {
            RaycastHit hit;
            Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity);
            if (hit.point != null && activeSummon != null)
            {
                playerSummon.summonShot(hit.point, activeSummon);
            }
        }
    }
    void moveCharacter()
    {
        float targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y);
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
        moving = true;   
    }
    void getSummon()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
                if (interactable.activeSummon != null)
                {
                    playerSummon.activeSummon = interactable.activeSummon;
                }
            }
        }
    }

}

