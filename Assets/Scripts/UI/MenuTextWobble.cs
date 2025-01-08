using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// code adapted from https://github.com/Madalaski/TextTutorial/blob/master/Assets/CharacterWobble.cs

public class CharacterWobble : MonoBehaviour
{
    private TMP_Text _textMesh;
    private Mesh _mesh;
    private Vector3[] _vertices;

    // Start is called before the first frame update
    public void Start()
    {
        _textMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public void Update()
    {
        _textMesh.ForceMeshUpdate();
        _mesh = _textMesh.mesh;
        _vertices = _mesh.vertices;

        for (int i = 0; i < _textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = _textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = new Vector2(Mathf.Sin((Time.time + i) * _sinAmp), Mathf.Cos((Time.time + i) * _cosAmp));
            _vertices[index] += offset;
            _vertices[index + 1] += offset;
            _vertices[index + 2] += offset;
            _vertices[index + 3] += offset;
        }

        _mesh.vertices = _vertices;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    [SerializeField] private float _sinAmp = 3.3f;
    public float SinAmp
    {
        get { return _sinAmp; }
        set { _sinAmp = value; }
    }

    [SerializeField] private float _cosAmp = 2.5f;
    public float CosAmp
    {
        get { return _cosAmp; }
        set { _cosAmp = value; }
    }
}
