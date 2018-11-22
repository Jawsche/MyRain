using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;


public class PlayerControl : MonoBehaviour {

    //attributes
    [Range (1, 10)]
    public float a_maxSpeed = 8.5f;
    [Range(1, 10)]
    public float a_jumpStrangth = 5.0f;
    [Range(1, 10)]
    public float fallMultiplier = 2.5f;
    [Range(1, 10)]
    public float lowJumpMultiplier = 2f;

    public TopChucnk topChunck;



    

    //internal variables
    private Rigidbody2D p_rb;
    private CircleCollider2D p_colliderBase;
    private BoxCollider2D p_colliderHead;
    private Animator p_anim;
    private float p_facingDir = 1.0f; 
    private bool p_grounded = false;
    private bool p_standing = true;
    private bool p_isWalking = false;
    public float a_maxSpeedInit;



    private void Awake()
    {
        p_rb = GetComponent<Rigidbody2D>();
        p_colliderBase = GetComponent<CircleCollider2D>();
        p_colliderHead = GetComponent<BoxCollider2D>();
        p_anim = GetComponent<Animator>();
        a_maxSpeedInit = a_maxSpeed;
    }


    void Update()
    {
        //horizontal input float
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        //call grounding method
        CheckGrounded();

        //Update Animator
        UpdateAnimator(h);

        //Run while grounded
        if (h != 0)// && p_grounded)
        {
            p_rb.velocity = new Vector2(h * a_maxSpeed, p_rb.velocity.y);
            if(p_grounded)
                p_isWalking = true;
            else p_isWalking = false;
        }
        

        if (h < 0.0f)
            p_facingDir = -1.0f;
        if (h > 0.0f)
            p_facingDir = 1.0f;

        //Jump
        if (CrossPlatformInputManager.GetButtonDown("Jump") && p_grounded)
            p_rb.AddForce(Vector2.up * a_jumpStrangth, ForceMode2D.Impulse);
        if (p_rb.velocity.y < 0)
            p_rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        else if (p_rb.velocity.y > 0.0f && !CrossPlatformInputManager.GetButton("Jump"))
            p_rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;

        //Crouch
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && p_standing == true)
        {
            p_standing = false;
            //float tempX;
            //float tempY;
            //tempX = topChunck.relativeOffset.y * p_facingDir;
            //tempY = topChunck.relativeOffset.x;
            //topChunck.relativeOffset = new Vector2(tempX, tempY);
            if (p_facingDir == 1.0f)
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles,  new Vector3(0.0f, 0.0f, -90.0f), 1);
            else if (p_facingDir == -1.0f)
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            a_maxSpeed *= 0.7f;
        }
        //stand back up
        else if (CrossPlatformInputManager.GetButtonDown("Fire1") && p_standing == false)
        {
            p_standing = true;
            //topChunck.relativeOffset = topChunck.relativeOffsetInit;
            if (p_facingDir == 1.0f)
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            else if (p_facingDir == -1.0f)
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            a_maxSpeed = a_maxSpeedInit;
        }
    }
    private void CheckGrounded()
    {
        //Set Grounded variable
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.6f);
        if (hit.collider != null)
        {
            p_grounded = true;
            Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.red);
            //Debug.Log(hit.collider.name);
        }
        else
        {
            p_grounded = false;
        }
    }
    private void UpdateAnimator(float h)
    {
        p_anim.SetBool("IsWalking", p_isWalking);
        p_anim.SetFloat("Horizontal", h);
    }



}
