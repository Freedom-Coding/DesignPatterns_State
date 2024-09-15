using UnityEngine;
using System.Collections;

public class ExampleShipControl : MonoBehaviour {

	[SerializeField] float acceleration_amount = 1f;
	[SerializeField] float rotation_speed = 1f;
	[SerializeField] private Rigidbody2D rb;

	private void Update () 
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		rb.AddForce(verticalInput * transform.up * acceleration_amount * Time.deltaTime);
		rb.AddTorque(-horizontalInput * rotation_speed * Time.deltaTime);
	}
}
