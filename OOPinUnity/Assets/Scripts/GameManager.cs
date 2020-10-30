/*
 * Anthony Wessel
 * Assignment 6
 * Manages the game state and loads and unloads levels
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class GameManager : Singleton<GameManager>
{
    public int score;

    public GameObject pauseMenu;
    public Text pauseMenuTitle;
    public Text resumeButtonText;

    private string CurrentLevelName = string.Empty;

    // methods to load and unload scenes
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }
        CurrentLevelName = levelName;

        // Activate the new scene
        StartCoroutine(ActivateScene(levelName));

        // Make sure that the pause menu is hidden
        currentPausedState = GameState.PAUSED;
        Unpause();
    }

    // Activate a scene after loading it, so that all instantiated objects will
    // be put in the right scene
    IEnumerator ActivateScene(string levelName)
    {
        Scene s = SceneManager.GetSceneByName(levelName);
        while (!s.isLoaded)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(s);
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + levelName);
            return;
        }
    }

    public void UnloadCurrentLevel()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(CurrentLevelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + CurrentLevelName);
            return;
        }
    }


    public enum GameState
    {
        PAUSED,
        WON,
        LOST
    }
    GameState currentPausedState = GameState.PAUSED;

    public void Pause(GameState state)
    {
        currentPausedState = state;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        switch (state)
        {
            case GameState.PAUSED:
                pauseMenuTitle.text = "Paused";
                resumeButtonText.text = "Resume";
                break;
            case GameState.WON:
                pauseMenuTitle.text = "You win!";
                resumeButtonText.text = "Replay";
                break;
            case GameState.LOST:
                pauseMenuTitle.text = "You lost!";
                resumeButtonText.text = "Try again";
                break;
        }
    }
    public void Pause()
    {
        Pause(GameState.PAUSED);
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        if (currentPausedState != GameState.PAUSED)
        {
            UnloadCurrentLevel();
            LoadLevel("GameScene");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pause();
    }
}
