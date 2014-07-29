using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {


	public GameObject floorAround;
	public GameObject floorCross;
	public GameObject floorCrossX;
	public GameObject floorVertical;
	public GameObject floorHorizontal;
	public GameObject floorNone;

	enum FLOOR_ID{

		FLOOR_AROUND,
		FLOOR_CROSS,
		FLOOR_CROSSX,
		FLOOR_VERTICAL,
		FLOOR_HORIZONTAL,
		FLOOR_NONE,

		FLOOR_MAX,
	};

	public int floorNum = 12;
	public int floorSpan = 20;

	// Use this for initialization
	void Start () {

		Vector3 pos = new Vector3(0f,0f,0f);
		GameObject floor = null;

		for(int i = 0;i < floorNum;i++){

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
			}

			if(floor != null)
				floor.transform.position = pos;

			pos.z += floorSpan;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}