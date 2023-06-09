using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMind : MonoBehaviour
{
    [SerializeField]
    FieldManager fm;

    [SerializeField]
    EnemyManager em;

    [SerializeField]
    int numberOfBlocks;

    [SerializeField]
    GameObject buff;

    [SerializeField]
    float timeBetweenWaves;
    [SerializeField]
    float timeToSpawnBuff;

    float timeSinceWaveComplete = 0;
    bool noEnemiesFlag;
    bool doesBuffSpawned;

    [SerializeField]
    int enemies;
    [SerializeField]
    int startEnemies;

    int waveNumber;

    public bool game = false;

    void Update()
    {
        if (!game)
            return;
        bool noEnemiesFlag = checkForEnemies();
        if (noEnemiesFlag) {
            timeSinceWaveComplete += Time.deltaTime;
        } else {
            timeSinceWaveComplete = 0;
        }
        if (timeSinceWaveComplete > timeToSpawnBuff && !doesBuffSpawned) {
            doesBuffSpawned = true;
            Instantiate(buff, position:transform.position, rotation:transform.rotation);
        }
        if (timeSinceWaveComplete > timeBetweenWaves) {
            doesBuffSpawned = false;
            nextStep();
        }
    }

    bool checkForEnemies()
    {
        return GameObject.FindObjectOfType<BasicEnemy>() == null;
    }
    
    void nextStep()
    {
        enemies = waveNumber * 3 + startEnemies;
        fm.clear();
        numberOfBlocks = Random.Range(20, 150);
        fm.generateFieldMarine(numberOfBlocks);
        em.generateWave(enemies);
        AstarPath.active.Scan();
    }

    public void endGame()
    {
        em.clearEnemies();
        enemies = startEnemies;
        game = false;
        GameObject.Find("Hub").GetComponent<Hub>().SpawnGuns();
    }

    public void newGame()
    {
        waveNumber = 1;
        game = true;
        GameObject.Find("Player").transform.position = new Vector3(0, 0, 0);
        nextStep();
    }
}
