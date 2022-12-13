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
            var list = new List<PlayerData>();
            list.Add(new PlayerData("Danila",1005));
            list.Add(new PlayerData("Yarik",1234));
            list.Add(new PlayerData("Nookie",12));
            TextContainer.SetText(FormatTopString(list));
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