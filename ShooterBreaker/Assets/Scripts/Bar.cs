using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {
	public enum STATE {
		WAITING = 0,
		RELEASED,
		TOUCHED
	}
	STATE m_state;
	public Vector2 m_initialPos;
	Rigidbody2D m_rigidBody;
	SpringJoint2D m_springJoint;
	Vector3 m_touchPos;
	Rect m_screenRect;
	public GameObject[] m_rope;

	public STATE GetState() {
		return m_state;
	}
	
	public void SetState(STATE state)
	{
		m_state = state;

		switch (m_state) {
		case STATE.WAITING:
			m_rigidBody.position = m_initialPos;
			break;
		case STATE.RELEASED:
			m_touchPos = Vector3.zero;
			break;
		case STATE.TOUCHED:
			m_touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			break;
		}
	}

	// Use this for initialization
	void Start () {
		m_rigidBody = GetComponent<Rigidbody2D> ();
		m_springJoint = GetComponent<SpringJoint2D> ();
		m_springJoint.anchor = new Vector2 (200, m_springJoint.anchor.y);
	}

	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
	}

	void OnMouseDown()
	{
		SetState (STATE.TOUCHED);
	}

	void OnMouseDrag()
	{
		if (m_state == STATE.TOUCHED) {
			Vector3 dist = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_touchPos;
			Vector3 newPos = m_rigidBody.position;
			newPos.x += dist.x;
			m_rigidBody.MovePosition(newPos);
			m_touchPos += dist;
		}
	}

	void OnMouseUp()
	{
		SetState (STATE.RELEASED);
	}
}
