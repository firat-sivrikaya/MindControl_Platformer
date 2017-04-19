using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	public EmoGyroData bibidi;
	void Start () {
		Debug.Log("start player");
	}
	
	// Update is called once per frame
	void Update () {
		int bounce;
		bounce = 0;//bibidi.GTempY;
		//Debug.Log(bounce);
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

				//replace by acceleration
			if (bounce > 50)
				{
				Vector3 position = this.transform.position;
				position.y = position.y + 0.2f;
				this.transform.position = position;
				}
	}
}
