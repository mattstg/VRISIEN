using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Left")|| collision.collider.gameObject.CompareTag("Right")) //mixamorig:RightHand 
        {
            Debug.Log("Collectable thing touched");
            Debug.Log("Tag of the thing is " + collision);
            gameObject.SetActive(false);
            if (gameObject.name.Equals("SwordCustom"))
            {
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotSword();
            }
            else if (gameObject.name.Equals("SD_Card"))
            {
                CollectableManager.Instance.GotChip();
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
            }
            else if (gameObject.name.Equals("OrnateBook"))
            {
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotBook();
            }
        }
    }
}
