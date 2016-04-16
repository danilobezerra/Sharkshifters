using UnityEngine;

public class ProjectileController : MonoBehaviour {
	[SerializeField] private AudioClip hitSound;
	
	public delegate void ShootAction(Transform transform);
	public delegate void HitAction(ProjectileController projectile, GameObject other);
	
	public event ShootAction OnShoot;
	public event HitAction OnHitOther;
	
	private void Update() {
		if (OnShoot != null) {
			OnShoot(transform);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (OnHitOther != null) {
			OnHitOther(this.GetComponent<ProjectileController>(), other.gameObject);
		}
	}
	
	private void OnBecameInvisible() {
		Destroy(gameObject);
	}
	
	public void Discard() {
		AudioSource.PlayClipAtPoint(hitSound, transform.position);
		Destroy(gameObject);
	}
 }
