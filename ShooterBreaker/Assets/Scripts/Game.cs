using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public GameObject m_bar;
	public GameObject m_ball;

	// Use this for initialization
	void Start () {
		m_bar.GetComponent<Rigidbody2D> ().position = new Vector2(0.0f, 0.0f);
		Vector2 size = m_bar.GetComponent<BoxCollider2D> ().size;
		Vector2 size2 = m_ball.GetComponent<BoxCollider2D> ().size;
		m_ball.GetComponent<Rigidbody2D> ().position = new Vector2 (0.0f, (size.y + size2.y)/2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
