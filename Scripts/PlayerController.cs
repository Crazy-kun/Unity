using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	private Vector3 angleVelocity;
	private Rigidbody rb;
	public Boundary boundary;
	public float tilt;
	public Text info;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		float moveHorizotnal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizotnal, 0f, moveVertical);
		rb.velocity = movement*speed;

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0f, 0f, rb.velocity.x*-tilt);

		info.text = "moveHorizotnal: " + moveHorizotnal.ToString();
		info.text += "\n rb.velocity.x: " + rb.velocity.x.ToString();
		info.text += "\n rb.rotation.x: " + rb.rotation.x.ToString();
	}

	void Update()
	{
		if (Input.GetButton("Jump") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
