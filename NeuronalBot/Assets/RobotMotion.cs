using UnityEngine;
using System.Collections;
using System;

public class RobotMotion : MonoBehaviour {

	public float speed;
	private Animator animator;
	public float[] distanciaReal = new float[4];
	public Localization localization;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float currentSpeed = 0.0f;
		
		if (Input.GetAxis ("Horizontal") > 0) 
		{
			transform.Rotate (-Vector3.down);
			for (int i = 0; i<localization.generacionActual.Count; i++) 
			{
				localization.generacionActual[i].cubo.transform.Rotate (-Vector3.down);
			}
		}
		if (Input.GetAxis ("Horizontal") < 0) 
		{
			transform.Rotate (Vector3.down);
			for (int i = 0; i<localization.generacionActual.Count; i++) 
			{
				localization.generacionActual[i].cubo.transform.Rotate (Vector3.down);
			}
		}
		
		if (Input.GetAxis ("Vertical") > 0) {
			currentSpeed = speed;
			transform.Translate (Vector3.forward * Time.deltaTime);
			if (transform.position.z < -10) {
				transform.position = new Vector3 (
				transform.position.x,
				transform.position.y,
				transform.position.z + 20);
			}
			if (transform.position.z > 10) {
				transform.position = new Vector3 (
				transform.position.x,
				transform.position.y,
				transform.position.z - 20);
			}
			if (transform.position.x < -10) {
				transform.position = new Vector3 (
				transform.position.x + 20,
				transform.position.y,
				transform.position.z);
			}
			if (transform.position.x > 10) {
				transform.position = new Vector3 (
				transform.position.x - 20,
				transform.position.y,
				transform.position.z);
			}
			for (int i = 0; i<localization.generacionActual.Count; i++) {
				localization.generacionActual [i].cubo.transform.Translate (Vector3.forward * Time.deltaTime);
				if (localization.generacionActual [i].cubo.transform.position.z < -10) {
						localization.generacionActual [i].cubo.transform.position = new Vector3 (
						localization.generacionActual [i].cubo.transform.position.x,
						localization.generacionActual [i].cubo.transform.position.y,
						localization.generacionActual [i].cubo.transform.position.z + 20);
				}
				if (localization.generacionActual [i].cubo.transform.position.z > 10) {
						localization.generacionActual [i].cubo.transform.position = new Vector3 (
						localization.generacionActual [i].cubo.transform.position.x,
						localization.generacionActual [i].cubo.transform.position.y,
						localization.generacionActual [i].cubo.transform.position.z - 20);
				}
				if (localization.generacionActual [i].cubo.transform.position.x < -10) {
						localization.generacionActual [i].cubo.transform.position = new Vector3 (
						localization.generacionActual [i].cubo.transform.position.x + 20,
						localization.generacionActual [i].cubo.transform.position.y,
						localization.generacionActual [i].cubo.transform.position.z);
				}
				if (localization.generacionActual [i].cubo.transform.position.x > 10) {
						localization.generacionActual [i].cubo.transform.position = new Vector3 (
						localization.generacionActual [i].cubo.transform.position.x - 20,
						localization.generacionActual [i].cubo.transform.position.y,
						localization.generacionActual [i].cubo.transform.position.z);
				}
			}
		}
		animator.SetFloat("Speed", currentSpeed);
		calDistanciaReal ();

	}
	private void calDistanciaReal(){
		float x = animator.transform.position.x;
		float z = animator.transform.position.z;
		distanciaReal[0] = (float)(Math.Sqrt  (Math.Pow (10.0 - x, 2) + Math.Pow (10.0 - z, 2)));
		distanciaReal [1] = (float) (Math.Sqrt (Math.Pow (10.0 - x, 2) + Math.Pow (-10.0 - z, 2)));
		distanciaReal [2] = (float)(Math.Sqrt (Math.Pow (-10.0 - x, 2) + Math.Pow (10.0 - z, 2)));
		distanciaReal [3] = (float)(Math.Sqrt (Math.Pow (-10.0 - x, 2) + Math.Pow (-10.0 - z, 2)));
	}
}
