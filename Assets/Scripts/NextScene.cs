using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    public void loadNextScene() {
        if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
