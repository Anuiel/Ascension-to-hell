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
    Buff buff;

    [SerializeField]
    float timeBetweenWaves;
    [SerializeField]
    float timeToSpawnBuff;

    float timeSinceWaveComplete = 0;
    bool noEnemiesFlag;
    bool doesBuffSpawned;

    [SerializeField]
    int enemies;

    void Update()
    {
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
        fm.clear();
        fm.generateFieldMarine(numberOfBlocks);
        em.generateWave(enemies);
        AstarPath.active.Scan();
    }
}
