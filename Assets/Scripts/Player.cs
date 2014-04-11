using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector3 moveDestination;
	public int cellIndex = 0;
	
	public bool move = false;
	public bool move_second = false;
	
	public string name = "Player";
	public int pieceId = 0;

	void Awake(){
		moveDestination = transform.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void TurnUpdate(){

	}

	public Vector3 calcCoordination(int turnNumber, int totalNumberOfPlayer){
		//Debug.Log (cellIndex+"");
		cellIndex %= 40;
		Vector3 pos = GameManager.cells [cellIndex];


		int rawInd = cellIndex % 40;
		int hZ = 0, vX = 0;
		if (rawInd >= 0 && rawInd <= 10)
			hZ = -1;
		if (rawInd >= 20 && rawInd <= 30)
			hZ = 1;
		if (rawInd >= 10 && rawInd <= 20)
			vX = -1;
		if (rawInd >= 30 && rawInd <= 40)
			vX = 1;


		switch (totalNumberOfPlayer) {
				
//			case 1: 
//				return new Vector3(pos.x+0.3f, pos.y, pos.z-0.3f);
//				break;
			case 2: 
				if( turnNumber == 0 )
				return new Vector3(pos.x+( vX * 0.6f), pos.y, pos.z);
				if( turnNumber == 1 )
				return new Vector3(pos.x, pos.y, pos.z+( hZ * 0.6f));
				
				break;
			case 3: 
				if( turnNumber == 0 )
				return new Vector3(pos.x+ (vX * 0.6f) , pos.y, pos.z);
				if( turnNumber == 1 )
				return new Vector3(pos.x+( vX * 0.3f), pos.y, pos.z + ( hZ * 0.3f) );
				if( turnNumber == 2 )
					return new Vector3(pos.x, pos.y, pos.z + (hZ * 0.6f) );
				
				break;
			case 4: 


				if( turnNumber == 0 )
				return new Vector3(pos.x+ (0.75f * vX), pos.y, pos.z);
				if( turnNumber == 1 )
				return new Vector3(pos.x+ (0.5f * vX), pos.y, pos.z + (0.25f * hZ));
				if( turnNumber == 2 )
				return new Vector3(pos.x+ (0.25f * vX), pos.y, pos.z + (0.5f * hZ));
				if( turnNumber == 3 )
				return new Vector3(pos.x, pos.y, pos.z + (0.75f * hZ));
				break;		
			default:
				return new Vector3();
				break;
		}
		return new Vector3();

	}
}
