using UnityEngine;
using UnityEngine.Networking;


public class BuildingController : NetworkBehaviour {
   [SyncVar]
    public int zRotation; //syncvar doesn't work well
    public GameObject building;
    public bool rotationLock=true;

    private GameObject spawnPoint;
    private GameObject inBuildingObject;
    private bool isBuilding;

    
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
                    inBuildingObject = Instantiate(building, spawnPoint.transform);
                }
                else
                {
                    Destroy(inBuildingObject);
                }
            }
                

            if (isBuilding && rotationLock)
            {
                inBuildingObject.transform.rotation = Quaternion.Euler(0, 0, zRotation);
            }

            if (isBuilding && Input.GetKeyDown(KeyCode.F))
            {
                zRotation-=45;  
            }

            if (isBuilding && Input.GetKeyDown(KeyCode.G))
            {
                zRotation+=45;
            }

            if (isBuilding && Input.GetKeyDown(KeyCode.Z))
            {
                CmdSpawnConstruction(zRotation); //syncvar doesn't work well
            }
           
        }
    }

    [Command]
    public void CmdSpawnConstruction(int rotation)
    {
        PlayerResourceScript resource= gameObject.GetComponent<PlayerResourceScript>();

        if (resource.UseResource(10))
        {
            CmdRealBuild(rotation);          
        }

    }
    [Command]
    public void CmdRealBuild(int rotation )
    {
        GameObject toBuild = Instantiate(building, spawnPoint.transform);
        if (rotationLock)
        {
            toBuild.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        toBuild.transform.parent = null;        
        NetworkServer.Spawn(toBuild);
        toBuild.GetComponent<WallScript>().RpcSetup(toBuild.transform.localScale.x, toBuild.transform.localScale.y, toBuild.transform.localScale.z);

    }
}
