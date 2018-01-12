using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	private float						speed = 25;
	[HideInInspector]public AudioClip 	audio_shoot;
	private AudioSource 				source_shoot;
	public bool							coldWeapon;									// false : arme a feu, true : arme blanche
	private int							i = 0;
	private IEnumerator					routine = null;
	private Collider2D					enemy;
	[HideInInspector]public GameObject	player;

	void Awake() {
		source_shoot = GetComponent<AudioSource>();
		source_shoot.PlayOneShot (audio_shoot, 0.7f);
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		Debug.Log (player);
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
		if (obj.gameObject.layer == 10)
		{
			transform.GetComponent<SpriteRenderer>().enabled = false;
			enemy = obj;
			enemy.gameObject.GetComponent<Enemy>().dead = true;
			enemy.gameObject.GetComponent<Enemy>().source_death.PlayOneShot (enemy.gameObject.GetComponent<Enemy>().audio_death, 0.7f);
			routine = RepeatAnime();
			StartCoroutine(routine);
		}
	}

	IEnumerator RepeatAnime()
	{
		yield return new WaitForSeconds(2);
		player.GetComponent<Player>().frags++;
		Destroy(enemy.gameObject);
		Destroy(gameObject);
	}
}
