using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collectable thing touched");
        Debug.Log("Tag of the thing is " + collision);

        if (collision.collider.gameObject.CompareTag("Left")|| collision.collider.gameObject.CompareTag("Right")) //mixamorig:RightHand 
        {
            UIManager.Instance.SpawnUI(gameObject.transform);
           
            if (gameObject.name.Equals("SwordCustom"))
            {
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotSword();
            }
            else if (gameObject.name.Equals("SD_Card"))
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                CollectableManager.Instance.GotChip();
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                UIManager.Instance.ui.textDesc.text = "Test Chip UI";
                CollectableManager.Instance.DestroyObject(gameObject);
            }
            else if (gameObject.name.Equals("OrnateBook"))
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotBook();
                UIManager.Instance.ui.textDesc.text = "Test Book UI";
                CollectableManager.Instance.DestroyObject(gameObject);
            }
        }
    }
}
