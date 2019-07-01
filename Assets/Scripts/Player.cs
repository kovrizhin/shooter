using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;
    [SyncVar]
    private float curHealth;
    [SerializeField]
    private Text info;
    [SerializeField]
    private Text health;

    private void Awake()
    {
        curHealth = maxHealth;
        //info.text = "";
       // health.text = curHealth.ToString();
    }

    public void takeDamage(float hit)
    {
        curHealth -= hit;
        Debug.Log("Current health: " + curHealth + " hitted: " + hit);

        if (curHealth < 0)
        {
            GameManager.playerLose(this.name);

        }
        RpcUpdateHealth();
    }

    [ClientRpc]
    public void RpcUpdateHealth()
    {
        health.text = curHealth.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        //  print("Hit " + this.name);
        //  if (other.gameObject.CompareTag("Bullet"))
        //  {
        //     print("Hit " + this.name);
        //       CmdTakeDamage(this.name, 10);
        //  }
    }

    //[Command]
    void CmdTakeDamage(string id, float damage)
    {
        Debug.Log("To player -> " + id + " was shooted");

        Player player = GameManager.getPlayer(id);
        player.takeDamage(damage);
    }

    public float getHealth()
    {
        return curHealth;
    }

    [ClientRpc]
    public void RpcWin()
    {
  //      info.text = "WIN";
    }

    [ClientRpc]
    public void RpcLose()
    {
  //      info.text = "LOSE";
    }
}
