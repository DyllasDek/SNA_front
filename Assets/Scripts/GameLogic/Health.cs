using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace DefaultNamespace
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int MaxHealth;
        [SerializeField] private List<GameObject> HealthObjects;
        public static Health Instance;
        public Action Died;
        private int _curHealth;

        private void Awake()
        {
            Instance = this;
            _curHealth = MaxHealth;
            foreach (var obj in HealthObjects)
            {
                obj.SetActive(true);
            }
        }

        private void OnEnable()
        {
            
            _curHealth = MaxHealth;
            foreach (var obj in HealthObjects)
            {
                obj.SetActive(true);
            }
        }

        public void TakeDamage()
        {
            _curHealth -= 1;
            var curHealthImageIndex = Mathf.Clamp(_curHealth,0,HealthObjects.Count-1);
            HealthObjects[curHealthImageIndex].SetActive(false);
            if (_curHealth <= 0)
            {
                Die();
            }
            else
            {
                
                CameraShake.ShakeCamera(0.25f,4f);
            }
        }

        private void Die()
        {
            CameraShake.ShakeCamera(0.25f,10f);
            Died?.Invoke();
        }
    }
}