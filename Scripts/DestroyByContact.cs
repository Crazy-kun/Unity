using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{   
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameControllerScript;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
			gameControllerScript = gameControllerObject.GetComponent<GameController>();
		if (gameControllerScript == null)
			Debug.Log("Cannot fond 'GameController' script");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
			return;
		Instantiate(explosion, transform.position, transform.rotation);
		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameControllerScript.GameOver();
		}
		gameControllerScript.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
