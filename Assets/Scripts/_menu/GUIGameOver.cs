using UnityEngine;

public sealed class GUIGameOver : MonoBehaviour {
	
	#region Fields
	private float buttonWitdh = 300;
	private float buttonHeight = 100;
	
	private float bntJogarOffsetX = 300;
	private float bntJogarOffsetY = 100;
	#endregion
	
	#region Unity API
	void Start(){
		bntJogarOffsetX = (Screen.width / 2) - (buttonWitdh / 2);
		bntJogarOffsetY = (Screen.height / 2) - (buttonHeight / 2);
	}
	
	void OnGUI(){
		if (GUI.Button(new Rect(bntJogarOffsetX,bntJogarOffsetY,buttonWitdh,buttonHeight), "Game over\n\nClique aqui para jogar novamente")) {
			Application.LoadLevel(1);
		}
	}
	#endregion
	
}
