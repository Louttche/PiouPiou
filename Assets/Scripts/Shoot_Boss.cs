using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Boss : MonoBehaviour {

    [SerializeField]
    private float speed;

    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x - speed * Time.deltaTime, position.y);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));

        if (transform.position.x < min.x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Hit player");
            Destroy(col.gameObject);
            Game_Manager.gm.PlayerDied = true;
            Destroy(gameObject);
        }
    }
}
