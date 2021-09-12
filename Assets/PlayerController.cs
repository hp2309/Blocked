using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    
    public CharacterController controller;

    public GameObject player;
    public Transform camera;
    
    public Transform groundCheck;
    public LayerMask groundMask;

    private bool isGrounded;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float pushFactor = 0f;
    
    private float turnSmoothVelocity;
    private bool flag_notFalling = true;

    Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = this.GetComponentInChildren<Animator>();
        player = this.gameObject;
        animator.speed = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(this.transform.position.y < -4f && flag_notFalling)
        {
            animator.SetTrigger("isFalling");
            flag_notFalling = false;
        }

        move();
        gravityCorrection();
        jump();
        //attack();
    }

    void attack()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            animator.SetTrigger("Attack");
            velocity.y = Mathf.Sqrt((jumpHeight + 2f) * -2f * gravity);
        }
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("JumpTrigger");
        }
    }

    void gravityCorrection()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

}



