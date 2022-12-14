using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BasicWeapon
{
    [SerializeField]
    GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        Starting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot(Vector2 point)
    {
        base.Shoot(point);
        GameObject bul = Instantiate(bullet, position:transform.position, rotation:transform.rotation);
        Vector2 pos = transform.position;
        bul.GetComponent<PistolBullet>().SetDirection(shotPoint - pos);
    }
}
