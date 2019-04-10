using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public List<Sprite> symbolImages;
    private bool moving;
    private Vector3 targetPos;
    private double maxDist;
    public GameObject HistorySequence;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        targetPos = new Vector3(0, 0, 0);
    }

    public void SetImage(int symbolInd) {
        Debug.Log("setting image " + symbolInd);
        this.GetComponent<UnityEngine.SpriteRenderer>().sprite = symbolImages[symbolInd];
    }

    public void MoveTo(Vector3 endPos, double duration) {
        moving = true;
        targetPos = endPos;
        // targetPos = transform.TransformPoint(endPos);
        Debug.Log(targetPos);
    }
    // Update is called once per frame
    void Update()
    {       
        if (this.transform.position == targetPos) {
            moving = false;
        }

        if (moving == true) {
            // relativeDir = Vector3.Normalize()

            maxDist = (targetPos - this.transform.position).magnitude;
            if (maxDist > 0.2f) {
                maxDist = 0.2f;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, (float) maxDist);
            }
            else {
                transform.position = targetPos;   
            }
            // transform.MoveTowards(targetPos)
            
        }
        
    }

    void OnMouseDown() {
        Debug.Log(this.name);
        HistorySequence.GetComponent<SequenceManager>().addToHistory(this.gameObject);

    }
}
