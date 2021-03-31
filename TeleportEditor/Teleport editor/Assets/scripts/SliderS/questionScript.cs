using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class questionScript : MonoBehaviour
{
    private GameDataFactory repo;
    private RestService restService;
    [SerializeField]
    private TMP_Text questionTitle, questionText;
    private int currentQuestionId = 0;
    [SerializeField]
    private Slider autoSlider, fietsSlider, busSlider, voetgangSlider, deelAutoSlider;
    private List<Slider> sliders;
    // Start is called before the first frame update
    void Start()
    {
        repo = new GameDataFactory();
        sliders = new List<Slider>();
        sliders.AddRange(new[] { autoSlider, fietsSlider, busSlider, voetgangSlider, deelAutoSlider });
        foreach (Slider slider in sliders)
        { //Hide all sliders
            slider.gameObject.SetActive(false);
        }
        GameObject.Find("QuestionNextButtonText").GetComponent<TMP_Text>().text = "Start";
        restService = FindObjectOfType<RestService>();
    }

    public void NextButton()
    {
        GameObject.Find("QuestionNextButtonText").GetComponent<TMP_Text>().text = "Volgende";
        currentQuestionId++;
        QuestionData data = repo.GetQuestion(currentQuestionId);
        if (data != null)
        { //Questiondata is not null, so show question
            questionTitle.text = data.Title;
            questionText.text = data.Question;

            switch (data.QuestionType)
            { //Show the correct slider
                case QuestionType.Auto:
                    autoSlider.gameObject.SetActive(true); break;
                case QuestionType.Fiets:
                    fietsSlider.gameObject.SetActive(true); break;
                case QuestionType.Bus:
                    busSlider.gameObject.SetActive(true); break;
                case QuestionType.Voetganger:
                    voetgangSlider.gameObject.SetActive(true); break;
                case QuestionType.Deelauto:
                    deelAutoSlider.gameObject.SetActive(true); break;
            }
        }
        else
        { //Questiondata is null, we're gonna upload/save it!
            GameObject.Find("QuestionNextButtonText").GetComponent<TMP_Text>().text = "Upload";
            //restService.SaveResponse();
        }

        if (repo.GetQuestion(currentQuestionId + 1) == null)
        { //Check if next question will be null to change buttontext in advance
            GameObject.Find("QuestionNextButtonText").GetComponent<TMP_Text>().text = "Upload";
        }
    }

    public QuestionResponse GetQuestionResponse()
    {
        QuestionResponse response = new QuestionResponse()
        {
            QuestionResponseLines = new QuestionResponseLine[sliders.Count]
        };

        QuestionType questionType = QuestionType.Auto;
        for (int i = 0; i < sliders.Count; i++)

        {
            switch (sliders[i].GetComponentInChildren<TMP_Text>().text)
            {
                case string a when a.Contains("Auto"):
                    questionType = QuestionType.Auto; break;
                case string a when a.Contains("Fiets"):
                    questionType = QuestionType.Fiets; break;
                case string a when a.Contains("Bus"):
                    questionType = QuestionType.Bus; break;
                case string a when a.Contains("Voet"):
                    questionType = QuestionType.Voetganger; break;
                case string a when a.Contains("Deel-auto"):
                    questionType = QuestionType.Deelauto; break;
            }

            response.QuestionResponseLines[i] = new QuestionResponseLine() { QuestionResult = (int)sliders[i].value, QuestionType = questionType };
        }

        return response;

    }
}
