using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLevel : MonoBehaviour
{
    public int numEnemiesKilled;
    public bool FoundSecretExit, SaiSaysYes, FoundCollectible, UsedCollectible, KilledBoss;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            numEnemiesKilled++;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            numEnemiesKilled--;

        if (Input.GetKeyDown(KeyCode.X))         // Secret Exit : Transform
            FoundSecretExit = !FoundSecretExit;
        if (Input.GetKeyDown(KeyCode.S))         // Sai controls collectible state : Conditions for collectible to be active
            SaiSaysYes = !SaiSaysYes;
        if (Input.GetKeyDown(KeyCode.C))         // Collectible has been grabbed
            FoundCollectible = !FoundCollectible;
        if (Input.GetKeyDown(KeyCode.E))         // Unsure how to use. Added this here to skip the grind after getting collectible
            UsedCollectible = !UsedCollectible;
        if (Input.GetKeyDown(KeyCode.F))         // Fucked the boss' shit up
            KilledBoss = !KilledBoss;


        anim.SetInteger("EnemiesKilled", numEnemiesKilled);
        
        if (FoundSecretExit)
            anim.SetTrigger("FoundSecretExit");
        if (SaiSaysYes)
            anim.SetTrigger("SaiSaysYes");
        if (FoundCollectible)
            anim.SetTrigger("FoundCollectible");
        if (UsedCollectible)
            anim.SetTrigger("UseCollectible");
        if (KilledBoss)
            anim.SetTrigger("KilledBoss");
    }
}
