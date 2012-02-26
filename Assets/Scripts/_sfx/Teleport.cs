using UnityEngine;

public sealed class Teleport : MonoBehaviour {
	
	#region Unity Editor Fields
	public Transform Destination;
	#endregion
	
	#region Unity API
	void OnTriggerEnter(Collider other) {
		Vector3 currentPosition = other.transform.position;
		Vector3 targetPosition = Destination.position;
		Vector3 position = new Vector3(targetPosition.x, currentPosition.y, currentPosition.z);
		
		other.transform.position = position;
	}
	#endregion
}
