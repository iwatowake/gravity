using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	
	public float speed = 6.0F;
	public float jumpSpeed = 1.0F;
	public float    rotateSpeed=10f;
	public float gravity = 20.0F;
	
	private Vector3 moveDirection = Vector3.zero;
	private    CharacterController controller;
	private	 Rigidbody mRigidBody;

	public Vector3 mForcePosR;
	public Vector3 mForcePosL;

	float mCameraRotX = 0f;
	float mCameraRotY = 0f;
	float mCameraRotZ = 0f;

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

		Vector3 speed = ovrCamera.transform.forward;
		transform.position += speed * jumpSpeed;

		if(Input.GetButtonDown("Jump")){
			
			state = STATE.STATE_MOVE;
		}

	}
	
	//標準的なジャンプコントロール
	void moveControl (){

		// rotate
		float rotX = Input.GetAxisRaw("Vertical2");
		float rotY = Input.GetAxisRaw("Horizontal2");
		float rotZ = 0.0f;
		
		// final direction
		Vector3 rotateVec = new Vector3(rotX,rotY,rotZ);
		
		mRigidBody.transform.Rotate(rotateVec);

		// powers
		float powerY = 0.0f;
		float powerX = Input.GetAxisRaw("Horizontal");
		float powerZ = Input.GetAxisRaw("Vertical");

		// RButton
		if(Input.GetButton("jetR")){
			powerY = 1.0f;
		}
		// RTrigger
		if(Input.GetAxisRaw("RT") != 0.0f){
			powerY = -1.0f;
		}

		// final direction
		Vector3 powerVec = new Vector3(powerX,powerY,powerZ);

		powerVec = powerVec.normalized;
		
		//mRigidBody.AddForce(powerVec * power);

		Vector3 front = Camera.mainCamera.transform.TransformDirection(Vector3.forward);
		Vector3 right = Camera.mainCamera.transform.TransformDirection(Vector3.right);
		Vector3 up =  Camera.mainCamera.transform.TransformDirection(Vector3.up);

		powerVec = powerX * right + powerY * up + powerZ * front;
		powerVec *= power;
		rigidbody.AddForce(powerVec);


		if(Input.GetButtonDown("Jump")){

			state = STATE.STATE_JUMP;
		}
//
//				// カメラの回転（仮）.
//		if(Input.GetKeyDown(KeyCode.UpArrow)){
//			mCameraRotX += 0.1f;
//		}
//		else if(Input.GetKeyDown(KeyCode.DownArrow)){
//			mCameraRotX -= 0.1f;
//		}
//		if(Input.GetKeyDown(KeyCode.RightArrow)){
//			mCameraRotY += 0.1f;
//
//		}
//		else if(Input.GetKeyDown(KeyCode.LeftArrow)){
//			mCameraRotY -= 0.1f;
//		}
//
////		Camera.mainCamera.transform.Rotate(new Vector3(mCameraRotX,mCameraRotY,mCameraRotZ));

	}
	
	//移動処理 
	void attachMove (){
		//moveDirection.y -= gravity * Time.deltaTime;
	}
	
	//キャラクターを進行方向へ向ける処理 
	void attachRotation(){
		var moveDirectionYzero = -moveDirection;
		moveDirectionYzero.y=0;

		//ベクトルの２乗の長さを返しそれが0.001以上なら方向を変える（０に近い数字なら方向を変えない） 
		if (moveDirectionYzero.sqrMagnitude > 0.001){
			
			//２点の角度をなだらかに繋げながら回転していく処理（stepがその変化するスピード） 
			float    step = rotateSpeed*Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward,moveDirectionYzero,step,0f);
			
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}
}