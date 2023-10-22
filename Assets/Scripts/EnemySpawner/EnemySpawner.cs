using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpawnState { NotSpawning, SpawningWave }
public class EnemySpawner : MonoBehaviour
{
    Transform player;
    public int enemyLevel = 1;
    public int enemyWave = 1;
    public float totalWaveTime = 30f;
    private float currentEnemyWaveTime;
   
    public float timeSpawnInterval = 10f;
    private bool isWaitingWaveTime;

    public int totalEnemyPerWave = 9;
    public int numberOfEnemyInWave;

    public List<GameObject> enemyListPrefab = new List<GameObject>();
    private SpawnState spawnState = SpawnState.NotSpawning;

    public float minXOffset = -15f; 
    public float maxXOffset = 15f;  
    public float minYOffset = 3f; 
    public float maxYOffset = 10f;

   
    private void Start()
    {
        currentEnemyWaveTime = totalWaveTime;
        StartCoroutine(SpawnQuaiVatRoutine());

    }

    private void Update()
    {
        if(isWaitingWaveTime== false)
        {
            currentEnemyWaveTime -= Time.deltaTime;
            UIIngame.Instance.UpdateWaveTimeSlider(totalWaveTime, currentEnemyWaveTime);
        }
        bool isGameOver = GameManager.Instance.IsGameOver();

        if (spawnState == SpawnState.NotSpawning && currentEnemyWaveTime < 0 && isGameOver== false)
        {
            enemyWave++;
            enemyLevel++;
            totalEnemyPerWave++;
            currentEnemyWaveTime = totalWaveTime;

            UIIngame.Instance.UpdateWaveText(enemyWave);

            StartCoroutine(SpawnQuaiVatRoutine());
        }

    }

    IEnumerator SpawnQuaiVatRoutine()
    {
        isWaitingWaveTime = true;
        UIIngame.Instance.DisplayWaveNoti();
        yield return new WaitForSeconds(5);
        UIIngame.Instance.HideWaveNoti();
        isWaitingWaveTime = false;

        spawnState = SpawnState.SpawningWave;

        currentEnemyWaveTime = totalWaveTime;


        for (int i = 0; i < CalculateEnemyInWave(); i++)
            {
                SpawnEnemy();
                float randomInterval = Random.Range(0.1f, 1f);
                yield return new WaitForSeconds(randomInterval);
            }

            yield return new WaitForSeconds(timeSpawnInterval);
        
        spawnState = SpawnState.NotSpawning;
    }
    int CalculateEnemyInWave()
    {
        return totalEnemyPerWave / 3;
    }

    int CalculateOddEnemyInWave()
    {
       
            return totalEnemyPerWave % 3;
      
    }

    void SpawnEnemy()
    {
        player = PlayerManager.Instance.player.transform;

        float randomXOffset = Random.Range(minXOffset, maxXOffset);
        float randomYOffset = Random.Range(minYOffset, maxYOffset);
        int enemyIndex = Random.Range(0, enemyListPrefab.Count);

        Vector3 playerPosition = player.position;

        Vector3 spawnPosition = new Vector2(playerPosition.x + randomXOffset, playerPosition.y + randomYOffset);
        Instantiate(enemyListPrefab[enemyIndex], spawnPosition, Quaternion.identity);

    }

}
