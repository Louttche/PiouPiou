using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public int Health = 10000;

    public void TakeDamage(int dmg)
    {
        Health -= dmg;

        //if health is <= 0 then die
    }
}
