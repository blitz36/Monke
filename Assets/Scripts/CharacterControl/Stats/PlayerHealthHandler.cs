using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthHandler : MonoBehaviour
{
  private float inCombatTimer;
  public float timeBeforeCombatEnds;
  public bool inCombat = false;
  public float hitRecoveryTime;

  public void Update() {
    inCombatCheck();
  }

  public void inCombatCheck() {
      inCombatTimer += Time.deltaTime;
      if (inCombatTimer > timeBeforeCombatEnds) {
        inCombat = false;
      }
    }

  public void healDamage(float heal, playerStatManager PSM) {
    PSM.currentHealth += heal;
    if (PSM.currentHealth > PSM.maxHealth.Value) {PSM.currentHealth = PSM.maxHealth.Value;}
    if (PSM.numShields == 0 && PSM.currentHealth > 0) {PSM.numShields = 1;}
    PSM.healthBar.SetHealth(PSM.currentHealth/PSM.maxHealth.Value);
  }

  public IEnumerator baseHealOverTime(playerStatManager PSM) {
    while (true) {
      if (inCombat) {

      }
      else {
        healDamage(PSM.healthRegenValue.Value, PSM);
      }

      yield return new WaitForSeconds(1f);
    }
  }

  public void die(){
    SceneManager.LoadScene("DeathScene");
    Time.timeScale=1f;
    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
    GameObject[] objs2 = GameObject.FindGameObjectsWithTag("MainCamera");
    Destroy(objs[0]);
    Destroy(objs[0].transform.parent.gameObject);
    Destroy(objs2[0]);
  }

  public void TakeDamage(float damage, GameObject Enemy, playerStatManager PSM) {
    if (PSM.isHit || PSM.dashState == false) return;
    inCombatTimer = 0f;
    inCombat = true;
    EnemyStatManager ESM = Enemy.GetComponentInChildren<EnemyStatManager>();

    Vector3 forward = PSM.transform.forward;
    Vector3 toOther = Enemy.transform.position - PSM.transform.position;
    if (PSM.blockState == true && (Vector3.Dot(forward.normalized, toOther.normalized) > 0)) {
      if (PSM.isParry == true) {
        PSM.parryVFX.playVFX();
        PSM.parried = true;
        PSM.blockTrigger = false;
        if (ESM != null) {
          ESM.stunTime = PSM.stunTime;
          ESM.stunned = true;
        }
        Debug.Log("Parried");
      }
      else {
        PSM.currentHealth -= damage * 0.1f;
        PSM.isHit = true;
        PSM.HitVFX.playVFX();
        Debug.Log("BLOCKED");
        StartCoroutine(notHit(PSM));
      }
    }
    else {
      PSM.currentHealth -= damage;
      PSM.isHit = true;
      PSM.HitVFX.playVFX();
      StartCoroutine(notHit(PSM));
    }

    if (PSM.currentHealth <= 0) {
      if (PSM.numShields == 0) {
        die();
      }
      PSM.numShields -= 1;
      if (PSM.numShields > 0) {
        PSM.currentHealth = PSM.maxHealth.Value;
      }
    }
    if (PSM.currentHealth < 0) {
      PSM.currentHealth = 0;
    }
    PSM.healthBar.SetHealth(PSM.currentHealth/PSM.maxHealth.Value);
  }

  public IEnumerator notHit(playerStatManager PSM) {
    yield return new WaitForSeconds(hitRecoveryTime);
    PSM.isHit = false;
  }
}
