using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{   
    [SerializeField]
    GameObject player;
    [SerializeField]
    float speed;
    [SerializeField]
    float attack_range;

    [SerializeField]
    Rigidbody2D rb;

    private float distanse;

    void Awake()
    {
        // rb = gameObject.AddComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    // void Update()
    // {  
    //     distanse = Vector2.Distance(transform.position, player.transform.position);
    //     Vector2 direction = player.transform.position - transform.position;
    //     direction = direction.normalized;

    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

    //     transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    //     transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    // }
    void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction = direction.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
