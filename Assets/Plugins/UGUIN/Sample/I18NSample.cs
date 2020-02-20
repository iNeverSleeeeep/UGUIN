using System.Collections;
using System.Collections.Generic;
using UGUIN;
using UnityEngine;

[RequireComponent(typeof(INText))]
public class I18NSample : MonoBehaviour
{
    public string[] Languages;

    INText text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<INText>();
        StartCoroutine(UpdateLanguage());
    }

    // Update is called once per frame
    IEnumerator UpdateLanguage()
    {
        text.color = new Color(0, 0, 0, 0);
        var index = 0;
        while (true)
        {
            while (text.color.a < 1)
            {
                text.color += new Color(0, 0, 0, Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            while (text.color.a > 0)
            {
                text.color -= new Color(0, 0, 0, Time.deltaTime);
                yield return null;
            }
            index++;
            if (index == Languages.Length)
                index = 0;
            if (index < Languages.Length)
                I18N.Load(new Dictionary<string, string>() { {"你好",Languages[index]} });
        }
    }
}
