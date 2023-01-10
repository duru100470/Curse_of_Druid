using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedPlatform : RuleTile
{
    void Update()
    {
        //곡괭이 사용 시: 곡괭이 스크립트에 접근해 OnUse 변수로 판단
        /*if(GameObject.Find("곡괭이").GetComponent<스크립트>().OnUse == true)
        {
            DestroyPlatform();
        }*/
    }

    void DestroyPlatform()
    {
        this.gameObject.SetActive(false);
    }
}
