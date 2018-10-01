using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Player : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Boss_Health bh;

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x + speed * Time.deltaTime, position.y);
        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.x > max.x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player")
        {
            if (bh == null)
                bh = col.GetComponent<Boss_Health>();

            bh.TakeDamage(Game_Manager.gm.currentWeapon.Damage);
            Destroy(gameObject);
        }
    }
}