using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GameDataFactory
{
    private static GameData model;
    private static string filePath = "gamedata.json";
    private const string getUrl = "https://localhost:44352/api/values/GameData";
    public GameDataFactory()
    {
        UpdateGameData();
    }

    public IEnumerator UpdateGameData()
    { //Get api data
        UnityWebRequest www = UnityWebRequest.Get(getUrl);
        yield return www.SendWebRequest();

        //if (www.result == UnityWebRequest.Result.Success) -> replace with this for 2020, www.isNetworkError depreciated in Unity 2020
        if (www.error == null)
        {//Yeey no errors!
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(www.downloadHandler.text);
            sw.Close();
            model = JsonUtility.FromJson<GameData>(www.downloadHandler.text);
        }
        else
        {
            //An error occured, read settings from file
            StreamReader sr = new StreamReader(filePath);
            model = JsonUtility.FromJson<GameData>(sr.ReadToEnd());
        }
    }

    public void SetFirstQuestion()
    {
        GameObject questionTitle = GameObject.Find("QuestionTitle");
        GameObject questionText = GameObject.Find("QuestionText");
        questionTitle.GetComponent<TMP_Text>().text = model.Title;
        questionText.GetComponent<TMP_Text>().text = model.IntroText;
    }

    public QuestionData GetQuestion(int id)
    {
        return model.QuestionDatas.FirstOrDefault(x => x.Position == id);
    }

    public GameData GetGameData()
    {
        return model;
    }
}
