using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int m_healthPoint;
	public Rigidbody2D m_rigidBody;

	// Use this for initialization
	void Start () {
		m_healthPoint = 1;
		m_rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
