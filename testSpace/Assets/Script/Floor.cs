using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	float angle = 0f;
	public float rotSpeed = 1f;
	public float localy = 5f;
	Vector3 rotation;

	public Vector3 targetPos;
	public float moveSpeed = 5f;

	public bool isEnd = false;		// 端っこかどうか.

	bool isRotate;
	// Use this for initialization
	void Start () {

		rotation = new Vector3(0f,0f,0f);
		//targetPos = new Vector3(0f,0f,100f);
	}
	
	// Update is called once per frame
	void Update () {
		MoveToTargetPos();
		Rotate();
	}

	// 回転.
	void Rotate(){
		if(isRotate){
			angle += rotSpeed * Time.deltaTime;
			rotation.z = rotSpeed;
			transform.Rotate(rotation);
		}

	}

	// 規定位置までの移動.
	void MoveToTargetPos(){

		Vector3 dist = targetPos - transform.position;

		if(dist.magnitude >= 10f){
			dist = dist.normalized;

			transform.position += dist * moveSpeed;
		}
		else{
			transform.position = targetPos;
		}

	}

	// 壁とかに当たった時.
	void OnCollisionEnter(Collision col)
	{
		int a = 0;
		Debug.Log("hit");
	}

	public void sendEndMessage(){
		isEnd = false;

	}

	public void setTargetPos(Vector3 pos){
		targetPos = pos;
	}

	public void setRotate(bool flg){
		isRotate = flg;
	}
	

	// 端っこかをセット.
	public void setEnd(bool flg){
		isEnd = flg;
		GameObject.Find("FloorManager").GetComponent<FloorManager>().SetCreate();
	}

	// 

}