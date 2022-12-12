using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class PushableObject : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;

        private void Start()
        {
            Destroy(gameObject, 10f);
        }

        private void Update()
        {
            if (-10f > transform.position.y)
            {
                transform.DOScale(0, 0.2f).OnComplete(() => { Destroy(gameObject); });
            }
        }
    }
}