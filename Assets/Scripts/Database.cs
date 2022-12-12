using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class Database : MonoBehaviour
    {
        public PlayerData PlayerData;
        public static Database Instance;
        public string Address = "http://localhost:3000/Players";

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

        private void ParsePlayerProfile(string text)
        {
            if (text.Equals("{}")) return;
            PlayerData = PlayerData.Parse(text);
        }

        private void ParseTopResults(string text)
        {
            if (text.Equals("{}")) return;
            PlayerData = PlayerData.Parse(text);
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
            using UnityWebRequest request = UnityWebRequest.Get(Address + "/" + addressName);
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