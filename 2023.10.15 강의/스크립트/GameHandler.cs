using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; //using ���� �̿��ϼž��մϴ�.

public class GameHandler : MonoBehaviour
{
    public string ServerURL = "http://127.0.0.1:5000/game";

    public Button scissors;
    public Button rock;
    public Button paper;

    private void Start()
    {
        scissors.onClick.AddListener(() => PlayGame("����"));
        rock.onClick.AddListener(() => PlayGame("�ָ�"));
        paper.onClick.AddListener(() => PlayGame("��"));
    }

        void PlayGame(string choice)
    {
        StartCoroutine(send(choice));
    }

    IEnumerator send(string playerChice)
    {

        var requestData = new { choice = playerChice };
        string json = JsonUtility.ToJson(requestData);
        using (UnityWebRequest reqeust =
            UnityWebRequest.Post(ServerURL, json))
        {
            reqeust.SetRequestHeader("Content-Type", "application/json");
            yield return reqeust.SendWebRequest();

            if (reqeust.result == UnityWebRequest.Result.Success)
            {
                string serverResponse = reqeust.downloadHandler.text;
                Debug.Log(serverResponse);
            }
            else
            {
                Debug.LogError(reqeust.error);
            }
        }
    }
}
