using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 1000000;
	public int currentHealth;
	public Slider healthSlider;

	Controller2D controller;

	bool isDead;
	bool damaged;

	void Awake(){
		controller = GetComponent<Controller2D>();
		currentHealth = startingHealth;
	}

	void Update(){
		if (damaged){
			print ("hit");
		}
		damaged = false;
	}

	public void TakeDamage(int amount){
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		if(currentHealth <= 0 && !isDead){
			Death();
		}
	}

	void Death(){
		isDead = true;
		controller.enabled = false;

	}
}
