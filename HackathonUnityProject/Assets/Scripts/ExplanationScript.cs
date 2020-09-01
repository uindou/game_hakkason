using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ExplanationScript : MonoBehaviour
{
    public GameObject Explanation;
    public Vector3 SpownPos;

    RectTransform Rectra;

    GameObject ParentCanvas;

    public float moveSpeed = 1.0f;
    public float destroyX = -1250.0f;
    public float spownX = -500.0f;

    bool onceSpown = true;

    // Start is called before the first frame update
    void Start()
    {
        Rectra = GetComponent<RectTransform>();
        ParentCanvas = GameObject.Find("TitleCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Rectra.localPosition.x < destroyX)
        {
            Destroy(this.gameObject);
        }
        else if ((Rectra.localPosition.x < spownX)&&(onceSpown))//説明を生成
        {
            GameObject Expla2 = Instantiate(Explanation) as GameObject;

            Expla2.transform.parent = ParentCanvas.transform;//親をCanvasに設定

            Expla2.GetComponent<RectTransform>().localPosition = SpownPos;
            Expla2.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

            onceSpown = false;
        }
        else
        {
            Rectra.localPosition = new Vector3(Rectra.localPosition.x - moveSpeed, Rectra.localPosition.y, 0);
        }
    }
}
