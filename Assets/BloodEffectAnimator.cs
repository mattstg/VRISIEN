using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BloodEffectAnimator : MonoBehaviour
{
    public RawImage BloodEffectImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var tempColor = BloodEffectImage.color;
        tempColor.a = (201f - PlayerManager.Instance.player.hp)/100;
        print(PlayerManager.Instance.player.hp);
        BloodEffectImage.color = tempColor;
    }
}
