using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField]
    private int maxHit = 3;
    
    public void GetHit()
    {
        maxHit--;

        if (maxHit == 0)
        {
            SoundManager.Instance.PlaySound("Destroy Shield");
            Destroy(gameObject);
            
            return;
        }
        
        SoundManager.Instance.PlaySound("Hit Shield");
    }
}
