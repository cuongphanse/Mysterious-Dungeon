using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTriggers : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>(); 
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
