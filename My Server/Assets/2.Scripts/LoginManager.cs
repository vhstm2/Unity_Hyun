using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;





public struct LoginData
{
    public string id, pw;

}


public class LoginManager : MonoBehaviour {

    //서버에게 요청
    //서버에서 등록된 주소로 요청을 받고 
    //서버가 응답 을 할때까지 클라이언트는 응답이 올떄까지 계속 요청한다.

    [SerializeField]        
    private InputField[] inputFields;

    public void PostButtonClicked()
    {
        StartCoroutine(PostText());
    }



    public void buttonTestClick()
    {
        StartCoroutine(GetText());
    }


    IEnumerator PostText()
    {
        LoginData data = new LoginData {id = "12", pw = "1234"};

        string postData = JsonUtility.ToJson(data);
        byte[] sendData = Encoding.UTF8.GetBytes(postData);

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/test/login", sendData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type","Application/json");
            yield return www.SendWebRequest();

            Debug.Log(www.downloadHandler.text);
            inputFields[0].text = www.downloadHandler.text;
        }

       
    }


    IEnumerator GetText()
    {                                                           //? 변수명을 할당해서 값을 대입
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/test/addscore?score=3000&nicname=oo"))
        {
            yield return www.SendWebRequest();

            Debug.Log(www.downloadHandler.text);
            inputFields[1].text = www.downloadHandler.text;
        }
    }




}
