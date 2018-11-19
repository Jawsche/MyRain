using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;


public class PlayerControl : MonoBehaviour {

    //attributes
    public float a_maxSpeed = 5.0f;

    public float a_jumpStrangth = 5.0f;

    

    //internal variables
    private Rigidbody2D p_rb;
    private Collider2D p_collider;
    private bool p_grounded = false;

    //Debug Variables
    public Vector2 VelTest;


    void Start () {
        p_rb = GetComponent<Rigidbody2D>();
        p_collider = GetComponent<Collider2D>();
	}
	
	void Update () {
        //horizontal input float
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        //Run while grounded
        if (h != 0 && p_grounded)
        {
            Vector2 temp;
            temp = p_rb.velocity;
            temp.x = a_maxSpeed * h;
            p_rb.velocity = temp;
        }

        //mid-air movement
        if (h != 0 && !p_grounded)
            p_rb.AddForce(Vector2.right * h * a_maxSpeed, ForceMode2D.Force);

        //Basic Jump
        if (CrossPlatformInputManager.GetButtonDown("Jump") && p_grounded)
            p_rb.AddForce(Vector2.up * a_jumpStrangth, ForceMode2D.Impulse);


        ////Debug Velocity
        //VelTest = p_rb.velocity;
        //Debug.Log(VelTest);
    }

    void FixedUpdate()
    {

        //maxspeed while moving
        if (p_rb.velocity.magnitude > a_maxSpeed && p_grounded)
        {
            p_rb.velocity = p_rb.velocity.normalized * a_maxSpeed;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.6f);
        //Debug.DrawLine(transform.position, transform.up * -0.6f, Color.red);
        if (hit.collider != null)
        {
            p_grounded = true;
            Debug.Log(hit.collider.name);
        }
        else
        {
            p_grounded = false;
        }
        
    }
}
