using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : BasicWeapon
{
    [SerializeField]
    float damage;
    [SerializeField]
    LineRenderer lineRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        Starting();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Shoot(Vector2 point)
    {
        if (!base.Shoot(point)) {
            return false;
        }
        Vector2 gunPos = transform.position;
        Vector2 direction = shotDirection;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction);
        StartCoroutine(RenderShootTrail(gunPos, hitInfo.point));

        if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Debug.Log(hitInfo.collider);
            hitInfo.collider.gameObject.GetComponent<LivingCreature>().takeDamage(damage);
        }
        return true;
    }

    private IEnumerator RenderShootTrail (Vector2 from, Vector2 to) {
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }
}
