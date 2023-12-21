using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextEffects : MonoBehaviour
{
     private TMP_Text _text;

    [Header("Wiggle")]
    [SerializeField] private float _wiggleSpeed = 0.1f;
    [SerializeField] private float _wiggleAmplitude = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.ForceMeshUpdate();
        Mesh mesh = _text.mesh;

        for (int i = 0; i < _text.textInfo.linkCount; i++)
        {

            if (_text.textInfo.linkInfo[i].GetLinkID().ToLower() == "wiggle")
            {
                //Debug.Log(_text.textInfo.linkInfo[i].linkTextfirstCharacterIndex +", " + _text.textInfo.linkInfo[i].linkTextLength);

                Wiggle(_text.textInfo.linkInfo[i].linkTextfirstCharacterIndex, _text.textInfo.linkInfo[i].linkTextLength, _text, mesh);
            }
        }

        _text.canvasRenderer.SetMesh(mesh);
    }
    private Mesh Wiggle(int startIndex, int length, TMP_Text text , Mesh mesh)
    {
        
        Vector3[] vertices = mesh.vertices;
        for (int i = startIndex; i - startIndex< length; i++)
        {
            int index = text.textInfo.characterInfo[i].vertexIndex;
            Vector3 offset = WiggleMath((Time.time + index )* _wiggleSpeed) * _wiggleAmplitude;

            for (int j = index; j < index + 4; j++)
            {
                vertices[j] += offset;

            }
        }
        mesh.vertices = vertices;
        return mesh;
    }

    private Vector2 WiggleMath(float time)
    {
        return new Vector2(Mathf.Cos(time), Mathf.Sin(time));
    }
}
