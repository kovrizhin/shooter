using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private string remoteLayerName = "RemotePlayer";
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private AudioListener audioListener;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
            audioListener.enabled = false;
        }
        else
        {
            audioListener.enabled = true;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            gameObject.layer = LayerMask.NameToLayer("LocalPlayer");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        GameManager.unregisterPlayer(transform.name);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.registerPlayer(netId, player);

    }
}
