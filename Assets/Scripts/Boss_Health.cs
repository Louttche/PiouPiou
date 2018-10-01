using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health : MonoBehaviour {

    public int maxhealth = 250;
    public int currenthealth;
    private Vector2 maxScale;
    private Vector2 currentScale;
    [HideInInspector]
    public Vector2 position;

    private void Start()
    {
        currenthealth = maxhealth;
        maxScale = gameObject.transform.localScale;
        currentScale = maxScale;
        position = transform.position;
    }

    public void TakeDamage(int dmg)
    {
        currenthealth -= dmg;
        currentScale.x -= Map(dmg, 0, maxhealth, 0, maxScale.x);

        gameObject.transform.localScale = currentScale;

        if (currenthealth <= 0)
            Destroy(gameObject);
    }
    
    //Visual "Healthbar" accurate change when damage taken
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}