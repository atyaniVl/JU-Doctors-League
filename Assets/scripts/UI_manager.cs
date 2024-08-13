using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_manager : MonoBehaviour
{
    [SerializeField] private int stage;
    [SerializeField] private TextMeshProUGUI questionText, ramaining_questions_For30, 
        ramaining_questions_For50, ramaining_questions_For_general, correctOrWrong;
    [SerializeField] private int startQuestionsCount30, startQuestionsCount50, startGeneralCount;
     private int generalQuestionsCount, questionsCount30, questionsCount50;

    [SerializeField] private List<Button> options;
    [SerializeField] private Button questionImageBtn;
    [SerializeField] private Image questionImage;
    [SerializeField] private GameObject TypeSelectBlock, QuestionBlock, AnswerReturnBlock, timeOutBlock;
    [SerializeField] private Color correctCol, wrongCol;

    [SerializeField] private QuizManager quiz_manager;
    [SerializeField] private Timer timerObj;
    private Question question;
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
        NextLevel(1);
        questionImageBtn.interactable = false;
        questionImage.enabled = false;
        if (!quiz_manager.isEmpty()&&stage==1)
        {
            ramaining_questions_For30.text = startQuestionsCount30.ToString();
            ramaining_questions_For50.text = startQuestionsCount50.ToString();
            ramaining_questions_For_general.text = startGeneralCount.ToString();
            questionsCount30 = startQuestionsCount30;
            questionsCount50 = startQuestionsCount50;
            generalQuestionsCount = startGeneralCount;
        }
        else if (!quiz_manager.isEmpty2() && stage == 2)
        {
            ramaining_questions_For30.text = startQuestionsCount30.ToString();
            questionsCount30 = startQuestionsCount30;
        }
        else
            Debug.Log("lists are empty");

    }
    public bool isRemaining(int level)
    {
        if (level == 30)
        {
            if (questionsCount30 > 0)
                return true;
            else
                return false;
        }
        else if (level == 50)
        {
            if (questionsCount50 > 0)
                return true;
            else
                return false;
        }
        else if (level == 10)
        {
            if (generalQuestionsCount > 0)
                return true;
            else
                return false;

        }
        else
            return false;
    }
    public void resetRound()
    {
        if(stage==1)
        {
            if (!quiz_manager.isEmpty()&& !isRemaining(50)&& !isRemaining(30) && !isRemaining(10))
            {
                ramaining_questions_For30.text = startQuestionsCount30.ToString();
                ramaining_questions_For50.text = startQuestionsCount50.ToString();
                ramaining_questions_For_general.text = startGeneralCount.ToString();
                questionsCount30 = startQuestionsCount30;
                questionsCount50 = startQuestionsCount50;
                generalQuestionsCount = startGeneralCount;
            }
            else
                Debug.Log("lists are empty");
        }
        else if (stage == 2)
        {
            if (!quiz_manager.isEmpty2() && !isRemaining(30))
            {
                ramaining_questions_For30.text = startQuestionsCount30.ToString();
                questionsCount30 = startQuestionsCount30;
            }
            else
                Debug.Log("lists are empty");
        }
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
    public void setQuestion(Question questionWasSelected, int questionLevel, bool imageExistance)
    {
        NextLevel(2);
        questionImage.enabled = false;
        question = questionWasSelected;
        questionText.text = question.questionInfo;
        List<string> answerList = new List<string>(question.questionChoices);
        char CHAR = 'a';
        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TMP_Text>().text =CHAR++ +")"+ answerList[i];
            options[i].name = answerList[i];
        }
        if (questionLevel == 30)
            ramaining_questions_For30.text = (--questionsCount30).ToString();
        else if (questionLevel == 50)
            ramaining_questions_For50.text = (--questionsCount50).ToString();
        else if (questionLevel == 10)
            ramaining_questions_For_general.text = (--generalQuestionsCount).ToString();
        if (imageExistance)
        {
            questionImageBtn.interactable = true;
            questionImage.sprite = question.questionImage;
        }
        else
            questionImageBtn.interactable = false;
    }
    public void imageSwiper()
    {
        if(questionImage.enabled)
        {
            questionImage.enabled = false;
        }
        else
        {
            questionImage.enabled = true;
        }
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
                SoundManager.instance.correctSound();
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

}
