using Gmae.Firebase;
using System;
using TMPro;
using UnityEngine;

public class LogInSystem : MonoBehaviour
{
    public TMP_InputField _email;
    public TMP_InputField _password;


    private void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }

    private void OnChangedState(bool isSign)
    {
        // string text = $"{(isSign == true ? "로그인" : "로그아웃")} : {FirebaseAuthManager.Instance.UserId}";
    }

    public void Register()
    {
        string e = _email.text;
        string p = _password.text;

        if (IsValidEmail(e) == false)
        {
            Debug.LogError("이메일 형식이 유효하지 않습니다.");
            return;
        }

        if (IsValidPassword(p, out string error) == false)
        {
            Debug.LogError("비밀번호 유효성 오류: " + error);
            return;
        }

        FirebaseAuthManager.Instance.Register(e, p);
    }


    public void LogIn()
    {
        string e = _email.text;
        string p = _password.text;

        FirebaseAuthManager.Instance.LogIn(e, p);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }


    private bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private bool IsValidPassword(string password, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (password.Length < 6)
        {
            errorMessage = "비밀 번호는 최소 6글자 이상이어야 합니다.";
            return false;
        }

        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]") == false)
        // {
        //     errorMessage = "대문자를 포함해야 합니다.";
        //     return false;
        // }
        // 
        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]") == false)
        // {
        //     errorMessage = "소문자를 포함해야 합니다.";
        //     return false;
        // }
        // 
        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]") == false)
        // {
        //     errorMessage = "숫자를 포함해야 합니다.";
        //     return false;
        // }
        // 
        // if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]") == false)
        // {
        //     errorMessage = "특수문자를 포함해야 합니다.";
        //     return false;
        // }

        return true;
    }

}
