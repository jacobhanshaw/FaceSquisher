using Meta;
using UnityEngine;
using System.Collections;

public class PinchScript : MonoBehaviour
{
		private const float PINCH_DEBOUNCE_TIME = 0.5f;

		private float leftPinchDebounce = -1.0f;
		private float rightPinchDebounce = -1.0f;
		private bool leftPinchReleased = true;
		private bool rightPinchReleased = true;
		public GameObject pinchPrefab;

		// Use this for initialization
		void Start ()
		{
				Meta.InputIndicators.Instance.handCloud = true;
				//	pinchPrefab = GameObject.CreatePrimitive (PrimitiveType.Cube);
				//		pinchPrefab.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		}
	
		// Update is called once per frame
		void Update ()
		{
				CheckForPinch (Hands.left.gesture, ref leftPinchReleased, ref leftPinchDebounce);
				CheckForPinch (Hands.right.gesture, ref rightPinchReleased, ref rightPinchDebounce);
		}

		private void CheckForPinch (Gesture gesture, ref bool pinchReleased, ref float pinchDebounce)
		{
				if (gesture.type != Meta.MetaGesture.PINCH) {
						pinchDebounce += Time.deltaTime;
						if (pinchDebounce > PINCH_DEBOUNCE_TIME)
								pinchReleased = true;
				} else {
						if (pinchReleased && gesture.isValid) {
								pinchReleased = false;
								Transform init = Instantiate (pinchPrefab.transform, gesture.position, Quaternion.identity) as Transform;
								if (init.name.Equals ("splash(Clone)"))
										init.rotation = Camera.main.transform.rotation;
		
						}
						pinchDebounce = 0.0f;
				}
				
		}
}
