using System;
using System.Collections;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text huntTitleText;

    private bool _assignmentFetched = false;
    private Assignment _currentAssignment = null;
    private float _fetchInterval = 5f; // Interval in seconds
    private float _timeSinceLastFetch = 0f;

    private void Start()
    {
        UpdateHuntTitle();
    }

    private void UpdateHuntTitle()
    {
        Debug.Log(ParticipationStore.HuntTitle);
        huntTitleText.text = ParticipationStore.HuntTitle;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_assignmentFetched)
        {
            _timeSinceLastFetch += Time.deltaTime;

            if (_timeSinceLastFetch >= _fetchInterval)
            {
                _timeSinceLastFetch = 0f;
                StartCoroutine(FetchAssignment());
            }
        }
    }

    private IEnumerator FetchAssignment()
    {
        var token = TokenStorage.UserToken;
        const string HUNT_API_URL = "http://localhost:5500/participants/api";

        Debug.Log(token);

        // Create an instance of the TokenRequest class and serialize it to JSON
        var assignmentRequest = new AssignmentRequest { token = token };
        string dataJson = JsonUtility.ToJson(assignmentRequest);

        // Use UnityWebRequest.Post to send the JSON body
        using (UnityWebRequest www = UnityWebRequest.Put(HUNT_API_URL + "/Participants/CurrentAssignment", dataJson))
        {
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(dataJson));
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log($"Response Text: {responseText}");

                if (string.IsNullOrEmpty(responseText))
                {
                    Debug.LogError("Error: Response is empty");
                }
                _assignmentFetched = true;
            }
        }
    }
}

[Serializable]
public sealed class AssignmentRequest
{
    public string token;
}

[Serializable]
public sealed class AssignmentResponse
{
    public int HintType { get; set; }
    public string HintData { get; set; }
    public int SolutionType { get; set; }
}
