using UnityEngine;
using System.Collections;

public class PieceOnGameSetup : MonoBehaviour {

	public static bool turn = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(turn)
			transform.Rotate(Vector3.up * Time.deltaTime * 100);
	}
}
