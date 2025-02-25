using System.Collections;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ParticipantsService : MonoBehaviour
{
    // ACHTUNG: NGNIX IST CASE SENSITIVE!!!
    [SerializeField]
    private const string HUNT_API_URL = "http://localhost:5500/participants/api";

    /// <summary>
    /// Performs a login request to the backend.
    /// </summary>
    /// <param name="username">Username of participant.</param>
    /// <param name="password">Password of participant.</param>
    /// <returns>The login response containing a token for the participant as well as one participation.</returns>
    public static IEnumerator LoginRequest(string username, string password)
    {
        var formData = new LoginForm { username = username, password = password };
        string jsonData = JsonUtility.ToJson(formData);

#pragma warning disable IDE0063 // Use simple 'using' statement


        using (UnityWebRequest request = new(HUNT_API_URL + "/Participants/Login", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for the response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
                Debug.Log($"Response: {request.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"Response: {request.downloadHandler.text}");

                if (request.responseCode == 200)
                {
                    var tokenResponse = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                    TokenStorage.UserToken = tokenResponse.token;
                    Debug.Log(tokenResponse.huntTitle);
                    ParticipationStore.HuntTitle = tokenResponse.huntTitle;

                    SceneManager.LoadScene("MainScreen");
                }
                else
                {
                    Debug.LogError($"Login failed with status code: {request.responseCode}");
                }
            }
        }
#pragma warning restore IDE0063 // Use simple 'using' statement
    }

    [System.Serializable]
    private sealed class LoginForm
    {
        public string username;
        public string password;
    }

    [System.Serializable]
    public sealed class LoginResponse
    {
        public string token;
        public string huntTitle;
    }

}
