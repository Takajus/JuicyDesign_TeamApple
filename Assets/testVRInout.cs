using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testVRInout : MonoBehaviour
{
    public InputActionProperty trigger, movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.action.WasPressedThisFrame())
        {
            Debug.Log("test");
        }

        Debug.Log(movement.action.WasPerformedThisFrame().ToString());
        Debug.Log(movement.action.ReadValue<Vector2>().x.ToString());

        Vector2 move = movement.action.ReadValue<Vector2>();
        Debug.Log(move.x.ToString() + " " + move.y.ToString());
    }
}
