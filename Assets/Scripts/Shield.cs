using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public int currentGuard;
    public int maxGuard;
    private playerStatus ps;
    private SpriteRenderer spriteRenderer;

    public int CurrentGuardGet() {
        return currentGuard;
    }
    public int MaxGuardGet() {
        return maxGuard;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxGuard = ps.MaxGuardGet();
        currentGuard = maxGuard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
