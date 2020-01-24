using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callformanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //EnemyManager.Instance.Initialize();
        BulletManager.Instance.Initialize();
      //  Debug.Log("CallforManager start()");
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyManager.Instance.Refresh();
        BulletManager.Instance.Refresh();
       // Debug.Log("CallforManager Update()");
    }
}
