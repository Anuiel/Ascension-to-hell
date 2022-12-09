using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeing : LivingCreature
{
    [SerializeField]
    List<BasicWeapon> equippedWeapon = new(2) { null, null };
    int weaponIdx;
    
    // Start is called before the first frame update
    void Start()
    {
        weaponIdx = 0;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (equippedWeapon[i] != null)
            {
                equippedWeapon[i].transform.position = transform.position;
            }
        }
    }

    void WeaponDrop(int idx)
    {
        BasicWeapon removed = equippedWeapon[idx];
        removed.GetComponent<BoxCollider2D>().enabled = true;
        removed.GetComponent<SpriteRenderer>().enabled = true;
        equippedWeapon[idx] = null;
    }

    public void WeaponPick(BasicWeapon gun)
    {
        if (equippedWeapon[weaponIdx] != null)
        {
            WeaponDrop(weaponIdx);
        }
        gun.GetComponent<BoxCollider2D>().enabled = false;
        gun.GetComponent<SpriteRenderer>().enabled = false;
        equippedWeapon[weaponIdx] = gun;
    }

    public void ShootGun(Vector2 point)
    {
        if (equippedWeapon[weaponIdx] != null)
        {
            equippedWeapon[weaponIdx].Shoot(point);
        }
    }
}
