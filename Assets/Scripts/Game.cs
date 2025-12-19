using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public event Action GameStarted;
    public event Action GameEnded;
    
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;        
        _startScreen.Open();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;        
        _endGameScreen.Open();
        GameEnded?.Invoke();
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        StartGame();
    }
    private void OnPlayButtonClick()
    {
        _startScreen.Close();        
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;        
        _player.Reset();
        GameStarted?.Invoke();
    }
}
