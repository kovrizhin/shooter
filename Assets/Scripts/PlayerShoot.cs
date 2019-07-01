
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public Weapon weapon;

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject gun;

    private void Start()
    {
        if (camera == null)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    [Client]
    void shoot()
    {
        print("fire");
        RaycastHit hit;



          if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, weapon.range, mask))
          {
              print("Hit " + hit.collider.name);
            /*
               CmdPlayerShoot(gun.transform.position, camera.transform.forward);
               */

            CmdPlayerShoot(hit.collider.name, weapon);

        }

    }
    /*
    [Command]
    void CmdPlayerShoot(Vector3 position, Vector3 forward)
    {
        
        Debug.Log("To player -> " + id + " was shooted");

        Player player = GameManager.getPlayer(id);
        player.takeDamage(damage);

    


        RpcClientShoot(position, forward);
    }
*/

    [Command]
    void CmdPlayerShoot(string id, Weapon weapon)
    {
    
        Debug.Log("To player -> " + id + " was shooted");

        Player player = GameManager.getPlayer(id);
        player.takeDamage(weapon.damage);

    


        //RpcClientShoot(position, forward);
    }

    //[ClientRpc]
    void RpcClientShoot(Vector3 position, Vector3 forward)
    {
        GameObject bullet = Instantiate(NetworkManager.singleton.spawnPrefabs[0], position, Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = forward * 50f;
        Destroy(bullet, 1.0f);
        NetworkServer.Spawn(bullet);
    }
}
