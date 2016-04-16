using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	private delegate void Attack();
	
	private event Attack OnShoot;
	
	[SerializeField] private GameObject projectilePrefab;

	[SerializeField] private Transform startingPoint;
	
	[SerializeField] private float projectileVelocity;
	
	[SerializeField] private AudioClip shootingSound;
	
	[Range(1.5f, 3f)] [SerializeField] private float coolDown;
	
	private float counter;
	
	
	private void Start() {
		this.OnShoot += HandleAttack;
	}
	
	private void Update() {
		if (this.OnShoot != null) {
			this.OnShoot();
		}
	}
	
	private void HandleAttack() {
		if (counter < Time.time) {
			GameObject instance = InstantiateProjetile();
		
			var projectile = instance.GetComponent<ProjectileController>();
			projectile.OnHitOther += HitPlayerAction;
			projectile.OnShoot += ShootAction;

			ResetCounter();
		}
	}
	
	private GameObject InstantiateProjetile() {
		Vector2 shotPosition = startingPoint.transform.position;
		AudioSource.PlayClipAtPoint(shootingSound, shotPosition);
		
		Object obj = Instantiate(projectilePrefab, shotPosition, Quaternion.identity);
		return obj as GameObject;
	}
	
	private void ShootAction(Transform transform) {
		transform.Translate(Vector2.down * Time.deltaTime * projectileVelocity);
	}
	
	private void HitPlayerAction(ProjectileController projectile, GameObject other) {
		if (other.CompareTag("Player")) {
			Debug.Log("Player Hit!!");
			
			projectile.Discard();
			// TODO: Decrease player health
			GameManager.instance.health -= Random.Range(0.05f ,0.1f);
		}
	}
	
	private void ResetCounter() {
		counter = Time.time + Random.Range(0, coolDown);
	}
}
