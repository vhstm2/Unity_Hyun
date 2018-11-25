using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;


public struct SignData      //로그인
{
    public string id, pw;
}

public struct SignUpData    //회원가입
{
    public string id, pw , name;
}



public class LoginManager : MonoBehaviour {


    [SerializeField]
    private InputField IDField;

    [SerializeField]
    private InputField PWField;


    //로그인
    public void OnSignInButtonClicked()
    {

        string id = IDField.text;
        string pw = PWField.text;

        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(pw))
        {
            StartCoroutine(signin(id,pw));
        }


    }

    public void OnSignUpButtonClicked()
    {

    }

    IEnumerator signin(string id, string pw)
    {
        SignData data = new SignData { id = "goya", pw = "1234" };
        string jsonData = JsonUtility.ToJson(data);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/users/login", sendData))
        {

            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            Debug.Log(www.downloadHandler.text);
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }


    //IEnumerator signUp(string id , string pw, string name)
    //{
    //    //using (UnityWebRequest www)
    //}




}
