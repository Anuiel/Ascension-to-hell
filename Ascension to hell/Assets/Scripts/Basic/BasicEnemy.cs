using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : LivingCreature
{
    [SerializeField] private float damage;
    public int price = 1;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerBeing>().takeDamage(damage);
        }
    }
}
