using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testVRInout : MonoBehaviour
{
    public InputActionProperty input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.action.WasPressedThisFrame())
        {
            print("test");
        }
    }
}
