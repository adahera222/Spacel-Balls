using System.Collections;
using UnityEngine;

public sealed class Player : MonoBehaviour{
	
	#region Unity Editor Fields
	public float Speed = 20f;
	public float InvisibleTime = 1.5f;
	public float RespawnSpeed = .0005f;
	public float BlinkRate = 0.1f;
	public int AmountOfBlinks = 10;
	#endregion
	
	#region Fields
	private Transform _transform;
	private GameManager _game;
	private MeshRenderer _renderer;
	private Collider _collider;
	private int _blinkCount;
	
	private Vector3 _spawn;
	private Vector3 _final;
	#endregion
	
	#region Unity API
	// Use this for initialization
	void Start () {
		_transform = transform;
		_game = GameManager._instance;
		_renderer = GetComponent<MeshRenderer>();
		_collider = collider;
		
		_final = _transform.position;
		_spawn = new Vector3(_final.x,-6.2f,_final.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (_game.GameState != GameState.Explosion){
			float horizontal = Input.GetAxis("Horizontal");
			if (horizontal == 0)
				return;
			
			_transform.Translate(Vector3.right * horizontal * Speed * Time.deltaTime, Space.World);
		}
	}
	#endregion
	
	#region Public API
	public void Die(){
		_game.GameState = GameState.Explosion;
		_game.PlayerLives--;
		StartCoroutine(DestroyPlayer());
	}
	#endregion
	
	#region Helpers
	IEnumerator DestroyPlayer(){
		_renderer.enabled = false;
		_collider.enabled = false;
		
		_transform.position = _spawn;
		yield return new WaitForSeconds(InvisibleTime);
		
		_renderer.enabled = true;
		_collider.enabled = true;
		
		while (_transform.position.y < _final.y){
			float amountToMove = RespawnSpeed * Time.deltaTime;
			_transform.position = new Vector3(0,_transform.position.y + amountToMove,_transform.position.z);
			
			//Aguarda 1 quadro
			yield return 0;
		}
		
		_game.GameState = GameState.Invincible;
		
		while(_blinkCount < AmountOfBlinks) {
			_renderer.enabled = !_renderer.enabled;
			
			if (_renderer.enabled){
				_blinkCount++;
			}
			
			yield return new WaitForSeconds(BlinkRate);
		}
		
		_blinkCount = 0;
		_game.GameState = GameState.Playing;
	}
	#endregion
}
