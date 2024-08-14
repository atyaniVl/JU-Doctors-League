using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] List<Question> _30pointQ, _50pointQ, _10pointQ;
    private Question selectedQuestion;
    [SerializeField] private UI_manager quizUIobj;
    [SerializeField] private Timer timerObj;
    SpriteRenderer sprtRndr;
    int questionIndex =0;
    bool isThereImg;
    public bool isEmpty(List<Question> myList=null)
    {
        if (myList == null)
            return isEmpty(_30pointQ) && isEmpty(_50pointQ);
        else if (myList.Count == 0)
            return true;
        else
            return false;
    }
    public bool isEmpty2(List<Question> myList=null)
    {
        if (myList == null)
            return isEmpty2(_30pointQ)  ;
        else if (myList.Count == 0)
            return true;
        else
            return false;
    }
    private bool roundEnds(int level)
    {
        if (!quizUIobj.isRemaining(level))
            return true;
        else
            return false;
    }
    public void Question_select(int level)
    {
        if (level == 30)
        {
            if (!isEmpty(_30pointQ) && !roundEnds(30))
            {
                int val = Random.Range(0, _30pointQ.Count);
                selectedQuestion = _30pointQ[val];
                isThereImg = selectedQuestion.questionImage != null;
                print(isThereImg);
                quizUIobj.setQuestion(selectedQuestion, 30, isThereImg);
                _30pointQ.RemoveAt(val);
            }
            else
                Debug.Log("there are no question in the list");
        }
        else if (level == 50)
        {
            if (!isEmpty(_50pointQ) && !roundEnds(50))
            {
                int val = Random.Range(0, _50pointQ.Count);
                selectedQuestion = _50pointQ[val];
                isThereImg = selectedQuestion.questionImage != null;
                quizUIobj.setQuestion(selectedQuestion, 50, isThereImg);
                _50pointQ.RemoveAt(val);
            }
            else
                Debug.Log("there are no question in the list");
        }
        else if (level == 10)
        {
            if (!isEmpty(_10pointQ) && !roundEnds(10))
            {
                int val = Random.Range(0, _10pointQ.Count);
                selectedQuestion = _10pointQ[val];
                isThereImg = selectedQuestion.questionImage != null;
                quizUIobj.setQuestion(selectedQuestion, 10, isThereImg);
                _10pointQ.RemoveAt(val);
            }
            else
                Debug.Log("there are no question in the list");
        }
        else if(level==2)
        {
            if (!isEmpty(_30pointQ) && !roundEnds(30))
            {
                int val = questionIndex++;
                selectedQuestion = _30pointQ[val];
                isThereImg = selectedQuestion.questionImage != null;
                quizUIobj.setQuestion(selectedQuestion, 30, isThereImg);
            }
            else
            {
                Debug.Log("there are no question in the list");
            }

        }
        else
            Debug.Log("you are selecting non-exist question");
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
        /*else if(level==)
        {
            this is for any other question
        }*/

    }

    public bool Answer(string answered)
    {
        Debug.Log("Answer is checked");
        bool correctAns = false;
        if (answered == selectedQuestion.correctChoice)
            return !correctAns;
        else
            return correctAns;
    }
    public string GetRightAnswer()
    {
        char CHAR = 'a';
        for(int i=0; i<4; i++)
        {
            if(selectedQuestion.questionChoices[i]== selectedQuestion.correctChoice)
            {
                break;
            }
            CHAR++;
        }
        return CHAR+") "+selectedQuestion.correctChoice;
    }
}
[System.Serializable]
public class Question
{
    public string questionInfo;
    public List<string> questionChoices;
    public string correctChoice;
    public Sprite questionImage;
}
