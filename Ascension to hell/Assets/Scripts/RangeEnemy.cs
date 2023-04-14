using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float shootSpeed;
    [SerializeField]
    float maxDistance;

    private RaycastHit2D[] hits = new RaycastHit2D[4];
    private ContactFilter2D filter2D;
    
    private bool canShoot = true;
    private float shootCooldown;
    private float timeFromLastShot = 0;

    void Start() {
        shootCooldown = 1 / shootSpeed;

        filter2D = new ContactFilter2D {
            useTriggers = false,
            useLayerMask = true
        };
        filter2D.SetLayerMask(LayerMask.GetMask("Player") | LayerMask.GetMask("Wall"));
    }

    void Update() {
        if (timeFromLastShot <= shootCooldown) {
            timeFromLastShot += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = DirectionToPlayer();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (canShoot && PlayerVisiable()) {
            Shoot();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.transform.position);
    }

    private Vector2 DirectionToPlayer() {
        return player.transform.position - transform.position;
    }

    private void Shoot() {
        if (canShoot && timeFromLastShot >= shootCooldown) {
            GameObject bul = Instantiate(
                bullet, 
                position: transform.position, 
                rotation: Quaternion.AngleAxis(-90f, Vector3.forward) * transform.rotation
            );
            bul.GetComponent<EnemyBullet>().SetDirection(DirectionToPlayer());
            timeFromLastShot = 0;
        }
    }

    private bool PlayerVisiable() {
        Vector2 direction = DirectionToPlayer();
        int objectsHit = Physics2D.Raycast(transform.position, direction, filter2D, hits, maxDistance);
        return (objectsHit > 0 && hits[0].collider != null && hits[0].collider.gameObject == player);
    }
}
