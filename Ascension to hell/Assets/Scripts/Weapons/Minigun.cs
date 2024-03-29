using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : BasicWeapon
{
    [SerializeField]
    float shotPower;
    [SerializeField]
    float shootRecoil;
    [SerializeField]
    float coolingTime;
    [SerializeField]
    float heatingSpeed;
    [SerializeField]
    float coolingSpeed;
    [SerializeField]
    float maxShotSpeed;

    private float timeFromLastShot;
    private float trueShotsPerSeconds;
    private Vector2 lastShotDirection;
    // Start is called before the first frame update
    void Start()
    {
        Starting();
        timeFromLastShot = 0;
        trueShotsPerSeconds = shotsPerSeconds;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeFromLastShot += Time.deltaTime;
        if (timeFromLastShot < shootRecoil) {
            player.GetComponent<Rigidbody2D>().AddForce(lastShotDirection * -shotPower);
        }

        if (timeFromLastShot > coolingTime) {
            if (shotsPerSeconds > trueShotsPerSeconds) {
                shotsPerSeconds -= Time.deltaTime * coolingSpeed;
            }
        } else {
            if (shotsPerSeconds < maxShotSpeed) {
                shotsPerSeconds += Time.deltaTime * heatingSpeed;
            }
        }
    }

    public override bool Shoot(Vector2 point)
    {
        if (!base.Shoot(point)) {
            return false;
        }
        GameObject bullet = base.SpawnBullet();
        
        Vector2 pos = transform.position;
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        timeFromLastShot = 0;    
        lastShotDirection = new(shotDirection.x, shotDirection.y);    
        //bul.GetComponent<PistolBullet>().SetDirection(shotDirection);
        bullet.transform.Rotate(0, 0, Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg);
        return true;
    }
}
