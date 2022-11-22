using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Transform transform;
    public AnimationCurve curve;

    private bool isKey;

    private float timer;
    private float curveValue;

    //editable values
    public float speed;
    public float length;

    void Start()
    {
        transform = GetComponent<Transform>();

        // Insuring the bool is set to false at the start.
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
            // the timer is deltatime * speed where speed just accelerates time
            timer += speed * Time.deltaTime;

            // the length is just a factor on the "lerp function": Evaluate
            curveValue = length * curve.Evaluate(timer);

            // You just have to change it to the right dimension (x, y, z)
            transform.position = new Vector3(0, 0, -curveValue);
        }

        // timer resets after 1 because this is considered a lerp
        if (timer >= 1)
        {
            timer = 0;
            isKey = false;
        }
    }
}
