using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

        public void ResetTransform()
        {
            tranBone.localPosition = boneDefaultPos;
            tranBone.localRotation = boneDefaultQuat;
        }
    }

    public enum GrandmaAnimationState
    {
        Idle,
        Charge,
        Jump,
        Hit
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
         
            foreach (Tweener t in tweeners)
            {
                t.Kill();
            }
            tweeners.Clear();
            tweeners = new List<Tweener>();

            Dictionary<string, GrandmaBone>.ValueCollection boneValues = bones.Values;
            foreach (GrandmaBone gb in boneValues)
            {
                //DOTween.Kill(gb.objBone.GetInstanceID());
                gb.ResetTransform();
            }

            switch (value)
            {
                case GrandmaAnimationState.Idle:
                    OnIdleInit();
                    break;
                case GrandmaAnimationState.Charge:
                    OnChargeInit();
                    break;
                case GrandmaAnimationState.Jump:
                    OnJumpInit();
                    break;
                case GrandmaAnimationState.Hit:
                    OnHitInit();
                    break;
            }
        }
    }

    private float animationTime;
    private float animationTimer;
    public float chargeAmount = 0;
    private Dictionary<string, GrandmaBone> bones;
    private List<Tweener> tweeners = new List<Tweener>();

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
        animationState = GrandmaAnimationState.Idle;
		
	}

    private void OnIdleInit()
    {
        var t = 1.5f;

        tweeners.Add(bones["Head"].tranBone.DOLocalRotate(new Vector3(-5.581f, 0, 0), t).SetLoops(-1, LoopType.Yoyo));
        tweeners.Add(bones["Shoulder"].tranBone.DOLocalRotate(new Vector3(-90, 0, -2.73f), t).SetLoops(-1, LoopType.Yoyo));
        tweeners.Add(bones["RightArm"].tranBone.DOLocalRotate(new Vector3(-12.352f, -3.933f, -32.648f), t).SetLoops(-1, LoopType.Yoyo));
        tweeners.Add(bones["RightHand"].tranBone.DOLocalRotate(new Vector3(-6.354f, -10.169f, -0.953f), t).SetLoops(-1, LoopType.Yoyo));
    }

    private void OnChargeInit()
    {

    }

    private void OnJumpInit()
    {
        var t = 0.1f;
        tweeners.Add(bones["Head"].tranBone.DOLocalRotate(new Vector3(-5.581f, 0, 0), t).SetLoops(-1, LoopType.Yoyo));

        bones["Shoulder"].tranBone.localEulerAngles = new Vector3(-90, 0, -19.324f);
        tweeners.Add(bones["Shoulder"].tranBone.DOLocalRotate(new Vector3(-90, 0, -24.337f), t).SetLoops(-1, LoopType.Yoyo));

        bones["RightArm"].tranBone.localEulerAngles = new Vector3(14.84f, -3.044f, 0);
        tweeners.Add(bones["RightArm"].tranBone.DOLocalRotate(new Vector3(-18.941f, -4.13f, -32.625f), t).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo));

        bones["LeftArm"].tranBone.localEulerAngles = new Vector3(39.311f, -4.768f, 32.699f);
        tweeners.Add(bones["LeftArm"].tranBone.DOLocalRotate(new Vector3(-5.889f, -3.366f, 0), t).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo));

        //tweeners.Add(bones["RightHand"].tranBone.DOLocalRotate(new Vector3(-6.354f, -10.169f, -0.953f), t).SetLoops(-1, LoopType.Yoyo));

        bones["RightLeg"].tranBone.localEulerAngles = new Vector3(-131.984f, -83.736f, 129.321f);
        tweeners.Add(bones["RightLeg"].tranBone.DOLocalRotate(new Vector3(-96.533f, -50.09f, 94.26f), t).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo));

        bones["LeftLeg"].tranBone.localEulerAngles = new Vector3(-78.392f, 68.73f, -113.113f);
        tweeners.Add(bones["LeftLeg"].tranBone.DOLocalRotate(new Vector3(-45.441f, 84f, -129f), t).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo));

    }

    private void OnHitInit()
    {

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
    }
	
    private void OnJumpUpdate()
    {

    }

    private void OnHitUpdate()
    {

    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            animationState = GrandmaAnimationState.Idle;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            animationState = GrandmaAnimationState.Jump;


        if(sound != null){
            sound.chargeAmount = this.chargeAmount;
        }   

        switch (animationState)
        {
            case GrandmaAnimationState.Idle:
                OnIdleUpdate();
                break;
            case GrandmaAnimationState.Charge:
                OnChargeUpdate();
                break;
            case GrandmaAnimationState.Jump:
                OnJumpUpdate();
                break;
            case GrandmaAnimationState.Hit:
                OnHitUpdate();
                break;
        }
	}
}
