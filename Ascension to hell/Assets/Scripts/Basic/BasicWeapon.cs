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

    [SerializeField]
    protected GameObject bullet;

    protected float damageMultiplier;

    protected int currentShots;
    protected Camera cm;
    protected Vector2 shotDirection;


    protected bool canShoot = true;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        Starting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual GameObject SpawnBullet() {
        GameObject bul = Instantiate(bullet, position:transform.position, Quaternion.identity);
        bul.GetComponent<BasicBullet>().damage *= damageMultiplier;
        bul.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        return bul;
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

    public void ApplyBuff(Buff buff) {
        damageMultiplier = buff.ApplyPower(damageMultiplier);
    }

    protected virtual IEnumerator Reload() {
        canShoot = false;
        yield return new WaitForSeconds(1f / shotsPerSeconds);
        canShoot = true;
    }

    protected virtual void Starting()
    {
        currentShots = maxShots;
        damageMultiplier = 1;
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    protected virtual Vector2 GetMousePosition(Vector2 point)
    {
        Vector2 MousePos = cm.ScreenToWorldPoint(point);
        return MousePos;
    }

    protected virtual Vector2 GetShootingDirection(Vector2 point) {
        Vector2 direction = GetMousePosition(point) - new Vector2(transform.position.x, transform.position.y);
        return RandomRotation(direction, 0);
    }

    protected Vector2 RandomRotation(Vector2 vector, float angle) {
        float newAngle = Mathf.Atan2(vector.y, vector.x) + Random.Range(-angle, angle) * Mathf.Deg2Rad / 2;
        return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }
}
