using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; //새롭게 추가한 항목

public class APIHandler : MonoBehaviour
{
    public string serverURL = "http://127.0.0.1:5000/api/senddata";

    IEnumerable SendDataToServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("data", "a");
        using (UnityWebRequest request = 
            UnityWebRequest.Post(serverURL, form))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                string serverResponse = request.downloadHandler.text;
                Debug.Log("Server Response:" + serverResponse); 
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}
