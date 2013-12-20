using UnityEngine;
using System.Collections;

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
						lineRenderers [i].SetActive (true);
						lineRenderers [i].name = "Line " + lines;
						lineRenderers [i].transform.parent = line.transform.parent;
						lineRenderers [i].GetComponent<LineRenderer> ().material = new Material (shader);
						lineRenderers [i].GetComponent<LineRenderer> ().SetWidth (0.50F, 0.50F);
						lineRenderers [i].GetComponent<LineRenderer> ().SetColors (Color.green, Color.green);
				}
		}

		void Update ()
		{
				float[] spectrum = source.GetSpectrumData (1024, 0, FFTWindow.BlackmanHarris);
				for (int i = 1; i<1023; i++) {
						Debug.DrawLine (new Vector3 (i - 1, Mathf.Log (spectrum [i - 1]) + 10, 2), new Vector3 (i, Mathf.Log (spectrum [i]) + 10, 2), Color.cyan);
						lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (0, new Vector3 (i - 1, Mathf.Log (spectrum [i - 1]) + 10, 2));
						lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (i, Mathf.Log (spectrum [i]) + 10, 2));
				}
		}

		void OnGUI ()
		{
				if (GUI.Button (new Rect (0, 0, 50, 20), "Play")) {
						if (source.isPlaying == false) {
								source.Play ();
						}
				}

				if (GUI.Button (new Rect (0, 20, 50, 20), "Pause")) {
						if (source.isPlaying == true) {
								source.Pause ();
						}
				}
		}
	
}
