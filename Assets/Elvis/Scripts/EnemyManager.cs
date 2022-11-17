using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int line;
    
    [SerializeField]
    private int column;

    private Transform _transform;
    
    [SerializeField]
    private float distance = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();

        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnEnemies()
    {
        Vector3 center = _transform.position - 
                         new Vector3(
                             line * 0.5f * distance, 
                             column * 0.5f * distance, 0);

        for (int i = 0; i < line; ++i)
        {
            for (int j = 0; j < column; ++j)
            {
                Instantiate(enemyPrefab, 
                    new Vector3(center.x + i * distance, center.y + j * distance, 0), 
                    Quaternion.identity);
            }
        }
    }
}
