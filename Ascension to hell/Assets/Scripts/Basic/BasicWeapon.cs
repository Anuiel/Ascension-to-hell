using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField]
    protected int maxShots;
    [SerializeField]
    protected float shotsPerSeconds;
    [SerializeField]
    protected float spreadAngle;
    protected int currentShots;
    protected Camera cm;
    protected Vector2 shotDirection;

    protected bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        Starting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool Shoot(Vector2 point)
    {
        if (!canShoot) {
            return false;
        }
        shotDirection = GetShootingDirection(point);
        currentShots -= 1;
        StartCoroutine(Reload());
        return true;
    }

    private IEnumerator Reload() {
        canShoot = false;
        yield return new WaitForSeconds(1f / shotsPerSeconds);
        canShoot = true;
    }

    protected virtual void Starting()
    {
        currentShots = maxShots;
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    protected virtual Vector2 GetMousePosition(Vector2 point)
    {
        Vector2 MousePos = cm.ScreenToWorldPoint(point);
        return MousePos;
    }

    protected virtual Vector2 GetShootingDirection(Vector2 point) {
        Vector2 direction = GetMousePosition(point) - new Vector2(transform.position.x, transform.position.y);
        return RandomRotation(direction, spreadAngle);
    }

    private Vector2 RandomRotation(Vector2 vector, float angle) {
        float newAngle = Mathf.Atan2(vector.y, vector.x) + Random.Range(-angle, angle) * Mathf.Deg2Rad / 2;
        return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }
}
