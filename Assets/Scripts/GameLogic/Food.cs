using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace DefaultNamespace
{
    public class Food : PushableObject
    {
        private bool _isCollected;

        private void OnDestroy()
        {
            if (!_isCollected)
            {
                ScoreText.Instance.RemoveStreak();
            }
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }

        public void Collect()
        {
            if (_isCollected) return;
            ScoreText.Instance.UpdateStreak();
            ScoreText.Instance.AddScore();
            _isCollected = true;
            transform.DOScale(0, 0.1f).OnComplete(() => { Destroy(gameObject); });
        }

        public bool IsAvailable()
        {
            return !_isCollected;
        }
    }
}