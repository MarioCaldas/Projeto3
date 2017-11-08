using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingSystem : MonoBehaviour {

    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects currentObject;
    private Vector3 currentPos;
    public Transform currentPreview;
    public Transform player;
    public RaycastHit hit;
    public LayerMask layer;
    private Vector3 currentRotation;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public bool isBuilding;

    public Camera mainCamera;


    private void Start()
    {



        currentObject = objects[0];
        ChangeCurrentBuilding(0);



        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        Debug.DrawRay(player.position + Vector3.up * 2, player.forward - Vector3.up * 0.3f, Color.black);

        if (isBuilding)
        {
            startPreview();

            if(Input.GetButtonDown("Fire1"))
            {
                Build();
            }
        }

        //if (Input.GetKeyDown("0") || Input.GetKeyDown("1"))
        if (Input.GetKeyDown("0"))
        {
            //SwitchCurrentBuild();
            ChangeCurrentBuilding(0);
        }
    }

    public void SwitchCurrentBuild()
    {
        for (int i = 0; i < 2; i++)
        {
            if(Input.GetKeyDown("" + i))
            {
                Debug.Log("Entrou SwitchCurrentBuild");
                ChangeCurrentBuilding(i);
            }
        }
    }

    public void ChangeCurrentBuilding(int cur)
    {
        currentObject = objects[cur];
        if(currentPreview != null)
        {
            Destroy(currentPreview.gameObject);
        }
        Debug.Log("Entrou ChangeCurrentBuilding");
        GameObject curprev = Instantiate(currentObject.preview, currentPos, Quaternion.Euler(currentRotation)) as GameObject;
        currentPreview = curprev.transform;

    }

    public void startPreview()
    {
        
        if (Physics.Raycast(player.position + Vector3.up * 2, player.forward - Vector3.up * 0.4f, out hit, 40, layer))
        
        {
            //if (hit.transform != this.transform)
            //{
                Debug.Log("Entrou hit");
                showPreview(hit);
            //}

        }

    }

    public void showPreview(RaycastHit hit2)
    {
        Debug.Log("Entrou showPreview");
        currentPos = hit2.point;

        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), (Mathf.Round(currentPos.z)));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentPreview.position = currentPos;

        if(Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Entrou Fire2");

            currentRotation += new Vector3(0, 90, 0);
        }
        currentPreview.localEulerAngles = currentRotation;
    }

    public void Build()
    {
        PreviewObject PO = currentPreview.GetComponent<PreviewObject>();
        if(PO.IsBuildable)
        {
            Instantiate(currentObject.prefab, currentPos, Quaternion.Euler(currentRotation));
        }
    }


    [System.Serializable]
    public class buildObjects
    {
        public string name;
        public GameObject prefab;
        public GameObject preview;
        public int gold;
    }
}
