using System;
using DefaultNamespace;
using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    [SerializeField] private Health Health;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Food food))
        {
            if (!food.IsAvailable()) return;
            food.Collect();
        }

        if (other.gameObject.TryGetComponent(out DamageSource source))
        {
            if (!source.IsAvailable()) return;
            Health.TakeDamage();
            source.Collect();
        }
    }
}