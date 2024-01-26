using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public void Btn_GamePlay(string _scene)
    {
        if(!string.IsNullOrEmpty(_scene))
            SceneManager.LoadScene(_scene);
    }
}
