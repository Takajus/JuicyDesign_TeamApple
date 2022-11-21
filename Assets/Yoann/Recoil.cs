using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private float timer;
    private Transform transform;

    private bool isKey;

    public float freq;
    public float amp;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        freq = 64;
        amp = 0.5f;
        isKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isKey = true;
        } 

        if (isKey) 
        {
            timer += Time.deltaTime;
            transform.position = new Vector3(0, -amp * Mathf.Sin(timer * freq) + amp, 0);
        }

        if (timer >= (Mathf.PI / (freq / 2)))
        {
            timer = 0;
            isKey = false;
        }
    }
}
