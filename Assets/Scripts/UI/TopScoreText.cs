using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TopScoreText : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text TextContainer;

        private void OnEnable()
        {
            Database.Instance.GetTopProfiles();
            
        }

        private void Update()
        {
            if (isActiveAndEnabled)
            {
                TextContainer.SetText(Database.Instance.TopScoreString);
            }
        }

        public static string FormatTopString(List<PlayerData> playerDatas)
        {
            int order = 1;
            var finalString = "";

            foreach (var player in playerDatas)
            {
                if (player.CurrentName.Equals("")) return finalString;

                finalString +=  order+". "+player.CurrentName+":    " +player.CurrentMaxScore;
                
                order++;
            }
                
            return finalString;
        }
        
    }
}