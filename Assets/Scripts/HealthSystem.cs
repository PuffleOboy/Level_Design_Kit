using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int maxHealth;
    private int health;

    public HealthSystem(int health)
    {
        this.maxHealth = health;
        this.health = health;
    }

    public int GetMaxHealth() => maxHealth;

    public int GetHealth()
    {
        return health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
    }


}