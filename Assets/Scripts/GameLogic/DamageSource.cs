using UnityEngine;

namespace DefaultNamespace
{
    public class DamageSource : PushableObject
    {
        private bool _isCollected;

        public void Collect()
        {
            if(_isCollected) return;
            _isCollected = true;
            Destroy(gameObject);
        }

        public bool IsAvailable()
        {
            return !_isCollected;
        }
    }
}