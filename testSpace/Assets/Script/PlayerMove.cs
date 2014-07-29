using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	
	Vector3 speed;
	public float jumpSpeed = 1.0F;
	public float    rotateSpeed=10f;
	public float gravity = 20.0F;
	
//	private Vector3 moveDirection = Vector3.zero;
	private    CharacterController controller;
	private	 Rigidbody mRigidBody;

	public Vector3 mForcePosR;
	public Vector3 mForcePosL;

//	float mCameraRotX = 0f;
//	float mCameraRotY = 0f;
//	float mCameraRotZ = 0f;

	enum STATE{
		STATE_MOVE,
		STATE_JUMP,

	}

	STATE state;

	public OVRCamera ovrCamera;

	public float power = 1.0f;

	// Use this for initialization
	void Start () {

		// 通常状態.
		state = STATE.STATE_MOVE;

		mRigidBody = GetComponent<Rigidbody>();

		mForcePosL = new Vector3(-0.25f,0.0f,0.0f);
		mForcePosR = new Vector3(0.25f,0.0f,0.0f);

	}
	
	// Update is called once per frame
	void Update () {

		switch(state){
		case STATE.STATE_MOVE:
			moveControl();
			break;
		case STATE.STATE_JUMP:
			jumpControl();
			break;

		}
	}

	void jumpControl() {
	
		//transform.position += speed * jumpSpeed;

		if(Input.GetButtonDown("Jump")){
			
			state = STATE.STATE_MOVE;
			//speed = ovrCamera.transform.forward;

		}

		//デバッグ用戻し（RBで初期位置にもどります）.
		if(Input.GetButtonDown("jetR")){
			transform.position = Vector3.zero;
			state = STATE.STATE_MOVE;
			speed = Vector3.zero;

		}

	}
	
	// 主に体の回転
	void moveControl (){

		// rotate
		float rotX = Input.GetAxisRaw("Vertical2");
		float rotY = Input.GetAxisRaw("Horizontal2");
		float rotZ = 0.0f;
		
		// final direction
		Vector3 rotateVec = new Vector3(rotX,rotY,rotZ);
		
		mRigidBody.transform.Rotate(rotateVec);

// ぷれいやーの移動はとりあえず消します.
//		// powers
//		float powerY = 0.0f;
//		float powerX = Input.GetAxisRaw("Horizontal");
//		float powerZ = Input.GetAxisRaw("Vertical");
//
//		// RButton
//		if(Input.GetButton("jetR")){
//			powerY = 1.0f;
//		}
//		// RTrigger
//		if(Input.GetAxisRaw("RT") != 0.0f){
//			powerY = -1.0f;
//		}
//
//		// final direction
//		Vector3 powerVec = new Vector3(powerX,powerY,powerZ);
//
//		powerVec = powerVec.normalized;
//		
//		//mRigidBody.AddForce(powerVec * power);
//
//		Vector3 front = Camera.mainCamera.transform.TransformDirection(Vector3.forward);
//		Vector3 right = Camera.mainCamera.transform.TransformDirection(Vector3.right);
//		Vector3 up =  Camera.mainCamera.transform.TransformDirection(Vector3.up);
//
//		powerVec = powerX * right + powerY * up + powerZ * front;
//		powerVec *= power;
//		rigidbody.AddForce(powerVec);


		if(Input.GetButtonDown("Jump")){

			transform.parent = null;

			state = STATE.STATE_JUMP;

			// この時向いている方向に飛んでく.
			speed = Camera.main.transform.TransformDirection(Vector3.forward);
			speed = speed.normalized;

			rigidbody.AddForce(speed * jumpSpeed);
		}
	}

	// 壁とかに当たった時.
	void OnCollisionEnter(Collision col)
	{
		if (state == STATE.STATE_JUMP) {
			speed = Vector3.zero;
			state = STATE.STATE_MOVE;
			rigidbody.velocity = Vector3.zero;
			transform.parent = col.gameObject.transform.parent;
		}
	}


}