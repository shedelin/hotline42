using UnityEngine;
using System.Collections;

public class BulletEnemy : MonoBehaviour
{
	private float						speed = 15;
	[HideInInspector]public AudioClip 	audio_shoot;
	private AudioSource 				source_shoot;
	public bool							coldWeapon;									// false : arme a feu, true : arme blanche
	private int							i = 0;
	private IEnumerator					routine = null;
	private Collider2D					enemy;

	void Start()
	{
		source_shoot = GetComponent<AudioSource>();
		source_shoot.PlayOneShot (audio_shoot, 0.7f);
	}

	// Update is called once per frame
	void Update ()
	{
		if (enemy)
		{
			enemy.gameObject.transform.Rotate(0, 0, 500 * Time.deltaTime, Space.Self);
		}
		else
		{
			if (!coldWeapon)
				transform.Translate(Vector3.right * speed * Time.deltaTime);
			else
			{
				transform.Translate(Vector3.right);
				if (i > 0)
					Destroy(gameObject);
			}
			if (i++ > 100)
				Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D (Collider2D obj)
	{
		if (obj.gameObject.layer == 8)
			Destroy(gameObject);
		if (obj.gameObject.layer == 11)
		{
			transform.GetComponent<SpriteRenderer>().enabled = false;
			enemy = obj;
			enemy.gameObject.GetComponent<Player>().dead = true;
			if (enemy.gameObject.transform.childCount == 5)
				enemy.gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().enabled = true;
			enemy.gameObject.GetComponent<Player>().source_death.PlayOneShot (enemy.gameObject.GetComponent<Player>().audio_death, 0.7f);
			routine = RepeatAnime();
			StartCoroutine(routine);
		}
	}

	IEnumerator RepeatAnime()
	{
		yield return new WaitForSeconds(2);
	}
}
