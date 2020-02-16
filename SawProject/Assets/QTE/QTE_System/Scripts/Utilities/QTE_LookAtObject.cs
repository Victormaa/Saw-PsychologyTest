/*Script that allows world space UI buttons to turn/rotate to look at an object*/

using UnityEngine;
using System.Collections;

public class QTE_LookAtObject : MonoBehaviour {

	public Transform ObjectToLookAT;
	public bool InvertDirection;
	public bool YAxisOnly;

	public float damping = 1.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Vector3 v = ObjectToLookAT.position - transform.position;
		//v.x = v.z = 0.0f;
		if(InvertDirection){
			if(YAxisOnly){
				var lookPos = ObjectToLookAT.position - transform.position;
                lookPos.y = 0;
                //lookPos.x = transform.position.x;
                // lookPos.z = transform.position.z;      
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = rotation;
                transform.rotation *= Quaternion.Euler(0, 180f, 0);
            }
            else{
				transform.LookAt(2 * transform.position - ObjectToLookAT.position);
			}
				
		}else{
			if(YAxisOnly){
				var lookPos = ObjectToLookAT.position - transform.position;
				lookPos.y = 0;
				var rotation = Quaternion.LookRotation(lookPos);
				transform.rotation =rotation;
			}else{
				transform.LookAt( ObjectToLookAT ); 
			}
		}

		//transform.Rotate(0,180,0);
	
	}
}
