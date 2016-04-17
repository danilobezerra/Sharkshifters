using UnityEngine;
using UnityEngine.UI;

public class GameOverActions : MonoBehaviour {
	[SerializeField] private Text _scoreText;
	public string scoreText {
		get { return _scoreText.text; }
		set { _scoreText.text = value; }
	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}
	
	public void SetGameOverState() {
		GameManager.instance.gameState = GameManager.GameState.GameOver;
	}
}
