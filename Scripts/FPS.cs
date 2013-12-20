using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour
{
	
		public bool show = true;
	
		//FPS
		public GUIStyle fpsStyle;
		float updateInterval = 0.5F;
		float fps;
		private float accum = 0;
		private int frames = 0;
		private float timeleft;
		string output;
	
		void Start ()
		{
				timeleft = updateInterval;
		}
	
		void Update ()
		{
				if (show == true) {
						timeleft -= Time.deltaTime;
						accum += Time.timeScale / Time.deltaTime;
						++frames;
			
						if (timeleft <= 0.0) {
								fps = accum / frames;
								float fpsFloat = 0.00F;
								fpsFloat = fps;
								output = "FPS: " + fpsFloat.ToString ("0.00");
								timeleft = updateInterval;
								accum = 0.0F;
								frames = 0;
						}
				}
		}
	
		void OnGUI ()
		{
				GUI.depth = -2000;
				if (show == true) {
						if (fps > 30) {
								fpsStyle.normal.textColor = Color.green;
						} else if (fps < 30) {
								fpsStyle.normal.textColor = Color.yellow;
						} else if (fps < 10) {
								fpsStyle.normal.textColor = Color.red;
						}

			
						float outputWidth = fpsStyle.CalcSize (new GUIContent (output)).x;
						GUI.Label (new Rect (Screen.width - outputWidth, 0, 100, 50), output, fpsStyle);
				}

		}
}

