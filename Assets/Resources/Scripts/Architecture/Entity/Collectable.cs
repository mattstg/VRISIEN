using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collectable thing touched");
        Debug.Log("Tag of the thing is " + collision);

        if (collision.gameObject.CompareTag("Left")|| collision.gameObject.CompareTag("Right")) //mixamorig:RightHand 
        {
        UIManager.Instance.SpawnUI(gameObject.transform);
            
            gameObject.SetActive(false);
            if (gameObject.name.Equals("SwordCustom"))
            {
                gameObject.layer = LayerMask.NameToLayer("Blade");
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotSword();
                UIManager.Instance.ui.textDesc.text = "Test Sword UI";
            }
            else if (gameObject.name.Equals("SD_Card"))
            {
                CollectableManager.Instance.GotChip();
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                UIManager.Instance.ui.textDesc.text = "Test Chip UI";
            }
            else if (gameObject.name.Equals("OrnateBook"))
            {
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotBook();
                UIManager.Instance.ui.textDesc.text = "Test Book UI";
            }
        }
    }
}
