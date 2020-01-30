using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectableType { Sword, Chip, Book }
    public CollectableType ctype;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collectable thing touched");
        Debug.Log("Tag of the thing is " + collision.gameObject.name + " which is tag: " + collision.gameObject.tag);

        if(gameObject.GetComponent<OVRGrabbable>().isGrabbed) //mixamorig:RightHand 
        {
            UIManager.Instance.SpawnUI(gameObject.transform);
           
            //if (gameObject.CompareTag("SwordCollectable"))
            if(ctype == CollectableType.Sword)
            {
                Debug.Log("Swiirdok Here");
                gameObject.layer = LayerMask.NameToLayer("Blade");
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotSword();
                UIManager.Instance.ui.textDesc.text = "Test Sword UI";
            }
            //else if (gameObject.CompareTag("ChipCollectable"))
            else if (ctype == CollectableType.Chip)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                CollectableManager.Instance.GotChip();
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                UIManager.Instance.ui.textDesc.text = "Test Chip UI";
                CollectableManager.Instance.DestroyObject(gameObject);
            }
            //else if (gameObject.CompareTag("BookCollectable"))
            else if (ctype == CollectableType.Book)
            {
                //gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                SoundManager.Instance.PlayMusic("Collectibles_Grab1", gameObject);
                CollectableManager.Instance.GotBook();
                UIManager.Instance.ui.textDesc.text = "Test Book UI";
                CollectableManager.Instance.DestroyObject(gameObject);
            }
        }
    }
}
