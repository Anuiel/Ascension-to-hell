using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BasicWeapon
{
    [SerializeField]
    GameObject bullet;
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
            GameObject bul = Instantiate(bullet, position:transform.position, rotation:transform.rotation);
            Vector2 pos = transform.position;
            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //bul.GetComponent<PistolBullet>().SetDirection(shotDirection);
            Vector2 pelletShotDirection = RandomRotation(shotDirection, spreadAngle);
            bul.transform.Rotate(0, 0, Mathf.Atan2(pelletShotDirection.y, pelletShotDirection.x) * Mathf.Rad2Deg);
        }
        return true;
    }
}
