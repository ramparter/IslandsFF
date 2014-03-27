using UnityEngine;
using System.Collections;


public enum ValuableType
{
    gemstone,
    //scrapMetal,
   // mineral
}

public class Valuable : MonoBehaviour {

    public ValuableType type = ValuableType.gemstone;
    public int quantity = 1;

}
