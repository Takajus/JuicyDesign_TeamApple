using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Range(0f, 5f)] public float shakeTrapDuration = 1.5f;
    [Range(0f, 3f)] public float shakeTrapMagnitude = 0.4f;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CameraShakeTrap(shakeTrapDuration, shakeTrapMagnitude));
        }
    }
        
    public IEnumerator CameraShakeTrap(float duration, float magnitude)
    {
        Debug.Log("CameraShake");
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
    }
}
