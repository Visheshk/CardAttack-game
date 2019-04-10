using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public List<Sprite> symbolImages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetImage(int symbolInd) {
        Debug.Log("setting image " + symbolInd);
        this.GetComponent<UnityEngine.SpriteRenderer>().sprite = symbolImages[symbolInd];
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
