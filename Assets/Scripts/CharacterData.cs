using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "Character", menuName = "CreateCharacter")]
public class CharacterData : ScriptableObject {
    [Header("キャラ名称")] public String characterName;
    [Header("キャラクターID")] public int characterID;
    [Header("最大HP")] public int maxHP;
    [Header("歩行速度")] public float walkSpeed;
    [Header("ジャンプ速度")] public float jumpSpeed;
    [Header("落下速度")] public float gravity;
    [Header("ジャンプ高度")] public float jumpHeight;
    [Header("ジャンプ時間")] public float jumpTIme;
    [Header("ジャンプの表現曲線")] public AnimationCurve jumpCurve;
    [Header("起き攻めスキル（攻撃）")] public List<Skill> atkOkisemeSkill;
    [Header("起き攻めスキル（防御）")] public List<Skill> defOkisemeSkill;
    
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
