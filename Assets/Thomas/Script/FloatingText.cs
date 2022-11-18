using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private float DestroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
