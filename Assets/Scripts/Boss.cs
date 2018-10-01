using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour{

    [SerializeField]
    private int MaxHealth = 0;
    //public Sprite BossSprite;
    public int currentHealth;
    private Component[] Boss_Health_Scripts;

    public void Start()
    {
        Boss_Health_Scripts = gameObject.GetComponentsInChildren<Boss_Health>();
        //BossSprite = bossSprite;

        foreach (Boss_Health bh in Boss_Health_Scripts)
        {
            MaxHealth += bh.maxhealth;
        }
        currentHealth = MaxHealth;
    }

    private void Update()
    {
        int temp = 0;
        foreach (Boss_Health bh in Boss_Health_Scripts)
        {
            temp += bh.currenthealth;
        }
        currentHealth = temp;
    }
}
