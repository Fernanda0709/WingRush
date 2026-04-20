using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClient : MonoBehaviour
{
    public string baseUrl = "http://localhost:5005/server";
    public event Action<int, ServerData> OnDataReceived;
    public bool LastRequestSuccess { get; private set; }

    public IEnumerator GetPlayerData(string gameId, string playerId)
    {
        string url = $"{baseUrl}/{gameId}/{playerId}";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                LastRequestSuccess = true;
                var data = JsonUtility.FromJson<ServerData>(webRequest.downloadHandler.text);
                OnDataReceived?.Invoke(Convert.ToInt16(playerId), data);
            }
            else
            {
                LastRequestSuccess = false;
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError($"GET Error: {webRequest.error}");
                }
            }
        }
    }

    public IEnumerator PostPlayerData(string gameId, string playerId, ServerData data)
    {
        string url = $"{baseUrl}/{gameId}/{playerId}";
        string jsonData = JsonUtility.ToJson(data);
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
        }
    }
}