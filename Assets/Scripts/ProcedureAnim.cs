using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureAnim : MonoBehaviour
{

    public Transform pointer;

    // private float len = 0.1f;
    private float fullLenght = 0f;
    private Transform[] segments;
    private float[] segmentLenghts; 
    private Vector3 offset;

    void Start()
    {
        segments = GetComponentsInChildren<Transform>();
        segmentLenghts = new float[segments.Length];
        for (int i = 0; i < segments.Length-2; i++){
            segmentLenghts[i] = Vector3.Distance(segments[i].position, segments[i+1].position);
            fullLenght += segmentLenghts[i];
        }
        segmentLenghts[segments.Length-2] = 0.1f;
        segmentLenghts[segments.Length-1] = 0f;
    }

    void Update()
    {
        Vector3 targetPosition = pointer.position;
        targetPosition = CheckLenghtByTargetPosition(targetPosition);

        for (int i = segments.Length-1; i > 0; i--){
            Vector3 dir = targetPosition - segments[i].position; 
            float a = Vector2.SignedAngle(dir, Vector2.down)*-1f;
            if (i == segments.Length-2) a = 0f;
            // if (i == 1 && a < 0) a *= -1f;

            // Debug.DrawLine(segments[i].position, segments[i].position);

            segments[i].localRotation =  Quaternion.Euler(a*-1f, 0f, 0f);
            float dx = Mathf.Cos(Mathf.Deg2Rad*(a+270f))*segmentLenghts[i];
            float dy = Mathf.Sin(Mathf.Deg2Rad*(a+270f))*segmentLenghts[i];
            segments[i].position = targetPosition - new Vector3(dx, dy);

            targetPosition = segments[i].position;
        }

        for (int i = 0; i < segments.Length-2; i++){
            Debug.DrawLine(segments[i].position, segments[i+1].position, Color.red);
            fullLenght += segmentLenghts[i];
        }

        Vector3 rootDir = segments[1].localPosition;
        targetPosition.z = 0f;
        for (int i = 1; i < segments.Length-1; i++){
            segments[i].localPosition -= rootDir; 
        }
    }

    private Vector3 CheckLenghtByTargetPosition(Vector3 targetPosition){
        Vector2 dir = targetPosition - transform.position;
        float dirLen = Mathf.Sqrt(dir.x*dir.x + dir.y*dir.y);
        if ( dirLen >= fullLenght){
            float a = Vector2.SignedAngle(dir, Vector2.right)*-1f;
            float dx = Mathf.Cos(Mathf.Deg2Rad*a) * fullLenght;
            float dy = Mathf.Sin(Mathf.Deg2Rad*a) * fullLenght;
            return transform.position + new Vector3(dx, dy);
        } else return targetPosition;
    } 
}
