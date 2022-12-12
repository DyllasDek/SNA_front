using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIMaxScoreText : MonoBehaviour
    {
        private TMP_Text _tmpText;

        private void Start()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        private void LateUpdate()
        {
            _tmpText.SetText("Your record: {0}", Database.Instance.PlayerData.CurrentMaxScore);
        }
        
    }
}