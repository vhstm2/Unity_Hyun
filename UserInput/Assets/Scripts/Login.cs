using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;


public struct LoginResult
{
    public int result;
}


public struct LoginForm
{
    public string username;
    public string password;
}

public enum ResponseType
{
    INVALID_USERNAME = 0,
    INVALID_PASSWORD,
    SUSSESS
}

public class Login : MonoBehaviour {

    public InputField idInputField;
    public InputField passwordInputField;

    [SerializeField]
    private Button login;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public void OnClickLoginButton()
    {
        LoginForm loginForm = new LoginForm();

        loginForm.username = idInputField.text;
        loginForm.password = passwordInputField.text;

        if (string.IsNullOrEmpty(loginForm.username) ||
            string.IsNullOrEmpty(loginForm.password))
        {
            Debug.Log("아이디 패스워드를 입력하세요");
            StartCoroutine(loginbuttonOneClick());
            
        }
        else
        {

            StartCoroutine(Logins(loginForm));
            StartCoroutine(loginbuttonOneClick());
        }
    }

    IEnumerator Logins(LoginForm loginForm)
    {
        login.interactable = false;
      //  login.gameObject.SetActive(false);


        string data = JsonUtility.ToJson(loginForm);
        byte[] sendData = Encoding.UTF8.GetBytes(data);

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/users/signin", sendData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                Debug.Log(www.error);
                StartCoroutine(loginbuttonOneClick());
            }
            else
            {
                string resultstr = www.downloadHandler.text;

                var result = JsonUtility.FromJson<LoginResult>(resultstr);


                if (result.result == 2)
                {
                    login.interactable = true;
                    //  login.gameObject.SetActive(true);


                    Debug.Log(www.downloadHandler.text);

                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    IEnumerator loginbuttonOneClick()
    {
        
        login.interactable = false;
      //  login.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        login.interactable = true;


    }
}
