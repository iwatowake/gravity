using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {


	public GameObject floorAround;
	public GameObject floorCross;
	public GameObject floorCrossX;
	public GameObject floorVertical;
	public GameObject floorHorizontal;
	public GameObject floorNone;

	// 床の種類
	enum FLOOR_ID{

		FLOOR_AROUND,
		FLOOR_CROSS,
		FLOOR_CROSSX,
		FLOOR_VERTICAL,
		FLOOR_HORIZONTAL,
		// ↓回転する.
		FLOOR_CROSS_R,
		FLOOR_CROSSX_R,
		FLOOR_VERTICAL_R,
		FLOOR_HORIZONTAL_R,

		FLOOR_NONE,
		FLOOR_MAX,
	};

	// 状態.
	enum FLOOR_STATE {
		STATE_INIT,
		STATE_WAIT,
		STATE_CREATE,
	};

	FLOOR_STATE state = FLOOR_STATE.STATE_INIT;

	public int floorNumBase = 12;		// 床生成数.
	int floorNum;
	public int floorSpan = 20;		// 床感覚.

	Vector3 floorPos; // 停止位置.
	Vector3 generatePos;	// 生成位置.

	public float generateTime = 30f;	// 生成タイミング
	public float timerSpeed = 10f;		// カウントの速さ.
	float waitTime = 0f;				// カウンタ.

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		switch(state){

		case FLOOR_STATE.STATE_INIT:

			Init ();

			break;

		case FLOOR_STATE.STATE_WAIT:

			if(Input.GetKeyDown(KeyCode.A)){
				state = FLOOR_STATE.STATE_CREATE;
			}

			break;

		case FLOOR_STATE.STATE_CREATE:

			CreateFloors();

			break;
		}
	}

	// 初期処理.
	void Init(){

		floorNum = floorNumBase;
		floorPos = new Vector3(0f,0f,30f);
		generatePos = new Vector3(0f,0f,500f);

		GameObject floor;

		// 最初の床を作る.
		for(int i = 0; i < floorNumBase; i++ ){

			floor = SetFloorRandom();

			// 位置のセット.
			if(floor != null){
				floor.transform.position = floorPos;

				// 端っこ設定.
				if(i == floorNumBase - 1){
					floor.GetComponent<Floor>().setEnd(true);
				}

			}


			floorPos.z += floorSpan;
			generatePos.z += floorSpan;
		}

		state = FLOOR_STATE.STATE_CREATE;
	}

	// 床の生成.
	void CreateFloors(){

		waitTime += Time.deltaTime;

		GameObject floor = null;

		// 時間差で基底数床を生成していく.
		if(waitTime >= generateTime){
			
			waitTime = 0f;

			floor = SetFloorRandom();
			
			floorPos.z += floorSpan;
			generatePos.z += floorSpan;
			
			floorNum --;
			//for(int i = 0;i < floorNum;i++){
			if(floorNum <= 0){	

				if(floor != null){
					floor.GetComponent<Floor>().setEnd(true);
				}

				floorNum = floorNumBase;
				state = FLOOR_STATE.STATE_WAIT;

			}
		}
	}

	// 床の生成判定(共通部分)
	GameObject SetFloorRandom(){

		GameObject floor = null;

		//　仮にランダムでフロアを決める.
		int id = Random.Range(0,(int)FLOOR_ID.FLOOR_MAX);
		
		switch(id){
		case (int)FLOOR_ID.FLOOR_AROUND:
			
			floor = Object.Instantiate(floorAround) as GameObject;
			
			break;
			
		case (int)FLOOR_ID.FLOOR_NONE:
			
			floor = Object.Instantiate(floorNone) as GameObject;
			
			break;
			
		case (int)FLOOR_ID.FLOOR_CROSS:
			
			floor = Object.Instantiate(floorCross) as GameObject;
			
			break;
			
		case (int)FLOOR_ID.FLOOR_CROSSX:
			
			floor = Object.Instantiate(floorCrossX) as GameObject;
			
			break;
			
		case (int)FLOOR_ID.FLOOR_HORIZONTAL:
			
			floor = Object.Instantiate(floorHorizontal) as GameObject;
			
			break;
			
		case (int)FLOOR_ID.FLOOR_VERTICAL:
			
			floor = Object.Instantiate(floorVertical) as GameObject;
			
			break;
			
			
			// ここから回転するタイプ.
		case (int)FLOOR_ID.FLOOR_CROSS_R:
			
			floor = Object.Instantiate(floorCross) as GameObject;
			
			if(floor != null){
				floor.GetComponent<Floor>().setRotate(true);
			}
			
			break;
			
		case (int)FLOOR_ID.FLOOR_CROSSX_R:
			
			floor = Object.Instantiate(floorCrossX) as GameObject;
			
			if(floor != null){
				floor.GetComponent<Floor>().setRotate(true);
			}
			
			break;
			
		case (int)FLOOR_ID.FLOOR_HORIZONTAL_R:
			
			floor = Object.Instantiate(floorHorizontal) as GameObject;
			
			if(floor != null){
				floor.GetComponent<Floor>().setRotate(true);
			}
			
			break;
			
		case (int)FLOOR_ID.FLOOR_VERTICAL_R:
			
			floor = Object.Instantiate(floorVertical) as GameObject;

			if(floor != null){
				floor.GetComponent<Floor>().setRotate(true);
			}
			
			break;
		}
		
		
		// 位置のセット.
		if(floor != null){
			floor.transform.position = generatePos;
			floor.GetComponent<Floor>().setTargetPos(floorPos);
			
		}

		return floor;
	}


	public void SetCreate(){
		state = FLOOR_STATE.STATE_CREATE;
	}
}