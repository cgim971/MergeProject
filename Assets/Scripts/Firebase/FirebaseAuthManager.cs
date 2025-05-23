using Firebase.Auth;
using System;
using UnityEngine;

namespace Gmae.Firebase
{
    public class FirebaseAuthManager
    {

        private static FirebaseAuthManager _instance = null;

        public static FirebaseAuthManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FirebaseAuthManager();
                }

                return _instance;
            }
        }


        private FirebaseAuth _auth; // 로그인 / 회원가입 등에 사용
        private FirebaseUser _user; // 인증이 완료된 유저 정보

        public Action<bool> LoginState;

        public string UserId => _user.UserId;


        public void Init()
        {
            _auth = FirebaseAuth.DefaultInstance;

            if (_auth.CurrentUser != null)
            {
                LogOut();
            }

            _auth.StateChanged += OnChanged;
        }


        private void OnChanged(object sender, EventArgs e)
        {
            // 이전 상태와 같으면 반환
            if (_auth.CurrentUser == _user)
                return;

            // true면 로그인 된 상태
            bool isSigned = _auth.CurrentUser != null;

            // false면 로그인 된 상태가 아니다
            // _user가 null이 아니면 로그 아웃 상태
            if (isSigned == false && _user != null)
            {
                _user = null;
                LoginState?.Invoke(false);
                return;
            }

            if (isSigned == true)
            {
                _user = _auth.CurrentUser;
                LoginState?.Invoke(true);
                return;
            }
        }



        public void Register(string email, string password)
        {
            _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                // 회원 가입 취소
                if (task.IsCanceled == true)
                {
                    Debug.LogError("회원가입 취소");
                    return;
                }

                // 회원 가입 실패
                if (task.IsFaulted == true)
                {
                    // 회원 가입 실패 이유 => 이메일이 비정상 / 비밀번호가 너무 간단 / 이미 가입된 이메일 등등...
                    Debug.LogError("회원가입 실패");
                    return;
                }

                AuthResult result = task.Result;
                FirebaseUser newUser = result.User;

                Debug.Log("회원가입 완료");
            });
        }


        public void LogIn(string email, string password)
        {
            _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled == true)
                {
                    Debug.LogError("로그인 취소");
                    return;
                }

                if (task.IsFaulted == true)
                {
                    // 로그인 실패 이유 => 이메일이 없거나 / 이메일과 비밀번호가 다른 경우
                    Debug.LogError("로그인 실패");
                    return;
                }

                AuthResult result = task.Result;
                FirebaseUser newUser = result.User;

                Debug.Log("로그인 완료");
            });
        }


        public void LogOut()
        {
            _auth.SignOut();
            Debug.Log("로그아웃");
        }
    }
}
