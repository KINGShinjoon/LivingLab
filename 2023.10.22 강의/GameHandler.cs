using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [System.Serializable]
    public class PlayerChoiceData
    {
        public string choice;
    }

    public string ServerURL = "http://127.0.0.1:5000/game";

    public Button scissors;
    public Button rock;
    public Button paper;

    private void Start()
    {
        scissors.onClick.AddListener(() => PlayGame("scissors"));
        rock.onClick.AddListener(() => PlayGame("rock"));
        paper.onClick.AddListener(() => PlayGame("paper"));
    }

    void PlayGame(string choice)
    {
        PlayerChoiceData requestData = new PlayerChoiceData
        {
            choice = choice
        };

        StartCoroutine(SendRequest(requestData));
    }

    IEnumerator SendRequest(PlayerChoiceData requestData)
    {
        string json = JsonUtility.ToJson(requestData);
        Debug.Log("Request JSON: " + json);

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(ServerURL, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string serverResponse = request.downloadHandler.text;
            Debug.Log("Server Response: " + serverResponse);
        }
        else
        {
            Debug.LogError("Request Error: " + request.error);
        }
    }
}
