using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour {
    public bool iced = false;
    public PhysicsMaterial2D mice;
    public PhysicsMaterial2D mstandar;
    private BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
     
    }

    public void getfrozzen()
    {
        iced = true;
        Debug.Log("je suis glacé");
        GetComponent<SpriteRenderer>().color = new Color(0.2f,0.5f,1f);
        box.size = box.size + new Vector2(0, 0.00001f);
        box.sharedMaterial = mice;
        box.size = box.size - new Vector2(0, 0.00001f);
    }

    public void getwarmed()
    {
        iced = false;
        Debug.Log("je suis chaud");
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        box.size = box.size + new Vector2(0, 0.00001f);
        box.sharedMaterial = mstandar;
        box.size = box.size - new Vector2(0, 0.00001f);
    }

}
