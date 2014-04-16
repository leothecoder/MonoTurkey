using UnityEngine;
using System.Collections;

public class Hrules : MonoBehaviour
{

		


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
		public GUISkin myStyle = null;
		Texture2D image = null;
		Texture2D title = null;
		public 	int k = 10;
		public int l = 60;
		/*
	 	*
	 	*	**************************House Rules Variables*************************
	 	*									END
	 	*
	 	*/
		// Use this for initialization
		void Start ()
		{
				image = (Texture2D)Resources.Load ("background-monopoly");
				title = (Texture2D)Resources.Load ("titleHouse");
				
				toolbarInt [0] = 0;		
				toolbarInt [1] = 1;		
				toolbarInt [2] = 2;		
				toolbarInt [3] = 3;		
				toolbarInt [4] = 1;		
		
				
				defaultRules = "Houses per hotel:" + housesPerHotel [toolbarInt [0]] + "  Free Parking:" + freeParking [toolbarInt [1]] +
			
						"  Salary:" + selStrings [toolbarInt [2]] + "   Mortgage rate:" + mortgageRate [toolbarInt [3]] + "   Luxury Tax:" + luxTax [toolbarInt [4]];

		}

		void Awake ()
		{

		}
		// Update is called once per frame
		void Update ()
		{
				
		}

		void OnGUI ()
		{

				if (myStyle != null) {
			
						GUI.skin = myStyle;
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
			//here save the datebase
			
		}
		if (GUI.Button (new Rect ((a -a/6)-(a/16+10), b / 6 + 7 * spaceRule, a / 16, b / 15), "Back", "turnLimit")) {
			//back only
			Application.LoadLevel(0);	
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


		






		
		       
		
}






 