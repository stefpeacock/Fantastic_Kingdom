using UnityEngine;
using System.Collections;

public class Barracks : MonoBehaviour {
	
	public GameObject PikemanPrefab;

	private Vector3 SpawnPoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnUnit", 5f, AllySpawner.SpawnTime);

		// Get spawn position
		GameObject gO = new GameObject ();
		gO.transform.position = transform.position;
		gO.transform.rotation = transform.rotation;
		gO.transform.Translate (new Vector3 (0, 1, 2));
		SpawnPoint = gO.transform.position;
	}

	void SpawnUnit(){
		if (AllySpawner.Allies.Count >= AllySpawner.PopulationCap)
			return;
		GameObject ally = (GameObject)Instantiate(PikemanPrefab, SpawnPoint, this.transform.rotation);
		ally.transform.Translate (new Vector3 (0 , 0, 4));
		Ally allyScript = ally.GetComponent<Ally>();
		allyScript.goldPile = GameObject.Find("GoldPile");
		allyScript.speed = 3;
		allyScript.enemiesLayer = AllySpawner.EnemiesLayer;
		allyScript.attackRangeTier2 = AllySpawner.attackRangeTier2;
		allyScript.attackRangeTier3 = AllySpawner.attackRangeTier3;
		AllySpawner.Allies.Add(ally);
	}
}
