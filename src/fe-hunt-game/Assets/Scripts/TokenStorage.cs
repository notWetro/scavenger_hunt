using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenStorage : MonoBehaviour
{
    private static string _userToken = string.Empty;
    public static string UserToken
    { 
        get => _userToken; 
        set => _userToken = value;
    }
}
