using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;



[System.Serializable]
public class ServerDate
{
    public string date;
}

public class UIMANAGER : MonoBehaviour {

    [SerializeField]
    private Text timeText;


    IEnumerator GetDate()
    {
        //웹서버에 요청하는 기능을 담은 클래스
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/date"))
        {
            yield return www.SendWebRequest();

            //서버가 오류가 생겻을떄
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);//서버에 전달받은 녀석을 문자열로 받을수있다.?
                string result = www.downloadHandler.text;
                ServerDate date = JsonUtility.FromJson<ServerDate>(result);
                timeText.text = date.date;
            }
        }
    }


    #region UI_Events
    public void OnGetTimeButtonCilker()
    {

        StartCoroutine(GetDate());
        
    }
    #endregion



}
