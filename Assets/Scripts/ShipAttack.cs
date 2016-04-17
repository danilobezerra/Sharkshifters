using UnityEngine;

public class ShipAttack : MonoBehaviour {	
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform startingPoint;
	[SerializeField] private float projectileVelocity;
	[SerializeField] private AudioClip shootingSound;
	
	public void HandleAttack() {
		GameObject instance = InstantiateProjetile();
		
		var projectile = instance.GetComponent<ProjectileController>();
		projectile.OnHitOther += HitEnemyAction;
		projectile.OnShoot += ShootAction;
	}
	
	private GameObject InstantiateProjetile() {
		Vector2 shotPosition = startingPoint.transform.position;
		AudioSource.PlayClipAtPoint(shootingSound, shotPosition);
		
		Object obj = Instantiate(projectilePrefab, shotPosition, Quaternion.identity);
		return obj as GameObject;
	}
	
	private void ShootAction(Transform transform) {
		transform.Translate(Vector2.up * Time.deltaTime * projectileVelocity);
	}
	
	private void HitEnemyAction(ProjectileController projectile, GameObject other) {
		if (other.CompareTag("Enemy")) {
			projectile.Discard();
			// TODO: Kill cyborg shark
			GameManager.instance.score += 50;
			//Destroy(other.gameObject);
			var attack = other.GetComponent<EnemyAttack>();
			attack.BreakArmor();
			
			var movement = other.GetComponent<UnitMovement>();
			movement.ChangeMovement();
		} else if (other.CompareTag("NotEnemy")) {
			projectile.Discard();
			// TOOD: Kill seagull
			GameManager.instance.score -= 100;
			Destroy(other.gameObject);
		}
	}
}
