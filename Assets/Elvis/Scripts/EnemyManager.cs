using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } }

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int line;
    
    [SerializeField]
    private int column;
    private Transform _tr;
    
    [SerializeField]
    private float distance = 1.5f;

    [SerializeField] 
    private float distanceToPlayer = 1;
    [SerializeField]
    private float distanceToMove = 1;
    [SerializeField]
    private float delay = 1.5f;

    private bool _canMove = true;
    
    [SerializeField]
    private float fireRate;
    
    private bool _canShot = true;
    
    [SerializeField]
    private Material[] materials;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _tr = GetComponent<Transform>();

        SpawnEnemies();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.GetIsEndGame()) return;
        
        if (!IsEnemyAlive())
            GameManager.Instance.SetEndGame();
        
        if (_canMove)
            StartCoroutine(MoveEnemyToPlayerCoroutine());
    }
    
    private void SpawnEnemies()
    {
        Vector3 center = _tr.position - 
                         new Vector3(
                             line * 0.5f * distance, distanceToPlayer, 
                             column * 0.5f * distance);

        for (int i = 0; i < line; ++i)
        {
            for (int j = 0; j < column; ++j)
            {
                GameObject enemy = Instantiate(enemyPrefab, 
                    new Vector3(center.x + i * distance, 1, center.y + j * distance), 
                    Quaternion.Euler(90, 0, 0));
                
                if (i == 0)
                    enemy.GetComponent<Renderer>().material = materials[0];
                else if (i == line - 1)
                    enemy.GetComponent<Renderer>().material = materials[1];
                else
                    enemy.GetComponent<Renderer>().material = materials[2];

                enemy.transform.SetParent(gameObject.transform);
            }
        }
    }

    private void MoveEnemyToPlayer()
    {
        _tr.position += Vector3.back * distanceToMove;
        _canMove = false;
    }
    
    private bool IsEnemyAlive()
    {
        return transform.childCount > 0;
    }
    
    private IEnumerator MoveEnemyToPlayerCoroutine()
    {
        MoveEnemyToPlayer();
        yield return new WaitForSeconds(delay);
        _canMove = true;
    }
    
    public bool GetCanShot()
    {
        return _canShot;
    }

    public void SetCanShotTrue(bool value)
    {
        _canShot = value;
        StartCoroutine(ShotDelay());
    }

    public void DestroyEnemyInSameLine(GameObject enemy)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (Math.Abs(e.transform.position.z - enemy.transform.position.z) < 0.2f)
            {
                Destroy(e);
            }
        }
    }

    public void DestroyEnemyInSameColumn(GameObject enemy)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (Math.Abs(e.transform.position.x - enemy.transform.position.x) < 0.2f)
            {
                Destroy(e);
            }
        }
    }
    
    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(fireRate);
        _canShot = true;
    }
}
