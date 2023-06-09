using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour
{
    [SerializeField]
    public float maxHP;
    [SerializeField]
    public float currentHP;

    [SerializeField]
    HealthBar healthBar;

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
        if (healthBar) {
            healthBar.UpdateBar();
        }
    }

    virtual public void onDeath()
    {
        // end game when player is dead
        if (gameObject.CompareTag("Player")) {
            Application.Quit();
        }
        Destroy(gameObject);
    }
}
