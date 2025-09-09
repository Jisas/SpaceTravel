using UnityEngine;

public class BulletMovement : MonoBehaviour 
{
	void Update () 
	{
		transform.position += transform.forward * Time.deltaTime * 1000f;	
	}
}
