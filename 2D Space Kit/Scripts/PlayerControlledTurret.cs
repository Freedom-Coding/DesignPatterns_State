using UnityEngine;
using System.Collections;
using StrategyPattern;

public class PlayerControlledTurret : MonoBehaviour {

	[SerializeField] private GameObject guidedMissilePrefab;
	[SerializeField] private GameObject piercingMissilePrefab;
	[SerializeField] private GameObject bouncingMissilePrefab;
	[SerializeField] private GameObject[] barrels;
	[SerializeField] private float rotationSpeed = 4f;
	[SerializeField] private float shotSpeed = 400;
	[SerializeField] private MissileTypeSelector missileTypeSelector;
	[SerializeField] private Transform lockedEnemy;
	
	private int barrel_index = 0;

	private void Update () 
	{
		Vector2 turretPosition = Camera.main.WorldToScreenPoint(transform.position);
		Vector2 direction = (Vector2)Input.mousePosition - turretPosition;
		transform.rotation = Quaternion.Euler (new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, 
			(Mathf.Atan2 (direction.y,direction.x) * Mathf.Rad2Deg) - 90f, rotationSpeed * Time.deltaTime)));

		if (Input.GetMouseButtonDown(0) && barrels != null) 
		{
			IMissileStrategy missileStrategy = null;
			GameObject missileToInstantiate = null;

			switch (missileTypeSelector.selectedMissile)
            {
				case MissileType.Guided:
					missileStrategy = new GuidedMissileStrategy(lockedEnemy, 3f);
					missileToInstantiate = guidedMissilePrefab;
					break;
				case MissileType.Bouncing:
					missileStrategy = new BouncingMissileStrategy(transform.up);
					missileToInstantiate = bouncingMissilePrefab;
					break;
				case MissileType.Piercing:
					missileStrategy = new PiercingMissileStrategy(transform.up);
					missileToInstantiate = piercingMissilePrefab;
					break;
            }

			GameObject bullet = Instantiate(missileToInstantiate, barrels[barrel_index].transform.position, transform.rotation);
			bullet.GetComponent<Missile>().Initialize(missileStrategy, shotSpeed);

			barrel_index++;
			
			if (barrel_index >= barrels.Length)
            {
				barrel_index = 0;
			}
		}
	}
}
