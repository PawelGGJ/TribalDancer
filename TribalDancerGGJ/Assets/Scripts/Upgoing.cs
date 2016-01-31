using UnityEngine;
using System.Collections;

public class Upgoing : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   transform.Translate(0f, .5f * Time.deltaTime, 0f);
	}
}
