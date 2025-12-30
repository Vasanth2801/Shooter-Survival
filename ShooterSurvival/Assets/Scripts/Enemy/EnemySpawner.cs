using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public float timeBetweenSpawns = 3f;
        public float timeBetweenWaves = 10f;
        public int enemiesCount;
    }

    [Header("Wave Settings")]
    public Wave[] waves;
    [SerializeField] float countDown;
    public Transform[] spawnPoint;
    public int currentWave = 0;
    private bool countDownToBegin;

    void Start()
    {
        countDownToBegin = true;
        for(int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesCount = waves[i].enemies.Length;
        }
    }

    void Update()
    {
        if(currentWave >= waves.Length)
        {
            Debug.Log("All Waves Completed");
            return;
        }

        if(countDownToBegin == true)
        {
            countDown -= Time.deltaTime;
        }

        if(countDown <= 0f)
        {
            countDownToBegin = false;
            countDown = waves[currentWave].timeBetweenWaves;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWave].enemiesCount == 0)
        {
            countDownToBegin = true;
            currentWave++;
        }
    }

    IEnumerator SpawnWave()
    {
        if(currentWave < waves.Length)
        {
            for(int i =0; i < waves[currentWave].enemies.Length; i++)
            {
                Transform spawnPoints = spawnPoint[Random.Range(0,spawnPoint.Length)];
                GameObject enemy = Instantiate(waves[currentWave].enemies[i],spawnPoints.position,Quaternion.identity);
                yield return new WaitForSeconds(waves[currentWave].timeBetweenWaves);
            }
            Debug.Log("Wave Spawned");
        }
    }
}