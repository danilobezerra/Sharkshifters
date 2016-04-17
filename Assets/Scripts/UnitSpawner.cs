using UnityEngine;

public class UnitSpawner : MonoBehaviour {
	private const float MIN_SPAWN_COOLDOWN = .5f;
	private const float MAX_SPAWN_COOLDOWN = 1.5f;
	private const float MIN_ENEMY_COOLDOWN = 1.5f;
	private const float MAX_ENEMY_COOLDOWN = 3f;
	
	[SerializeField] private GameObject[] unitsPrefabs;

	[Range(MIN_SPAWN_COOLDOWN, MAX_SPAWN_COOLDOWN)] [SerializeField] private float spawnCoolDown;
	[Range(MIN_ENEMY_COOLDOWN, MAX_ENEMY_COOLDOWN)] [SerializeField] private float enemyCoolDown;

	private float counter;
	
	private void Start() {
		ResetCounter();
	}
	
	private void Update() {
		if (counter < Time.time) {
			GameObject unit = InstantiateUnit();
			
			if (unit.CompareTag("Enemy")) {
				var attack = unit.GetComponent<EnemyAttack>();
				attack.coolDown = enemyCoolDown;
			}
			
			DecreaseSpawnCoolDown();
			DecreaseEnemyCoolDown();
			ResetCounter();
		}
	}
	
	private GameObject InstantiateUnit() {
		GameObject prefab = unitsPrefabs[Random.Range(0, unitsPrefabs.Length)];
		
		Vector2 instancePosition = transform.position;
		instancePosition.y = Random.Range(0, Camera.main.orthographicSize);
		
		Object instance = Instantiate(prefab, instancePosition, Quaternion.identity);
		return instance as GameObject;
	}
	
	private void ResetCounter() {
		counter = Time.time + Random.Range(0, spawnCoolDown);
	}
	
	public void DecreaseSpawnCoolDown() {
		if (spawnCoolDown > MIN_SPAWN_COOLDOWN) spawnCoolDown -= .01f;
	}
	
	public void DecreaseEnemyCoolDown() {
		if (enemyCoolDown > MIN_ENEMY_COOLDOWN) enemyCoolDown -= .01f;
	}
}
