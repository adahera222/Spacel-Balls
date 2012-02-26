using UnityEngine;

public sealed class Shooter : MonoBehaviour {
	
	#region Unity Editor API
	public float ProjectileOffset = .85f;
	public AudioClip ShootSound;
	#endregion
	
	#region Fields
	private Transform _transform;
	private GameManager _game;
	private AudioManager _audio;
	#endregion
	
	#region Unity API
	// Use this for initialization
	void Start () {
		_transform = transform;
		_game = GameManager._instance;
		_audio = AudioManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if (_game.GameState != GameState.Explosion && Input.GetKeyDown(KeyCode.Space) ) {
			Shoot();
		}
	}
	#endregion
	
	private void Shoot(){
		GameObject go = _game.NextProjectile();
		if (go != null) {
			Vector3 currPosition = _transform.position;
			Vector3 position = new Vector3(currPosition.x, currPosition.y + ProjectileOffset, currPosition.z);
			
			go.transform.position = position;
			go.transform.rotation = _transform.rotation;
			go.GetComponent<Projectile>().enabled = true;
			//_audio.PlaySFX(ShootSound);
		}
	}
}
