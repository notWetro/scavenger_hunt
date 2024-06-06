using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class GetApiData : MonoBehaviour
{
    private string url = "https://localhost:7161/api";

    public void GetData()
    {
        StartCoroutine(FetchData());
    }
    public IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{url}/Hunts/1"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                
                var hunt = JsonConvert.DeserializeObject<Hunt>(request.downloadHandler.text);
                Debug.Log(hunt.Title + ":" + hunt.Description);
                
                //Debug.Log(hunts[0].Title + ": " + hunts[0].Description);
            }
        }
    }
}
