using UnityEngine;
using System.Collections;
using System;

public class Particula : MonoBehaviour {

	public int index;
	public float peso;
	public float[] distancia = new float[4];
	public GameObject cubo;
	
	public Particula()
	{
		cubo = null;
		peso = 0.0f;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void calcularDistanciasParticula(){
		float x = cubo.transform.position.x;
		float z = cubo.transform.position.z;
		distancia[0] = (float)(Math.Sqrt  (Math.Pow (10.0 - x, 2) + Math.Pow (10.0 - z, 2)));
		distancia[1] = (float) (Math.Sqrt (Math.Pow (10.0 - x, 2) + Math.Pow (-10.0 - z, 2)));
		distancia[2] = (float) (Math.Sqrt (Math.Pow (-10.0 - x, 2) + Math.Pow (10.0 - z, 2)));
		distancia[3] = (float) (Math.Sqrt (Math.Pow (-10.0 - x, 2) + Math.Pow (-10.0 - z, 2)));
	}
}
