using UnityEngine;
using System.Collections;

public class QTE_Response_ChangeMaterialColor : MonoBehaviour
{
	

	public GameObject ObjectToFade;
	public bool Fade = true;
	public Color Success1 = Color.green;
	public Color Success2 = Color.blue;
	public Color Success3 = Color.yellow;
	public Color Success4 = Color.magenta;
	public Color Fail = Color.red;
	
	// Use this for initialization
	void Start ()
	{
		

		
		if (ObjectToFade != null) {
			//var material = ObjectToFade.renderer.material;			
			
			Color color = GetComponent<Renderer> ().material.color;
			color.a = 0.0f;
			ObjectToFade.GetComponent<Renderer> ().material.color = color;
			
		} else {
			Color color = GetComponent<Renderer> ().material.color;
			color.a = 0.0f;
			GetComponent<Renderer> ().material.color = color;
		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (GetComponent<Renderer> ().material.color.a > 0 && Fade) {
			if (ObjectToFade != null) {
				Color newColor = ObjectToFade.GetComponent<Renderer> ().material.color;
				newColor.a -= 0.005f;
				ObjectToFade.GetComponent<Renderer> ().material.color = newColor;	
			} else {
				Color newColor = GetComponent<Renderer> ().material.color;
				newColor.a -= 0.005f;
				GetComponent<Renderer> ().material.color = newColor;
			}
		}		
		
		if (QTE_main.Singleton.TriggeringObject == this.gameObject) {		
			
			//while the QTE is happening
			if (QTE_main.Singleton.QTEactive) {
			}

			if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {
			
				//if the QTE completed, and he succedded with option 1
				if (QTE_main.Singleton.succeeded) {
					if (ObjectToFade != null) {
						//var material = ObjectToFade.renderer.material;			
					
						Color color = ObjectToFade.GetComponent<Renderer> ().material.color;
						color = Success1;
						ObjectToFade.GetComponent<Renderer> ().material.color = color;
					
					} else {
						Color color = GetComponent<Renderer> ().material.color;
						color = Success1;
						GetComponent<Renderer> ().material.color = color;
					}
				}
			
				//if the QTE completed, and he succedded with option 2 (Dual QTE only)
				if (QTE_main.Singleton.succeeded2) {
					if (ObjectToFade != null) {
						//var material = ObjectToFade.renderer.material;			
					
						Color color = ObjectToFade.GetComponent<Renderer> ().material.color;
						color = Success2;

						ObjectToFade.GetComponent<Renderer> ().material.color = color;
					
					} else {
						Color color = GetComponent<Renderer> ().material.color;
						color = Success2;

						GetComponent<Renderer> ().material.color = color;
					}
				
				}
			
				//if the QTE completed, and he succedded with option 3 (Tri QTE only)
				if (QTE_main.Singleton.succeeded3) {
					if (ObjectToFade != null) {
						//var material = ObjectToFade.renderer.material;			
					
						Color color = ObjectToFade.GetComponent<Renderer> ().material.color;
						color = Success3;

						ObjectToFade.GetComponent<Renderer> ().material.color = color;
					
					} else {
						Color color = GetComponent<Renderer> ().material.color;
						color = Success3;
						GetComponent<Renderer> ().material.color = color;
					}
				}
			
				//if the QTE completed, and he succedded with option 4 (Quad QTE only)
				if (QTE_main.Singleton.succeeded4) {				
					if (ObjectToFade != null) {
						//var material = ObjectToFade.renderer.material;			
					
						Color color = ObjectToFade.GetComponent<Renderer> ().material.color;
						color = Success4;
						ObjectToFade.GetComponent<Renderer> ().material.color = color;
					
					} else {
						Color color = GetComponent<Renderer> ().material.color;
						color = Success4;
						GetComponent<Renderer> ().material.color = color;
					}
				}
			
				//if the QTE completed, and he failed
				if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer) {
					if (ObjectToFade != null) {
						//var material = ObjectToFade.renderer.material;			
					
						Color color = ObjectToFade.GetComponent<Renderer> ().material.color;
						color = Fail;
						ObjectToFade.GetComponent<Renderer> ().material.color = color;
					
					} else {
						Color color = GetComponent<Renderer> ().material.color;
						color = Fail;
						GetComponent<Renderer> ().material.color = color;
					}
				
				}
			}
		}
	
	}
}
