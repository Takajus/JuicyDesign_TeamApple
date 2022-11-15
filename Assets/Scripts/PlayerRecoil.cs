using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    [Range(0f, 5f)] public float moveBackDistance;
    [Range(0f, 5f)] public float recoilTime;
    public GameObject Player;

    private Vector3 targetedPosition;

    private void Start()
    {
        targetedPosition.z = Player.transform.position.z - moveBackDistance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Recoil(targetedPosition.z, recoilTime));
        }
    }
    
    public IEnumerator Recoil(float targetDistance, float time)
    {
        Debug.Log("Recoil");
        Vector3 originalPos = Player.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < time)
        {
            /* float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Player.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            */
            yield return null;
        }

        Player.transform.localPosition = originalPos;
    }
}
