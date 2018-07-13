using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameManagerClass Stats;
    public GameObject Grid;
    GameObject currentGrid;
	void Awake ()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        GameplayUI.SetActive(false);
        MainMenuUI.SetActive(false);
        FrontScreenUI.SetActive(true);     
    }


    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape) )
        {
            NextLevel();
        }
    }
    public void NextLevel()
    {
        Destroy(currentGrid);
        Stats.NextLevel();
        currentGrid = Instantiate(Grid, new Vector3(0, 10, 0), Quaternion.identity);
        currentGrid.name = "Grid";
    }

    public void ReloadLevel()
    {
        Destroy(currentGrid);
        currentGrid = Instantiate(Grid, new Vector3(0, 10, 0), Quaternion.identity);
        currentGrid.name = "Grid";
    }


    [SerializeField]
     GameObject GameplayUI, MainMenuUI, FrontScreenUI;

    public void OpenMenu()
    {
        GameplayUI.SetActive(false);
        FrontScreenUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void StartGame()
    {
        GameplayUI.SetActive(true);
        FrontScreenUI.SetActive(false);
        MainMenuUI.SetActive(false);
        Stats = new GameManagerClass();
        currentGrid = Instantiate(Grid, new Vector3(0, 10, 0), Quaternion.identity);
        currentGrid.name = "Grid";
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
    #else
         Application.Quit();
    #endif
    }



}



