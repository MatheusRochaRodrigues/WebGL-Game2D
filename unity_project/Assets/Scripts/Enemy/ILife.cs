using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILife
{
    void isDamage(int Damage, Transform colision, Item item = null);
}
