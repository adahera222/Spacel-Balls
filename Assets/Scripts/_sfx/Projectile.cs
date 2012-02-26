using UnityEngine;

public sealed class Projectile : MonoBehaviour {
	
	#region Unity Editor Fields
	public float Speed = 10f;
	#endregion
	
	#region Fields
	private Transform _transform;
	private MeshRenderer _renderer;
	private Collider _collider;
	#endregion
	
	#region Unity API
	// Use this for initialization
	void Awake () {
		_transform = transform;
		_renderer = gameObject.GetComponent<MeshRenderer>();
		_collider = gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		_transform.Translate(Vector3.up * Speed * Time.deltaTime,Space.World);
	}
	
	void OnEnable() {
		if (!_renderer.enabled){
			_renderer.enabled = true;
			_collider.enabled = true;
		}
	}
	
	void OnDisable () {
		if (_renderer.enabled){
			_renderer.enabled = false;
			_collider.enabled = false;
		}
	}
	#endregion
	
	#region Public API
	public void Explode(){
		enabled = false;
	}
	#endregion
}
