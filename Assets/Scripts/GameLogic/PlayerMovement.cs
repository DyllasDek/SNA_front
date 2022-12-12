using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> MovePoints;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private Ease MovementEase;
    TweenerCore<Vector3, Vector3, VectorOptions> movement;

    private void Start()
    {
        var point = GetClosestPoint();
        movement = transform.DOMove(point.position, 1/MovementSpeed).SetEase(MovementEase);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            movement?.Kill();
            var point = GetClosestPoint();
            movement = transform.DOMove(point.position, 1/MovementSpeed).SetEase(MovementEase);
        }
    }

    public Transform GetClosestPoint()
    {
        
        var inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var distance=Mathf.Infinity;
        var closestPoint = MovePoints[0];
        foreach (var point in MovePoints)
        {
            var curDistance = Vector2.Distance(point.position, inputPosition);
            if ( curDistance< distance)
            {
                distance = curDistance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
}
