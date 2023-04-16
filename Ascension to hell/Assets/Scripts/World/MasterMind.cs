using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMind : MonoBehaviour
{
    [SerializeField]
    FieldManager fm;

    [SerializeField]
    EnemyManager em;

    bool flag;

    [SerializeField]
    int numberOfBlocks;

    // Start is called before the first frame update
    void Start()
    {
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            fm.generateField(numberOfBlocks);
        }
        flag = false;
    }
}
