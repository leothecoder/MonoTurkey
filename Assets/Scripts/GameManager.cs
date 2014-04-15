using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public GUISkin gameBoardSkin ;
	public GameObject userPlayerPrefab; 
	public GameObject[] pieces = new GameObject[10];
	public GameObject[] piecesSpinning = new GameObject[10];
	public Texture GameBoardCamera;
	public Texture[] GameBoardCams = new Texture[4];
	public Texture2D backgroundImage;
	public Vector3 pLocation = new Vector3 (-30f, -0.5f, 3f);

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

	/*
	 *	alertWhichPlayerBegins
	 *	waitingToRollDice
	 *	waitingToFinishTurn 
	 *	waitingToMovePiece
	 * 
	 */
	public static string currentAction ="alertWhichPlayerBegins";	

	float w, h, wHalf, hHalf;
	float wBox, hBox;

	void Awake(){
		
		w = Screen.width;
		h = Screen.height;
		wHalf = w / 2;
		hHalf = h / 2;
		wBox = 240;
		hBox = 180;

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

	public static void navigateNextAction(string action){
		currentAction = action;
	}

	public void switchDicesStateWaitinToRollDice(){

		Dice.switchGravity (false);

		GameObject dice1 = GameObject.Find ("Dice1");
		GameObject dice2 = GameObject.Find ("Dice2");
		dice1.transform.position = new Vector3(0f,4f,1.5f);
		dice2.transform.position = new Vector3(1f,4f,1.5f);
		Dice.waitingToRoolDice = true;
	}
	
	private Vector2 scrollViewVector = Vector2.zero;
	public string innerText = "";
	void OnGUI() {
		
		if (gameBoardSkin != null) {
			GUI.skin = gameBoardSkin;
		}  

		//here, draw background image on the screen
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundImage);  

		GUI.DrawTexture (new Rect (5, 5, w * 0.7f, h - 50), GameBoardCamera);

		float left = wHalf - (wHalf*0.5f) - (wBox * 0.3f),
		top = hHalf - (hBox * 0.3f);
		switch(currentAction){

			case "alertWhichPlayerBegins": 
				GUI.Box (new Rect (left,top, wBox,hBox),"", "dialog");
				GUI.Box (new Rect (left+10,top+60, 90,90), "","piecespinning");
				GUI.DrawTexture (new Rect (left+10,top+60, 90,90), GameBoardCams[currentPlayerIndex]);
				GUI.Label(new Rect(left+20, top+20, 120, 40), ""+players[currentPlayerIndex].name+" starts first");
			
				if (GUI.Button (new Rect (left+120, hHalf+40, 100,40),"", "ok")) {
					navigateNextAction("waitingToRollDice");
					
					switchDicesStateWaitinToRollDice();

				}
				
				break;
				
			case "waitingToRollDice": 
				GUI.Box (new Rect (left,top, wBox,hBox), ""+players[currentPlayerIndex].name, "dialog");
				GUI.Box (new Rect (left+10,top+60, 90,90), "","piecespinning");
				GUI.DrawTexture (new Rect (left+10,top+60, 90,90), GameBoardCams[currentPlayerIndex]);
				if (GUI.Button (new Rect (left+120, hHalf+40, 100,40),"", "rolldice")) {

					Dice.waitingToRoolDice = false;
					Dice.switchGravity(true);
					dice = new Dice();
					dice.RollDice();
					rolling = true;
					currentAction = "";	//set null to display none of boxes on screen
					innerText += "Player"+ (currentPlayerIndex+1) +" rolled dice\n";
				}	
				break;
				
			case "waitingToMovePiece": 
				GUI.Box (new Rect (left,top, wBox,hBox), ""+players[currentPlayerIndex].name, "dialog");
				GUI.Box (new Rect (left+10,top+60, 90,90), "","piecespinning");
				GUI.DrawTexture (new Rect (left+10,top+60, 90,90), GameBoardCams[currentPlayerIndex]);
				if (GUI.Button (new Rect (left+120, hHalf+40,100,40), "", "move")) {
					moveCurrent();
					currentAction = "";	//set null to display none of boxes on screen
				}
					
				break;
			case "waitingPlayerToTakeAction":
				GUI.Box (new Rect (left,top, wBox,hBox), ""+players[currentPlayerIndex].name, "dialog");
				GUI.Label(new Rect(left+20, top+20, 120, 60), "Buy, Pay rent etc actions will hold on in this window.");
				
				if (GUI.Button (new Rect (left+120, hHalf+40,100,40), "", "done")) {
					
					currentAction = "waitingToFinishTurn";	
				}

				break;
			case "waitingToFinishTurn": 
				GUI.Box (new Rect (left,top, wBox,hBox), ""+players[currentPlayerIndex].name,"dialog");
				GUI.Box (new Rect (left+10,top+60, 90,90), "","piecespinning");
				GUI.DrawTexture (new Rect (left+10,top+60, 90,90), GameBoardCams[currentPlayerIndex]);
				if (GUI.Button (new Rect (left+120, hHalf+40,100,40), "", "finishturn")) {
					nextTurn();
					innerText += "Player "+(currentPlayerIndex+1) +"'s Turn\n";
					currentAction="waitingToRollDice";
					switchDicesStateWaitinToRollDice();
				}
				break;
		}

		//drawing right side information panels
		float bHeight = 0;
		for (int i=0; i<=numberOfPlayers; i++) {
			if( i<numberOfPlayers){
				if(i==currentPlayerIndex){
					bHeight = 250;
					GUI.Box(new Rect (Screen.width-300, 10 + ((i)*50), 300, bHeight),players[i].name);
				}else{
					//calculate top 
					float topPanel = 10 + ((i)*50);
					if(bHeight > 0)
						topPanel = 10 + ((i)*50)+bHeight-50;

					GUI.Button(new Rect (Screen.width-300, topPanel, 300, 50), players[i].name);
				}
			}else{
				//calculate top 		
				float topPanel = 10 + ((i)*50);
				if(bHeight > 0)
					topPanel = 10 + ((i)*50)+bHeight-50;

				GUI.Button(new Rect (Screen.width-300,  topPanel, 300, 50), "BANK");
			}
		}
		
		scrollViewVector = GUI.BeginScrollView (new Rect (Screen.width-300, h-200, 300, 200), scrollViewVector, new Rect (0, 0, 300, 400));
		innerText = GUI.TextArea (new Rect (0, 0, 400, 400), innerText);
		GUI.EndScrollView();
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
			player = ((GameObject)Instantiate (pieces[chosenPieces[i]],//userPlayerPrefab,//userPlayerPrefab,//chosenPieces[i], 
		  	  new Vector3 (pos.x, pos.y, pos.z), Quaternion.Euler (new Vector3 ()))).GetComponent<Player> ();
			player.name = chosenNames[i];
			players.Add (player);

			//instantiate spining pieces
			Instantiate (piecesSpinning[chosenPieces[i]],
			             new Vector3 (pLocation.x-(i*20f), pLocation.y, pLocation.z), Quaternion.Euler (new Vector3 ()));
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
