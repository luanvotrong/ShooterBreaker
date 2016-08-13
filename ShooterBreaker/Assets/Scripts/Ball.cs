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
	BoxCollider2D m_collider;
	Rect m_screenRect;
	float m_angle;
	float m_force = 500;

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
			m_angle = Random.Range(25, 155);
			break;
		}
	}

	// Use this for initialization
	void Start () {
		m_rigidBody = GetComponent<Rigidbody2D> ();
		m_collider = GetComponent<BoxCollider2D> ();

		m_screenRect.height = Camera.main.orthographicSize * 2;
		m_screenRect.width = m_screenRect.height / Screen.height * Screen.width;
		m_screenRect.position = Camera.main.GetComponent<Transform> ().position;
	}

	void FixedUpdate()
	{
		switch(m_state)
		{
		case STATE.FLYING:
			float rad = m_angle * Mathf.Deg2Rad;
			Vector2 vec = new Vector2 (Mathf.Cos(rad), Mathf.Sin(rad));
			vec *= m_force * Time.deltaTime;
			m_rigidBody.position += vec;
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		switch (m_state) {
		case STATE.FLYING:
			Vector2 pos = m_rigidBody.position;
			Vector2 size = m_collider.size;
			if(Mathf.Abs(m_angle) > 90)
			{
				if(pos.x - size.x/2 <= m_screenRect.x - m_screenRect.width/2)
				{
					m_angle = -180 - m_angle;
				}
			}
			else if(Mathf.Abs(m_angle) < 90)
			{
				if (pos.x + size.x/2 >= m_screenRect.x + m_screenRect.width/2) 
				{
					m_angle = -180 - m_angle;
				}
			}
			if(m_angle > 0)
			{
				if(pos.y + size.y/2 >= m_screenRect.y + m_screenRect.height/2)
				{
					m_angle = -m_angle;
				}
			}
			else if(m_angle < 0)
			{
				if (pos.y - size.y/2 <= m_screenRect.y - m_screenRect.height/2) 
				{
					m_angle = -m_angle;
				}
			}
			
			if(m_angle > 180)
			{
				m_angle = 360 - m_angle;
			}
			else if(m_angle < -180)
			{
				m_angle = 360 + m_angle;
			}
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		switch (m_state) {
		case STATE.FLYING:
			if (m_angle < 0) {
				m_angle = -m_angle;
			}

			Vector2 pos = m_rigidBody.position;

			if (col.gameObject.tag == "Enemy") {
				Destroy (col.gameObject);
			}
			break;
		}
	}

	void OnMouseDown()
	{
		if (m_state == STATE.WAITING) {
			SetState(STATE.FLYING);
		}
	}
}
