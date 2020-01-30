using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathScreenAnimator : MonoBehaviour
{
    public Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateDeathScreen());
    }

    IEnumerator AnimateDeathScreen()
    {
        textObject.text = "Restarting in 3";
        yield return new WaitForSeconds(1f); ;
        textObject.text = "Restarting in 2";
        yield return new WaitForSeconds(1f); ;
        textObject.text = "Restarting in 1";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        
    }
}
