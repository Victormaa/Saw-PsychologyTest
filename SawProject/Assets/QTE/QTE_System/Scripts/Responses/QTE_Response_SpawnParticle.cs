/*QTE Response Script that spawns a particle*/

using UnityEngine;
using System.Collections;

public class QTE_Response_SpawnParticle : MonoBehaviour
{
	

	public Transform SpawnLocation;
	public Transform ActiveParticlePrefab;
	public Transform Success1ParticlePrefab;
	public Transform Success2ParticlePrefab;
	public Transform Success3ParticlePrefab;
	public Transform Success4ParticlePrefab;
	public Transform FailParticlePrefab;

	// Use this for initialization
	void Start ()
	{
		

	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (QTE_main.Singleton.TriggeringObject == this.gameObject) {		
			
			if (QTE_main.Singleton.QTEactive && ActiveParticlePrefab != null) {
				if (SpawnLocation == null) {
					Instantiate (ActiveParticlePrefab, transform.position, transform.rotation);
				} else {
					Instantiate (ActiveParticlePrefab, SpawnLocation.position, SpawnLocation.rotation);
				}
			}
			if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {
			
				//if the QTE completed, and he succedded with option 1
				if (QTE_main.Singleton.succeeded) {
					if (SpawnLocation == null) {
						Instantiate (Success1ParticlePrefab, transform.position, transform.rotation);
					} else {
						Instantiate (Success1ParticlePrefab, SpawnLocation.position, SpawnLocation.rotation);
					}
				}
			
				//if the QTE completed, and he succedded with option 2 (Dual QTE only)
				if (QTE_main.Singleton.succeeded2) {
					if (SpawnLocation == null) {
						Instantiate (Success2ParticlePrefab, transform.position, transform.rotation);
					} else {
						Instantiate (Success2ParticlePrefab, SpawnLocation.position, SpawnLocation.rotation);
					}
				}
			
				//if the QTE completed, and he succedded with option 3 (Tri QTE only)
				if (QTE_main.Singleton.succeeded3) {
					if (SpawnLocation == null) {
						Instantiate (Success3ParticlePrefab, transform.position, transform.rotation);
					} else {
						Instantiate (Success3ParticlePrefab, SpawnLocation.position, SpawnLocation.rotation);
					}
				}
			
				//if the QTE completed, and he succedded with option 4 (Quad QTE only)
				if (QTE_main.Singleton.succeeded4) {
					if (SpawnLocation == null) {
						Instantiate (Success4ParticlePrefab, transform.position, transform.rotation);
					} else {
						Instantiate (Success4ParticlePrefab, SpawnLocation.position, SpawnLocation.rotation);
					}
				}
			
				//if the QTE completed, and he failed
				if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer) {
					if (SpawnLocation == null) {
						Instantiate (FailParticlePrefab, transform.position, transform.rotation);
					} else {
						Instantiate (FailParticlePrefab, SpawnLocation.position, SpawnLocation.rotation);
					}
				}
			}
		}
	
	}
}
