using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour
{
    [SerializeField]
    protected float maxHP;
    protected float currentHP;
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

    public void takeDamage(float dmg)
    {
        currentHP -= dmg;
    }

    public virtual void onDeath()
    {
        Destroy(gameObject);
    }
}
