using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    public float damage;
    Vector2 direction = new Vector2(1, 0);

    void FixedUpdate()
    {
        gameObject.transform.Translate(speed * Time.fixedDeltaTime * direction);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
