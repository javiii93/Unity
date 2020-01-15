using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLayering : MonoBehaviour {
    public string sortLayerString = "";
    
    void Start() {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName= sortLayerString;
      
    }
}
