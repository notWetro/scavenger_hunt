using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameInputField;

    [SerializeField]
    private TMP_InputField passwordInputField;

    [SerializeField]
    private const string HUNT_API_URL = "http://localhost:7105/api";

    public void OnLoginButtonPressed()
    {
        // Update the username and password from the input fields
        string m_Username = usernameInputField.text;
        string m_Password = passwordInputField.text;

        Debug.Log($"Pressed Login-Button with Username-Input '{m_Username}'");

        StartCoroutine(LoginRequest(m_Username, m_Password));
    }

    private IEnumerator LoginRequest(string username, string password)
    {
        var formData = new LoginForm { username = username, password = password };
        string jsonData = JsonUtility.ToJson(formData);

#pragma warning disable IDE0063 // Use simple 'using' statement
        using (UnityWebRequest request = new UnityWebRequest(HUNT_API_URL + "/Participant/Login", "POST"))
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
            }
            else
            {
                Debug.Log($"Response: {request.downloadHandler.text}");

                if (request.responseCode == 200)
                {
                    var tokenResponse = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                    TokenStorage.UserToken = tokenResponse.token;

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
    private sealed class LoginResponse
    {
        public string token;
    }
}
