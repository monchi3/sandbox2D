using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Flash : MonoBehaviour
{
    private Image img;
    private int i;
    // Start is called before the first frame update
    public void DoFlash() {
        i = 1;
    }
    
    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;
    }


    // Update is called once per frame
    void Update()
    {
        if (i==1) {
            img.color = new Color(230 / 255f, 230 / 255f, 230 / 255f, 1);
            i = 2;
        }
        else if(i==2) {
            img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
        }
        if (img.color.a == 0) i = 0;
    }
}
