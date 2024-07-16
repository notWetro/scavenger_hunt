using TMPro;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameInputField;

    [SerializeField]
    private TMP_InputField passwordInputField;

    public void OnLoginButtonPressed()
    {
        // Update the username and password from the input fields
        string m_Username = usernameInputField.text;
        string m_Password = passwordInputField.text;

        Debug.Log($"Pressed Login-Button with Username-Input '{m_Username}'");

        StartCoroutine(ParticipantsService.LoginRequest(m_Username, m_Password));
    }
}
