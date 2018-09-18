using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    /* Platform order of id's is: 
     
         7      8      9
    
         4      5      6
         
         1      2      3        
    */

    private Sprite[] Sprites;
    public GameObject Player;
    private GameObject player;
    public GameObject PlayerWeapon;
    public Weapon currentWeapon;
    public Grid g;

    void Start () {
        //Get Sprite Sheet
        Sprites = Resources.LoadAll<Sprite>("Mobile - Pac-Man Championship Edition - Main Sprites iOS");
        
        //Create Player
        if (g.gridmade == true)
        {
            player = Instantiate(Player);
            Platform p = g.FindPlatformById(7);
            player.transform.parent = g.transform; //Make player a child of the grid
            player.transform.position = new Vector2(p.Position.x, p.Position.y + (p.Size.y * 2)); //0.6 -> wanted y increase from p's position
            InitWeapon();
            g.gridmade = false;
        }
        InvokeRepeating("Shoot", .001f, .5f);
    }

    public void Shoot()
    {
        GameObject p = Instantiate(PlayerWeapon, player.transform.position, Quaternion.identity) as GameObject;
        //Make the weapon (bullet) a child of the player
        p.transform.parent = player.transform;
    }

    public void InitWeapon()
    {
        //if x weapon equipped then currentWeapon = x
        currentWeapon = new Weapon("Normal Weapon", GetSpriteByName("Mobile - Pac-Man Championship Edition - Main Sprites iOS_64"), 10);
        //Set the weapon object's sprite
        PlayerWeapon.GetComponent<SpriteRenderer>().sprite = currentWeapon.WeaponSprite;
    }

    public Sprite GetSpriteByName(string name)
    {
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (Sprites[i].name == name)
                return Sprites[i];
        }
        return null;
    }
}