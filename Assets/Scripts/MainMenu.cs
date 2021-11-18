using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void UiButton(int id) {
        switch (id) {
            case 1: //Play Button
                SceneManager.LoadScene(1);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
