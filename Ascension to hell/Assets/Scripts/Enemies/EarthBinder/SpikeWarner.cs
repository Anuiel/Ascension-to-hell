using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWarner : MonoBehaviour
{
    [SerializeField]
    float life = 3f;
    float curLife;

    private void Start()
    {
        curLife = life;
    }

    private void Update()
    {
        if (curLife < 0)
            Destroy(gameObject);
        curLife -= Time.deltaTime;
    }
}
