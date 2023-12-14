using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] OVRHand lefthand;
    [SerializeField] OVRSkeleton leftSkeleton;
    [SerializeField] OVRHand righthand;
    [SerializeField] OVRSkeleton rightSkeleton;
    [SerializeField] GameObject moveOriginPos;
    [SerializeField] GameObject rotateOriginPos;
    [SerializeField] float rotateSpeed = 2f;
    private float speed = 0.1f;
    private bool isPinchingLeft = false;
    private bool isPinchingRight = false;
    private Vector3 rotateVec = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Thumb, Index, Middle, Ring, Pinky
        // x,z移動
        if(lefthand.GetFingerIsPinching(OVRHand.HandFinger.Thumb) &&
            lefthand.GetFingerIsPinching(OVRHand.HandFinger.Index)) // 人差し指と親指がピンチしているかどうか
            {
                var indexTipPos = leftSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position; 
                if(!isPinchingLeft)
                {
                    isPinchingLeft = true;
                    moveOriginPos.transform.position = indexTipPos;
                    Debug.Log(indexTipPos);
                }
                Debug.Log(moveOriginPos.transform.position);
                var relativePos = indexTipPos - moveOriginPos.transform.position;
                transform.position += new Vector3(relativePos.x, 0, relativePos.z).normalized * speed;
                // Debug.Log("Pinching");
            }
        else
        {
            if(isPinchingLeft)
            {
                isPinchingLeft = false;
            }
        }

        // y軸回転
        if(righthand.GetFingerIsPinching(OVRHand.HandFinger.Thumb)) // 中指と親指がピンチしているかどうか
            {
                var indexTipPos = rightSkeleton.Bones[(int) OVRSkeleton.BoneId.Hand_IndexTip].Transform.position; 
                if(!isPinchingRight)
                {
                    isPinchingRight = true;
                    rotateOriginPos.transform.position = indexTipPos;
                    Debug.Log(indexTipPos);
                }
                Debug.Log(rotateOriginPos.transform.position);
                var relativePos = indexTipPos - rotateOriginPos.transform.position;
                if(relativePos.x > 0)
                {
                    rotateVec = new Vector3(0, 1, 0)* rotateSpeed;
                }
                else if(relativePos.x < 0)
                {
                    rotateVec = new Vector3(0, -1, 0)* rotateSpeed;
                }
                else
                {
                    rotateVec = new Vector3(0, 0, 0);
                }
                transform.Rotate(rotateVec);
                // Debug.Log("Pinching");
            }
        else
        {
            if(isPinchingRight)
            {
                isPinchingRight = false; 
                rotateVec = new Vector3(0, 0, 0);
            }
        }
    }
}
