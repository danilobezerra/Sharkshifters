using UnityEngine;

public class ProjectileController : MonoBehaviour {
	[SerializeField] private AudioClip hitSound;
	
	public delegate void ShootAction(Transform transform);
	public delegate void HitAction(GameObject projectile);
	
	public event ShootAction OnShoot;
	public event HitAction OnHitOther;
	
	private void Update() {
		if (OnShoot != null) {
			OnShoot(transform);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (OnHitOther != null) {
			AudioSource.PlayClipAtPoint(hitSound, transform.position);
			OnHitOther(other.gameObject);
			Destroy(gameObject);
		}
	}
	
	private void OnBecameInvisible() {
		Destroy(gameObject);
	}
 }
