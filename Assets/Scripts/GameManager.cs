using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void RestartLevel()
    {
        SceneManager.LoadScene(PlayState.Instance.currentScene);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
