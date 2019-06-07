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
    private PlayerWeaponManager_Inventory Inventory;

    void Start () {
        isBuilding = false;
        spawnPoint = transform.Find("FirePointPivot/FirePoint").gameObject;
        Inventory = FindObjectOfType<PlayerWeaponManager_Inventory>();
    }

	void Update () {
        
        if (hasAuthority)
        {
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

            if (isBuilding && Input.GetKeyDown(KeyCode.E))
            {
                CmdSpawnConstruction(zRotation); //syncvar doesn't work well
            }
           
        }
    }

    public void ChangeBuildingStatus()
    {
        Inventory.canAttack = !Inventory.GetCurrentWeapon().activeSelf;
        Inventory.CmdSetActiveWeapon(!Inventory.GetCurrentWeapon().activeSelf);


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
