using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {

    public string Name;
    public Sprite WeaponSprite;
    public int Damage;

    public Weapon(string name, Sprite weaponSprite, int damage)
    {
        Name = name;
        WeaponSprite = weaponSprite;
        Damage = damage;
    }
}
