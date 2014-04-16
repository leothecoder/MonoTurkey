using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {
	
	public GUISkin guiskin = null;
	public static int numberOfPlayers = 2;
	Texture2D image = null;
	
	float pLeft = 0;
	float pTop = 0;

	int[] flags = new int[4];		//used for pieces to determine selected or not
	
	//these are for pieces
	public static string CurrentMenu;
	public GameObject prefab;

	public static string[] names = new string[4];
	public GameObject[] pieces = new GameObject[10];
	public Texture[] textureTargetCam = new Texture[4];
	public static GameObject[] piecesOnScene = new GameObject[4];
	int[] currentPieceIndexOnScene = new int[4]{-1,-1,-1,-1};

	bool[] flagPieceNotUsed = new bool[10]{true, true, true, true, true, true, true, true, true, true};
	public Vector3 location = Vector3.zero;
	/*
	 	*
	 	*	**************************House Rules Variables*************************
	 	*									START
	 	*
	 	*/	   
	private int turnLimit = 0;
	private string rules = "";
	private string defaultRules = "";
	int a = Screen.width;
	int b = Screen.height;
	string[] selStrings = new string[]{"0", "100", "200", "400"};
	private int[] toolbarInt = new int[6];
	private string[] housesPerHotel = {"4", "5"};
	private string[] freeParking = {"0", "250","500","TAX"};
	private string[] mortgageRate = {"0%", "5%","10%","20%"};
	private string[] luxTax = {"0", "75","150","300"};
	public GUISkin myHouseRuleStyle = null;
	Texture2D houseRuleTitle = null;
	public 	int k = 10;
	public int l = 60;
	/*
	 	*
	 	*	**************************House Rules Variables*************************
	 	*									END
	 	*
	 	*/
	void Awake(){
		
		/// 
		/// Selects the current menu
		/// 
		CurrentMenu = "Main";

		//
		//	House Rules Screen initializations
		//
		toolbarInt [0] = 0;		
		toolbarInt [1] = 1;		
		toolbarInt [2] = 2;		
		toolbarInt [3] = 3;		
		toolbarInt [4] = 1;		
		
		
		defaultRules = "Houses per hotel:" + housesPerHotel [toolbarInt [0]] + "  Free Parking:" + freeParking [toolbarInt [1]] +
			
			"  Salary:" + selStrings [toolbarInt [2]] + "   Mortgage rate:" + mortgageRate [toolbarInt [3]] + "   Luxury Tax:" + luxTax [toolbarInt [4]];
		//
		//	House Rules Screen initializations END
		//

	}

	// Use this for initialization
	void Start ()
	{

		//
		//load background image from resources
		//
		image = (Texture2D)Resources.Load ("background-monopoly");

		//
		//Generates pieces to be selected for each players
		//
		generatePieces ();

	}

	void generatePieces(){

		for (int i = 0; i<numberOfPlayers; i++) {

			int nextPiece = nextPieceAvailable(i);
			Vector3 pos = new Vector3(location.x-(i*20), location.y, location.z);
			piecesOnScene[i] = ((GameObject)Instantiate (pieces [nextPiece], pos, Quaternion.identity));
			currentPieceIndexOnScene [i] = nextPiece;
			flagPieceNotUsed[nextPiece] = false;

			//initialize player names
			names[i] = "Player"+(i+1);
		}
	}

	//<summary>
	//	Finds and returns next piece available in order to be selected by user
	//</summary>
	int nextPieceAvailable(int ind){
		int res = 0;
		int i = 0;
		while (i<10) {			
			ind = (ind + i) % 10;
			if (flagPieceNotUsed[ind]){
				res = ind % 10;
				break;
			}
			i++;
		}
		return res;
	}
		
	//<summary>
	//	Finds and returns previous piece available in order to be selected by user
	//</summary>
	int prevPieceAvailable(int ind){
		int res = 0;
		int i = 0;
		while (i<10) {			
			ind = (ind-i);
			if(ind<0)
				ind = 10 + ind;
			if (flagPieceNotUsed[ind]){
				res = ind % 10;
				break;
			}
			i++;
		}
		return res;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnGUI ()
	{
		//here, draw background image on the screen
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), image);  
		
		if (guiskin != null) {
			GUI.skin = guiskin;
		}  
		
		if (CurrentMenu == "Main") {
			Menu_Main ();
		}
		if (CurrentMenu == "Menu_PlayerProperties") {
			Menu_PlayerProperties ();
		}		
		if (CurrentMenu == "Menu_HouseRules") {
			Menu_HouseRules ();
		}
		if (CurrentMenu == "Menu_Settings") {
			Menu_Settings ();
		}
	}
	
	public void NavGate (string nextMenu)
	{
		CurrentMenu = nextMenu;
	}
	
	private void Menu_Main ()
	{			
		//monopoly title
		GUI.Button (new Rect (Screen.width*0.17f, Screen.height * 0.05f, Screen.width*0.66f, Screen.height*0.25f), "", "mainMenuTitle");

		int w = 240, h= 80;

		//button play
		if (GUI.Button (new Rect (Screen.width / 2 - w/2, Screen.height / 2 - 100, w, h), "", "bluebutton")) {
			//play
			//Debug.Log ("Play");
			NavGate ("Menu_PlayerProperties");
			
		}	
		GUI.Label (new Rect (Screen.width / 2 - w/2, Screen.height / 2 - 100, w, h),Lang.giveMeText( "newgame"));	
		
		//button settings
		if (GUI.Button (new Rect (Screen.width / 2 - w/2, Screen.height / 2 , w, h), "", "bluebutton")) {
			//settings
			//Debug.Log ("Settings");
			NavGate ("Menu_Settings");
			
		}
		GUI.Label (new Rect (Screen.width / 2 - w/2, Screen.height / 2 , w, h), Lang.giveMeText( "settings"));
		
		//button quit
		if (GUI.Button (new Rect (Screen.width / 2 - w/2, Screen.height / 2 + 100, w, h), "", "bluebutton")) {
			//quit
			//Debug.Log ("Quit");
			
		}
		GUI.Label (new Rect (Screen.width / 2 - w/2, Screen.height / 2 +100, w, h), Lang.giveMeText( "quit"));
		
		
	}
	
	public string stringToEdit = "Player1";
	
	private void Menu_PlayerProperties ()
	{
		int w = 240, h = 80;
		if (GUI.Button (new Rect (20, Screen.height - (h+10), w, h),"", "bluebutton")) {			
			NavGate ("Main");
		} 
		GUI.Label (new Rect (20, Screen.height - (h+10), w, h), Lang.giveMeText( "back")); 

		//house rules
		if (GUI.Button (new Rect (Screen.width - (w+10)*2, Screen.height - (h+10), w, h),"", "bluebutton")) {			
			//NavGate ("HouseRules");
			//Application.LoadLevel(2);
			NavGate ("Menu_HouseRules");	
		} 
		GUI.Label (new Rect (Screen.width - (w+10)*2, Screen.height - (h+10),w,h), Lang.giveMeText( "houserules")); 

		if (GUI.Button (new Rect (Screen.width - (w+10), Screen.height - (h+10),w,h), "", "bluebutton")) {
			//GameManager.pieces = pieces;
			GameManager.chosenPieces = currentPieceIndexOnScene;
			GameManager.numberOfPlayers = numberOfPlayers;
			GameManager.chosenNames = names;
			Application.LoadLevel(1);	
		}
		GUI.Label (new Rect (Screen.width - (w+10), Screen.height - (h+10),w,h), Lang.giveMeText( "play")); 
		
		PlayerPanel ();	
		
	}

	private void Menu_HouseRules(){
		if (myHouseRuleStyle != null) {
			
			GUI.skin = myHouseRuleStyle;
		}
		
		int spaceRule = (int)b / 12 + 10;
		GUI.Box (new Rect (0, 0, a, b), "", "background-s");
		GUI.Box (new Rect (Screen.width / 2 - 200, 25, 400, 60), "", "title-s");
		
		//create the buttons
		
		GUI.Box (new Rect (10, b / 6, a / 2 - 10, b / 12), "", "rules");
		GUI.Box (new Rect (10, b / 6 + spaceRule, a / 2 - 10, b / 12), "", "rules");
		GUI.Box (new Rect (10, b / 6 + 2 * spaceRule, a / 2 - 10, b / 12), "", "rules");
		GUI.Box (new Rect (10, b / 6 + 3 * spaceRule, a / 2 - 10, b / 12), "", "rules");
		GUI.Box (new Rect (10, b / 6 + 4 * spaceRule, a / 2 - 10, b / 12), "", "rules");
		
		GUI.Box (new Rect (a / 2 + a / 8, b / 6 + 3 * spaceRule, a / 6, b / 12), "", "rules");
		GUI.Label (new Rect (a / 2 + a / 8 - 2, b / 6 + 3 * spaceRule + 2, a / 6, b / 12), "TURN LIMIT", "deneme");
		GUI.Label (new Rect (a / 2 + a / 8 - (10 + a / 20), b / 6 + 3 * spaceRule+4, a / 30, b / 15), "" + turnLimit, "rules");
		if (GUI.Button (new Rect (a / 2 + a / 8 - (10 + a / 20), b / 6 + 2 * spaceRule, a / 30, b / 15), "+10", "turnLimit")) {
			turnLimit += 10;
			
		}
		if (GUI.Button (new Rect (a / 2 + a / 8 - (10 + a / 20), b / 6 + 4 * spaceRule, a / 30, b / 15), "-10", "turnLimit")) {
			
			if (turnLimit > 0)
				turnLimit -= 10;
		}
		
		
		
		//accept and cancel buttons
		if (GUI.Button (new Rect (a -a/6, b / 6 + 7 * spaceRule, a / 8, b / 15), "Accept Rules", "turnLimit")) {
			//
			//here save the datebase
			//

			GameSetup.CurrentMenu = "Menu_PlayerProperties";
			
		}
		if (GUI.Button (new Rect ((a -a/6)-(a/16+10), b / 6 + 7 * spaceRule, a / 16, b / 15), "Back", "turnLimit")) {
			//back only
			GameSetup.CurrentMenu = "Menu_PlayerProperties";			
		}
		
		//*********************************************
		
		
		
		
		toolbarInt [0] = GUI.Toolbar (new Rect (a / 4, b / 6 + 2, a / 8 - 2, b / 12 - 4), toolbarInt [0], housesPerHotel);
		toolbarInt [1] = GUI.Toolbar (new Rect (a / 4, b / 6 + spaceRule + 2, a / 4 - 2, b / 12 - 4), toolbarInt [1], freeParking);
		toolbarInt [2] = GUI.Toolbar (new Rect (a / 4, b / 6 + 2 * spaceRule + 2, a / 4 - 2, b / 12 - 4), toolbarInt [2], selStrings);
		toolbarInt [3] = GUI.Toolbar (new Rect (a / 4, b / 6 + 3 * spaceRule + 2, a / 4 - 2, b / 12 - 4), toolbarInt [3], mortgageRate);
		toolbarInt [4] = GUI.Toolbar (new Rect (a / 4, b / 6 + 4 * spaceRule + 2, a / 4 - 2, b / 12 - 4), toolbarInt [4], luxTax);
		
		
		//draw the rules
		
		
		
		GUI.Label (new Rect (10, b / 6, a / 4 - 20, b / 12 - 4), "HOUSES PER HOTEL", "deneme");
		GUI.Label (new Rect (10, b / 6 + spaceRule, a / 4 - 10, b / 12 - 4), "FREE PARKING", "deneme");
		GUI.Label (new Rect (10, b / 6 + 2 * spaceRule, a / 4 - 10, b / 12 - 4), "SALARY", "deneme");
		GUI.Label (new Rect (10, b / 6 + 3 * spaceRule, a / 4 - 10, b / 12 - 4), "MORTGAGE RATE", "deneme");
		GUI.Label (new Rect (10, b / 6 + 4 * spaceRule, a / 4 - 10, b / 12 - 4), "LUXURY TAX", "deneme");
		
		
		rules = "Turn limit:" + turnLimit+"   Houses per hotel:" + housesPerHotel [toolbarInt [0]] + "  Free Parking:" + freeParking [toolbarInt [1]] +
			
			"  Salary:" + selStrings [toolbarInt [2]] + "   Mortgage rate:" + mortgageRate [toolbarInt [3]] + "   Luxury Tax:" + luxTax [toolbarInt [4]];
		GUI.Label (new Rect (10, b / 6 + 5 * spaceRule, a, b / 8 - 4), rules, "labelString");
		
		GUI.Label (new Rect (10, b / 6 + 6 * spaceRule, a, b / 8 - 4), "Default Rules: Turn:0   " + defaultRules, "labelString");
		

	}
	
	private void Menu_Settings (){
		
		
	}
	
	public bool checkPiece (int index)
	{
		
		if (flags [index] == 0)
			return true;
		else
			return false;
		
	}
	
	public void PlayerPanel ()
	{
		int w = 200;
		int h = 300;
		int ww=240, hh=80;

		int total = w * numberOfPlayers + (20 * numberOfPlayers - 1);
		float startPoint = (Screen.width / 2) - (total / 2);
		float startPointTop = Screen.height / 2 - (h * 0.75f);
		for (int i=0; i< numberOfPlayers; i++) {
			//panel and its label
			GUI.Box (new Rect (startPoint + ((w + 20) * i), startPointTop, w, h), names[i]);
			GUI.Label (new Rect (startPoint + ((w + 20) * i) + 10, startPointTop + 30, 40, 20), "Name", "label1");
			
			//player name text area
			names[i] = GUI.TextArea (new Rect (startPoint + ((w + 20) * i) + 60, startPointTop + 30, 100, 25), names[i], 25);
			if (i > numberOfPlayers-2 && numberOfPlayers>2) {
				if (GUI.Button (new Rect (startPoint + ((w + 20) * i) + 25 +15, startPointTop + 30 + 40, 120, 40), "","bluebutton")) {
					//destroyPieces();
					int pieceInd = currentPieceIndexOnScene [i];					
					flagPieceNotUsed[pieceInd] = true;
					Destroy( piecesOnScene[i]);
					currentPieceIndexOnScene [i] = -1;
					numberOfPlayers--;
					//generatePieces();

				}
				GUI.Label (new Rect (startPoint + ((w + 20) * i) + 25 +15, startPointTop+ 30 + 40, 120, 40), Lang.giveMeText( "remove")); 

			}
			
			if (numberOfPlayers < 4){
				if (GUI.Button (new Rect (Screen.width / 2 - (ww/2+25), startPointTop + h + 20, ww, hh),"", "bluebutton")) { 
					//destroyPieces();
					int ind = numberOfPlayers;
					numberOfPlayers++;	
					names[ind] = "Player"+numberOfPlayers;
					//generatePieces();

					int nextPiece = nextPieceAvailable(i);
					Vector3 pos = new Vector3(location.x-(ind*20), location.y, location.z);
					piecesOnScene[ind] = ((GameObject)Instantiate (pieces [nextPiece], pos, Quaternion.identity));
					currentPieceIndexOnScene [ind] = nextPiece;
					flagPieceNotUsed[nextPiece] = false;
				
					Debug.Log (" : "+nextPiece);
					

				}
				GUI.Label (new Rect (Screen.width / 2 - (ww/2+25), startPointTop + h + 20, ww, hh), Lang.giveMeText( "addplayer")); 
			}


			pLeft = startPoint + ((w + 20) * i) + 25 + 25;
			pTop = startPointTop + h/2 + 25;
			GUI.Box (new Rect (pLeft, pTop, 100, 100), "", "boxes");
			
			int arrowW = 30;
			int arrowH = 40;
			
			if(GUI.Button (new Rect ((pLeft - 35), pTop + 30, arrowW, arrowH), "", "lArrow")) {

				Debug.Log ("Left Arrow no:"+(i+1));

				int currentPiece = currentPieceIndexOnScene[i];
				int prevPiece = prevPieceAvailable(currentPiece);
				Destroy(piecesOnScene[i]);
				
				Vector3 pos = new Vector3(location.x-i*20, location.y, location.z);
				piecesOnScene[i] = ((GameObject)Instantiate (pieces [prevPiece], pos, Quaternion.identity));
				currentPieceIndexOnScene [i] = prevPiece;
				flagPieceNotUsed[prevPiece] = false;
				flagPieceNotUsed[currentPiece] = true;

			}

			if(GUI.Button (new Rect ((pLeft + 105), pTop + 30, arrowW, arrowH), "", "rArrow")) {
				
				int currentPiece = currentPieceIndexOnScene[i];
				int nextPiece = nextPieceAvailable(currentPiece);
				Destroy(piecesOnScene[i]);
				
				Debug.Log (currentPiece+" : "+nextPiece);
				Vector3 pos = new Vector3(location.x-i*20, location.y, location.z);
				piecesOnScene[i] = ((GameObject)Instantiate (pieces [nextPiece], pos, Quaternion.identity));
				currentPieceIndexOnScene [i] = nextPiece;
				flagPieceNotUsed[nextPiece] = false;
				flagPieceNotUsed[currentPiece] = true;
			}
			
			GUI.DrawTexture (new Rect (pLeft, pTop, 100, 100), textureTargetCam [i]);  
			flags [i] = 1;
			
			//Debug.Log ("Flags i:" + i + "elemanı:" + flags [i]);


			
		}	
		
	}
}
