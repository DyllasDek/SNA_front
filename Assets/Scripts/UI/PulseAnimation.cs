using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public Vector3 Scale;
    public Vector3 Rotation;

    public float Speed;
    Tweener _scaleCore;
    Tweener _rotateCore;
    private void OnEnable()
    {
        _scaleCore?.Kill();
        _rotateCore?.Kill();
        _scaleCore = transform.DOScale(Scale, Speed).SetSpeedBased().SetLoops(-1, LoopType.Yoyo);
        _rotateCore = transform.DOPunchRotation(Rotation, Speed,0).SetSpeedBased().SetLoops(-1, LoopType.Yoyo);
    }
    
    
}