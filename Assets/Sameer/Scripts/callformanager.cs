using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callformanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.Instance.Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyManager.Instance.Refresh();

    }
}
