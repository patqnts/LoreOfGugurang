using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public InventoryWeaponCheck weaponCheck;
    public Animator animator;
    public int damageAttack;
    public AudioSource audioSource;
    public AudioClip[] clips;

    // 0 - attack
    // 1 - death
    // 2- orasyon sound effect
    public void PlaySound(int index)
    {
        audioSource.clip = clips[index];
        audioSource.Play();
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        weaponCheck = GameObject.FindObjectOfType<InventoryWeaponCheck>();
    }

    public void DoMethod(string weaponID)
    {
        weaponCheck.IsWeaponEquipped(weaponID);
    }
    private bool isDead=false;
    public void Death()
    {
        isDead= true;
        animator.Play("Death");
    }

    public void SetDead()
    {
        isDead = true;
    }

    public void DisabeSelf()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isDead)
        {
            animator.Play("Attack");
        }
        
    }

    public void HideSelf()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Usable>().enabled = false;
    }

    public void AttackMethod()
    {
        SinagScript.instance.TakeDamage(damageAttack);
    }
}
