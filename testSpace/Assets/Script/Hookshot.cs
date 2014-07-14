using UnityEngine;
using System.Collections;

public class Hookshot : MonoBehaviour {

	public bool 		isShot 		= false;
	public GameObject	targetObj 	= null;
	public float		maxLen		= 500.0f;
	public float		minLen		= 1.0f;
	public float		pow			= 0.05f;
	public Vector3		hitPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawLine( amera.mainCamera.transform.position + Vector3.down * 0.5f
		Debug.DrawRay( Camera.mainCamera.transform.position + Vector3.down * 0.5f, Camera.mainCamera.transform.forward );
		shot();
		hookin();
	}

	// 飛ぶ.
	void hookin(){
		if( isShot == false )
			return;

		Vector3 dir = hitPos - transform.position;
		float	len = dir.magnitude;

		if( len < minLen ){
			isShot = false;
			targetObj = null;
		}

		transform.position += dir * pow;
	}

	// 発射.
	void shot(){
		if( isShot == true )
			return;

		// RButton
		if(Input.GetButtonDown("RB")){
			RaycastHit hit;
			// カメラ方向にレイを飛ばす.
			Vector3 dir = Camera.mainCamera.transform.forward;
			if( Physics.Raycast( Camera.mainCamera.transform.position, dir, out hit, Mathf.Infinity ) )
			{
				if( hit.collider.gameObject == null )
					return;
				if( hit.collider.gameObject.layer == 8 )
					return;
				Debug.Log( hit.collider.gameObject.layer );
				targetObj = hit.collider.gameObject;
				hitPos = hit.point;
				isShot = true;
			}
		}
	}
}
