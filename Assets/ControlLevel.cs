using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLevel : MonoBehaviour
{
    public int numEnemiesKilled;
    public bool FoundSecretExit, SaiSaysYes, FoundCollectible, UsedCollectible, KilledBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            numEnemiesKilled++;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            numEnemiesKilled--;

        if (Input.GetKeyDown(KeyCode.X))         // Secret Exit
            FoundSecretExit = !FoundSecretExit;
        if (Input.GetKeyDown(KeyCode.S))         // Secret Exit
            SaiSaysYes = !SaiSaysYes;
        if (Input.GetKeyDown(KeyCode.C))         // Secret Exit
            FoundCollectible = !FoundCollectible;
        if (Input.GetKeyDown(KeyCode.E))         // Secret Exit
            UsedCollectible = !UsedCollectible;
        if (Input.GetKeyDown(KeyCode.F))         // Secret Exit
            KilledBoss = !KilledBoss;
    }
}
