using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private int rows;
    [SerializeField]
    private int cols;
    [SerializeField]
    private Vector2 gridSize;
    [SerializeField]
    private Vector2 gridOffset;

    [SerializeField]
    private Sprite platformSprite;
    private Vector2 platformSize;
    private Vector2 platformScale;
    private int platformID = 1;

    [HideInInspector]
    public Vector2 OriginalSpriteSize;
    public float Sizex = (float)1.5; //Changeable value to fit platform's sprite wanted appearance x scale
    public float Sizey = 4; //Changeable value to fit platform's sprite wanted appearance y scale

    public bool gridmade;

    private List<Platform> platforms = new List<Platform>();

    void Start()
    {
        gridmade = false;
        OriginalSpriteSize = platformSprite.bounds.size;
        InitPlatforms(); //Initialize all platforms
    }

    public Platform FindPlatformById(int id)
    {
        foreach (Platform p in platforms)
        {
            if (id == p.Id)
            {
                return p;
            }
        }
        return null;
    }

    void InitPlatforms()
    {
        //creates an empty object and adds a sprite renderer component -> set the sprite to platformSprite
        GameObject platformObject = new GameObject();       
        platformObject.AddComponent<SpriteRenderer>().sprite = platformSprite;
        platformSize = OriginalSpriteSize;

        
        //get the new platform size -> adjust the size of the platforms to fit the size of the grid
        Vector2 newPlatformSize = new Vector2(gridSize.x / (float)cols , gridSize.y / (float)rows);

        //Get the scales so you can scale the platforms and change their size to fit the grid (anything after 'platformSize.x/y' depends on how you want the platform to appear in the grid)
        platformScale.x = newPlatformSize.x / platformSize.x / Sizex;
        platformScale.y = newPlatformSize.y / platformSize.y / Sizey;

        platformSize = newPlatformSize;

        platformObject.transform.localScale = new Vector2(platformScale.x, platformScale.y);
        
        gridOffset.x = -(gridSize.x / 2) + platformSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + platformSize.y / 2;
        

        //fill the grid with platforms by using Instantiate
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                //add the platform size so that no two platforms will have the same x and y position
                Vector2 pos = new Vector2(col * platformSize.x + gridOffset.x + transform.position.x, row * platformSize.y + gridOffset.y + transform.position.y);

                //instantiate the game object, at position pos, with rotation set to identity
                GameObject cO = Instantiate(platformObject, pos, Quaternion.identity) as GameObject;

                //set the parent of the platform to GRID so you can move the cells together with the grid;
                cO.transform.parent = transform;
                cO.name = "Platform " + platformID;

                //Add the platform in the list with an id
                platforms.Add(new Platform(platformID++, pos, OriginalSpriteSize));
            }
        }

        //destroy the object used to instantiate the cells
        Destroy(platformObject);
        gridmade = true;

        //Display info on all platforms in list
        /*foreach (Platform p in platforms)
        {
            Debug.LogFormat("ID: {0}, Position: {1}, Size: {2}", p.Id, p.Position, p.Size);
        }*/
    }

    //so you can see the width and height of the grid on editor
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}
