using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class gameManager : MonoBehaviour
{
    public static bool isRestart = false;

    public void restartGame()
    {
        isRestart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
    }

    public void exitGame()
    {
            #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        Application.Quit();

    }




}
