﻿using UnityEngine;
using System.Collections;

public class VortexLauncher : MonoBehaviour
{
	AudioSource repulsionSound;

	// Use this for initialization
	void Start ()
	{
		repulsionSound = GetComponent<AudioSource> ();
	}
	
	IEnumerator OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player" && !StateManager.canPassBarrier) {
			Debug.Log ("Vector on " + other.gameObject);

			repulsionSound.Play ();

			//Get an array of components that are of type Rigidbody
			Rigidbody[] bodies = other.gameObject.GetComponentsInChildren<Rigidbody> ();

			//For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
			foreach (Rigidbody rb in bodies) {
				rb.AddForce (Vector3.up * 20f, ForceMode.VelocityChange);
				rb.isKinematic = false;
			}
			other.gameObject.GetComponent<Animator> ().enabled = false;
			other.gameObject.transform.position += Vector3.back * 2f;

			yield return new WaitForSeconds (3f);
			foreach (Rigidbody rb in bodies) {
				rb.isKinematic = true;
			}
			other.gameObject.GetComponent<Animator> ().enabled = true;
		}
	}
}
