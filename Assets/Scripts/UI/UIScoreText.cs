using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIScoreText : MonoBehaviour
    {
        private TMP_Text _tmpText;

        private void Start()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        private void LateUpdate()
        {
            _tmpText.SetText("Your score: {0}", Database.Instance.PlayerData.CurrentScore);
        }
        
    }
}