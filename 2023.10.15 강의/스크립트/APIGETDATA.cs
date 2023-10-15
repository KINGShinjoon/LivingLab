using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIGETDATA : MonoBehaviour
{
    public string serverURL = "http://127.0.0.1:5000/api/data";

    [System.Serializable]
    public class DataItem
    {
        public int id;
        public string name;
    }

    private void Start()
    {
        
         StartCoroutine(GetData());
        
    }
    IEnumerator GetData()
    {
        using(UnityWebRequest request = 
            UnityWebRequest.Get(serverURL))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                string decodeResponse = System.Text.RegularExpressions.Regex.
                    Unescape(jsonResponse);

                DataItem[] dataItems = JsonUtility.FromJson
                    <DataItem[]>(decodeResponse);

                foreach(var item in dataItems)
                {
                    Debug.Log("ID: " + item.id + ", Name: " + item.name);
                }
                //Debug.Log("Received:" +  jsonResponse);
            }
            else
            {
                Debug.LogError("Error :" + request.error);
            }
        }
    }
}
