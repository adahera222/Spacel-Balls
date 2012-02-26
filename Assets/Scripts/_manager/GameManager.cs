using UnityEngine;

public sealed class GameManager : MonoBehaviour {

	#region Unity Editor Fields
	public int PlayerLives = 3;
	public int ProjectileLimit = 3;
	public GameObject ProjectilePrefab;
	
	public GameObject EnemyPrefab;
	public int EnemyKilled;
	public float EnemySpeedIncreaseFactor;
	#endregion
	
	#region Fields
	public static GameManager _instance;
	private AudioManager _audio;
	private GameObject[] _projectileBuffer;
	private GameObject _enemy;
	private Vector3 _spawn = new Vector3(0, 0, 2);
	private GameState _state;
	#endregion
	
	public GameState GameState {
		get { return _state; }
		set { 
			Debug.Log("de -> " + _state + " para -> " + value); 
			_state = value;
		}
	}
	
	#region Unity API
	void Awake(){
		_instance = this;
		
		_audio = AudioManager.GetInstance();
		_projectileBuffer = new GameObject[ProjectileLimit];
		
		GameState = GameState.Playing;
	}
	
	void Start(){
		// Configura o audio
		_audio.StartUp();
		
		// Desabilita o Mesh Renderer de todos os componentes com a TAG "Invisible"
		GameObject[] obj = GameObject.FindGameObjectsWithTag("Invisible");
		
		foreach (GameObject o in obj){
			//Debug.Log("Desligando MeshRenderer para: " + o);
			o.GetComponent<MeshRenderer>().enabled = false;
		}
		
		// Configura os projeteis
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		for (int i = 0; i < ProjectileLimit; i++){
			_projectileBuffer[i] = (GameObject) Instantiate(ProjectilePrefab, _spawn, player.transform.rotation);
			_projectileBuffer[i].GetComponent<Projectile>().enabled = false;
		}
		
		// Configura o inimigo
		GameObject top = GameObject.Find("Top Wall");
		Vector3 topPos = top.transform.position;
		Vector3 enemySpawn = new Vector3(topPos.x , topPos.y, topPos.z);
		_enemy = (GameObject) Instantiate(EnemyPrefab, enemySpawn, top.transform.rotation);
		
	}
	
	void Update(){
		if (PlayerLives == 0) {
			Application.LoadLevel(2);
		}
	}
	
	void OnGUI(){
		GUI.Label( new Rect(10,10,100,20), "Lives: " + PlayerLives);
		GUI.Label( new Rect(10,30,200,20), "Enemys Killed: " + EnemyKilled);
	}
	#endregion
	
	#region Public API
	public GameObject NextProjectile(){
		// poucas formas seriam pior que essa... mas para um demo ta lindo!!
		foreach (GameObject p in _projectileBuffer) {
			if (p.GetComponent<Projectile>().enabled == false){
				return p;
			}
		}
		return null;
	}
	#endregion
	
	#region Helpers
	void GameOver(){

		
	}
	#endregion
}
