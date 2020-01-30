using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void Stun();

    void SwordHit();

    void ApplyDamage(int damageAmt);
}
