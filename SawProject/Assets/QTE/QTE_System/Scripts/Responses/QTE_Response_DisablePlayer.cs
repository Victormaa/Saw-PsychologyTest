/* This Repsone script disables Movement of Unity's third person character, this is to prevent the user from failing a QTE because they may want to move the character instead*/

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class QTE_Response_DisablePlayer : MonoBehaviour
	{
	

		private GameObject Player;
		//private ThirdPersonUserControl PlayerController;
		private ThirdPersonUserControl Script;

		// Use this for initialization
		void Start ()
		{
		

			Player = GameObject.FindGameObjectWithTag ("Player");
			//PlayerController = Player.GetComponent<ThirdPersonUserControl> ();
			Script = Player.GetComponent<ThirdPersonUserControl> ();
	
		}

		public void SetPlayerControllerParameters (bool flip)
		{
			//PlayerController.enabled = flip;
			//Script.enabled = flip;
			Script.DisableMovement = flip;
		}
	
		// Update is called once per frame
		void Update ()
		{
		
			if (QTE_main.Singleton.TriggeringObject == this.gameObject) {		
			
				//while the QTE is happening
				if (QTE_main.Singleton.QTEactive) {
					SetPlayerControllerParameters (true);
					//Player.animation.Play("idle");
					//Player.GetComponent<Animation>().Blend("idle");
				}

				if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {
			
					//if the QTE completed, and he succedded with option 1
					if (QTE_main.Singleton.succeeded) {
						SetPlayerControllerParameters (false);
					}
			
					//if the QTE completed, and he succedded with option 2 (Dual QTE only)
					if (QTE_main.Singleton.succeeded2) {
						SetPlayerControllerParameters (false);
					}
			
					//if the QTE completed, and he succedded with option 3 (Tri QTE only)
					if (QTE_main.Singleton.succeeded3) {
						SetPlayerControllerParameters (false);
					}
			
					//if the QTE completed, and he succedded with option 4 (Quad QTE only)
					if (QTE_main.Singleton.succeeded4) {
						SetPlayerControllerParameters (false);
					}
			
			
					//if the QTE completed, and he failed
					if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer) {
						SetPlayerControllerParameters (false);
					}
				}
			}
	
		}
	}
}
