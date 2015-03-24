using UnityEngine;
using System.Collections;

public class BattlePostEffect {

	public static void SetReverse(float lifetime = -1 ){
		GameObject battleCamera = Camera.main.gameObject;
		GameObject uicamera = GameObject.Find("UICamera");

		GrayOut grayout = battleCamera.AddComponent<GrayOut>();
		EdgeDown edgedown = battleCamera.AddComponent<EdgeDown>();

		GrayOut uigrayout = uicamera.AddComponent<GrayOut>();
		EdgeDown uiedgedown = uicamera.AddComponent<EdgeDown>();

		if( lifetime != -1 && lifetime > 0){
			GameObject.Destroy( grayout, lifetime);
			GameObject.Destroy( edgedown, lifetime);
			GameObject.Destroy( uigrayout, lifetime);
			GameObject.Destroy( uiedgedown, lifetime);
		}

	}

	public static void RemoveReverse( ){

		EdgeDown edgedownmain = Camera.main.gameObject.GetComponent<EdgeDown>();
		if( edgedownmain != null)GameObject.Destroy( edgedownmain );

		EdgeDown edgedownui = GameObject.Find("UICamera").GetComponent<EdgeDown>();
		if( edgedownui != null)GameObject.Destroy( edgedownui );

	}

}
