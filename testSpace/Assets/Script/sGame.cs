//----------------------------------------------------------
// using
//----------------------------------------------------------
using UnityEngine;

//----------------------------------------------------------
//	@class sGame
//	@brief ゲームシステムクラス(シングルトン)
//----------------------------------------------------------
public class sGame : Singleton<sGame> {

	//----------------------------------------------------------
	// @enum 	Scene
	// @brief	シーンの列挙型
	//----------------------------------------------------------
	public enum Scene{
		Title,		//!< タイトルシーン
		GameMain	//!< ゲームメイン
	};

	//----------------------------------------------------------
	// private valiables
	//----------------------------------------------------------
	private Scene currentScene = Scene.GameMain;	//!< 現在のシーン

	//----------------------------------------------------------
	// public method
	//----------------------------------------------------------
	//--------------------------------------------------------
	// 初期化
	//----------------------------------------------------------
	public void Awake()
	{
		// 唯一のインスタンスが自分以外だったら自身を削除
		if(this != Instance)
		{
			Destroy(this);
			return;
		}

		// 削除しないようにする
		DontDestroyOnLoad(this.gameObject);
	}    

	//----------------------------------------------------------
	// システムの更新
	//----------------------------------------------------------
	public void Update()
	{
		switch (currentScene)
		{
			// タイトル
			case Scene.Title:
				
				// エンターキーを押したらゲームメインへ
				if (Input.GetKey(KeyCode.Return))
				{
					SceneChange( Scene.GameMain );
				}

			break;

			// ゲームメイン
			case Scene.GameMain:
				
			break;
		}
	}

	//----------------------------------------------------------
	// シーンを遷移させる
	//
	// @param scene 遷移するシーン
	//----------------------------------------------------------
	public void SceneChange( Scene scene )
	{
		switch (scene)
		{
			case Scene.Title:
				Application.LoadLevel ("Title");
			break;

			case Scene.GameMain:
				Application.LoadLevel ("GameMain");
			break;
		}

		currentScene = scene;
	}
}

