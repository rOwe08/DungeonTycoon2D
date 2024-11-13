using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGateScript : MonoBehaviour
{
    private Animator gateAnim;
    private Animator flamesAnim;

    public GameObject flamesObject;

    void Start()
    {
        if(flamesObject != null)
        {
            flamesAnim = flamesObject.GetComponent<Animator>();
        }
        gateAnim = GetComponent<Animator>();
    }

    public void CloseMainGate()
    {
        gateAnim.SetBool("IsOpening", false);
        gateAnim.SetBool("IsClosing", true);

        if(flamesAnim != null)
        {
            flamesAnim.SetBool("IsOpening", false);
            flamesAnim.SetBool("IsClosing", true);
        }
    }

    public void OpenMainGate()
    {
        gateAnim.SetBool("IsClosing", false);
        gateAnim.SetBool("IsOpening", true);

        if(flamesAnim != null)
        {
            flamesAnim.SetBool("IsClosing", false);
            flamesAnim.SetBool("IsOpening", true);
        }
    }
}

