using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YParallax : MonoBehaviour
{

    [SerializeField] Vector2 _minMaxHeight;
    [SerializeField] Vector2 _minMaxOffset;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float a = (transform.parent.position.y - _minMaxHeight.x) / (_minMaxHeight.y - _minMaxHeight.x);
        
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp( _minMaxOffset.x, _minMaxOffset.y, a), transform.localPosition.z);

    }
}
