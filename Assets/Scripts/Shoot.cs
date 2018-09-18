using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;

	void Start () {
        rb.velocity = transform.right * speed;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}