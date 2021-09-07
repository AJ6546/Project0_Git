using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileInstantiator : MonoBehaviourPun
{
    public string projectile1;
    public PoolManager poolManager;
    public Transform instantiatorTransform;
    public string playerId;
    void Start()
    {
        poolManager = PoolManager.instance;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            playerId = photonView.Owner.NickName;
        }
        else
        {
            playerId = PlayerStats.USERID;
        }
    }
    public void SpawnProjectile(GameObject player)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                ProjectileInstantiator pi = player.GetComponent<ProjectileInstantiator>();
                string projectile = pi.projectile1;
                Vector3 spawnPos = pi.instantiatorTransform.position;
                Quaternion rot = pi.transform.rotation;
                Vector3 targetPos = pi.GetComponent<xFighter>().GetAimLocation();
                string playerId = pi.playerId;
                player.GetComponent<PhotonView>().RPC("Spawn", RpcTarget.AllBuffered
                    , projectile, spawnPos, rot, playerId, targetPos);
            }
        }
        else 
        {
            Vector3 spawnPos = instantiatorTransform.position;
            Vector3 targetPos = GetComponent<Fighter>().GetAimLocation();
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
            poolManager.Spawn(projectile1, spawnPos, transform.rotation,
                PlayerStats.USERID, targetPos);
        }
    }
    [PunRPC]
    void Spawn(string projectile, Vector3 spawnPos, Quaternion rot,string playerId, Vector3 targetPos)
    {
        Debug.Log("I am: " + playerId+"\nspawnPos: "+ spawnPos+"\nrotation: "+rot+
            "\nProjectile: "+projectile+"\nPoolManager: "+poolManager);
        transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        poolManager.Spawn(projectile1, spawnPos, rot, playerId,targetPos);

    }
}
