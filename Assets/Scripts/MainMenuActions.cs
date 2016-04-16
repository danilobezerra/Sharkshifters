using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour {
	[SerializeField] private string gameSceneName;
	
	private bool gameIsReady = false;
	
	private void Update() {
		if (gameIsReady && Input.anyKeyDown) {
			SceneManager.LoadScene(gameSceneName);
		}
	}
	
	public void SetGameReady() {
		if (!gameIsReady) gameIsReady = true;
	}
}
