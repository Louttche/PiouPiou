using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour {

    public GameObject bullet;
    Boss_Health bh;

	// Use this for initialization
	void Start () {
        InvokeRepeating("ShootB", 1, 0.5f);
	}
    
    void ShootB()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        bh.TakeDamage(10);
        Destroy(gameObject);
    }
}
