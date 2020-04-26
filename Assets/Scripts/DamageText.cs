using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class DamageText : MonoBehaviour
{
    public Text damage;



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    public void SetText(string value)
    {
        damage.text = value;
    }
}
