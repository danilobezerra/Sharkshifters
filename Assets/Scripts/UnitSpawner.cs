using UnityEngine;

public class UnitSpawner : MonoBehaviour {
	[SerializeField] private GameObject[] unitsPrefabs;

	[Range(.5f, 1.5f)] [SerializeField] private float coolDown;

	private float counter;
	
	private void Start() {
		ResetCounter();
	}
	
	private void Update() {
		if (counter < Time.time) {
			GameObject unit = InstantiateUnit();
			//transform.SetParent(unit.transform);

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
		counter = Time.time + Random.Range(0, coolDown);
	}
}
