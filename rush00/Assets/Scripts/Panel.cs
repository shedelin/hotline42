using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel : MonoBehaviour {

	public GameObject Name_weapon;
	public GameObject munition;
	public GameObject player;
	private Weapon weapon;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.childCount >= 4) {
			weapon = player.transform.GetChild (3).GetComponent<Weapon> ();
			Name_weapon.GetComponent<Text> ().text = weapon.name;
			if (!weapon.coldWeapon)
				munition.GetComponent<Text> ().text = weapon.ammo.ToString ();
			else
				munition.GetComponent<Text> ().text = "Inf";
		} else {
			Name_weapon.GetComponent<Text> ().text = "No Weapon";
			munition.GetComponent<Text> ().text = "0";
		}
	}
}
