using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject userPlayerPrefab; 
	public GameObject[] pieces = new GameObject[10];
	public static int[] chosenPieces = new int[4];
	public static string[] chosenNames = new string[4];

	Dice dice = new Dice();
	
	public static int boardCellIndex = 0;
	int currentPlayerIndex = 0;
	public static int numberOfPlayers = 3;

	public static List<Vector3> cells = new List<Vector3>();
	public static List<Player> players = new List<Player>();
	
	static int secondMove;

	public static bool rolling = false;
	public static bool moving = false;
	public static bool idle = false;


	float w, h, wHalf, hHalf;

	void Awake(){
		
		w = Screen.width;
		h = Screen.height;
		wHalf = w / 2;
		hHalf = h / 2;

	}

	// Use this for initialization
	void Start () {

		initializeCellIndexes ();
		generatePlayers ( numberOfPlayers );
		determineCoordination ();
	}

	void initializeCellIndexes(){
		
		float x = 4f, z = -4f;
		for (int i = 0; i< 40; i++) {
			
			if (i >= 0 && i < 10) {
				cells.Add( new Vector3( x - (i*0.8f), 0.25f, -4f) );
			}else if (i >= 10 && i < 20) {
				cells.Add( new Vector3( -4f, 0.25f, z + ((i%10)*0.8f) ) );
				
			}else if (i >= 20 && i < 30) {
				cells.Add( new Vector3( -1f * x + ((i%10)*0.8f), 0.25f, 4) );
				
			}else if (i >= 30 && i < 40) {
				cells.Add( new Vector3( 4f, 0.25f, -1f * z - ((i%10)*0.8f)));
				
			}
		}
	}
	
	private Vector2 scrollViewVector = Vector2.zero;
	public string innerText = "";
	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), "Let's Go Team#12");
		// Make a background box
		GUI.Box (new Rect (10,40,100,120), "Actions Menu");
		
		// Make the first button. 
		if (GUI.Button (new Rect (20,70,80,20), "Roll Dice")) {
			dice = new Dice();
			dice.RollDice();
			rolling = true;
			innerText += "Player"+currentPlayerIndex +" rolled dice\n";
		}	
		
		// Make the second button.
		if (GUI.Button (new Rect (20,100,80,20), "Move")) {
//			dice = new Dice();
//			dice.move();
			moveCurrent();
		}
		
		// Make the third button.
		if (GUI.Button (new Rect (20,130,80,20), "Next Turn")) {
			nextTurn();
			innerText += "Player "+(currentPlayerIndex+1) +"'s Turn\n";
		}

		//ActionsBox ();
		
		scrollViewVector = GUI.BeginScrollView (new Rect (0, h-200, 200, 200), scrollViewVector, new Rect (0, 0, 400, 400));
		innerText = GUI.TextArea (new Rect (0, 0, 400, 400), innerText);
		GUI.EndScrollView();
	}

	public void ActionsBox(){
		float bWidth = 100, bHeight = 100;

		GUI.Box (new Rect (wHalf- bWidth/2, hHalf - bHeight/2, bWidth, bWidth), ""+players[currentPlayerIndex].name);
	}

	// Update is called once per frame
	void Update () {
		players[currentPlayerIndex].TurnUpdate ();

		//Debug.Log (Piece.move_second.ToString () + " " +Piece.move.ToString ());
		if (players[currentPlayerIndex].move_second == true && players[currentPlayerIndex].move == false) {
			Debug.Log (players[currentPlayerIndex].move_second.ToString () + " - " +players[currentPlayerIndex].move.ToString () + " + " +secondMove);
			
			players[currentPlayerIndex].cellIndex += secondMove;
			//int ind = GameManager.boardCellIndex;			
			players[currentPlayerIndex].moveDestination =
				players[currentPlayerIndex].calcCoordination( currentPlayerIndex, numberOfPlayers );
			players[currentPlayerIndex].move = true;
			players[currentPlayerIndex].move_second = false;
		}
	}

	public void nextTurn(){
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex ++;
		} else {
			currentPlayerIndex = 0;
		}
		print ("Player" + (currentPlayerIndex + 1) + "'s Turn");
	}

	public void moveCurrent(){
		Player player = players [currentPlayerIndex];

		//
		//calculate total dice result
		//
		GameObject dice1 = GameObject.Find ("Dice1");
		GameObject dice2 = GameObject.Find ("Dice2");
		
		int res1 = dice.diceResult (dice1);
		int res2 = dice.diceResult (dice2);
		Debug.Log ("Result of Dice1 is "+ res1 + " and Dice2 is "+res2 );
		//piece.Move(res1, res2);

		int diceTotal = res1 + res2;

		//
		//according to dice result 
		//move only current player
		//
		player.move = true;

		int valInd = player.cellIndex;
		int modVal = ((valInd % 40) % 10);
		if (modVal + diceTotal > 10) {
			int firstMove = 10 - modVal;
			secondMove = diceTotal - firstMove;
			
			Debug.Log ("firstMove: " + firstMove+ ", secondMove: "+secondMove);
			//
			//eğer dice result needs to go through o corner than 
			//we need to move our piece to second coordination
			//
			player.cellIndex += firstMove;		
			player.moveDestination = player.calcCoordination( currentPlayerIndex, numberOfPlayers );
			player.move_second = true;
			
		} else {
			
			player.cellIndex += diceTotal;
			player.moveDestination = player.calcCoordination( currentPlayerIndex, numberOfPlayers );
		}
	}

	void generatePlayers(int numberOfPlayers){
		Player player;
		Vector3 pos = cells [0];
		for (int i = 0; i< numberOfPlayers; i++) {
			player = ((GameObject)Instantiate (userPlayerPrefab,//pieces[chosenPieces[i]],//userPlayerPrefab,//chosenPieces[i], 
		  	  new Vector3 (pos.x, pos.y, pos.z), Quaternion.Euler (new Vector3 ()))).GetComponent<Player> ();
			//player.name = GameSetup.names[i];
			players.Add (player);
		}
	}

	void determineCoordination (){
		
		Player player;
		Vector3 pos = cells [0];

		switch (players.Count) {

		case 1: 
			player = players[0];
			player.transform.position = new Vector3(pos.x+0.3f, pos.y, pos.z-0.3f);
			break;
		case 2: 
			player = players[0];
			player.transform.position = new Vector3(pos.x+0.6f, pos.y, pos.z);
			player = players[1];
			player.transform.position = new Vector3(pos.x, pos.y, pos.z-0.6f);

			break;
		case 3: 
			player = players[0];
			player.transform.position = new Vector3(pos.x+0.6f, pos.y, pos.z);
			player = players[1];
			player.transform.position = new Vector3(pos.x+0.3f, pos.y, pos.z-0.3f);
			player = players[2];
			player.transform.position = new Vector3(pos.x, pos.y, pos.z-0.6f);

			break;
		case 4: 
			player = players[0];
			player.transform.position = new Vector3(pos.x+0.75f, pos.y, pos.z);
			player = players[1];
			player.transform.position = new Vector3(pos.x+0.5f, pos.y, pos.z-0.25f);
			player = players[2];
			player.transform.position = new Vector3(pos.x+0.25f, pos.y, pos.z-0.5f);
			player = players[3];
			player.transform.position = new Vector3(pos.x, pos.y, pos.z-0.75f);
			break;


		}

	}
}
