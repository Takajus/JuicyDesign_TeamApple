using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testVRInout : MonoBehaviour
{
    public InputActionProperty trigger, movement, test;
    public bool VrVsInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.action.WasPressedThisFrame() || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("test");
        }

        //Debug.Log(movement.action.WasPerformedThisFrame().ToString());
        //Debug.Log(movement.action.ReadValue<Vector2>().x.ToString());

        Vector2 move = movement.action.ReadValue<Vector2>();
        //Debug.Log(move.x.ToString() + " " + move.y.ToString());

        //Vector2 movement1 = test.action.ReadValue<Vector2>();
        Vector2 movement1 = vectorTest();
        
        Debug.Log($"move: {movement1.x}, {movement1.y}");
        
    }

    Vector2 vectorTest()
    {
        Vector2 movement2 = new Vector2();
        if (VrVsInput)
            movement2 = test.action.ReadValue<Vector2>();
        else if (!VrVsInput)
        {
            float velo = Input.GetAxis("Horizontal");
            movement2 = new Vector3(velo, 0, 0);
        }

        return movement2;
    }

}
