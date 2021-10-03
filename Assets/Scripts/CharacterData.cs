using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "Character", menuName = "CreateCharacter")]
public class CharacterData : ScriptableObject {
    [Header("�L��������")] public String characterName;
    [Header("�L�����N�^�[ID")] public int characterID;
    [Header("�ő�HP")] public int maxHP;
    [Header("���s���x")] public float walkSpeed;
    [Header("�W�����v���x")] public float jumpSpeed;
    [Header("�������x")] public float gravity;
    [Header("�W�����v���x")] public float jumpHeight;
    [Header("�W�����v����")] public float jumpTIme;
    [Header("�W�����v�̕\���Ȑ�")] public AnimationCurve jumpCurve;
    [Header("�N���U�߃X�L���i�U���j")] public List<Skill> atkOkisemeSkill;
    [Header("�N���U�߃X�L���i�h��j")] public List<Skill> defOkisemeSkill;
    
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
