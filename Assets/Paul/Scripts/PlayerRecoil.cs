using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    [Range(0f, 3f)] public float moveBackDistance = 1f;
    [Range(0f, 2f)] public float recoilTime;
    [Range(0f, 2f)] public float comeBackTime;
    public GameObject Player;

    private Vector3 _targetedPosition;
    private Vector3 _currentPosition;
    private bool _methodSettings = false;

    private float _timer = 0f;
    private float _firstPercent;
    private float _secondPercent;

    void Update()
    {
        _timer += Time.deltaTime;
        _firstPercent = _timer / recoilTime;
        _secondPercent = _timer / comeBackTime;

        if (Input.GetKey(KeyCode.A))
        {
            RecoilSin();
            //if (!_methodSettings)
            //{
            //    _methodSettings = true;
            //    _targetedPosition.x = Player.transform.position.x;
            //    _targetedPosition.y = Player.transform.position.y - moveBackDistance;
            //    _targetedPosition.z = Player.transform.position.z;
            //    _currentPosition = Player.transform.position;
            //}
            //if (Player.transform.position != _targetedPosition)
            //{
            //    Recoil();
            //}
            //else
            //{
            //    ComeBack();
            //}
            //TimeOver();
        }
    }
    
    public void Recoil()
    {
        Debug.Log("Recoil");
        Vector3 currentPosition = _currentPosition;
        Vector3 targetedPosition = _targetedPosition;
        
        Player.transform.position = Vector3.Lerp(currentPosition, targetedPosition, _firstPercent);
    }

    public void RecoilSin()
    {
        Player.transform.position = new Vector3(0, Mathf.Sin(_timer), 0);
    }

    public void ComeBack()
    {
        Debug.Log("Recoil");
        Vector3 currentPosition = _currentPosition;
        Vector3 targetedPosition = _targetedPosition;

        Player.transform.position = Vector3.Lerp(targetedPosition, currentPosition, _secondPercent);
    }

    public void TimeOver()
    {
        if(_timer >= recoilTime + comeBackTime)
        _timer = 0;
        _methodSettings = false;
    }
}
