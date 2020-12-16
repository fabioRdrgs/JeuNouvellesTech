using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void AppplicationQuit()
    {
        Debug.Log("Has quit");
        Application.Quit();
    }
}
