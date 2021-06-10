using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class CooldownTimer : MonoBehaviourPun
{
    public Dictionary<string, int> coolDownTime = new Dictionary<string, int>
    { {"Attack01",5 },
        {"Attack02", 7 },
        { "Attack03",10 }
    };
    public Dictionary<string, int> nextAttackTime = new Dictionary<string, int>
    {  {"Attack01",0 },
        {"Attack02", 0 },
        { "Attack03",0 }
    };
    private void Start()
    {

    }


    private void Update()
    { }

}










//public Dictionary<string, int> cooldownTime = new Dictionary<string, int>
//    {   { "Attack01", 0},
//        { "Attack02", 0},
//        { "Attack03", 0}};
//public Dictionary<string, int> nextAttackTime = new Dictionary<string, int>
//    {   { "Attack01", 0 },
//        { "Attack02", 0 },
//        { "Attack03", 0 }};


//public Dictionary<string, int> cooldownTime = new Dictionary<string, int>
//{   { "Attack01", 0},
//    { "Attack02", 0},
//    { "Attack03", 0}};
//public Dictionary<string, int> nextAttackTime = new Dictionary<string, int>
//{   { "Attack01", 0 },
//    { "Attack02", 0 },
//    { "Attack03", 0 }};
//private void Start()
//{

//}
//[SerializeField] int a01, a02, a03;
//cooldownTime["Attack01"] = a01;
//cooldownTime["Attack02"] = a02;
//cooldownTime["Attack03"] = a03;
//public string GetAttackTimer()
//{
//    string attackTimers=nextAttackTime["Attack01"].ToString() + ","+
//        nextAttackTime["Attack02"].ToString() +","
//        + nextAttackTime["Attack03"].ToString();
//    Debug.Log(attackTimers);
//    return attackTimers;
//}
//public void SetAttackTimer(string time)
//{
//    string[] s = time.Split(',');
//    for(int i=0; i<s.Length;i++)
//    {
//        nextAttackTime["Attack0"+(i+1)] = Convert.ToInt32(s[i]);
//        Debug.Log(nextAttackTime["Attack0" + (i + 1)]);
//    }

//}

