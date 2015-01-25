using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TouchManager : MonoBehaviour {
	GameObject selectedObj;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			LeftClick();
		}
		else if (Input.GetMouseButton (0)) {
			LeftDrag();
		}
		else if (Input.GetMouseButtonUp (0)) {
			LeftUp();
		}
		

		
		
	}
	
	
	// creates the ring that follows the player's mouse

	
	//selects the structre the mouse is over
	void LeftClick(){

		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100);
		//Debug.Log (Camera.main.ScreenToWorldPoint(Input.mousePosition));
		if (hit.collider != null) {

			if (hit.transform.tag == "Polygon"){

				selectedObj = hit.transform.gameObject;
			}
		}
	}
	
	void LeftDrag(){
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100);
		
		if (selectedObj != null) {
			Vector3 objPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			objPos.z = 0;
			selectedObj.transform.position = objPos;
		}
	}
	
	void LeftUp(){
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100);
		if (selectedObj != null) {
			selectedObj = null;
		}
	}



}
