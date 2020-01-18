﻿using System.Collections;
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

    public void Initialize()
    {
        GameObject book = Resources.Load<GameObject>("Prefabs/OrnateBook");
        collectables.Add(book);
        GameObject card = Resources.Load<GameObject>("Prefabs/SD_Card");
        collectables.Add(card);
        GameObject plasmaSword = Resources.Load<GameObject>("Prefabs/SwordCustom");
        collectables.Add(plasmaSword);

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
        //Call playermanager
        //Call levelmanager
    }

    public void GotCard()
    {
        //Call playermanager to increase speed
    }

    public void GotBook()
    {
        //Call playermanager to double slowmo time
    }

    public void SpawnCollectables()
    {
        //Check if trigger and then spawn them at possible spawn spots
    }

}

