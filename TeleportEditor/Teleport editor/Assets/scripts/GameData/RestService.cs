using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Networking;

public class RestService : MonoBehaviour
{
    private GameDataFactory repo;
    private GameData model;
    private TeleportData[] tpData;
    private TeleportPoint[] tpPoints;
    private List<int> deletedTpPoints;

    [SerializeField]
    private const string getUrl = "https://localhost:44352/api/values/GameData", postUrl = "https://localhost:44352/api/values/UpdateImportantObjects", filePath = "gamedata.json";    // Start is called before the first frame update


    void Start()
    {
        StartCoroutine(GetAllData(false));
    }


    private void SceneToModel()
    {
        tpData = new TeleportData[FindObjectsOfType<TpPoint>().Length];
        int i = 0;
        List<TpPoint> objs = FindObjectsOfType<TpPoint>().ToList();
        objs.Sort();
        foreach (TpPoint tpPoint in objs)
        {
            TeleportData data = new TeleportData
            {
                TeleportDataId = tpPoint.Id,
                Name = tpPoint.Name,
                X = tpPoint.pos.x,
                Y = 6f,
                Z = tpPoint.pos.z
            };
            tpData[i] = data;
            i++;
        }
        model.TeleportDatas = tpData;

        if (deletedTpPoints != null)
        { //If teleport points need to be deleted
            model.DeletedTeleportDatas = new int[deletedTpPoints.Count];
            for (int j = 0; j < deletedTpPoints.Count; j++)
            {
                model.DeletedTeleportDatas[j] = deletedTpPoints[j];
            }
        }
    }


    private void ModelToScene()
    {
        GameObject newObj;
        foreach (TeleportData tpDat in model.TeleportDatas)
        {
            // create sphere
            newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObj.transform.position = new Vector3(tpDat.X, tpDat.Y, tpDat.Z); // set position from data in level
            newObj.layer = 9; // assign to SpawnedObjects layer.

            //Add editor object component and feed data.
            TpPoint eo = newObj.AddComponent<TpPoint>();
            eo.pos = newObj.transform.position;
            eo.Name = tpDat.Name;
            eo.Id = tpDat.TeleportDataId;
        }

    }
    //http://stackoverflow.com/questions/3674692/mono-webclient-invalid-ssl-certificates
    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        // all Certificates are accepted
        return true;
    }
    public IEnumerator GetAllData(bool firstTime)
    { //Get api data

        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate; //To disable https warning
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();

        //if (www.result == UnityWebRequest.Result.Success) -> replace with this for 2020, www.isNetworkError depreciated in Unity 2020
        if (responseFromServer != null)
        {//Yeey no errors!
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(responseFromServer);
            sw.Close();
            Debug.Log(responseFromServer);

            model = JsonUtility.FromJson<GameData>(responseFromServer);
            Debug.Log(model.TeleportDatas[0].Name);
            ModelToScene();
            if (firstTime)
                repo.SetFirstQuestion();
            Debug.Log(model.IntroText);
        }
        else
        {
            //An error occured, read settings from file
            StreamReader sr = new StreamReader(filePath);
            model = JsonUtility.FromJson<GameData>(sr.ReadToEnd());
            Debug.Log(responseFromServer);
            Debug.Log(model.TeleportDatas[0].Name);
            ModelToScene();
            if (firstTime)
                repo.SetFirstQuestion();
        }

        yield return 0;
    }

    public void SaveResponse()
    {
        StartCoroutine(UploadResponse());
    }
    public void AddDeletedTpPoints(int tpPointId)
    {
        if (deletedTpPoints == null)
        {
            deletedTpPoints = new List<int>();
        }
        deletedTpPoints.Add(tpPointId);
    }

    public void DoReset()
    {
        model.Reset = true;
    }
    private IEnumerator UploadResponse()
    {//Because a lot of things weren't working using learn.unity's examples, the diy solution: 
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(FindObjectOfType<questionScript>().GetQuestionResponse())); //Bytearray of json'ned model 
        Debug.Log(jsonBinary);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer(); //Create downloadHandler
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary); //Create uploadHandler 
        uploadHandlerRaw.contentType = "application/json"; //Set contentType

        UnityWebRequest www = //Creating a webrequest with all ingredients
            new UnityWebRequest(postUrl, "POST", downloadHandlerBuffer, uploadHandlerRaw);


        yield return www.SendWebRequest(); //Send it

        if (www.isNetworkError) //Network errors? 
            Debug.LogError(string.Format("{0}: {1}", www.url, www.error));
        else
            Debug.Log(string.Format("Response: {0}", www.downloadHandler.text));
    }

    

    public IEnumerator UploadTeleportPoints()
    {//Because a lot of things weren't working using learn.unity's examples, the diy solution: 
        SceneToModel();
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(model)); //Bytearray of json'ned model 
        Debug.Log(jsonBinary);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer(); //Create downloadHandler
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary); //Create uploadHandler 
        uploadHandlerRaw.contentType = "application/json"; //Set contentType

        UnityWebRequest www = //Creating a webrequest with all ingredients
            new UnityWebRequest(postUrl, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest(); //Send it

        if (www.isNetworkError) //Network errors? 
            Debug.LogError(string.Format("{0}: {1}", www.url, www.error));
        else
            Debug.Log(string.Format("Response: {0}", www.downloadHandler.text));
    }
}