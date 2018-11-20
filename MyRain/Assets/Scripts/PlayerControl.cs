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



    

    //internal variables
    private Rigidbody2D p_rb;
    private CircleCollider2D p_colliderBase;
    private BoxCollider2D p_colliderHead;
    private bool p_grounded = false;



    private void Awake()
    {
        p_rb = GetComponent<Rigidbody2D>();
        p_colliderBase = GetComponent<CircleCollider2D>();
        p_colliderHead = GetComponent<BoxCollider2D>();
    }

	
	void Update () {
        //horizontal input float
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        //call grounding method
        CheckGrounded();

        //Run while grounded
        if (h != 0)// && p_grounded)
        {
            p_rb.velocity = new Vector2(h * a_maxSpeed, p_rb.velocity.y);
        }

        //Jump
        if (CrossPlatformInputManager.GetButtonDown("Jump") && p_grounded)
            p_rb.AddForce(Vector2.up * a_jumpStrangth, ForceMode2D.Impulse);
        if (p_rb.velocity.y < 0)
            p_rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        else if (p_rb.velocity.y > 0.0f && !CrossPlatformInputManager.GetButton("Jump"))
            p_rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
    }

    private void CheckGrounded()
    {
        //Set Grounded variable
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.1f);
        if (hit.collider != null)
        {
            p_grounded = true;
            Debug.DrawRay(transform.position, Vector3.down * 1.1f, Color.red);
            Debug.Log(hit.collider.name);
        }
        else
        {
            p_grounded = false;
        }
    }

 
}
