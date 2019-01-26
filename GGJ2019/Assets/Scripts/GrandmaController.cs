using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    [HideInInspector]
    public GrandmaElasticSoundController sound;

    public GameObject objHeadBone;
    public GameObject objFrontBone;
    public GameObject objBackBone;
    public GameObject objShoulderBone;
    public GameObject objLeftArmBone;
    public GameObject objRightArmBone;
    public GameObject objLeftForearmBone;
    public GameObject objRightForearmBone;
    public GameObject objLeftHandBone;
    public GameObject objRightHandBone;
    public GameObject objLeftLegBone;
    public GameObject objRightLegBone;
    public GameObject objLeftFootBone;
    public GameObject objRightFootBone;

    private struct GrandmaBone
    {
      
        public GameObject objBone;
        public Transform tranBone;
        public Vector3 boneDefaultPos;
        public Quaternion boneDefaultQuat;

        public GrandmaBone(GameObject objBone)
        {
            this.objBone = objBone;
            this.tranBone = objBone.transform;
            this.boneDefaultPos = tranBone.localPosition;
            this.boneDefaultQuat = tranBone.localRotation;
                
        }
    }

    public enum GrandmaAnimationState
    {
        Idle,
        Charge
    }

    private GrandmaAnimationState m_AnimationState;
    public GrandmaAnimationState animationState
    {
        get
        {
            return m_AnimationState;
        }
        set
        {
            m_AnimationState = value;
        }
    }

    private float animationTime;
    private float animationTimer;
    public float chargeAmount = 0;
    private Dictionary<string, GrandmaBone> bones;

    private void Awake()
    {
        sound = GetComponent<GrandmaElasticSoundController>();
        bones = new Dictionary<string, GrandmaBone>();
        bones.Add("Head", new GrandmaBone(objHeadBone));
        bones.Add("Front", new GrandmaBone(objFrontBone));
        bones.Add("Back", new GrandmaBone(objBackBone));
        bones.Add("Shoulder", new GrandmaBone(objShoulderBone));
        bones.Add("LeftArm", new GrandmaBone(objLeftArmBone));
        bones.Add("RightArm", new GrandmaBone(objRightArmBone));
        bones.Add("LeftForearm", new GrandmaBone(objLeftForearmBone));
        bones.Add("RightForearm", new GrandmaBone(objRightForearmBone));
        bones.Add("LeftHand", new GrandmaBone(objLeftHandBone));
        bones.Add("RightHand", new GrandmaBone(objRightHandBone));
        bones.Add("LeftLeg", new GrandmaBone(objLeftLegBone));
        bones.Add("RightLeg", new GrandmaBone(objRightLegBone));
        bones.Add("LeftFoot", new GrandmaBone(objLeftFootBone));
        bones.Add("RightFoot", new GrandmaBone(objRightFootBone));
    }

    // Use this for initialization
    void Start ()
    {
        animationState = GrandmaAnimationState.Charge;
		
	}

    private void OnIdleUpdate()
    {

    }

    private void OnChargeUpdate()
    {
        bones["Head"].tranBone.localPosition = Vector3.Lerp(bones["Head"].boneDefaultPos, new Vector3(0, 0.895f, 0.006f), chargeAmount);
        bones["Back"].tranBone.localPosition = Vector3.Lerp(bones["Back"].boneDefaultPos, new Vector3(-0.275f, 0, -1.534f), chargeAmount);
        bones["Back"].tranBone.localRotation = Quaternion.Lerp(bones["Back"].boneDefaultQuat, Quaternion.Euler(-90, 0, -82), chargeAmount);
        bones["Front"].tranBone.localPosition = Vector3.Lerp(bones["Front"].boneDefaultPos, new Vector3(-0.127f, 0, -0.241f), chargeAmount);
        bones["Front"].tranBone.localRotation = Quaternion.Lerp(bones["Front"].boneDefaultQuat, Quaternion.Euler(-90, 0, 96), chargeAmount);
        //bones["Shoulder"].tranBone.localPosition = Vector3.Lerp(bones["Shoulder"].boneDefaultPos, new Vector3(-0.127f, 0, -1.211f), chargeAmount);
        bones["Shoulder"].tranBone.localRotation = Quaternion.Lerp(bones["Shoulder"].boneDefaultQuat, Quaternion.Euler(-90, 0, 1.469f), chargeAmount);
        bones["RightForearm"].tranBone.localPosition = Vector3.Lerp(bones["RightForearm"].boneDefaultPos, new Vector3(-0.187f, 0, 0), chargeAmount);
        bones["RightForearm"].tranBone.localRotation = Quaternion.Lerp(bones["RightForearm"].boneDefaultQuat, Quaternion.Euler(46, 69.445f, 15.416f), chargeAmount);
        bones["LeftLeg"].tranBone.localPosition = Vector3.Lerp(bones["LeftLeg"].boneDefaultPos, new Vector3(-0.2896f, 0, 0), chargeAmount);
        bones["LeftLeg"].tranBone.localRotation = Quaternion.Lerp(bones["LeftLeg"].boneDefaultQuat, Quaternion.Euler(-37.303f, 84.735f, -130.79f), chargeAmount);
        bones["RightLeg"].tranBone.localPosition = Vector3.Lerp(bones["RightLeg"].boneDefaultPos, new Vector3(-0.2896f, 0, 0), chargeAmount);
        bones["RightLeg"].tranBone.localRotation = Quaternion.Lerp(bones["RightLeg"].boneDefaultQuat, Quaternion.Euler(-142.697f, -84.735f, 130.79f), chargeAmount);
        if(sound != null){
            sound.chargeAmount = this.chargeAmount;
        }        
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch (animationState)
        {
            case GrandmaAnimationState.Idle:
                OnIdleUpdate();
                break;
            case GrandmaAnimationState.Charge:
                OnChargeUpdate();
                break;
        }
	}
}
