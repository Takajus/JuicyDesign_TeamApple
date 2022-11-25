using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField]
    private int maxHit = 3;
    
    [SerializeField]
    private GameObject[] shieldZones;
    
    public void GetHit()
    {
        maxHit--;


        if (maxHit == 0)
        {
            DestroyShield();

            return;
        }
        
        shieldZones[maxHit].SetActive(false);
        SoundManager.Instance.PlaySound("Hit Shield");
    }

    public void DestroyShield()
    {
        SoundManager.Instance.PlaySound("Destroy Shield");
        Destroy(gameObject);
    }
}
