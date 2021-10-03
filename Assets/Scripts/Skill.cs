using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "CreateSkill")]
public class Skill : ScriptableObject
{
    public enum Type {
        strike,
        grapple,
        Atkguard,
        parry,
        dodge,
        Defguard,
        Invinsibility,
    }
    [SerializeField] private Type skillType;
    [SerializeField] private String skillName;
    [SerializeField] private int skillID;
    [SerializeField] private int skillDamage;
    
    public Type GetSkillType() {
        return skillType;
    }
    public String GetSkillName() {
        return skillName;
    }
    public int GetSkillID() {
        return skillID;
    }
    public int GetSkillDamage() {
        return skillDamage;
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
