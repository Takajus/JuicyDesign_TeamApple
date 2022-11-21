using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Transform transform;
    public AnimationCurve curve;

    private bool isKey;

    private float timer;
    public float curveValue;
    public float speed;
    public float length;

    void Start()
    {
        transform = GetComponent<Transform>();
        isKey = false;
    }

    void Update()
    {
        //Change the key to a bool of when the ship shoots. Just make sure the bool you are using returns false after the shot.
        if (Input.GetKeyDown(KeyCode.W))
        {
            isKey = true;
        } 

        if (isKey) 
        {
            timer += speed * Time.deltaTime;
            curveValue = length * curve.Evaluate(timer);

            // You just have to change it to the right dimension (x, y, z)
            transform.position = new Vector3(0, 0 , -curveValue);
        }

        if (timer >= 1)
        {
            timer = 0;
            isKey = false;
        }
    }
}
