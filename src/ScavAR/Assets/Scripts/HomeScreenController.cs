using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField]
    private string m_Username;

    public string Username
    {
        get => m_Username;
        set => m_Username = value;
    }

    public void OnLoginButtonPressed()
    {
        Debug.Log($"Pressed Login-Button with Username-Input '{m_Username}'");
        SceneManager.LoadScene("SampleScene");
    }
}
