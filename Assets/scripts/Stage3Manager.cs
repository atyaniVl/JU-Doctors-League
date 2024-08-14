using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    [SerializeField] List<FinalQuestion> finalQuestions;
    private FinalQuestion selectedQuestion;
    [SerializeField] private Stage3UI quizUIobj;
    [SerializeField] private Timer timerObj;
    int questionIndex=0;
    bool isThereImg;
    public bool isEmpty(List<FinalQuestion> myList = null)
    {
        if (myList == null)
            return isEmpty(finalQuestions);
        else if (myList.Count == 0)
            return true;
        else
            return false;
    }
    public void Question_select()
    {
        if (!isEmpty(finalQuestions) && quizUIobj.isRemaining())
        {
            int val = questionIndex++;
            selectedQuestion = finalQuestions[val];
            isThereImg = selectedQuestion.questionImage != null;
            quizUIobj.setQuestion(selectedQuestion, isThereImg);
        }
        else
        {
            Debug.Log("there are no question in the list");
        }
        for (int i = 0; i < 4; i++)
            if (selectedQuestion.questionChoices[i] == selectedQuestion.correctChoice)
            {
                GameObject backdrop = GameObject.Find("Image1");
                foreach (Transform child in backdrop.transform)
                {
                    child.gameObject.SetActive(true);
                }
                backdrop.transform.GetChild(i).gameObject.SetActive(false);
            }
    }
    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctChoice)
            return !correctAns;
        else
            return correctAns;
    }
}
[System.Serializable]
public class FinalQuestion
{
    public string questionInfo;
    public List<string> questionChoices;
    public string correctChoice;
    public Sprite questionImage;
}

