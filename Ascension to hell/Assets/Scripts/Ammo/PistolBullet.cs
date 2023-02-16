using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float dmg;
    [SerializeField]
    int pierce;
    [SerializeField]
    int ricochet;

    int flag = 1;

    Vector2 direction = new Vector2(1, 0);
    // Start is called before the first frame update

    public void setVelocity(Vector2 dir)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir * speed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        //gameObject.transform.Translate(speed * Time.fixedDeltaTime * direction);
        //gameObject.GetComponent<Rigidbody2D>().
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<LivingCreature>().takeDamage(dmg);
            pierce -= 1;
            if (pierce < 0)
                Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ricochet -= 1;
            if (ricochet < 0)
                Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            flag -= 1;
            if (flag < 0)
                Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<LivingCreature>().takeDamage(dmg);
            pierce -= 1;
            if (pierce < 0)
                Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ricochet -= 1;
            if (ricochet < 0)
                Destroy(gameObject);

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            flag -= 1;
            if (flag < 0)
                Destroy(gameObject);
        }
    }
}
