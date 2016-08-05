using UnityEngine;
using System.Collections;

public class BarController : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3 (transform.position.x + 1*Time.deltaTime, transform.position.y, transform.position.z);
	}

	void FixedUpdate(){
		//Vector3 vec = m_camera.ScreenToWorldPoint (Input.mousePosition );
	}

	void OnMouseDrag()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		Vector3 newpos = body.position;
		newpos.x = pos.x;
		body.position = newpos;
	}
}
