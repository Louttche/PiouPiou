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

    [SerializeField]
    private GameObject PlayerPrefab;
    [HideInInspector]
    public GameObject player;

    [SerializeField]
    private GameObject BossPrefab;
    public Boss currentBoss;

    [SerializeField]
    private GameObject WeaponPrefab;
    public Weapon currentWeapon;

    public Grid g;

    public static Game_Manager gm;

    private void Awake()
    {
        gm = this;
    }

    void Start () {
        //Get Sprite Sheet
        Sprites = Resources.LoadAll<Sprite>("Mobile - Pac-Man Championship Edition - Main Sprites iOS");
        
        //Create Player and Boss
        if (g.gridmade == true)
        {
            InitBoss();
            InitPlayer();
            InitWeapon();
            g.gridmade = false;
        }
        InvokeRepeating("Shoot", .001f, .2f);
    }

    private void Update()
    {
        Debug.LogFormat("Direction: {0}", Movement.m.direction);
        //If Boss is dead
        if (currentBoss.isDead)
        {
            Debug.Log("Boss is dead");
            //CancelInvoke("Shoot");
            Time.timeScale = 0; //Pause
        }
    }

    public void Shoot()
    {
        GameObject p = Instantiate(WeaponPrefab, player.transform.position, Quaternion.identity) as GameObject;
        //Make the weapon (bullet) a child of the player
        p.transform.parent = player.transform;
    }

    public void InitWeapon()
    {
        //if x weapon equipped then currentWeapon = x
        currentWeapon = new Weapon("Normal Weapon", GetSpriteByName("Mobile - Pac-Man Championship Edition - Main Sprites iOS_64"), 10);
        //Set the weapon object's sprite
        WeaponPrefab.GetComponent<SpriteRenderer>().sprite = currentWeapon.WeaponSprite;
    }

    public void InitPlayer()
    {
        player = Instantiate(PlayerPrefab);
        Platform p = g.FindPlatformById(7);
        player.transform.parent = g.transform; //Make player a child of the grid
        player.transform.position = new Vector2(p.Position.x, p.Position.y + (p.Size.y * 2)); //0.6 -> wanted y increase from p's position
    }

    public void InitBoss()
    {
        GameObject bossPosition = transform.Find("BossPosition").gameObject;
        currentBoss = new Boss(10000, Resources.Load<Sprite>("Boss")); //Boss consists of (Max health, sprite)
        //Set the boss object's sprite
        BossPrefab.GetComponent<SpriteRenderer>().sprite = currentBoss.BossSprite;
        //Create it in the scene
        GameObject boss = Instantiate(BossPrefab);
        boss.transform.position = bossPosition.transform.position;
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