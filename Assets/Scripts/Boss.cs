using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss {

    public int MaxHealth;
    public Sprite BossSprite;
    public int currentHealth;
    public bool isDead;

    public Boss(int maxHealth, Sprite bossSprite)
    {
        MaxHealth = maxHealth;
        BossSprite = bossSprite;
        currentHealth = MaxHealth;
        isDead = false;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
            isDead = true;
    }
}
