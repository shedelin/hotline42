using UnityEngine;
using System.Collections;

public class Patrolling_vert : MonoBehaviour {

	public float speed = 2.5f;
	public float rotationSpeed = 270.0f;
	public Vector3 pos_finale;
	private Vector3 pos_initiale;
	private Vector3 pos_to_go;
	private Quaternion qTo;
	private bool walk = false;
	private Enemy script_enemy;
	private SeePlayer script_see;

	void Start() {
		pos_initiale = transform.localPosition;
		script_enemy = this.GetComponent<Enemy> ();
		script_see = this.GetComponent<SeePlayer> ();
	}

	void Update() {
		if (!script_enemy.dead && !script_see.see) {
			if (!walk) {
				if (pos_to_go == pos_initiale)
					pos_to_go = pos_finale;
				else
					pos_to_go = pos_initiale;
				walk = true;
				transform.GetChild(2).GetComponent<Animator>().SetBool("move", true);
			}
		
			var dir = pos_to_go - transform.position;
		
			if (dir != Vector3.zero) {
				qTo = Quaternion.FromToRotation (-transform.up, dir) * transform.rotation;
				transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, Time.deltaTime * rotationSpeed);
			}
			transform.position = Vector3.MoveTowards (transform.position, pos_to_go, Time.deltaTime * speed);


			if (transform.position == pos_to_go) {
				walk = false;
				transform.GetChild(2).GetComponent<Animator>().SetBool("move", false);
			}
		}
	}
}
