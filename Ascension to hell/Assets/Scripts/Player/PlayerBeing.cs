using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeing : LivingCreature
{
    List<BasicWeapon> equippedWeapon = new(2);
    int weaponIdx;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WeaponDrop()
    {

    }

    void WeaponPick()
    {

    }
}
