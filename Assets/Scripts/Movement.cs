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
    DOWN,
    IDLE
}

public class Movement : MonoBehaviour {

    protected Joystick joystick;
    [HideInInspector]
    public static Movement m;
    [HideInInspector]
    public Direction direction = Direction.IDLE;

    private void Awake()
    {
        m = this;
    }

    // Use this for initialization
    void Start () {
        joystick = FindObjectOfType<Joystick>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //direction = GetDirectionKeys();
        direction = GetDirectionJoystick();
    }

    public Direction GetDirectionKeys()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            return Direction.UP;
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            return Direction.DOWN;
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            return Direction.RIGHT;
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            return Direction.LEFT;

        return Direction.IDLE;
    }

    public Direction GetDirectionJoystick()
    {
        if (Mathf.Abs(joystick.Direction.x) > Mathf.Abs(joystick.Direction.y))
        {
            if (joystick.Horizontal < 0)
            {
                //Debug.Log("Left");
                return Direction.LEFT;               
            }
            else if (joystick.Horizontal > 0)
            {
                //Debug.Log("Right");
                return Direction.RIGHT;
            }
        }
        else
        {
            if (joystick.Vertical > 0)
            {
                //Debug.Log("Up");
                return Direction.UP;
            }
            else if (joystick.Horizontal < 0)
            {
                //Debug.Log("Down");
                return Direction.DOWN;
            }
        }

        return Direction.IDLE;
    }


}
