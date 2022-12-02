using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour
{
    [SerializeField]
    int maxHP;
    int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            onDeath();
        }
    }

    public void takeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    public virtual void onDeath()
    {
        Destroy(gameObject);
    }
}
