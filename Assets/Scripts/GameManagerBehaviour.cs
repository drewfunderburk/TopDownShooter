using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GameEvent();

public class GameManagerBehaviour : MonoBehaviour
{
    [SerializeField] private static bool _gameOver = false;
    [SerializeField] private HealthBehaviour _playerHealth;
    [SerializeField] private GameObject _gameOverScreen;

    public static GameEvent OnGameOver;

    public static bool GameOver
    {
        get { return _gameOver; }
    }

    private void Start()
    {
        OnGameOver += ResetPlayer;
    }

    private void Update()
    {
        _gameOver = _playerHealth.Health <= 0;

        _gameOverScreen.SetActive(_gameOver);
    }

    public void ResetPlayer()
    {
        _playerHealth.TakeDamage(-10);
        transform.position = Vector3.zero;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
