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
    Rigidbody2D rb;

    private bool canShoot = true;


    void FixedUpdate()
    {
        Vector2 direction = DirectionToPlayer();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        Debug.Log(canShoot);
        Debug.Log(PlayerVisiable());

        if (canShoot && PlayerVisiable()) {
            Debug.Log("Shoot");
            Shoot();
        }

        Debug.Log(DirectionToPlayer().normalized);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, player.transform.position);
    }

    private Vector2 DirectionToPlayer() {
        return player.transform.position - transform.position;
    }

    private void Shoot() {
        if (canShoot) {
            GameObject bul = Instantiate(
                bullet, 
                position: transform.position, 
                rotation: Quaternion.AngleAxis(-90f, Vector3.forward) * transform.rotation
            );
            bul.GetComponent<EnemyBullet>().SetDirection(DirectionToPlayer());
            StartCoroutine(ShootCooldown());
        }
    }

    private bool PlayerVisiable() {
        Vector2 direction = DirectionToPlayer();
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction);
        return hitInfo.collider.gameObject == player;
    }

    private IEnumerator ShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(1f / shootSpeed);
        canShoot = true;
   }
}
