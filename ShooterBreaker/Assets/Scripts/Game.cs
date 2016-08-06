using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public GameObject m_objectBar;
	Bar m_scriptBar;
	BoxCollider2D m_colliderBar;

	public GameObject m_objectBall;
	Ball m_scriptBall;
	BoxCollider2D m_colliderBall;

	// Use this for initialization
	void Start () {
		m_scriptBar = m_objectBar.GetComponent<Bar> ();
		m_scriptBall = m_objectBall.GetComponent<Ball> ();

		m_colliderBar = m_scriptBar.GetComponent<BoxCollider2D> ();
		m_colliderBall = m_scriptBall.GetComponent<BoxCollider2D> ();

		m_scriptBar.m_initialPos = new Vector2(0.0f, 0.0f);
		m_scriptBall.m_initialPos = new Vector2 (0.0f, (m_colliderBar.size.y + m_colliderBall.size.y) / 2);

		m_scriptBar.SetState (Bar.STATE.WAITING);
		m_scriptBall.SetState (Ball.STATE.WAITING);
	}

	void FixedUpdate(){

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
