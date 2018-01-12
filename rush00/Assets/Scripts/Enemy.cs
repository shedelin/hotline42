using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject		weapon;
	public GameObject		bullet;
	public bool				dead = false;
	private Collider2D[]	colliderArray;
	private Vector2			point;
	public AudioClip 		audio_death;
	[HideInInspector]public AudioSource 	source_death;

	// Use this for initialization
	void Start () {
		weapon.transform.GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<Weapon>().wearing;
		weapon.GetComponent<Weapon>().ammo = 0;
		weapon.transform.position = transform.position;
		weapon.transform.rotation = transform.rotation;
		weapon.transform.localPosition = new Vector3(-0.15f, -0.39f, 0);
		transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = false;
		source_death = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
