﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;
    public Image damageImage;
    
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Text HealthText;
    public Image HPBar;

    Animator anim;
    AudioSource playerDamagedAudio;
    PlayerMovement playerMovement;

    bool isDead;
    bool damaged; //Is the player being damaged

    void Awake () {
        anim = GetComponent<Animator>();
        playerDamagedAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
    }
	
	void Update () {
        if (damaged)
        {
            if (damageImage != null)
                damageImage.color = flashColour;
        }
        else
        {
            if (damageImage != null)
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        if (HealthText != null)
            HealthText.text = "Health: " + currentHealth;
        if (HPBar != null)
            UpdateHealthBar();
        
    }

    //Carry out actions related to taking damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        playerDamagedAudio.Play();
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    //Controls what happens when the player dies
    void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");

        playerMovement.enabled = false;
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    //Updates the UI component health bar
    public void UpdateHealthBar()
    {
        float HPRatio = 200 * currentHealth / startingHealth;
        HPBar.rectTransform.sizeDelta = new Vector2(HPRatio, 22);
        //HPBar.rectTransform.localPosition = new Vector3(HPRatio / 2 - 100, 0, 0);
        HPBar.rectTransform.localPosition = new Vector3(HPRatio / 2 + 2, -13, 0);
    }
}
