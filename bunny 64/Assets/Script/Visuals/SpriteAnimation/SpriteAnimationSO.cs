using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimationSO_", menuName = "Scriptable Objects/Sprite Animation SO")]
public class SpriteAnimationSO : ScriptableObject
{
    public Sprite[] frameArray;
    public float secPerFrame;
    public bool isLoop;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtils.ValidateCheckEnumerableValues(this, nameof(frameArray), frameArray);
        HelperUtils.ValidateCheckPositiveValue(this, nameof(secPerFrame), secPerFrame, false);
    }
#endif
    #endregion
}
