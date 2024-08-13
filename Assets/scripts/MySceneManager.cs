using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
     public void LoadNextScene(int i)
     {
         SceneManager.LoadScene(i);
     }
    public void quit()
    {
        Application.Quit();
    }
}
