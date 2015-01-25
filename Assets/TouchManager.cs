using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TouchManager : MonoBehaviour {
	public static TouchManager Instance;
	GameObject selectedObj;
	Vector3 dragOffset;



	// Use this for initialization
	void Start () {
		Instance = this;
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

				Vector3 dragPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				dragPoint.z = 0;
				dragOffset = selectedObj.transform.position - dragPoint;
			}
		}
	}
	
	void LeftDrag(){
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100);
		
		if (selectedObj != null) {
			Vector3 objPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			objPos.z = 0;
			selectedObj.transform.position = objPos + dragOffset;
		}
	}
	
	void LeftUp(){
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100);
		if (selectedObj != null) {
			selectedObj = null;
		}
	}



}
