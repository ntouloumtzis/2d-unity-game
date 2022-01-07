using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour 
{
    [System.Serializable] // so we can tweak the wave system inside of Unity
    public class Wave 
    {
        public Enemy[] enemies; // an array, which contains every possible enemy to spawn
        public int count; // how many enemies will spawn inside of a wave
        public float timeBetweenSpawns; // how much time 
    }

    public Wave[] waves; // an array, which holds the number of waves
    public Transform[] spawnPoints; // contains all the points that the enemies can spawn from
    public float timeBetweenWaves; // how much time does it need, so the next wave begins

    private Wave currentWave; // store the current wave in the game, so we can adjust it in Unity
    private int currentWaveIndex; // each wave is an index of the waves array (wave 1 = wave[0], etc)
    private Transform player; // a reference to the player
    private bool spawningFinished; // to define when the wave spawns are finished

    public GameObject boss; // reference boss
    public Transform bossSpawnPoint; // which position does he spawn

    public GameObject healthBar;

    private void Start()
    {
        // reference the player with its corresponding tag and start the first wave
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(CallNextWave(currentWaveIndex));
    }

    private void Update()
    {
        // if waves are finished and no enemy tag is on the map
        if (spawningFinished == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            spawningFinished = false; // waves are finished

            // if waves aren't finished yet
            if (currentWaveIndex + 1 < waves.Length)
            {
                // call the next wave
                currentWaveIndex++;
                StartCoroutine(CallNextWave(currentWaveIndex));

            } else {
                // spawn (who spawns, which position, which rotation)
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                healthBar.SetActive(true);
            }
        }
    }

    // A coroutine, which waits for a period of time based on how much the timeBetweenWaves is and call a coroutine with the next wave
    IEnumerator CallNextWave(int waveIndex) 
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(waveIndex));
    }

    // A coroutine for the waves spawning functionality
    IEnumerator SpawnWave (int waveIndex) 
    {
        currentWave = waves[waveIndex];

        // run as many times as the number of waves are
        for (int i = 0; i < currentWave.count; i++)
        {
            // when player dies, stop wave spawning
            if (player == null)
            {
                yield break;
            }

            // spawn the random enemies to random spawns
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpawnPoint.position, transform.rotation);

            // when waves finish
            if (i == currentWave.count - 1)
            {
                spawningFinished = true;
            } else {
                spawningFinished = false;
            }

            // call the next wave based on timeBetweenWaves input value
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }
}
