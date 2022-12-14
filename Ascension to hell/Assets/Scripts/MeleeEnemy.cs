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
    Rigidbody2D rb;

    private float distanse;

    void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction = direction.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
