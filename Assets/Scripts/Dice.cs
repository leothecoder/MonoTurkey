using UnityEngine;
using System.Collections;


public class Dice : MonoBehaviour {
	
	public double randomSpin = 4.0;
	public double randomVelocity = 1.0;
	public static int diceTotal  = 0;

	GameObject dice1;
	GameObject dice2;
	public static bool gravityIsOn = true;
	public static bool waitingToRoolDice=false;

//	int s=0;

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		
		dice1 = GameObject.Find ("Dice1");
		dice2 = GameObject.Find ("Dice2");
		switchGravity (false);
//		RollDice ();
//		GameManager.rolling = true;

	}

	public static void switchGravity(bool val){
		gravityIsOn = val;
		
		GameObject dice1 = GameObject.Find ("Dice1");
		GameObject dice2 = GameObject.Find ("Dice2");
		dice1.rigidbody.useGravity = val;
		dice2.rigidbody.useGravity = val;
	}

	int CalcSideUp() {
		float dotFwd = Vector3.Dot(transform.forward, Vector3.up);
		if (dotFwd > 0.99f) return 6;
		if (dotFwd < -0.99f) return 1;
		
		float dotRight = Vector3.Dot(transform.right, Vector3.up);
		if (dotRight > 0.99f) return 2;
		if (dotRight < -0.99f) return 5;
		
		float dotUp = Vector3.Dot(transform.up, Vector3.up);
		if (dotUp > 0.99f) return 4;
		if (dotUp < -0.99f) return 3;
		
		return 0;
	}
	
	void Update() {

		if (GameManager.rolling) {

//			dice1 = GameObject.Find ("Dice1");
//			dice2 = GameObject.Find ("Dice2");
			if ( rigidbody.IsSleeping() && rigidbody.velocity.magnitude == 0 && rigidbody.velocity.sqrMagnitude < .01f && rigidbody.angularVelocity.sqrMagnitude < .01f ){
				dice1 = GameObject.Find ("Dice1");
				dice2 = GameObject.Find ("Dice2");
		
				int res1 = diceResult (dice1);
				int res2 = diceResult (dice2);
				GameManager.rolling = false;
				GameManager.navigateNextAction("waitingToMovePiece");

				Debug.Log ("***Result of Dice1 is "+ res1 + " and Dice2 is "+res2 );
				//Debug.Log ("rolling: "+GameManager.rolling.ToString ());
			}
		}
		if (waitingToRoolDice) {
			
			dice1 = GameObject.Find ("Dice1");
			dice2 = GameObject.Find ("Dice2");
//			dice1.transform.rotation = Quaternion.LookRotation(Random.onUnitSphere);
			//			dice2.transform.rotation = Quaternion.LookRotation(Random.onUnitSphere);
			dice1.transform.Rotate(Vector3.right * Time.deltaTime*50f);
			dice2.transform.Rotate(Vector3.right * Time.deltaTime*50f);
		}

	}

	public void RollDice(){
		
		dice1 = GameObject.Find ("Dice1");
		dice2 = GameObject.Find ("Dice2");
		dice1.transform.position = new Vector3 (-0.06362738f, 8.163256f, -0.064404f);
		dice2.transform.position = new Vector3 (0.31145f, 8.41503f, -0.7932131f);


		dice1.transform.rotation = Quaternion.LookRotation(Random.onUnitSphere);
		dice1.rigidbody.angularVelocity = Random.insideUnitSphere * (float)randomSpin;
		dice1.rigidbody.velocity = Random.insideUnitSphere * (float)randomVelocity;
		
		dice2.transform.rotation = Quaternion.LookRotation(Random.onUnitSphere);
		dice2.rigidbody.angularVelocity = Random.insideUnitSphere * (float)randomSpin;
		dice2.rigidbody.velocity = Random.insideUnitSphere * (float)randomVelocity;

	}

	public void move(){
//		//Piece piece = new Piece ();
//		dice1 = GameObject.Find ("Dice1");
//		dice2 = GameObject.Find ("Dice2");
//
//		int res1 = diceResult (dice1);
//		int res2 = diceResult (dice2);
//		Debug.Log ("Result of Dice1 is "+ res1 + " and Dice2 is "+res2 );
//		//piece.Move(res1, res2);
//
//		Piece.move = true;
//		diceTotal = res1 + res2;
//
//		int valInd = GameManager.boardCellIndex;
//		int modVal = ((valInd % 40) % 10);
//		if (modVal + diceTotal > 10) {
//			int firstMove = 10 - modVal;
//			secondMove = diceTotal - firstMove;
//			
//			Debug.Log ("secondMove: "+secondMove+ ", firstMove: "+firstMove);
//
//			GameManager.boardCellIndex += firstMove;
//			int ind = GameManager.boardCellIndex;			
//			Piece.to = GameManager.cells [ind % 40];
//			Piece.move_second = true;
//
//		} else {
//
//			GameManager.boardCellIndex += diceTotal;
//			int ind = GameManager.boardCellIndex;
//			Piece.to = GameManager.cells [ind % 40];
//		}

	}

	public int diceResult(GameObject dice) {
		float dotFwd = Vector3.Dot(dice.transform.forward, Vector3.up);
		if (dotFwd > 0.99f) return 6;
		if (dotFwd < -0.99f) return 1;
		
		float dotRight = Vector3.Dot(dice.transform.right, Vector3.up);
		if (dotRight > 0.99f) return 2;
		if (dotRight < -0.99f) return 5;
		
		float dotUp = Vector3.Dot(dice.transform.up, Vector3.up);
		if (dotUp > 0.99f) return 4;
		if (dotUp < -0.99f) return 3;
		
		return 0;
	}

	
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "GameBoard") {
			//print ("Dice1 is collided");
			//			Debug.Log("Dice1 is collided");
		}
		
		if (col.gameObject.name == "GameBoard") {
			//print ("Dice2 is collided");
			//			Debug.Log("Dice2 is collided");
		}
	}
}
