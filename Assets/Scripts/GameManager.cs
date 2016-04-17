using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	
	private enum GameState { Playing, HighScore, GameOver }
	private GameState state = GameState.Playing;
	
	[SerializeField] private Text scoreText;
	[SerializeField] private Text healthText;
	
	[SerializeField] private string gameOverSceneName;
	[SerializeField] private string mainMenuSceneName;
	
	private int _score = 100;
	public int score {
		get { return _score; }
		set { 
			scoreText.text = string.Format("Score: {0:00000}", value);
			_score = value;
		}
	}
	
	private float _health = 1f;
	public float health {
		get { return _health; }
		set {
			healthText.text = string.Format("Health: {0:000}%", value * 100);
			_health = value;
		}
	}
	 
	private void Start() {
		if (!instance) {
			instance = gameObject.GetComponent<GameManager>();
		}
		
		DontDestroyOnLoad(gameObject);
	}
	
	private void Update() {
		switch (state) {
			case GameState.Playing:
				if (_health <= 0 | _score <= 0) {
					SceneManager.LoadScene(gameOverSceneName);
					state = GameState.HighScore;
				}
				
				break;
			case GameState.HighScore:
				// TODO: Show Score
				state = GameState.GameOver;
				break;
			case GameState.GameOver:
				if (Input.anyKeyDown) {
					SceneManager.LoadScene(mainMenuSceneName);
					Destroy(gameObject);
				}
				
				break;
		}
	}
}
