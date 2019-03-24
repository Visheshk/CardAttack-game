using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolDisplayer : MonoBehaviour
{
    // public symbolImage[] symbolImagesDict;
    public List<Sprite> symbolImages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetImage(int symbolInd) {
        this.GetComponent<UnityEngine.UI.Image>().sprite = symbolImages[symbolInd];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
