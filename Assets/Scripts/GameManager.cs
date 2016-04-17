using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	
	public enum GameState { Playing, HighScore, GameOver }
	private GameState _state = GameState.Playing;
	public GameState gameState {
		get { return _state; }
		set { _state = value; }
	}
	
	[SerializeField] private Text scoreText;
	[SerializeField] private Text healthText;
	
	[SerializeField] private string gameOverSceneName;
	[SerializeField] private string mainMenuSceneName;
	
	private int _score = 150;
	public int score {
		get { return _score; }
		set {
			if (value > 0) scoreText.text = string.Format("Score: {0:00000}", value);
			_score = value;
		}
	}
	
	private float _health = 1f;
	public float health {
		get { return _health; }
		set {
			if (value > 0) healthText.text = string.Format("Health: {0:000}%", value * 100);
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
		switch (_state) {
			case GameState.Playing:
				if (_health <= 0 | _score <= 0) {
					SceneManager.LoadScene(gameOverSceneName);
					_state = GameState.HighScore;
				}
				
				break;
			case GameState.HighScore:
				GameObject ui = GameObject.FindWithTag("UI");
				var actions = ui.GetComponent<GameOverActions>();
				actions.scoreText = string.Format("{0:00000}", _score > 0 ? _score : 0);
				
				//_state = GameState.GameOver;
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
