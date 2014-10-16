using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class Localization : MonoBehaviour {

	public List<Particula> generacionActual;
	public GameObject cubo;
	public GameObject[] postes = new GameObject[4];
	//private static RobotMotion robot = new RobotMotion();
	private float[] distanciasReales = new float[4];

	// Use this for initialization
	void Start () {
		generacionActual = new List<Particula> ();
		System.Random r = new System.Random ();
		float posCuboInicialz = (float)((r.Next(-9, 9)) + r.NextDouble());
		float posCuboInicialx = (float)((r.Next(-9, 9)) + r.NextDouble());
		float rotCuboInicialy = (float)((r.Next(360)) + r.NextDouble());
        cubo.transform.position = new Vector3 (posCuboInicialx, 0.0f, posCuboInicialz);
		cubo.transform.Rotate(0,rotCuboInicialy,0);
		Particula p = new Particula ();
		p.cubo = cubo;
		generacionActual.Add (p);
		for (int i = 1; i<1000; i++) {
			generacionActual.Add(new Particula());
			GameObject duplicado = (GameObject)Instantiate(cubo);
			generacionActual[i].cubo = duplicado;
			float posInicialz = (float)((r.Next(-9, 9)) + r.NextDouble());
			float posInicialx = (float)((r.Next(-9, 9)) + r.NextDouble());
			float rotInicialy = (float)((r.Next(360)) + r.NextDouble());
			generacionActual[i].cubo.transform.position = new Vector3 (posInicialx, 0.0f, posInicialz);
			generacionActual[i].cubo.transform.Rotate(0,rotInicialy,0);
			//generacionActual[i].cubo.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool sensing = (Input.GetKeyDown ("space"));
		if(!sensing){
			return;
		}

		calDistanciaReal ();
		for (int i = 0; i<this.generacionActual.Count; i++) {
			Particula p = this.generacionActual[i];
			float prob = 1.0f;
			for(int j = 0; j<4; j++){
				float dist = calcularDistancia(postes[j].transform.position.x, postes[j].transform.position.z, p.cubo.transform.position.x, p.cubo.transform.position.z);
				prob *= formulaGauss(dist, 0.1f, distanciasReales[j]);
			}
			p.peso = prob;
		}
		normalizar ();
		ruleta();
	}

	void normalizar(){
		float w = 0.0f;
		foreach (Particula p in this.generacionActual) {
			w += p.peso;
		}
		for (int i = 0; i<this.generacionActual.Count; i++) {
			this.generacionActual[i].peso /= w;
			print(this.generacionActual[i].peso);
		}
		print (w);
	}
	
	void ruleta(){
		System.Random r = new System.Random ();
		int index = r.Next(generacionActual.Count);
		float beta = 0;
		List<Vector3> posiciones = new List<Vector3> ();
		List<float> orientaciones = new List<float> ();
		float maximo = calcularMaximoPeso ();
		for (int i = 0; i<generacionActual.Count; i++) {
			beta += (float)(r.NextDouble() * 2.0f * maximo);
			while(beta > this.generacionActual[i].peso){
				beta -= this.generacionActual[index].peso;
				index = (index + 1) % generacionActual.Count;
			}
			posiciones.Add(this.generacionActual[index].cubo.transform.position);
			orientaciones.Add(this.generacionActual[index].cubo.transform.rotation.y);
		}
		for (int i = 0; i<this.generacionActual.Count; i++) {
			this.generacionActual[i].cubo.transform.position = posiciones[i];
			this.generacionActual[i].cubo.transform.Rotate(0, orientaciones[i], 0);
		}

	}
	
	float calcularMaximoPeso(){
		float maximo = 0.0f;
		foreach (Particula p in generacionActual) {
			maximo = Math.Max(maximo, p.peso);
		}
		return maximo;
	}
	
	float formulaGauss(float dist, float ruido, float medicion){
		return (float) ((Math.Exp((-(Math.Pow(medicion - dist, 2.0) / ruido))/2)) / (Math.Sqrt(2 * Math.PI * ruido)));
	}
	
	float calcularDistancia(float x1, float y1, float x2, float y2){
		return (float) Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
	}
	
	private void calDistanciaReal(){
		float x = transform.position.x;
		float z = transform.position.z;
		distanciasReales[0] = (float)(Math.Sqrt  (Math.Pow (10.0 - x, 2) + Math.Pow (10.0 - z, 2)));
		distanciasReales [1] = (float) (Math.Sqrt (Math.Pow (10.0 - x, 2) + Math.Pow (-10.0 - z, 2)));
		distanciasReales [2] = (float)(Math.Sqrt (Math.Pow (-10.0 - x, 2) + Math.Pow (10.0 - z, 2)));
		distanciasReales [3] = (float)(Math.Sqrt (Math.Pow (-10.0 - x, 2) + Math.Pow (-10.0 - z, 2)));
	}
}
