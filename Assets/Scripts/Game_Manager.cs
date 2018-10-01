using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    /* Platform order of id's is: 
    
         7      8      9
    
         4      5      6
         
         1      2      3      
    */

    private Sprite[] Sprites;
    public GameObject GameOverPanel;

    [SerializeField]
    private GameObject PlayerPrefab;
    [HideInInspector]
    public GameObject pl;

    public GameObject BossPrefab;
    private Boss currentBoss;
    private GameObject boss;
    [HideInInspector]
    public List<GameObject> bossHealths = new List<GameObject>();

    [SerializeField]
    private GameObject WeaponPrefab;
    public Weapon currentWeapon;

    [SerializeField]
    private GameObject BossWeaponPrefab;
    public Weapon bossWeapon;

    [HideInInspector]
    public Platform currentPlatform;

    [HideInInspector]
    public List<Ray2D> rays = new List<Ray2D>();

    //The platforms (by id) that can only go left/right || up/down
    private static int[] only_Left = new int[] {2 , 5 , 8 , 3 , 6 , 9 };
    private static int[] only_Right = new int[] { 1, 4, 7, 2, 5, 8 };

    private static int[] only_Up = new int[] { 1, 2, 3, 4, 5, 6 };
    private static int[] only_Down = new int[] { 7, 8, 9, 4, 5, 6 };

    private LayerMask PlayerLayer;

    public Grid g;

    public static Game_Manager gm;

    [HideInInspector]
    public bool PlayerDied;

    //MAIN
    private void Awake()
    {
        gm = this;
        PlayerLayer = 1 << LayerMask.NameToLayer("Player");
    }

    void Start () {
        GameOverPanel.SetActive(false);
        PlayerDied = false;
        //Get Sprite Sheet
        Sprites = Resources.LoadAll<Sprite>("Mobile - Pac-Man Championship Edition - Main Sprites iOS");

        Movement.m.direction = Direction.IDLE;

        //Create Player and Boss
        if (g.gridmade == true)
        {           
            InitBoss();
            InitPlayer();
            InitWeapon();
            StartCoroutine(Shoot_Boss());
            g.gridmade = false;
        }

        InvokeRepeating("Shoot_Player", .001f, .2f);
        //InvokeRepeating("Shoot_Boss", 2, 4);

        StartCoroutine(MovePlatform());
    }

    private void FixedUpdate()
    {
        //Debug.LogFormat("State: {0}", Movement.m.direction);

        if (currentBoss.currentHealth <= 0) //********************** Always true
        {
            Debug.Log("YOU WIN");
        }
        else if (PlayerDied == true)
        {
            GameOver();           
        }
    }   

    //SHOOTING
    public void Shoot_Player()
    {
        if (pl != null)
        {
            GameObject wp = Instantiate(WeaponPrefab, pl.transform.position, Quaternion.identity) as GameObject;
            //Make the weapon (bullet) a child of the player
            wp.transform.parent = pl.transform;
        }
    }

    public IEnumerator Shoot_Boss()
    {
        int i = 0;
        foreach (Ray2D r in rays)
        {
            RaycastHit2D hit = Physics2D.Raycast(r.origin, r.direction, 10, PlayerLayer);
            if (hit.collider != null)
            {
                GameObject wp = Instantiate(BossWeaponPrefab, r.origin, Quaternion.identity) as GameObject;
                wp.transform.parent = boss.transform;
            }
            i++;
        }       

        yield return new WaitForSeconds(2);
        StartCoroutine(Shoot_Boss());
    }

    //MOVEMENT
    public IEnumerator MovePlatform()
    {
        if (Movement.m.direction != Direction.IDLE)
        {
            Move();
            Movement.m.direction = Direction.IDLE;
        }

        //Have a delay to move between platforms smoothly
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MovePlatform());
    }

    public Platform FindXPlatformToMoveTo(Direction d)
    {
        if (d == Direction.LEFT)
        {
            foreach (int i in only_Left)
            {
                if (currentPlatform.Id == i)
                {
                    //if platform is not gone then
                    return g.FindPlatformById(i - 1);
                }
            }
        }
        else
        {
            foreach (int i in only_Right)
            {
                if (currentPlatform.Id == i)
                {
                    //if platform is not gone then
                    return g.FindPlatformById(i + 1);
                }
            }
        }

        return null;
    }

    public Platform FindYPlatformToMoveTo(Direction d)
    {
        if (d == Direction.UP)
        {
            foreach (int i in only_Up)
            {
                if (currentPlatform.Id == i)
                {
                    //if platform is not gone then
                    return g.FindPlatformById(i + 3);
                }
            }
        }
        else
        {
            foreach (int i in only_Down)
            {
                if (currentPlatform.Id == i)
                {
                    //if platform is not gone then
                    return g.FindPlatformById(i - 3);
                }
            }
        }

        return null;
    }

    public void Move()
    {
        Platform p;
        if ((Movement.m.direction == Direction.LEFT) || (Movement.m.direction == Direction.RIGHT))
            p = FindXPlatformToMoveTo(Movement.m.direction);
        else
            p = FindYPlatformToMoveTo(Movement.m.direction);

        Movement.m.direction = Direction.IDLE;
        
        if (p != null)
        {
            currentPlatform = p;
            PositionPlayerOnPlatform(currentPlatform);
        }
    }

    public void PositionPlayerOnPlatform(Platform p)
    {
        pl.transform.position = new Vector2(p.Position.x, p.Position.y + (p.Size.y * 2)); //0.6 -> wanted y increase from p's position
    }

    //INITIALIZATION
    public void InitWeapon()
    {
        //if x weapon equipped then currentWeapon = x
        currentWeapon = new Weapon("Normal Weapon", GetSpriteByName("Mobile - Pac-Man Championship Edition - Main Sprites iOS_64"), 10);
        //Set the weapon object's sprite
        WeaponPrefab.GetComponent<SpriteRenderer>().sprite = currentWeapon.WeaponSprite;
    }

    public void InitPlayer()
    {              
        pl = Instantiate(PlayerPrefab);
        pl.transform.parent = g.transform; //Make player a child of the grid
        currentPlatform = g.FindPlatformById(5);
        PositionPlayerOnPlatform(currentPlatform);
    }

    public void InitBoss()
    {
        //Set the boss object's sprite
        //BossPrefab.GetComponent<SpriteRenderer>().sprite = currentBoss.BossSprite;

        //Create it in the scene
        boss = Instantiate(BossPrefab);
        currentBoss = boss.GetComponent<Boss>();
    }

    //OTHER
    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void GameOver()
    {
        CancelInvoke();
        StopAllCoroutines();
        GameOverPanel.SetActive(true);
        PlayerDied = false;
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