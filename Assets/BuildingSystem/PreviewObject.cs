using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour {


    public List<Collider> col = new List<Collider>();

    public Material green;
    public Material red;
    public bool IsBuildable;

    public bool second;

    public PreviewObject childcol;

    public Transform graphics;

    public objectSorts sort;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 11)
            col.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            col.Remove(other);
        }
    }

    private void Update()
    {
        if(!second)
        ChangeColor();
    }

    public void ChangeColor()
    {
        if(sort == objectSorts.foundation)
        {
            if (col.Count == 0)
                IsBuildable = true;
            else
                IsBuildable = false;
        }
        else
        {
            if (col.Count == 0 && childcol.col.Count > 0)
                IsBuildable = true;
            else
                IsBuildable = false;
        }
       

        if(IsBuildable)
        {
            foreach(Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = green;
            }
        }
        else
        {
            foreach (Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = red;
            }
        }
        
    }
}
public enum objectSorts
{
    normal,
    foundation,
    floor
}
