using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopChucnk : MonoBehaviour
{

    public Transform parentNode;
    public Vector2 relativeOffset;
    public Vector2 relativeOffsetInit;
    public float returnSpeed = 15.0f;

    // Use this for initialization
    void Awake()
    {
        relativeOffsetInit = relativeOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector2(Mathf.Lerp(transform.eulerAngles.x, parentNode.localEulerAngles.x, Time.deltaTime * returnSpeed), Mathf.Lerp(transform.eulerAngles.y, parentNode.localEulerAngles.y, Time.deltaTime * returnSpeed));
        transform.position = new Vector2(Mathf.Lerp(transform.position.x, parentNode.position.x + relativeOffset.x, Time.deltaTime * returnSpeed), Mathf.Lerp(transform.position.y, parentNode.position.y + relativeOffset.y, Time.deltaTime * returnSpeed));
    }
}
