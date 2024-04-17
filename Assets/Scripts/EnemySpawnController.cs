using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public float spawnTimer;
        public float baseSpawnTimer;
        public int spawnCount;
        public bool isSpawning = false;
        public bool waveComplete = false;
    }



    public Wave[] wave;
    public Transform enemySpawnPoint;
    public GameObject enemyPrefab;
    public int waveCount;

    public float searchTimer;
    public float baseSearchTimer;

    private void Start()
    {
        searchTimer = baseSearchTimer;
    }

    void Update()
    {
        if (wave.Length > waveCount)
        {
            if ( !wave[waveCount].waveComplete)
            {
                if ((wave[waveCount].spawnTimer -= Time.deltaTime) <= 0 && wave[waveCount].spawnCount > 0)
                {
                    var enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);

                    wave[waveCount].spawnCount--;
                    wave[waveCount].spawnTimer = wave[waveCount].baseSpawnTimer;                    
                }                
                else if (wave[waveCount].spawnCount <= 0 )
                {
                    IsEnemyAlive();                    
                }
                else
                {
                    searchTimer = baseSearchTimer;
                }
            }
            else if(waveCount < wave.Length)
            {
                waveCount++;
            }
        }                       
    }

    void IsEnemyAlive()
    {
        if((searchTimer -= Time.deltaTime) <= 0)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                wave[waveCount].waveComplete = true;
                searchTimer = baseSearchTimer;
            }
            else
            {                
                wave[waveCount].waveComplete = false;
            }            
        }        
    }   
}
