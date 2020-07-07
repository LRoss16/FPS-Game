using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    public void onClick()
    {
        SceneManager.LoadScene("ForestScene");

    }
}
