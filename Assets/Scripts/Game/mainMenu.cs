using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Close()
    {
        Application.Quit();
        Debug.Log("Salir");
    }
}
