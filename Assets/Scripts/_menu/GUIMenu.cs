using UnityEngine;

public sealed class GUIMenu : MonoBehaviour {
	
	#region Fields
	private float buttonWitdh = 200;
	private float buttonHeight = 50;
	
	private float bntJogarOffsetX = 300;
	private float bntJogarOffsetY = 100;
	#endregion
	
	#region Unity API
	void Start(){
		bntJogarOffsetX = (Screen.width / 2) - (buttonWitdh / 2);
		bntJogarOffsetY = (Screen.height / 2) - (buttonHeight / 2);
	}
	
	void OnGUI(){
		if (GUI.Button(new Rect(bntJogarOffsetX,bntJogarOffsetY,buttonWitdh,buttonHeight), "Jogar")) {
			Application.LoadLevel(1);
		}
	}
	#endregion
	
}
