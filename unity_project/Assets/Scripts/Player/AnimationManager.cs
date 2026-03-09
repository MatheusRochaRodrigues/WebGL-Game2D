using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationManager
{
    public void ChangeAnimation(string animation, float crossfade = 0.2f, float time = 0);
}
