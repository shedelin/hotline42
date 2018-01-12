using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	public bool		end = false;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.GetComponent<BoxCollider2D>().offset = new Vector2(1.432592f, -1.094686f);
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		Debug.Log(obj.name);
		if (obj.gameObject.layer == 11)
			end = true;
	}
}
