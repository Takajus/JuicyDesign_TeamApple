using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;

public class ArchController : MonoBehaviour
{
    public Transform pointTransform;

    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 3, Color.yellow);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit))
        {
            //pointTransform.position = hit.transform.position;
            
            Debug.DrawRay(new Vector3(0,0,0), new Vector3(10, 0, 0), Color.yellow);
            Debug.Log("Did Hit");
        }
    }

}
