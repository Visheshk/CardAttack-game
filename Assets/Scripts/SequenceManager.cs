using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{   
    public float startX = -0.05f;
    public float gap = 0.012f;
    public Sequence history;
    private Vector3 targetPos;
    private float movementDuration;
    double newx;

    // Start is called before the first frame update
    void Start()
    {
        startX = -0.05f;
        gap = 0.012f;
        movementDuration = 1.0f;
        history = new Sequence();
    }

    public void addToHistory(GameObject newInput) {
      newx = startX + gap * history.Count;
      targetPos = new Vector3(0, 0, -0.5f);
      targetPos.x = (float) newx;
      Debug.Log(targetPos);
      targetPos = transform.TransformPoint(targetPos);
      // Debug.Log(targetPos);
      newInput.transform.SetParent(this.gameObject.transform);

      newInput.GetComponent<SymbolManager>().MoveTo(targetPos, (float) movementDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
