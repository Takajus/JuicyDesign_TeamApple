using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    private float shakeTrapDuration = 1.5f;
    [SerializeField]
    [Range(0f, 3f)] 
    private float shakeTrapMagnitude = 0.4f;
    
    void Update()
    {
        StartCoroutine(CameraShakeTrap(shakeTrapDuration, shakeTrapMagnitude));
    }
        
    public IEnumerator CameraShakeTrap(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
        
        enabled = false;
    }
}
