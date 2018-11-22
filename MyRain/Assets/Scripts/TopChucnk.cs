using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopChucnk : MonoBehaviour
{

    public Transform parentNode;
    public Vector2 targetVector;
    public Vector2 currentVector;
    private Vector2 targetVectorInit;
    public float returnSpeed = 100.0f;
    public float returnTorque = 25.0f;
    public HingeJoint2D theHinge;
    public JointMotor2D Motor;
    public float angle;

    // Use this for initialization
    void Awake()
    {
        targetVectorInit = targetVector;
        theHinge = GetComponent<HingeJoint2D>();
        Motor = theHinge.motor;
        //Motor.motorSpeed = 100;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentVector = transform.position - parentNode.position;
        //float angle = Vector2.Dot(targetVector, currentVector);
        angle = Vector2.SignedAngle(targetVector, currentVector);
        if (angle < 0)
        {
            //Motor.motorSpeed = 1.0f * -returnSpeed;
            Vector2 temp = (currentVector - targetVector)*100;
            Motor.motorSpeed = Mathf.Lerp(0.0f, 1.0f * -returnSpeed, temp.magnitude * Time.deltaTime);
            //Debug.Log(temp.magnitude);
        }
        else if (angle > 0)
        {
            //Motor.motorSpeed = 1.0f * returnSpeed;
            Vector2 temp = (currentVector - targetVector) * 100;
            Motor.motorSpeed = Mathf.Lerp(0.0f, 1.0f * returnSpeed, temp.magnitude * Time.deltaTime);
        }
        else Motor.motorSpeed = 0.0f;

        Debug.DrawRay(parentNode.position, currentVector, Color.magenta);
        Debug.DrawRay(parentNode.position, targetVector, Color.blue);
        Motor.maxMotorTorque = returnTorque;
        theHinge.motor = Motor;
        //Debug.Log(angle);

    }
}
