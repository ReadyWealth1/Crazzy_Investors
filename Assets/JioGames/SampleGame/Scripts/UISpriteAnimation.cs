using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jio.SampleGame
{
public class UISpriteAnimation : MonoBehaviour
{

    public Image m_Image;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .1f;
    Coroutine m_CorotineAnim;

    void OnEnable(){
        Func_PlayUIAnim();
    }

    void OnDisable(){
        Func_StopUIAnim();
    }

    public void Func_PlayUIAnim()
    {
        StartCoroutine(Func_PlayAnimUI());
    }

    public void Func_StopUIAnim()
    {
        StopCoroutine(Func_PlayAnimUI());
    }
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        m_Image.sprite = m_SpriteArray[1];
        yield return new WaitForSeconds(m_Speed);
        m_Image.sprite = m_SpriteArray[0];
        yield return new WaitForSeconds(m_Speed);
        m_Image.sprite = m_SpriteArray[1];
        yield return new WaitForSeconds(m_Speed);
        m_Image.sprite = m_SpriteArray[0];

        yield return new WaitForSeconds(2.0f);
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
}
}