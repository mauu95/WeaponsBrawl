using UnityEngine;
using UnityEngine.Networking;


public class BuildingController : NetworkBehaviour {
    [SyncVar]
    public int buildingType=0;
    public GameObject[] buildings;
    public GameObject spawnPoint;
    //public GameObject toSpawn;
    public GameObject inBuildingObject;
    public bool isBuilding;
    public bool rotationLock=true;

    // Use this for initialization

    void Start () {
        isBuilding = false;
        spawnPoint = transform.Find("FirePointPivot/FirePoint").gameObject;
    }


	
	// Update is called once per frame
	void Update () {
        
        if (hasAuthority)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isBuilding = !isBuilding;
                if (isBuilding)
                {
                    inBuildingObject = Instantiate(buildings[buildingType], spawnPoint.transform);
                }
                else
                {
                    Destroy(inBuildingObject);
                }
            }
                

            if (isBuilding && rotationLock)
            {
                inBuildingObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (isBuilding && Input.GetKeyDown(KeyCode.F))
            {
                buildingType--;
                while (buildingType <= 0)
                {
                    buildingType += buildings.Length;
                }
                buildingType = buildingType % buildings.Length;
                RefreshConstruction();
            }

            if (isBuilding && Input.GetKeyDown(KeyCode.G))
            {
                buildingType++;
                buildingType = buildingType % buildings.Length;
                RefreshConstruction();
            }

            if (isBuilding && Input.GetKeyDown(KeyCode.Z))
            {
                
                //inBuildingObject.transform.parent = null;
                //inBuildingObject.GetComponent<BoxCollider2D>().enabled=true;
                CmdSpawnConstruction();
                //inBuildingObject= Instantiate(toSpawn, spawnPoint.transform);
            }
           
        }
    }

    private void RefreshConstruction()
    {
        Destroy(inBuildingObject);
        inBuildingObject = Instantiate(buildings[buildingType], spawnPoint.transform);
    }

    [Command]
    public void CmdSpawnConstruction()
    {
        PlayerResourceScript resource= gameObject.GetComponent<PlayerResourceScript>();
        Debug.Log(resource.UseResource(10));

        if (resource.UseResource(10))
        {
            RpcRealBuild();          
        }

    }
    [ClientRpc]
    public void RpcRealBuild()
    {
        GameObject toBuild = Instantiate(buildings[buildingType], spawnPoint.transform);
        if (rotationLock)
        {
            toBuild.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //toBuild.transform.parent = null;
        toBuild.GetComponent<BoxCollider2D>().enabled = true;
        //NetworkServer.Spawn(toBuild);
        toBuild.transform.parent = null;
    }
}
