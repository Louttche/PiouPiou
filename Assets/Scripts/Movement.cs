using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   Joystick direction value:

          1

    -1         1
        
         -1
     */

public enum Direction
{
    UP,
    LEFT,
    RIGHT,
    DOWN
}

public class Movement : MonoBehaviour {

    protected Joystick joystick;
    [HideInInspector]
    public Direction direction;
    public static Movement m;

    private void Awake()
    {
        m = this;
    }

    // Use this for initialization
    void Start () {
        joystick = FindObjectOfType<Joystick>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(joystick.Direction.x) > Mathf.Abs(joystick.Direction.y))
        {
            if (joystick.Horizontal == -1)
                direction = Direction.LEFT;
            else if (joystick.Horizontal == 1)
                direction = Direction.RIGHT;
        }
        else
        {
            if (joystick.Vertical == 1)
                direction = Direction.UP;
            else if (joystick.Horizontal == -1)
                direction = Direction.DOWN;
        }
    }
}
