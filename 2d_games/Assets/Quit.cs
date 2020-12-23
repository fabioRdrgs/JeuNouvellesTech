using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void AppplicationQuit()
    {
        //Nous fait quitter l'application
        Debug.Log("Has quit");
        Application.Quit();
    }
}
