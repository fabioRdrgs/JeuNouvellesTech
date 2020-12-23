using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        //Nous fait aller sur la scene du jeux
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
