using UnityEngine;
using System.Collections;

public class PerfectSpawner2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      if (Input.GetKey(KeyCode.Space))
      {
         var perfect = Instantiate(Resources.Load(@"Prefabs/Perfect")) as GameObject;
         perfect.transform.position = new Vector2(-3.21f, -3.21f);
      }
	}
}
