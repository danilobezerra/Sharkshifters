using UnityEngine;
public class UnitMovement : MonoBehaviour {
	public delegate void Movement();
	
	public event Movement OnMove;
	
	[SerializeField] private float _velocity;
	public float velocity {
		get { return _velocity; }
		set { _velocity = value; }
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
			GameManager.instance.score -= 25;
		}
		
		Destroy(gameObject);
	}
	
	private void HandleMovement() {
		transform.Translate(Vector2.left * Time.deltaTime * _velocity);	
	}
}
