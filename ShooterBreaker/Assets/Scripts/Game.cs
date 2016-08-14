using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	Rect m_screenRect;
	public GameObject m_prefabEnemy;

	public GameObject m_objectBar;
	Bar m_scriptBar;
	BoxCollider2D m_colliderBar;

	public GameObject m_objectBall;
	Ball m_scriptBall;
	BoxCollider2D m_colliderBall;

	// Use this for initialization
	void Start () {
		m_screenRect.height = Camera.main.orthographicSize * 2;
		m_screenRect.width = m_screenRect.height / Screen.height * Screen.width;
		m_screenRect.position = Camera.main.GetComponent<Transform> ().position;

		m_scriptBar = m_objectBar.GetComponent<Bar> ();
		m_scriptBall = m_objectBall.GetComponent<Ball> ();

		m_colliderBar = m_scriptBar.GetComponent<BoxCollider2D> ();
		m_colliderBall = m_scriptBall.GetComponent<BoxCollider2D> ();

		m_scriptBar.m_initialPos = new Vector2(0.0f, 0.0f);
		m_scriptBall.m_initialPos = new Vector2 (0.0f, (m_colliderBar.size.y + m_colliderBall.size.y) / 2);

		m_scriptBar.SetState (Bar.STATE.WAITING);
		m_scriptBall.SetState (Ball.STATE.WAITING);

		//init enemies
		Rect enemyRect = new Rect(m_screenRect);
		enemyRect.x -= enemyRect.width / 2;
		enemyRect.y += enemyRect.height/2;
		enemyRect.height /= 4;
		Vector2 enemySize = m_prefabEnemy.GetComponent<BoxCollider2D> ().size;
		for (int x = 0; x < 8; x++) {
			float posx = enemyRect.x + enemyRect.width / 9 * (x + 1);
			for (int y = 0; y < 4; y++) {
				float posy = enemyRect.y - enemyRect.height / 5 * y;
				Instantiate (m_prefabEnemy, new Vector3 (posx + enemySize.x / 2, posy - enemySize.y, 0.0f), Quaternion.identity);
			}
		}

		//Init Camera edge collider
		EdgeCollider2D edgeCollider = Camera.main.GetComponent<EdgeCollider2D> ();
		Vector2[] points = new Vector2[5];
		points [0] = new Vector2 (m_screenRect.x - m_screenRect.width/2, 0 - m_screenRect.height/2);
		points [1] = new Vector2 (m_screenRect.x - m_screenRect.width/2, 0 + m_screenRect.height/2);
		points [2] = new Vector2 (m_screenRect.x + m_screenRect.width/2, 0 + m_screenRect.height/2);
		points [3] = new Vector2 (m_screenRect.x + m_screenRect.width/2, 0 - m_screenRect.height/2);
		points [4] = new Vector2 (m_screenRect.x - m_screenRect.width/2, 0 - m_screenRect.height/2);
		edgeCollider.points = points;
	}

	// Update is called each fixed deltaTime
	void FixedUpdate(){

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
