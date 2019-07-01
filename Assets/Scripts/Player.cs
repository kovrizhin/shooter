using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
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

    private Actions actions;

    private void Awake()
    {
        curHealth = maxHealth;
        actions = GetComponent<Actions>();
        //info.text = "";
        // health.text = curHealth.ToString();
    }

    public void takeDamage(float hit)
    {
        curHealth -= hit;
        Debug.Log(this.name + "Current health: " + curHealth + " hitted: " + hit);
      

        if (curHealth < 0)
        {
            GameManager.playerLose(this.name);

        }
        else {
        }
        RpcUpdateHealth();
    }

    [ClientRpc]
    public void RpcUpdateHealth()
    {
        actions.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
        if (health != null)
        {
            health.text = curHealth.ToString();
        }
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

        actions.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
    }
}
