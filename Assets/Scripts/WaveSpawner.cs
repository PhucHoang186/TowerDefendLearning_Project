using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Text Countdowntext;
    public Transform EnemyPrefab;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWave = 5f;
    public float countDown = 2f;
    private int waveIndex = 0;
    public static int enemyAlive = 0;
    void Start()
    {
        Countdowntext.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyAlive >0)
        {
            return;
        }
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWave;
            return;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        Countdowntext.text = string.Format("{0:00.00}", countDown);
    }
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds += 1;
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f/wave.spawnrate);
        }
        waveIndex++;
        if(waveIndex == waves.Length)
        {
            Debug.Log("Win");
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject EnemyPrefab)
    {
        enemyAlive++;
        var enemy = Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
