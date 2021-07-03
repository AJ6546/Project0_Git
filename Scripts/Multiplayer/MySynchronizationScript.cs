using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class MySynchronizationScript : MonoBehaviour, IPunObservable
{
    Rigidbody rb;
    PhotonView photonView;
    Animator playerAnimator;
    Vector3 networkPosition;
    Quaternion networkRotation;
    xHealth health;
    string playerID,targetName;

    [SerializeField] SyncManager syncManager;
    [SerializeField] CooldownTimer cd;

    public bool synchronizeVelocity = true;
    public bool synchronizeAngularVelocity = true;
    public bool isTeleportEnabled = true;
    public bool synchronizeHealth = true;
    public bool synchronizeHealRate = true;
    public bool synchronizeAttacks = true;
    public float teleportIfDistanceGreaterThan = 1.0f;
    private float distance;
    private float angle;
    void Awake()
    {
        health = GetComponent<xHealth>();
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        playerAnimator = GetComponent<Animator>();
        networkPosition = new Vector3();
        networkRotation = new Quaternion();
        syncManager = FindObjectOfType<SyncManager>();
        cd = GetComponent<CooldownTimer>(); 
    }
    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            rb.position = Vector3.MoveTowards(rb.position, networkPosition, distance * (1.0f / PhotonNetwork.SerializationRate));
            rb.rotation = Quaternion.RotateTowards(rb.rotation, networkRotation, angle * (1.0f / PhotonNetwork.SerializationRate));
            //health.SetHealth()
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            if (synchronizeVelocity)
            {
                stream.SendNext(rb.velocity);
            }
            if (synchronizeAngularVelocity)
            {
                stream.SendNext(rb.angularVelocity);
            }
            if(synchronizeHealth)
            {
                stream.SendNext(health.GetHealth());
            }
            if(synchronizeHealRate)
            {
                stream.SendNext(health.GetNextHealTime());
            }
            //if(synchronizeAttacks)
            //{
            //    stream.SendNext(cd.GetAttackTimer());
            //}
        }
        else if (stream.IsReading)
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            if (isTeleportEnabled)
            {
                if (Vector3.Distance(rb.position, networkPosition) > teleportIfDistanceGreaterThan)
                {
                    rb.position = networkPosition;
                }
            }

            if (synchronizeVelocity || synchronizeAngularVelocity)
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                if (synchronizeVelocity)
                {
                    rb.velocity = (Vector3)stream.ReceiveNext();
                    networkPosition += rb.velocity * lag;
                    distance = Vector3.Distance(rb.position, networkPosition);
                }
                if (synchronizeAngularVelocity)
                {
                    rb.angularVelocity = (Vector3)stream.ReceiveNext();
                    networkRotation = Quaternion.Euler(rb.angularVelocity * lag) * networkRotation;
                    angle = Quaternion.Angle(rb.rotation, networkRotation);
                }
            }

            if (synchronizeHealth)
            {
                health.SetHealth((float)stream.ReceiveNext());
            }

            if (synchronizeHealRate)
            {
                health.SetNextHealTime((float)stream.ReceiveNext());
            }
            //if (synchronizeAttacks)
            //{
            //    cd.SetAttackTimer((string)stream.ReceiveNext());
            //}
        }
     }


}

