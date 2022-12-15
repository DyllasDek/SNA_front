using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class Database : MonoBehaviour
    {
        public PlayerData PlayerData;
        public static Database Instance;
        public string TopScoreString="";
        public string Address = "http://localhost:30000";

        private void Start()
        {
            Instance = this;
            PlayerData = new PlayerData();
        }


        public void SendPlayerProfileToServer()
        {
            StartCoroutine(SendServerRequest("/updatescore", PlayerData.Stringify()));
        }

        public void GetPlayerProfile()
        {
            StartCoroutine(SendServerRequest("/getplayer", PlayerData.CurrentName, ParsePlayerProfile));
        }

        public void GetTopProfiles()
        {
            StartCoroutine(GetTopPlayers("/gettop", ParseTopResults));
        }   
        public static List<string> JSONToList(string json)
        {
            var onlyList = json.Split('[')[1].Split(']')[0];
            List<string> result = new List<string>();
            int lastObject = 0;
            int bracketCounter = 0;
            for (int i = 0; i < onlyList.Length; i++)
            {
                if (onlyList[i] != '{' && onlyList[i] != '}')
                {
                    continue;
                }

                if (onlyList[i] == '{')
                {
                    if (bracketCounter == 0)
                    {
                        lastObject = i;
                    }
                    bracketCounter++;
                }

                if (onlyList[i] == '}')
                {
                    bracketCounter--;
                }

                if (bracketCounter == 0)
                {
                    result.Add(onlyList.Substring(lastObject, i - lastObject + 1));
                }
            }

            return result;
        }

        private void ParsePlayerProfile(string text)
        {
            if (text.Equals("{}")) return;
            PlayerData = PlayerData.Parse(text);
        }

        private void ParseTopResults(string text)
        {
            if (text.Equals("{}")) return;
            var jsons = JSONToList(text);
            var list = new List<PlayerData>();
            foreach (var json in jsons)
            {
                list.Add(PlayerData.Parse(json));
            }
            TopScoreString = TopScoreText.FormatTopString(list);
        }

        IEnumerator SendServerRequest(string requestAddress, string data, Action<string> callback = null)
        {
            using UnityWebRequest request = new UnityWebRequest(Address + requestAddress, "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();


            if (request.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(request.downloadHandler.text);
            }
            else
            {
                Debug.Log(request.error);
            }
        }


        IEnumerator GetTopPlayers(string addressName, Action<string> callback = null)
        {
            using UnityWebRequest request = UnityWebRequest.Get(Address + addressName);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(request.downloadHandler.text);
            }
            else
            {
                Debug.Log(request.error);
            }
        }
    }
}