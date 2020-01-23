using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player")) //mixamorig:RightHand 
        {
            gameObject.SetActive(false);
            if (gameObject.name.Equals("SwordCustom"))
            {
                CollectableManager.Instance.GotSword();
            }
            else if (gameObject.name.Equals("SD_Card"))
            {
                CollectableManager.Instance.GotChip();
            }
            else if(gameObject.name.Equals("OrnateBook"))
                CollectableManager.Instance.GotBook();
        }
    }
}
