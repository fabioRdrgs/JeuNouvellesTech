using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameOver : MonoBehaviour
{
    public void RestartGame()
    {
        //Charge la scene de game over
        SceneManager.LoadScene(0);
    }
}
