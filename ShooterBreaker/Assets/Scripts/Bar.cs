using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar : MonoBehaviour {
	public enum STATE {
		WAITING = 0,
		RELEASED,
		TOUCHED
	}
	STATE m_state;
	public Vector2 m_initialPos;
	Rigidbody2D m_rigidBody;
	Vector3 m_touchPos;
	Rect m_screenRect;

	public GameObject m_prefabRope;
	List<GameObject> m_objectRopes = new List<GameObject> ();

	public STATE GetState() {
		return m_state;
	}
	
	public void SetState(STATE state)
	{
		m_state = state;

		switch (m_state) {
		case STATE.WAITING:
			m_rigidBody.position = m_initialPos;
			BoxCollider2D box = GetComponent<BoxCollider2D> ();
			Vector2 pos = m_rigidBody.position;
			pos.x -= box.size.x / 2;
			//8 hardcoded
			const int NumOfSegments = 7;
			for (int i = 0; i < NumOfSegments; i++) {
				GameObject rope = Instantiate (m_prefabRope);
				Rigidbody2D body = rope.GetComponent<Rigidbody2D> ();
				Vector2 size = rope.GetComponent<BoxCollider2D> ().size;
				Physics2D.IgnoreCollision (rope.GetComponent<BoxCollider2D> (), GetComponent<BoxCollider2D> ());

				pos.x += size.x / 2;
				body.position = pos;
				pos.x += size.x / 2;

				m_objectRopes.Add (rope);
			}

			const float springLength = 1;
			const float dampRatio = 0.8f;
			//Init spring
			for (int i = 0; i < m_objectRopes.Count; i++) {
				GameObject rope = m_objectRopes [i];
				Rigidbody2D body = rope.GetComponent<Rigidbody2D> ();
				Vector2 size = rope.GetComponent<BoxCollider2D> ().size;
				//head
				if (i == 0) {
					rope.AddComponent<SpringJoint2D> ();
					SpringJoint2D[] spring = rope.GetComponents<SpringJoint2D> ();
					//head spring
					SpringJoint2D head = spring[0];
					head.connectedBody = this.GetComponent<Rigidbody2D> ();
					head.connectedAnchor = new Vector2 (-box.size.x / 2, 0.0f);
					head.anchor = new Vector2 (-size.x / 2, 0.0f);
					head.distance = springLength;
					head.dampingRatio = dampRatio;
					//tail spring
					SpringJoint2D tail = spring[1];
					tail.connectedBody = m_objectRopes [1].GetComponent<Rigidbody2D> ();
					tail.connectedAnchor = new Vector2 (-size.x / 2, 0.0f);
					tail.anchor = new Vector2 (size.x / 2, 0.0f);
					tail.distance = springLength;
					tail.dampingRatio = dampRatio;
				}
				//tail
				else if (i == m_objectRopes.Count - 1) {
					SpringJoint2D spring = rope.GetComponent<SpringJoint2D> ();
					spring.connectedBody = this.GetComponent<Rigidbody2D> ();
					spring.connectedAnchor = new Vector2 (box.size.x / 2, 0.0f);
					spring.anchor = new Vector2 (size.x / 2, 0.0f);
					spring.distance = springLength;
					spring.dampingRatio = dampRatio;
				}
				//body
				else {
					SpringJoint2D spring = rope.GetComponent<SpringJoint2D> ();
					spring.connectedBody = m_objectRopes [i + 1].GetComponent<Rigidbody2D> ();
					spring.connectedAnchor = new Vector2 (-size.x / 2, 0.0f);
					spring.anchor = new Vector2 (size.x / 2, 0.0f);
					spring.distance = springLength;
					spring.dampingRatio = dampRatio;
				}
			}
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