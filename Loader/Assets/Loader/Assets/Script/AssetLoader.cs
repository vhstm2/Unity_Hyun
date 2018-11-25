using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetLoader : MonoBehaviour

{

    [SerializeField]

    string[] assetBundleNames;

    [SerializeField]
    //다운받은 씬 경로
    string assetBundleDirectory = "Assets/Loader/Assets";



    void Start()

    {

        LoadSceneFromAssetBundle("AdditionalLevel", false);

    }



    public void LoadSceneFromAssetBundle(string sceneName, bool isAdditive)

    {

        // 저장한 에셋 번들로부터 에셋 불러오기

        // gamescene.unity는 작성자가 로컬 저장소에 저장한 게임 씬들이 들어있는 에셋 번들이다.

        AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(assetBundleDirectory + "/", "AdditionalLevel.unity3d"));

        // 에셋 번들 내에 존재하는 씬의 경로를 모두 가져오기

        string[] scenes = assetBundle.GetAllScenePaths();

        string loadScenePath = null;

        foreach (string sname in scenes)

        {

            if (sname.Contains(sceneName))

            {

                loadScenePath = sname;

            }

        }



        if (loadScenePath == null) return;



        LoadSceneMode loadMode;



        if (isAdditive) loadMode = LoadSceneMode.Additive;

        else loadMode = LoadSceneMode.Single;



        SceneManager.LoadScene(loadScenePath, loadMode);

    }

}



