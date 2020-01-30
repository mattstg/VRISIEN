using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathScreenAnimator : MonoBehaviour
{
    public RawImage GameOverImage;
    public Text textObject;

    bool temp = true;

    private void Start()
    {
        GameOverImage.enabled = false;
        textObject.enabled = false;
    }

    private void Update()
    {
        if(!PlayerManager.Instance.player.isAlive)
        {
            PlayerDied();
        }
    }

    void PlayerDied()
    {
        if (temp)
        {
            temp = false;
            GameOverImage.enabled = true;
            textObject.enabled = true;
            StartCoroutine(AnimateDeathScreen());
        }
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
