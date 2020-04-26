using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPause){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUi.SetActive(false);
        Time.timeScale =1f;
        GameIsPause = false;
    }
    void Pause(){
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
    
}
