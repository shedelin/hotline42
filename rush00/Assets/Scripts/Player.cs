using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
	public GameObject						bullet;
	public bool								dead = false;
	private float							speed = 400;
	private int								i = 0;
	public AudioClip 						audio_death;
	public AudioClip 						audio_get_weapon;
	public AudioClip 						audio_get_sword;
	[HideInInspector]public AudioSource 	source_death;
	public int								frags = 0;

	// Use this for initialization
	void Start ()
	{
		source_death = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!dead)
		{
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			{
				if (Input.GetKey(KeyCode.W))
				{
					transform.GetComponent<Rigidbody2D>().velocity = Vector3.up * speed * Time.deltaTime;
					transform.GetComponent<Rigidbody2D>().AddForce(Vector3.up * speed * Time.deltaTime, ForceMode2D.Force);
				}
				if (Input.GetKey(KeyCode.S))
				{
					transform.GetComponent<Rigidbody2D>().velocity = Vector3.down * speed * Time.deltaTime;
					transform.GetComponent<Rigidbody2D>().AddForce(Vector3.down * speed * Time.deltaTime, ForceMode2D.Force);
				}
				if (Input.GetKey(KeyCode.A))
				{
					transform.GetComponent<Rigidbody2D>().velocity = Vector3.left * speed * Time.deltaTime;
					transform.GetComponent<Rigidbody2D>().AddForce(Vector3.left * speed * Time.deltaTime, ForceMode2D.Force);
				}
				if (Input.GetKey(KeyCode.D))
				{
					transform.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed * Time.deltaTime;
					transform.GetComponent<Rigidbody2D>().AddForce(Vector3.right * speed * Time.deltaTime, ForceMode2D.Force);
				}
				transform.GetChild(2).GetComponent<Animator>().SetBool("move", true);
			}
			else
			{
				transform.GetChild(2).GetComponent<Animator>().SetBool("move", false);
				transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				if (transform.childCount >= 4)
				{
					GameObject item = transform.GetChild(3).gameObject;
					item.GetComponent<SpriteRenderer>().sortingOrder = 0;
					item.transform.GetComponent<SpriteRenderer>().sprite = item.GetComponent<Weapon>().item;
					item.layer = 9;
					item.transform.GetComponent<BoxCollider2D>().enabled = true;
					item.transform.parent = null;
				}
			}
			Vector3		mouse_pos = Input.mousePosition;
			Vector3		object_pos = Camera.main.WorldToScreenPoint(transform.position);
			mouse_pos.x = mouse_pos.x - object_pos.x;
			mouse_pos.y = mouse_pos.y - object_pos.y;
			float		angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle + 90);
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				if (transform.childCount >= 4)
				{
					if (i < 0 || transform.GetChild(3).gameObject.GetComponent<Weapon>().coldWeapon)
					{
						i = transform.GetChild(3).gameObject.GetComponent<Weapon>().rate;
						if (transform.GetChild(3).gameObject.GetComponent<Weapon>().ammo > 0 || transform.GetChild(3).gameObject.GetComponent<Weapon>().coldWeapon)
						{
							if (!transform.GetChild(3).gameObject.GetComponent<Weapon>().coldWeapon)
								transform.GetChild(3).gameObject.GetComponent<Weapon>().ammo--;
							bullet.transform.GetComponent<SpriteRenderer>().sprite = transform.GetChild(3).gameObject.GetComponent<Weapon>().shoot;
							bullet.transform.GetComponent<Bullet>().audio_shoot = transform.GetChild(3).gameObject.GetComponent<Weapon>().audio_shoot;
							bullet.transform.GetComponent<Bullet>().coldWeapon = transform.GetChild(3).gameObject.GetComponent<Weapon>().coldWeapon;
							Instantiate(bullet, transform.GetChild(3).gameObject.transform.position, Quaternion.Euler(0, 0, angle));
						}
					}
				}
			}
			i--;
		}
	}
}
