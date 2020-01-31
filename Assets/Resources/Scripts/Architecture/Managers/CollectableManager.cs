using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager
{

    #region Singleton
    private static CollectableManager instance;
    private CollectableManager() { }
    public static CollectableManager Instance { get { return instance ?? (instance = new CollectableManager()); } }
    #endregion

    public List<GameObject> collectables;

    public bool gotSword;
    public bool gotChip;
    public bool gotBook;
    public GameObject[] BookLocations;
    public GameObject[] ChipLocations;
    public GameObject[] SwordLocations;
    public GameObject plasmaSword;
    public Blade specialBlade;
    public float goTimer;
    public void Initialize()
    {
        collectables = new List<GameObject>();
        GameObject book = Resources.Load<GameObject>("Prefabs/OrnateBook");
        collectables.Add(book);
        //collectables.Add(Resources.Load<GameObject>("Prefabs/OrnateBook"));
        GameObject card = Resources.Load<GameObject>("Prefabs/SD_Card");
        collectables.Add(card);
        plasmaSword = Resources.Load<GameObject>("Prefabs/SwordCustom");
        collectables.Add(plasmaSword);
        specialBlade = plasmaSword.GetComponent<Blade>();
        BookLocations = GameObject.FindGameObjectsWithTag("BookLocations");
        ChipLocations = GameObject.FindGameObjectsWithTag("ChipLocations");
        SwordLocations = GameObject.FindGameObjectsWithTag("SwordLocations");

        
        //GameObject.Instantiate(book, BookLocations[Random.Range(0, BookLocations.Length)].transform.position, Quaternion.identity);
        
        SpawnCollectables();

    }

    public void DestroyObject(GameObject go)
    {
        goTimer += Time.deltaTime;
        if(goTimer > 5)
        {
            GameObject.Destroy(go);
            goTimer = 0;
        }
    }

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {

    }

    public void Refresh()
    {
    }

    public void GotSword()
    {   
        
        gotSword = true;
        
        WeaponManager.Instance.blades.Add(specialBlade);
        specialBlade.Initialize();
        //Call playermanager
        //Call levelmanager
    }

    public void GotChip()
    {
        gotChip = true;
        PlayerManager.Instance.player.Acceleration = 0.1f;
        //Call playermanager to increase speed
    }

    public void GotBook()
    {
        gotBook = true;
        PlayerManager.Instance.player.slowMoActiveTime = 5;

        //Call playermanager to double slowmo time
    }

    public void SpawnCollectables()
    {
        GameObject.Instantiate(collectables[0], BookLocations[Random.Range(0, BookLocations.Length)].transform.position, Quaternion.identity);
        GameObject.Instantiate(collectables[1], ChipLocations[Random.Range(0, ChipLocations.Length)].transform.position, Quaternion.identity);
        GameObject.Instantiate(collectables[2], SwordLocations[Random.Range(0, SwordLocations.Length)].transform.position, Quaternion.identity);  
    }

}

