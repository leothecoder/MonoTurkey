using UnityEngine;
using System.Collections;

public class GameSetupManager : MonoBehaviour
{

	
		public GameObject[] pieces = new GameObject[10];
		public Vector3 location = Vector3.zero;
		GameObject piece = null;
		int current = 0;
		public Texture texture = null;

		// Use this for initialization
		void Start ()
		{
			piece = ((GameObject)Instantiate (pieces [current], pieces [current].transform.position, Quaternion.identity));
			//pieces [current].AddComponent ("Float");

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		void OnGUI ()
		{
			GUI.Box (new Rect (10, 10, 100, 100), "");
			//		
			//		renderer.material.SetTexture("_BumpMap", bumpMap);
			GUI.DrawTexture (new Rect (200, 10, 100, 100), texture);
	
			if (GUI.Button (new Rect (310, 60, 70, 20), "Next")) {
				current++;
				if (current >= pieces.Length)
						current = pieces.Length - 1;
				else {
						GameObject.DestroyObject (piece.gameObject);
						piece = ((GameObject)Instantiate (pieces [current], location, Quaternion.identity));
				}
			}
	
			if (GUI.Button (new Rect (120, 60, 70, 20), "Previous")) {
				current--;
				if (current < 0) {
						current = 0;
				} else {
						GameObject.DestroyObject (piece.gameObject);
						piece = ((GameObject)Instantiate (pieces [current], location, Quaternion.identity));
				}
			}
		}
}
