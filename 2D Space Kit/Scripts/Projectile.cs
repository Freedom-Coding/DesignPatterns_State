using UnityEngine;

public class Projectile : MonoBehaviour {
	public GameObject shoot_effect;
	public GameObject hit_effect;
	public GameObject firing_ship;
	
	void Start () {
		GameObject obj = Instantiate(shoot_effect, transform.position  - new Vector3(0,0,5), Quaternion.identity);
		obj.transform.parent = firing_ship.transform;
		Destroy(gameObject, 5f);
	}
	
	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.gameObject != firing_ship && col.gameObject.tag != "Projectile") 
		{
			Instantiate(hit_effect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}