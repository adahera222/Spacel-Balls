using System.Collections;
using UnityEngine;

public sealed class Enemy : MonoBehaviour {
	
	#region Unity Editor Fields
	public int SpeedMininum = 10;
	public int SpeedMaximum = 15;
	public float WallOffset = 3f;
	public float YieldTime = 0.5f;
	#endregion
	
	#region Fields
	private Transform _transform;
	private MeshRenderer _renderer;
	private Collider _collider;
	private Vector3 _spawn;
	private GameManager _game;
	private float _speed;
	private float _speedFactor = 1f;
	
	private Vector3 TopWallPosition;
	private Vector3 LeftWallPosition;
	private Vector3 RightWallPosition;
	#endregion
	
	#region Unity API
	// Use this for initialization
	void Awake () {
		_transform = transform;
		_renderer = gameObject.GetComponent<MeshRenderer>();
		_collider = gameObject.GetComponent<Collider>();
	}
	
	void Start(){
		GameObject top = GameObject.Find("Top Wall");
		GameObject left = GameObject.Find("Left Wall");
		GameObject right = GameObject.Find("Right Wall");
		
		TopWallPosition = top.transform.position;
		LeftWallPosition = left.transform.position;
		RightWallPosition = right.transform.position;
		
		StartCoroutine(AdjustPosition());
		
		_game = GameManager._instance;
	}
	
	// Update is called once per frame
	void Update () {
		_transform.Translate(Vector3.down * _speed * Time.deltaTime,Space.World);
	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Projectile") {
			other.GetComponent<Projectile>().Explode();
			_speedFactor += _game.EnemySpeedIncreaseFactor;
			Die();
		}
		
		if (other.tag == "Player"){
			if (_game.GameState == GameState.Playing){
				other.GetComponent<Player>().Die();
			}
			
			Die();
		}
		
		if (other.name == "Bottom Wall"){
			StartCoroutine(AdjustPosition());
		}
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
	
	#region Helpers
	private void Die(){
		
		StartCoroutine(AdjustPosition());
		_game.EnemyKilled++;
	}
	
	IEnumerator AdjustPosition(){
		float x = Random.Range(LeftWallPosition.x + WallOffset, RightWallPosition.x - WallOffset);
		_spawn = new Vector3(x , TopWallPosition.y, TopWallPosition.z);
		
		_renderer.enabled = false;
		_collider.enabled = false;
		_speed = Random.Range(SpeedMininum,SpeedMaximum) * _speedFactor;
		
		yield return new WaitForSeconds(YieldTime);
		_transform.position = _spawn;
		
		_renderer.enabled = true;
		_collider.enabled = true;
	}
	#endregion
}
