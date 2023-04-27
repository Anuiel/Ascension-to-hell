using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeZone : MonoBehaviour
{
    public bool explode;

    private void Start()
    {
        explode = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            explode = true;
        }
    }
}
