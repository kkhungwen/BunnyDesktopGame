using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region ANIMATOR PARAMETERS
    public static int idle = Animator.StringToHash("Idle");
    public static int patrol = Animator.StringToHash("Patrol");
    public static int stagger = Animator.StringToHash("Stagger");
    public static int drag = Animator.StringToHash("Drag");
    public static int fall = Animator.StringToHash("Fall");
    public static int eat = Animator.StringToHash("Eat");
    public static int dropOn = Animator.StringToHash("DropOn");
    public static int dropOnIsBunny = Animator.StringToHash("DropOnIsBunny");
    public static int idleIsBunny = Animator.StringToHash("IdleIsBunny");
    #endregion
}
