using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public enum STATE{
		WAITING = 0,
		FLYING
	}
	STATE m_state;
	public Vector2 m_initialPos;
	Rigidbody2D m_rigidBody;

	public STATE GetState() {
		return m_state;
	}

	public void SetState(STATE state) {
		m_state = state;

		switch (m_state) {
		case STATE.WAITING:
			m_rigidBody.position = m_initialPos;
			break;
		case STATE.FLYING:
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
}
