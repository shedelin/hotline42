using UnityEngine;
using System.Collections;

public class SeePlayer : MonoBehaviour {

	private Quaternion qTo;
	public GameObject player;
	public bool see = false;
	private GameObject weapon;
	private GameObject bullet;
	public float speed = 1.5f;
	public float rotationSpeed = 270.0f;
	private int				i = 0;
	private IEnumerator					routine = null;

	// Use this for initialization
	void Start () {
		weapon = transform.GetChild (3).gameObject;
		bullet = transform.GetComponent<Enemy>().bullet;
	}
	
	// Update is called once per frame
	void Update () {
		if (!transform.GetComponent<Enemy>().dead) {
			if (see) {
				Vector2 vec_p = player.transform.localPosition - this.transform.localPosition;
				transform.GetComponent<BoxCollider2D> ().enabled = false;
				transform.GetComponent<PolygonCollider2D> ().enabled = false;
				RaycastHit2D hit = Physics2D.Raycast (transform.localPosition, vec_p);
				transform.GetComponent<BoxCollider2D> ().enabled = true;
				transform.GetComponent<PolygonCollider2D> ().enabled = true;
				if (hit.collider.gameObject.layer == 11) {
					var dir = hit.collider.transform.localPosition - transform.localPosition;
				
					if (dir != Vector3.zero) {
						qTo = Quaternion.FromToRotation (-transform.up, dir) * transform.rotation;
						transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, Time.deltaTime * rotationSpeed);
					}
					transform.position = Vector3.MoveTowards (transform.position, hit.collider.transform.localPosition, Time.deltaTime * speed);
					transform.GetChild (2).GetComponent<Animator> ().SetBool ("move", true);
					if (i < 0 || transform.GetChild (3).gameObject.GetComponent<Weapon> ().coldWeapon) {
						i = transform.GetChild (3).gameObject.GetComponent<Weapon> ().rate;
						routine = RepeatAnime ();
						StartCoroutine (routine);
					}
				} else {
					see = false;
					transform.GetChild (2).GetComponent<Animator> ().SetBool ("move", false);
				}
			}
			i--;
		}
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.layer == 11)
		{
			see = true;
		}
	}

	IEnumerator RepeatAnime()
	{
		Vector3		player_pos = Camera.main.WorldToScreenPoint(player.transform.position);
		Vector3		object_pos = Camera.main.WorldToScreenPoint(transform.position);
		player_pos.x = player_pos.x - object_pos.x;
		player_pos.y = player_pos.y - object_pos.y;
		float		angle = Mathf.Atan2(player_pos.y, player_pos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle + 90);
		bullet.transform.GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<Weapon>().shoot;
		bullet.transform.GetComponent<BulletEnemy>().audio_shoot = weapon.GetComponent<Weapon>().audio_shoot;
		bullet.transform.GetComponent<BulletEnemy>().coldWeapon = weapon.GetComponent<Weapon>().coldWeapon;
		yield return Instantiate(bullet, weapon.transform.position, Quaternion.Euler(0, 0, angle));;

	}
}
