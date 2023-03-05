using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyNew : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float shootCooldown;
    [SerializeField]
    int burstCount;
    [SerializeField]
    float burstCooldown;
    [SerializeField]
    float maxDistance;

    private Transform playerPosition;
    private RaycastHit2D[] hits = new RaycastHit2D[4];
    private ContactFilter2D filter2D;
    
    private bool canShoot = true;
    private float timeFromLastShot = 0;

    private float burstShotCount = 0;
    private float timeFromLastBurst = 0;

    private void Awake() {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start() {
        timeFromLastShot = shootCooldown;
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
        if (timeFromLastBurst <= burstCooldown) {
            timeFromLastBurst += Time.deltaTime;
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

    private Vector2 DirectionToPlayer() {
        return playerPosition.position - transform.position;
    }

    private void Shoot() {
        if (canShoot) {
            if (timeFromLastShot >= shootCooldown) {
                timeFromLastBurst = burstCooldown;
                timeFromLastShot = 0;
                burstShotCount = 0;
            }
            if (timeFromLastBurst >= burstCooldown && burstShotCount < burstCount) {
                timeFromLastBurst = 0;
                burstShotCount++;
                GameObject bul = Instantiate(
                    bullet, 
                    position: transform.position, 
                    rotation: Quaternion.AngleAxis(-90f, Vector3.forward) * transform.rotation
                );
                bul.GetComponent<EnemyBullet>().SetDirection(DirectionToPlayer());
            }
        }
    }

    private bool PlayerVisiable() {
        Vector2 direction = DirectionToPlayer();
        int objectsHit = Physics2D.Raycast(transform.position, direction, filter2D, hits, maxDistance);
        if (objectsHit == 0 || hits[0].collider == null) {
            return false;
        }
        int hitLayer = (1 << hits[0].collider.gameObject.layer);
        return (objectsHit > 0 && hits[0].collider != null && ((LayerMask.GetMask("Player") & hitLayer) != 0));
    }
}
