using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    [SerializeField]private playerStatus ps;
    private Image greenBar;
    private Image guardBar;
    private Image exBar;

    private float currenthp;
    private float maxhp;
    private float currentGuard;
    private float maxGuard;
    private float currentEx;
    private float maxEx;

    // Start is called before the first frame update
    void Start()
    {
        greenBar = transform.Find("GreenBar").gameObject.GetComponent<Image>();
        guardBar = transform.Find("GuardBar").gameObject.GetComponent<Image>();
        exBar = transform.Find("ExBar").gameObject.GetComponent<Image>();

        maxhp = ps.MaxHPGet();
        maxGuard = ps.MaxGuardGet();
        maxEx = 100;
    }

    // Update is called once per frame
    void Update()
    {
        currenthp=ps.CurrentHPGet();
        currentGuard = ps.CurrentGuardGet();
        currentEx = ps.CurrentExGet();
         
        greenBar.fillAmount = currenthp / maxhp;
        guardBar.fillAmount = currentGuard / maxGuard;
        exBar.fillAmount = currentEx / maxEx;
    }
}
