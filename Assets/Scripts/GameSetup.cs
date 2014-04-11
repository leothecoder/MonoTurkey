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
//	public Texture2D[] pieces = new Texture2D[4];
	public string CurrentMenu;
	public GameObject prefab;

	public static string[] names = new string[4];
	public GameObject[] pieces = new GameObject[10];
	public Texture[] textureTargetCam = new Texture[4];
	public static GameObject[] piecesOnScene = new GameObject[4];
	int[] currentPieceIndexOnScene = new int[4]{-1,-1,-1,-1};

	bool[] flagPieceNotUsed = new bool[10]{true, true, true, true, true, true, true, true, true, true};
	public Vector3 location = Vector3.zero;
	
	// Use this for initialization
	void Start ()
	{
		CurrentMenu = "Main";
		//set the flag values		
//		for (int i=0; i<flags.Length; i++) {
//			flags [i] = 0;				
//		}

		//load background image
		image = (Texture2D)Resources.Load ("background-monopoly");

//		for(int i=0 ; i< pieces.Length; i++)
//			pieces[i] = GameObject.Find ("p"+i);

		generatePieces ();
		Debug.Log (currentPieceIndexOnScene.ToString ());

		for(int i = 0; i<numberOfPlayers; i++)
			names[i] = "Player"+(i+1);
	}

	void generatePieces(){
		for (int i = 0; i<numberOfPlayers; i++) {
//		Vector3 pos = new Vector3(location.x, location.y, location.z);
//		piecesOnScene[0] = ((GameObject)Instantiate (pieces [0], pos, Quaternion.identity));
//		currentPieceIndexOnScene [0] = 0;
//
//		pos = new Vector3(location.x-20, location.y, location.z);
//		piecesOnScene[1] = ((GameObject)Instantiate (pieces [1], pos, Quaternion.identity));
//		currentPieceIndexOnScene [1] = 1;
				
			int nextPiece = nextPieceAvailable(i);
			Vector3 pos = new Vector3(location.x-(i*20), location.y, location.z);
			piecesOnScene[i] = ((GameObject)Instantiate (pieces [nextPiece], pos, Quaternion.identity));
			currentPieceIndexOnScene [i] = nextPiece;
			flagPieceNotUsed[nextPiece] = false;

		}
	}

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

	void destroyPieces(){
		
		for (int i = 0; i<numberOfPlayers; i++) 
			Destroy (piecesOnScene [i]);

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
		GUI.Button (new Rect ((Screen.width / 2 - 200), Screen.height / 2 - 200, 400, 100), "", "b1");
		
		//button play
		if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 - 60, 120, 40), "", "b2")) {
			//play
			//Debug.Log ("Play");
			NavGate ("Menu_PlayerProperties");
			
		}		
		
		//button settings
		if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 , 120, 40), "", "b3")) {
			//settings
			//Debug.Log ("Settings");
			NavGate ("Menu_Settings");
			
		}
		
		//button quit
		if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 + 60, 120, 40), "", "b4")) {
			//quit
			//Debug.Log ("Quit");
			
		}
		
		
	}
	
	public string stringToEdit = "Player1";
	
	private void Menu_PlayerProperties ()
	{
		
		if (GUI.Button (new Rect (20, Screen.height - 50, 120, 40),"", "back")) {			
			NavGate ("Main");
		}  

		//house rules
		if (GUI.Button (new Rect (Screen.width - 250, Screen.height - 50, 120, 40),"", "houseRules")) {			
			//NavGate ("HouseRules");
			Application.LoadLevel(2);	
		} 

		if (GUI.Button (new Rect (Screen.width - 130, Screen.height - 50, 120, 40), "", "play")) {
			//GameManager.pieces = pieces;
			GameManager.chosenPieces = currentPieceIndexOnScene;
//			GameManager.chosenNames = names;
			Application.LoadLevel(1);	
		}
		
		PlayerPanel ();	
		
	}
	
	private void Menu_Settings ()
	{
		
		
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
		
		int total = w * numberOfPlayers + (20 * numberOfPlayers - 1);
		float startPoint = (Screen.width / 2) - (total / 2);

		for (int i=0; i< numberOfPlayers; i++) {


			GUI.Box (new Rect (startPoint + ((w + 20) * i), Screen.height / 2 - h / 2, w, h), "Player" + (i+1));
			GUI.Label (new Rect (startPoint + ((w + 20) * i) + 10, (Screen.height / 2 - h / 2) + 30, 40, 20), "Name");
			names[i] = GUI.TextArea (new Rect (startPoint + ((w + 20) * i) + 60, (Screen.height / 2 - h / 2) + 30, 100, 25), names[i], 25);
			if (i > numberOfPlayers-2 && numberOfPlayers>2) {
				if (GUI.Button (new Rect (startPoint + ((w + 20) * i) + 25 +15, (Screen.height / 2 - h / 2 + 30) + 40, 120, 40), "","remove")) {
					//destroyPieces();
					int pieceInd = currentPieceIndexOnScene [i];					
					flagPieceNotUsed[pieceInd] = true;
					currentPieceIndexOnScene [i] = -1;
					numberOfPlayers--;
					//generatePieces();

				}
			}
			
			if (numberOfPlayers < 4)
				if (GUI.Button (new Rect (Screen.width / 2 - 85, Screen.height / 2 + h / 2 + 20, 120, 40),"", "addPlayer")) { 
					//destroyPieces();
					numberOfPlayers++;	
					names[i] = "Player"+numberOfPlayers;
					//generatePieces();

					int nextPiece = nextPieceAvailable(i);
					Vector3 pos = new Vector3(location.x-(numberOfPlayers-1*20), location.y, location.z);
					piecesOnScene[i] = ((GameObject)Instantiate (pieces [nextPiece], pos, Quaternion.identity));
					currentPieceIndexOnScene [i] = nextPiece;
					flagPieceNotUsed[nextPiece] = false;
				
					Debug.Log (" : "+nextPiece);
				}



			pLeft = startPoint + ((w + 20) * i) + 25 + 25;
			pTop = (Screen.height / 2) + 25;
			GUI.Box (new Rect (pLeft, pTop, 100, 100), "", "boxes");
			
			int arrowW = 30;
			int arrowH = 30;
			
			if(GUI.Button (new Rect ((pLeft - 35), pTop + 30, arrowW, arrowH), "", "lArrow")) {

				Debug.Log ("Left Arrow no:"+(i+1));
//				currentPieceIndexOnScene[i]--;
//				if (currentPieceIndexOnScene[i] < 0)
//					currentPieceIndexOnScene[i] = 0;
//				else {
//					GameObject.DestroyImmediate (pieces [currentPieceIndexOnScene[i]].gameObject, true);
//					pieces [currentPieceIndexOnScene[i]] = ((GameObject)Instantiate (pieces [currentPieceIndexOnScene[i]], location, Quaternion.identity));
				//				}
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

//				Debug.Log ("Right Arrow no:"+(i+1));
//				currentPieceIndexOnScene[i]++;
//				if (currentPieceIndexOnScene[i] >= pieces.Length)
//					currentPieceIndexOnScene[i] = pieces.Length - 1;
//				else {
//					GameObject.DestroyImmediate (pieces [currentPieceIndexOnScene[i]].gameObject, true);
//
//					pieces [currentPieceIndexOnScene[i]] = ((GameObject)Instantiate (pieces [currentPieceIndexOnScene[i]], location, Quaternion.identity));
//				}
				
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
