using Gmae.Firebase;
using System;
using TMPro;
using UnityEngine;

public class LogInSystem : MonoBehaviour
{
    public TMP_InputField _email;
    public TMP_InputField _password;

    public TMP_Text _outputText;


    private void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }

    private void OnChangedState(bool isSign)
    {
        string text = $"{(isSign == true ? "로그인" : "로그아웃")} : {FirebaseAuthManager.Instance.UserId}";

        _outputText.SetText(text);
    }

    public void Create()
    {
        string e = _email.text;
        string p = _password.text;

        FirebaseAuthManager.Instance.Create(e, p);
    }


    public void Login()
    {
        string e = _email.text;
        string p = _password.text;

        FirebaseAuthManager.Instance.LogIn(e, p);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }
}
