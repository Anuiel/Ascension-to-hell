using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : BasicWeapon
{
    [SerializeField]
    float shotPower;
    [SerializeField]
    float shootRecoil;

    private float timeFromLastShot;

    private Vector2 lastShotDirection;
    // Start is called before the first frame update
    void Start()
    {
        Starting();
        timeFromLastShot = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeFromLastShot < shootRecoil) {
            player.GetComponent<Rigidbody2D>().AddForce(lastShotDirection * -shotPower);
            timeFromLastShot += Time.deltaTime;
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
