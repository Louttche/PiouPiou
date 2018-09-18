using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform {

    public int Id;
    public Vector2 Position;
    public Vector2 Size;

    public Platform(int id, Vector2 position, Vector2 size)
    {
        Id = id;
        Position = position;
        Size = size;
    }
}
