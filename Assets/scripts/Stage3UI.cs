using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage3UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText, ramaining_questions, correctOrWrong;
    [SerializeField] private int startQuestionsCount;
    private int questionsCount;

    [SerializeField] private List<Button> options;
    [SerializeField] private Button questionImageBtn;
    [SerializeField] private Image questionImage;
    [SerializeField] private GameObject TypeSelectBlock, QuestionBlock, AnswerReturnBlock, timeOutBlock;
    [SerializeField] private Color correctCol, wrongCol;

    [SerializeField] private Stage3Manager quiz_manager;
    [SerializeField] private Timer timerObj;
    private FinalQuestion question;
    private bool answered;
    private void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }
    void Start()
    {
        questionImageBtn.interactable = false;
        questionImage.enabled = false;
        NextLevel(1);
        if (!quiz_manager.isEmpty())
        {
            ramaining_questions.text = startQuestionsCount.ToString();
            questionsCount = startQuestionsCount;

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isRemaining()
    {
        if (questionsCount > 0)
            return true;
        else
            return false;
    }
    public void resetRound()
    {
            if (!quiz_manager.isEmpty() && !isRemaining())
            {
                ramaining_questions.text = startQuestionsCount.ToString();
                questionsCount = startQuestionsCount;
            }
            else
                Debug.Log("lists are empty");
    }

    private void setActiveRecursivly(Transform parent, bool activeState)
    {
        parent.gameObject.SetActive(activeState);
        foreach (Transform child in parent)
            setActiveRecursivly(child, activeState);
    }
    private void TypeSelectBlock_setActive(bool state)
    {
        setActiveRecursivly(TypeSelectBlock.transform, state);
    }
    private void QuestionBlock_setActive(bool state)
    {
        setActiveRecursivly(QuestionBlock.transform, state);
    }
    private void AnswerReturnBlock_setActive(bool state)
    {
        setActiveRecursivly(AnswerReturnBlock.transform, state);
    }
    private void timeOutBlock_setActive(bool state)
    {
        setActiveRecursivly(timeOutBlock.transform, state);
    }
    public void NextLevel(int label)
    {
        if (label == 1)
        {
            TypeSelectBlock_setActive(true);
            QuestionBlock_setActive(false);
            AnswerReturnBlock_setActive(false);
            timeOutBlock_setActive(false);
        }
        else if (label == 2)
        {
            TypeSelectBlock_setActive(false);
            QuestionBlock_setActive(true);
            AnswerReturnBlock_setActive(false);
            timeOutBlock_setActive(false);
        }
        else if (label == 3)
        {
            TypeSelectBlock_setActive(false);
            QuestionBlock_setActive(false);
            AnswerReturnBlock_setActive(true);
            timeOutBlock_setActive(false);
        }
        else if (label == 4)
        {
            TypeSelectBlock_setActive(false);
            QuestionBlock_setActive(false);
            AnswerReturnBlock_setActive(false);
            timeOutBlock_setActive(true);
        }
        else
            print("block level is wrong");
    }

    public void setQuestion(FinalQuestion selectedQuestion, bool imageExistance)
    {
        NextLevel(2);
        question = selectedQuestion;
        questionImage.enabled = false;
        questionText.text = question.questionInfo;
        List<string> answerList = new List<string>(question.questionChoices);
        char CHAR = 'a';
        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TMP_Text>().text = CHAR++ +")"+ answerList[i];
            options[i].name = answerList[i];
        }
            ramaining_questions.text = (--questionsCount).ToString();
        if (imageExistance)
        {
            questionImageBtn.interactable = true;
            questionImage.sprite = question.questionImage;
        }
        else
            questionImageBtn.interactable = false;
    }
    private void OnClick(Button btn)
    {
        if (!answered)
        {
            answered = true;
            bool val = quiz_manager.Answer(btn.name);
            NextLevel(3);
            if (val)
            {
                correctOrWrong.text = "correct answer!";
                correctOrWrong.color = Color.green;
            }

            else
            {
                correctOrWrong.text = "wrong answer!";
                correctOrWrong.color = Color.red;
                SoundManager.instance.wrongSound();
            }
            timerObj.stopTimer();
        }
        answered = false;
    }
    public void imageSwiper()
    {
        if (questionImage.enabled)
        {
            questionImage.enabled = false;
        }
        else
        {
            questionImage.enabled = true;
        }
    }

    public void showAnswer()
    {
        //SoundManager.instance.uncoverAnswer();
        correctOrWrong.text = "the correct answer is: " + question.correctChoice;
        int i = 0;
        for(; i < options.Count; i++)
        {
            if(quiz_manager.Answer(options[i].name))
                break;
        }
        if (i == 5)
        {
            correctOrWrong.text ="error!!!!!!!!!!";
            correctOrWrong.color = Color.red;
        }
        string one = 
        correctOrWrong.text = "the correct answer is:" + options[i].GetComponentInChildren<TMP_Text>().text;
        correctOrWrong.color = Color.green;
        NextLevel(3);
        timerObj.stopTimer();
    }
}

