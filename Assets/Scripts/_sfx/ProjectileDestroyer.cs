using UnityEngine;

public sealed class ProjectileDestroyer : MonoBehaviour {
	
	#region Unity API
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Projectile") {
			other.GetComponent<Projectile>().enabled = false;
		}
	}
	#endregion
}
