using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				Vector3 position = this.transform.position;
				position.x = position.x - 0.2f;
				this.transform.position = position;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				Vector3 position = this.transform.position;
				position.x = position.x + 0.2f;
				this.transform.position = position;
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				//replace by acceleration
				Vector3 position = this.transform.position;
				position.y = position.y + 2;
				this.transform.position = position;
			}
	}
}
