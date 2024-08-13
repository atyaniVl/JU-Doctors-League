using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource sus;
    [SerializeField] AudioSource sus2;
    [SerializeField] AudioClip click, correct, wrong, timeout, transition, timer, uncover;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        sus = GetComponent<AudioSource>();
    }
    public void timerSound()
    {
        sus2.Play();
    }
    public void timerSoundEnd()
    {
        sus2.Stop();
    }
    public void wipeSound()
    {
        sus.PlayOneShot(transition);
    }
    public void timeoutSound()
    {
        sus.PlayOneShot(timeout);
    }
    public void uncoverAnswer()
    {
        sus.PlayOneShot(uncover);
    }
    public void wrongSound()
    {
        sus.PlayOneShot(wrong);
    }
    public void correctSound()
    {
        sus.PlayOneShot(correct);
    }
    public void clickSound()
    {
        sus.PlayOneShot(click);
    }

}
