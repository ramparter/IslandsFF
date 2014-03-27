using UnityEngine;
using System.Collections;


public enum CloudType
{
    steam = 0,
    powerAttack,
    invincibility,
    slowMotion,
    minimize
}

public class Cloud : MonoBehaviour {

    public CloudType cloudType = CloudType.steam;
    public int steamAmount = 10;

}
