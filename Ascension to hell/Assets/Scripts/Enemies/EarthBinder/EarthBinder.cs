using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBinder : BasicEnemy
{
    [SerializeField]
    GameObject warner;

    [SerializeField]
    GameObject spike;

    [SerializeField]
    float castInterval = 5;

    float curInterval;

    [SerializeField]
    float castDelay = 2;

    float curDelay;

    GameObject player;

    List<Vector2> damageZone = new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1),
     new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1), new Vector2(2, 0), new Vector2(-2, 0), new Vector2(0, 2), new Vector2(0, -2)};


    Vector2 playerPos;

    new private void Start()
    {
        base.Start();
        curInterval = castInterval;
        curDelay = castDelay;
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
    }

    new private void Update()
    {
        base.Update();
        if (curInterval > 0)
        {
            curInterval -= Time.deltaTime;
        }
        else
        {
            cast();
            curInterval = castInterval;
        }
    }

    void cast()
    {
        playerPos = player.transform.position;
        for (int i = 0; i < damageZone.Count; i++)
        {
            Instantiate(warner, new Vector3((playerPos + damageZone[i]).x, (playerPos + damageZone[i]).y, 0), warner.transform.rotation);
        }
        Invoke(nameof(castSpikes), castDelay);
    }

    void castSpikes()
    {
        for (int i = 0; i < damageZone.Count; i++)
        {
            Instantiate(spike, new Vector3((playerPos + damageZone[i]).x, (playerPos + damageZone[i]).y, 0), spike.transform.rotation);
        }
    }
}
