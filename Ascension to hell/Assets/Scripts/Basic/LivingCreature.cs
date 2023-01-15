using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour
{
    [SerializeField]
    protected float maxHP;
    protected float currentHP;
    // Start is called before the first frame update
    protected void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (currentHP <= 0)
        {
            onDeath();
        }
    }

    virtual public void takeDamage(float dmg)
    {
        currentHP -= dmg;
    }

    public void onDeath()
    {
        // end game when player is dead
        if (gameObject.CompareTag("Player")) {
            Application.Quit();
        }
        Destroy(gameObject);
    }
}
