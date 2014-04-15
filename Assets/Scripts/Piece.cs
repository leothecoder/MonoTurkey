using UnityEngine;
using System.Collections;

public class Piece : Player {
	
	public static Vector3 pos;
	
	
	public static Vector3 from;
	public static Vector3 to ;
	
	
	void Awake () {
		pos = transform.position;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		//GameObject obj = GameObject.Find ("piece");
		if (move == true) {
			transform.position = Vector3.MoveTowards (transform.position, moveDestination, 3f * Time.deltaTime);
			//Debug.Log (move.ToString ());
			//Debug.Log (transform.position.ToString () + " * " + moveDestination.ToString());
			//pos = transform.position;
			if(  Vector3.Distance( transform.position, moveDestination ) <= 0.1f ){
				move = false;
				GameManager.navigateNextAction("waitingPlayerToTakeAction");

			}
		}
		
	}
	
	public override void TurnUpdate(){
		//		if (Vector3.Distance (moveDestination, transform.position) > 0.1f) {
		//
		//		}
		base.TurnUpdate ();
	}
	
	public void Move(int dice1, int dice2){
		//int total = dice1 + dice2;
		//Debug.Log ("Dice1: "+ dice1 + ", Dice2: "+dice2+", Total: "+ total);
		
		
	}
}
