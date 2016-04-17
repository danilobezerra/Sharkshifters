using UnityEngine;

public class ShipMovement : MonoBehaviour {
	[SerializeField] private float velocity;
	
	private Collider2D hitBox;
	private float xMin, xMax;
	
	private void Awake() {
		hitBox = GetComponent<Collider2D>();
	}
	
	private void Start() {
		float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		float halfSpriteWidth = hitBox.bounds.size.x / 2;	
		
		xMin = halfSpriteWidth - screenWidth;
		xMax = screenWidth - halfSpriteWidth;
	}
	
	public void HandleMovement(float xMove) {
		Vector2 translation = new Vector2(xMove, 0);
		transform.Translate(translation * Time.deltaTime * velocity);
		
		Vector2 position = transform.position;
		position.x = Mathf.Clamp(position.x, xMin, xMax);
		transform.position = position;
	}
}
