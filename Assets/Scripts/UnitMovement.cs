using UnityEngine;
public class UnitMovement : MonoBehaviour {
	public delegate void Movement();
	
	public event Movement OnMove;
	
	[SerializeField] private float _velocity;
	public float velocity {
		get { return _velocity; }
		set { _velocity = value; }
	}
	
	private Collider2D hitBox;
	
	private void Awake() {
		hitBox = GetComponent<Collider2D>();
	}
	
	private void Start() {
		this.OnMove += HandleMovement;
	}
	
	private void Update() {
		if (this.OnMove != null) {
			this.OnMove();
		}
	}
	
	private void OnBecameInvisible() {
		if (this.CompareTag("Enemy")) {
			GameManager.instance.score -= 10;
		}
		
		Destroy(gameObject);
	}
	
	private void HandleMovement() {
		transform.Translate(Vector2.left * Time.deltaTime * _velocity);	
	}
	
	private void HandleEnemyDeath() {
		transform.Translate(Vector2.down * Time.deltaTime * (_velocity * 2));	
	}
	
	public void ChangeMovement() {
		this.OnMove = null;
		hitBox.enabled = false;
		
		this.OnMove += HandleEnemyDeath;
	}
}
