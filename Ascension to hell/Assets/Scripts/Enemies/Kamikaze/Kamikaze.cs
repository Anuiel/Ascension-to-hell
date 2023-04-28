using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Kamikaze : BasicEnemy
{
    [SerializeField]
    KamikazeZone zone;

    [SerializeField]
    float dmg = 5f;

    CircleCollider2D damageZone;

    [SerializeField]
    BoxCollider2D player;

    [SerializeField]
    AIPath path;

    bool explode;

    [SerializeField]
    float explodeDelay = 2f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        explode = false;
        damageZone = zone.GetComponent<CircleCollider2D>();
        path = GetComponent<AIPath>();
        player = GameObject.Find("Player").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        explode = zone.explode;
        if (explode == true)
        {
            // path.canMove = false;
            Invoke(nameof(bomb), explodeDelay);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = DirectionToPlayer();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private Vector2 DirectionToPlayer() {
        return player.transform.position - transform.position;
    }

    void bomb()
    {
        if (damageZone.IsTouching(player))
        {
            player.GetComponent<PlayerBeing>().takeDamage(dmg);
        }
        Destroy(gameObject);
    }
}
