using UnityEngine;
using System.Collections;

public class SightManager : MonoBehaviour {

	public 	GUITexture[]	sight;
	
	void Start () {

		if (OVRDevice.IsHMDPresent () && OVRDevice.IsSensorPresent()) {
			Rect[] rect = new Rect[2];

			rect [0].width = rect [0].height =
			rect [1].width = rect [1].height = Screen.width * 0.025f;

			rect [0].x = Screen.width * 0.25f - rect [0].width * 0.5f;
			rect [0].y = Screen.height * 0.5f - rect [0].height * 0.5f;

			rect [1].x = Screen.width * 0.75f - rect [1].width * 0.5f;
			rect [1].y = Screen.height * 0.5f - rect [1].height * 0.5f;

			sight [0].pixelInset = rect [0];
			sight [1].pixelInset = rect [1];
		} else {
			Rect rect = new Rect();
			rect.width = rect.height = Screen.width * 0.025f;
			rect.x = Screen.width * 0.5f - rect.width * 0.5f;
			rect.y = Screen.height * 0.5f - rect.height * 0.5f;

			sight[0].pixelInset = rect;
			sight[1].gameObject.SetActive(false);
		}
	}

}