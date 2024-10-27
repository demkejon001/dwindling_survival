using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadPlayingScene()
    {
        SceneManager.LoadScene(1);
    }
}
