using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRTK;

public class ChalkWrite : MonoBehaviour
{
    public VRTK_InteractableObject linkedObject;
    public GameObject linePrefab;
    public GameObject currentLine;
    public GameObject tip;

    public LineRenderer lineRenderer;
    public List<Vector3> controllerPositions;

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }
        
    }

    protected virtual void OnDisable()
    {

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }


    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        startDraw();
    }

    private void startDraw()
    {
        
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        controllerPositions.Clear();
        controllerPositions.Add(tip.transform.position);
        controllerPositions.Add(tip.transform.position);
        lineRenderer.SetPosition(0, controllerPositions[0]);
        lineRenderer.SetPosition(1, controllerPositions[1]);

        Debug.Log("Am inceput");

    }

    private void updateLine(Vector3 newControllerPosition)
    {
        controllerPositions.Add(newControllerPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newControllerPosition);
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        stopDraw();
    }

    private void stopDraw()
    {
        Debug.Log("Gata");
        
    }

    public void Start()
    {
        
    }

    public void Update()
    {

        if (linkedObject.IsUsing())
        {
            
            Vector3 tempControllerPosition = tip.transform.position;
            if (Vector3.Distance(tempControllerPosition, controllerPositions[controllerPositions.Count() - 1]) > 0.001f)
            {
                updateLine(tempControllerPosition);
            }
        }


    }
}
