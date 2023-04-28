using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BasicWeapon
{
    [SerializeField]
    int pelletsCount;
    // Start is called before the first frame update
    void Start()
    {
        Starting();
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
        for (int i = 0; i < pelletsCount; ++i) {
            GameObject bullet = base.SpawnBullet();
            Vector2 pos = transform.position;
            Vector2 pelletShotDirection = RandomRotation(shotDirection, spreadAngle);
            bullet.transform.Rotate(0, 0, Mathf.Atan2(pelletShotDirection.y, pelletShotDirection.x) * Mathf.Rad2Deg);
        }
        return true;
    }
}
