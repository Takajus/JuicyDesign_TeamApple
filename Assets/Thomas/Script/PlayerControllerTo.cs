using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTo : MonoBehaviour
{
    private Vector3 PlayerPosition;

    [SerializeField]
    private float speed = 5f, limitx;

    [SerializeField]
    private GameObject bullet, shootingPoint;
    

    void Start()
    {
        PlayerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ShootingSystem();


    }

    void PlayerMovement()
    {
        PlayerPosition.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        PlayerPosition.x = Mathf.Clamp(PlayerPosition.x, -limitx, limitx);
        transform.position = PlayerPosition;
    }

    void ShootingSystem()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, shootingPoint.transform.position, shootingPoint.transform.rotation);
        }
    }
}
