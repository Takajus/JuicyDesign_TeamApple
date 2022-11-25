using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } }  

    [SerializeField]
    private GameObject[] enemyPrefab;
    public GameObject ArchInScene;

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
    private float movementDelay = 1.5f;

    private bool _canMove = true;
    
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float speed;

    private float electricTime = 1.5f;

    private bool destroyEnemyRow = false;

    private bool _canShot = true;

    [SerializeField]
    private Material[] materials;
    public List<GameObject> LightningList;
    public List<GameObject> ArchList;
    private GameObject ConcernedEnemy;


    private int _direction = 1;
    
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
        electricTime = 2;
        SpawnEnemies();
        SpawnArch();

        GameObject[] TempLightning = GameObject.FindGameObjectsWithTag("Lightning");

        for (int i = 0; i < TempLightning.Length; i++)
        {
            LightningList.Add(TempLightning[i]);
            LightningList[i].SetActive(false);
        }

        for (int i = 0; i < ArchList.Count; i++)
        {
            ArchList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.GetIsEndGame()) return;
        
        if (!IsEnemyAlive())
            GameManager.Instance.SetEndGame();
        
        if (_canMove)
            StartCoroutine(MoveEnemyToPlayerCoroutine());

        if(destroyEnemyRow)
            StartCoroutine(ElectricDelay(ConcernedEnemy));

        

        for (int i = 0; i < LightningList.Count; i++)
        {
            if (!LightningList[i])
                LightningList.RemoveAt(i);
        }

        for (int i = 0; i < ArchList.Count; i++)
        {
            if (!ArchList[i])
                ArchList.RemoveAt(i);
        }
        
        MoveHorizontal();
    }

    private void SpawnEnemies()
    {
        Vector3 center = _tr.position - 
                         new Vector3(
                             line * 0.5f * distance, distanceToPlayer, 
                             column * 0.5f * distance);

        for (int i = 0; i <= line; ++i)
        {
            for (int j = 0; j < column; ++j)
            {
                int index = UnityEngine.Random.Range(0,5);
                GameObject enemy = Instantiate(enemyPrefab[index], 
                    new Vector3(center.x + i * distance, 1, center.y + j * distance), 
                    Quaternion.Euler(0, 0, 0));
                enemy.transform.SetParent(gameObject.transform);
            }
        }
    }

    private void SpawnArch() 
    {

        Vector3 center = _tr.position -
                 new Vector3(
                     line * 0.5f * distance, distanceToPlayer,
                     column * 0.5f * distance);

        for (int i = 0; i < column; i++) 
        {
            GameObject arch = Instantiate(ArchInScene, new Vector3(0, 0, 0),
                Quaternion.Euler(0, 0, 0));
            ArchList.Add(arch);
            arch.GetComponent<LockPosition>().pos1.position = new Vector3(line * distance/2, 1, center.y + i * distance);
            arch.GetComponent<LockPosition>().pos2.position = new Vector3(line * -distance/2, 1, center.y + i * distance);
        }
    }

    private void MoveHorizontal()
    {
        _tr.position += Vector3.right * _direction * speed * Time.deltaTime;
    }

    public void SetDirection(int dir)
    {
        _direction = dir;
    }

    private void MoveEnemyToPlayer()
    {
        _tr.position += Vector3.back * distanceToMove;
        _tr.position += Vector3.back * distanceToMove;

        for (int i = 0; i < ArchList.Count; i++)
        {
            ArchList[i].GetComponent<LockPosition>().pos1.position += Vector3.back * distanceToMove * 2;
            ArchList[i].GetComponent<LockPosition>().pos2.position += Vector3.back * distanceToMove * 2;
        }
        _canMove = false;
    }
    
    private bool IsEnemyAlive()
    {
        return transform.childCount > 0;
    }
    
    private IEnumerator MoveEnemyToPlayerCoroutine()
    {
        MoveEnemyToPlayer();
        yield return new WaitForSeconds(movementDelay);
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

        for (int i = 0; i < LightningList.Count; i++)
        {
            if (Math.Abs(LightningList[i].transform.position.z - enemy.transform.position.z) < 0.2f)
            {
                LightningList[i].SetActive(true);
            }

        }

        for (int i = 0; i < ArchList.Count; i++)
        {
            if (Math.Abs(ArchList[i].GetComponent<LockPosition>().pos1.position.z - enemy.transform.position.z) < 0.2f)
            {
                ArchList[i].SetActive(true);
                ArchList[i].SetActive(true);
            }
        }

        ConcernedEnemy = enemy;
        destroyEnemyRow = true;
    }

    public void DestroyEnemyInSameColumn(GameObject enemy)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (Math.Abs(e.transform.position.x - enemy.transform.position.x) < 0.2f)
            {
                JuicyManager.Instance.DestructionSystem(e);
                JuicyManager.Instance.PopUpScoreSystem(e, "13");
                
                SoundManager.Instance.PlaySound("Destruction alien");
                
                // delay for destroy

                Destroy(e);
            }
        }
    }
    
    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(fireRate);
        _canShot = true;
    }


    private IEnumerator ElectricDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(electricTime);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (Math.Abs(e.transform.position.z - enemy.transform.position.z) < 0.2f)
            {
                JuicyManager.Instance.DestructionSystem(e);
                JuicyManager.Instance.PopUpScoreSystem(e, "13");

                SoundManager.Instance.PlaySound("Destruction alien");

                Destroy(e);
            }

            for (int i = 0; i < ArchList.Count; i++)
            {
                if (Math.Abs(ArchList[i].GetComponent<LockPosition>().pos1.position.z - enemy.transform.position.z) < 0.2f)
                {
                    Destroy(ArchList[i]);
                }
            }
        }
        destroyEnemyRow = false;
    }
}
