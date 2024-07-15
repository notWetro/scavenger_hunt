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

    // TODO: CHANGE THIS FIELD TO THE ACTUAL ADRESS OF OUR BACKEND API
    [SerializeField]
    private const string HUNT_API_URL = "http://localhost:5100/api";

    public void OnLoginButtonPressed()
    {
        // Update the username and password from the input fields
        string m_Username = usernameInputField.text;
        string m_Password = passwordInputField.text;

        Debug.Log($"Pressed Login-Button with Username-Input '{m_Username}'");

        StartCoroutine(ParticipantsService.LoginRequest(m_Username, m_Password));
    }
}
