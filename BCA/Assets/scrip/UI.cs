using UnityEngine;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public int sceneNum;
    public void LoadScene()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneNum);
        
    }
}
