using UnityEngine;
using System.Collections;
using System;

public class Visualizer : MonoBehaviour
{
		public Shader shader;
		public GameObject line;
		int lines = 0;
		AudioSource source;
		GameObject[] lineRenderers;
	
		void Start ()
		{
				source = this.audio;
				lineRenderers = new GameObject[1024];
				for (int i = 0; i<1023; i++) {
						lines++;
						lineRenderers [i] = (GameObject)GameObject.Instantiate (line);
						if (i != 0) {
								lineRenderers [i].SetActive (true);
						}
						lineRenderers [i].name = "Line " + lines;
						lineRenderers [i].transform.parent = line.transform.parent;
						lineRenderers [i].GetComponent<LineRenderer> ().material = new Material (shader);
						lineRenderers [i].GetComponent<LineRenderer> ().SetWidth (0.50F, 0.50F);
						lineRenderers [i].GetComponent<LineRenderer> ().SetColors (Color.green, Color.green);
				}
		}

		void Update ()
		{
				if (source.isPlaying == true) {
						float[] spectrum = source.GetSpectrumData (1024, 0, FFTWindow.BlackmanHarris);
						for (int i = 1; i<1023; i++) {
								Debug.DrawLine (new Vector3 (i - 1, Mathf.Log (spectrum [i - 1]) + 10, 2), new Vector3 (i, Mathf.Log (spectrum [i]) + 10, 2), Color.cyan);
								lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (0, new Vector3 (i - 1, Mathf.Log (spectrum [i - 1]) + 10, 2));
								lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (i, Mathf.Log (spectrum [i]) + 10, 2));
						}
				} else {
						for (int i = 1; i<1023; i++) {
								Debug.DrawLine (new Vector3 (0, 10, 2), new Vector3 (0, 10, 2), Color.cyan);
								lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (0, new Vector3 (i - 1, 0, 2));
								lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (i, 0, 2));
						}
				}
		}

		void OnGUI ()
		{
				GUI.Label (new Rect (0, 0, 200, 100), "Name: " + source.clip.name);
				TimeSpan ts = TimeSpan.FromSeconds ((source.clip.length - source.time));
				string timeLeft = ts.Minutes.ToString ("00") + ":" + ts.Seconds.ToString ("00");
				GUI.Label (new Rect (0, 20, 200, 100), "Time Left: " + timeLeft);
				if (GUI.Button (new Rect (0, 40, 50, 20), "Play")) {
						if (source.isPlaying == false) {
								source.Play ();
						}
				}
		
				if (GUI.Button (new Rect (0, 60, 50, 20), "Pause")) {
						if (source.isPlaying == true) {
								source.Pause ();
						}
				}
		}
	
}
