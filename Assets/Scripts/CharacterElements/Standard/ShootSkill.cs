using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ShootSkill", menuName = "CreateShootSkill")]

public class ShootSkill : ScriptableObject
{
    public GameObject bullet;
    public float CT;
    public int bulletCountLimit;
    public String skillName;


    public GameObject GetBullet() {
        return bullet;
    }
    public float GetCT() {
        return CT;
    }
    public int GetBulletCountLimit() {
        return bulletCountLimit;
    }
    public string GetSkillName() {
        return skillName;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
