using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    float dmg = 1f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBeing>().takeDamage(dmg);
        }
    }
}
