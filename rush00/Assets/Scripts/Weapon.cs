using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public Sprite		item;
	public Sprite		wearing;
	public Sprite		shoot;
	public AudioClip 	audio_shoot;
	public bool			coldWeapon;
	public int			ammo;
	public int			rate;
	private bool		anime = false;
	private int			i = 0;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!transform.parent)
		{
			if (i++ > 10)
			{
				i = 0;
				this.GetComponent<SpriteRenderer>().color = ((anime = !anime) ? Color.blue : Color.white);
			}
		}
		else
			this.GetComponent<SpriteRenderer>().color = Color.white;
	}
	
	void OnTriggerStay2D(Collider2D obj)
	{
		if (Input.GetKeyDown(KeyCode.E) && gameObject.layer == 9)
		{
			GameObject item = obj.gameObject;
			if (item.layer == 11 && item.transform.childCount < 4)
			{
				transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
				this.gameObject.layer = item.layer;
				transform.position = item.transform.position;
				transform.rotation = item.transform.rotation;
				transform.parent = item.transform;
				transform.GetComponent<SpriteRenderer>().sprite = item.transform.GetChild(3).GetComponent<Weapon>().wearing;
				transform.GetComponent<BoxCollider2D>().enabled = false;
				transform.localPosition = new Vector3(-0.15f, -0.39f, 0);
				if (!coldWeapon)
					item.GetComponent<Player>().source_death.PlayOneShot (item.GetComponent<Player>().audio_get_weapon, 0.7f);
				else
					item.GetComponent<Player>().source_death.PlayOneShot (item.GetComponent<Player>().audio_get_sword, 0.7f);
			}
		}
	}
}
