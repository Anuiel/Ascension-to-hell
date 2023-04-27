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
    int enemies;


    void Update()
    {
        checkForEnemies();
    }

    bool checkForEnemies()
    {
        if (GameObject.FindObjectOfType<BasicEnemy>() == null)
        {
            nextStep();
            return true;
        }
        return false;
    }

    void nextStep()
    {
        fm.generateFieldMarine(numberOfBlocks);
        em.generateWave(enemies);
    }
}
