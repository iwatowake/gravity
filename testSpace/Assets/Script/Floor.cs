using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	float angle = 0f;
	public float rotSpeed = 1f;
	public float localy = 5f;
	Vector3 rotation;
	// Use this for initialization
	void Start () {

		rotation = new Vector3(0f,0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		Rotate();
	}

	void Rotate(){

		angle += rotSpeed * Time.deltaTime;
		rotation.z = rotSpeed;
		transform.Rotate(rotation);
	}
}
