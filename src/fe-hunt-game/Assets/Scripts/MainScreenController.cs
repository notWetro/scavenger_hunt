using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameInputField;

    [SerializeField]
    private TMP_InputField passwordInputField;

    private bool _assignmentFetched = false;
    private Assignment _currentAssignment = null;
    private float _fetchInterval = 5f; // Interval in seconds
    private float _timeSinceLastFetch = 0f;

    [SerializeField]
    private const string HUNT_API_URL = "http://localhost:7105/api";

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

        Debug.Log($"Using token: {token}");

        using (UnityWebRequest request = new UnityWebRequest(HUNT_API_URL + "/Participant/Assignment", "GET"))
        {
            request.SetRequestHeader("Token", token);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Send the request and wait for the response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log($"Response Text: {responseText}");

                if (string.IsNullOrEmpty(responseText))
                {
                    Debug.LogError("Error: Response is empty");
                }
                else
                {
                    if (request.responseCode == 200)
                    {
                        try
                        {
                            // Deserialize JSON to Assignment object
                            var response = JsonUtility.FromJson<AssignmentResponse>(responseText);
                            
                            //_currentAssignment = new()
                            //{
                            //    Id = response.Id,
                            //    Hint = response.Hint,
                            //    Solution = new()
                            //    {
                            //        Type = response.Solution.Type,
                            //    }
                            //};
                            
                            _assignmentFetched = true;

                            if (_currentAssignment != null)
                            {
                                Debug.Log($"Assignment fetched: Hint Data - {_currentAssignment.Hint.Data}, Solution Type - {_currentAssignment.Solution.Type}");
                            }
                            else
                            {
                                Debug.LogError("Error: Deserialized assignment is null");
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"Error deserializing JSON: {e.Message}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Fetching assignment failed with status code: {request.responseCode}");
                    }
                }
            }
        }
    }
}

[Serializable]
public sealed class AssignmentResponse
{
    public int Id { get; set; }
    public Hint Hint { get; set; }
    public SolutionResponse Solution { get; set; }
}

[Serializable]
public sealed class SolutionResponse
{
    public int Id { get; set; }
    public int Type { get; set; }
}

