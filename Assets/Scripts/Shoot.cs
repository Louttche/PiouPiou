using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    public Boss boss;

	void Start () {
        boss = Game_Manager.gm.currentBoss;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (boss.MaxHealth > 0)
        {
            boss.TakeDamage(Game_Manager.gm.currentWeapon.Damage);
            Debug.LogFormat("Boss' current health is: {0}", boss.currentHealth);
        }
        Destroy(gameObject);
    }
}