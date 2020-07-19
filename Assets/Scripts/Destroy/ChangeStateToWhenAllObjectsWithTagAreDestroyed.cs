using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateToWhenAllObjectsWithTagAreDestroyed : MonoBehaviour
{
    private List<GameObject> _allObjectsWithTag = new List<GameObject>();
    public string Tag;
    public GameManager.State state;
    void Start()
    {
        StartCoroutine(UpdateTagObjects());
    }

    public IEnumerator UpdateTagObjects()
    {
        while (true)
        {
            _allObjectsWithTag.Clear();
            foreach (var VARIABLE in GameObject.FindGameObjectsWithTag(Tag))
            {
                _allObjectsWithTag.Add(VARIABLE);
            }

            if (_allObjectsWithTag.Count == 0)
            {
                break;
            }
            yield return new WaitForSeconds(3f);
        }

        GameManager.Instance.state = state;
    }

    public bool AllObjectsAreDestroyed()
    {
        _allObjectsWithTag.Clear();
        foreach (var VARIABLE in GameObject.FindGameObjectsWithTag(Tag))
        {
            _allObjectsWithTag.Add(VARIABLE);
        }
        return _allObjectsWithTag.Count == 0;
    }
}
