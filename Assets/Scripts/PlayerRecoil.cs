using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    [Range(0f, 1f)] public float moveBackDistance = 1f;
    [Range(0f, 2f)] public float recoilTime;
    public GameObject Player;

    private Vector3 _targetedPosition;
    private Vector3 _currentPosition;
    private bool _methodSettings = false;

    private float _timer = 0f;
    private float _percent;

    void Update()
    {
        _timer += Time.deltaTime;
        _percent = _timer / recoilTime;
        if (Input.GetKey(KeyCode.A))
        {
            if (!_methodSettings)
            {
                _methodSettings = true;
                _targetedPosition.x = Player.transform.position.x;
                _targetedPosition.y = Player.transform.position.y - moveBackDistance;
                _targetedPosition.z = Player.transform.position.z;
                _currentPosition = Player.transform.position;
            }
            Recoil(recoilTime);
        }
    }
    
    public void Recoil(float lerpTime)
    {
        Debug.Log("Recoil");
        Vector3 currentPosition = _currentPosition;
        Vector3 targetedPosition = _targetedPosition;

        if(_percent < recoilTime / 2)
        {
            Player.transform.position = Vector3.Lerp(currentPosition, targetedPosition, _percent);
        }
        else
        {
            Player.transform.position = Vector3.Lerp(targetedPosition, currentPosition, _percent);
        }

        if(_timer >= lerpTime)
        {
            _methodSettings = false;
            _timer = 0;
        }
    }
}
