using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : BasicWeapon
{
    [SerializeField]
    float damage;
    [SerializeField]
    float shotCooldown;
    [SerializeField]
    LineRenderer lineRenderer; 
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        Starting();
        canShoot = true;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot(Vector2 point)
    {
        if (!canShoot) {
            return;
        }

        base.Shoot(point);
        Vector2 gunPos = transform.position;
        Vector2 direction = shotPoint - gunPos;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction);
        StartCoroutine(RenderShootTrail(gunPos, hitInfo.point));

        if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Debug.Log(hitInfo.collider);
            hitInfo.collider.gameObject.GetComponent<LivingCreature>().takeDamage(damage);
        }
        StartCoroutine(ShootCooldown());
    }

    private IEnumerator RenderShootTrail (Vector2 from, Vector2 to) {
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }

    private IEnumerator ShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }
}
