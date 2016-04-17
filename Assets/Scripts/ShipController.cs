using UnityEngine;

public class ShipController : MonoBehaviour {
	private delegate void Movement(float xMove);
	private delegate void Attack();
	
	private event Movement OnMove;
	private event Attack OnAttack;
	
	[SerializeField] private string horizontalAxisName;
	[SerializeField] private string fireButtonName;
	
	private void Start() {
		var movement = GetComponent<ShipMovement>();
		this.OnMove += movement.HandleMovement;
		
		var attack = GetComponent<ShipAttack>();
		this.OnAttack += attack.HandleAttack;
	}
	
	private void Update() {
		if (OnMove != null) {
			OnMove(Input.GetAxis(horizontalAxisName));
		}
		
		if (OnAttack != null) {
			if (Input.GetButtonDown(fireButtonName)) OnAttack();
		}
	}
}
