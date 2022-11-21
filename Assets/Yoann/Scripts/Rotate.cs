using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    Transform transform;
    //set to zero will have no effect on the
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    [Range(1, 15)] public float SpeedRange;
    public float _speed;


    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        _speed = Random.Range(1, SpeedRange);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x + (Random.Range(1, _speed)) * Time.deltaTime, y + (Random.Range(1, _speed)) * Time.deltaTime, z + (Random.Range(1, _speed)) * Time.deltaTime);
    }
}
