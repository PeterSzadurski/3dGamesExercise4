using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    #region Properties
    private Animator _Anim;
    [SerializeField]
    private NavMeshAgent _Agent;

    private float _DistanceFromTarget;
    private int _PatrolTargetIndex = 0;

    [SerializeField]
    private float _FleeRange = 8;
    [SerializeField]
    private float AttackRange = 0;
    [SerializeField]
    private float fireTimer = 10;
    [SerializeField]
    private float reloadTimer = 3;

    [SerializeField]
    private int _ammoCap = 7;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform[] _PatrolTargets;
    [SerializeField]
    private Transform[] _FleeTargets;

    [SerializeField]
    private GameObject _TargetObject;
    [SerializeField]
    private float _Speed;

    private Transform _DetectPoint;
    public Transform _PatrolTarget { get; set; }

    [SerializeField]
    private float _FieldOfView = 2;
    [SerializeField]
    private float _ViewDistance = 3;

    private scr_Ai_Gun _Gun;

    #endregion

    #region Getters

    public float GetReloadTime()
    {
        return reloadTimer;
    }
    public Transform GetPatrolTargel()
    {
        return _PatrolTarget;
    }
    public GameObject GetTargetObject()
    {
        return _TargetObject;
    }

    public float GetFireTimer()
    {
        return fireTimer;
    }
    #endregion
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Gun = GetComponent<scr_Ai_Gun>();
        _DetectPoint = transform.Find("DetectionPoint");
        //_Gun = transform.Find()
    }

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void Flee()
    {
        _Agent.SetDestination(_FleeTargets.Aggregate((x, y) => Vector3.Distance(transform.position, y.position) >
        Vector3.Distance(transform.position, y.position) ? x : y).position);
    }

    public bool OnTarget
    {
        get
        {
            if (_Anim) return _Anim.GetBool("onTarget");
            _Anim = GetComponent<Animator>();
            return _Anim.GetBool("onTarget");
        }
        set
        {
            if (_Anim) _Anim.SetBool("onTarget", value);
            _Anim = GetComponent<Animator>();
            _Anim.SetBool("onTarget", value);
        }
    }

    public void GetNextPatrolTarget()
    {
        _PatrolTargetIndex++;

        if (_PatrolTargetIndex >= _PatrolTargets.Length)
        {
            _PatrolTargetIndex = 0;
        }
        _PatrolTarget = _PatrolTargets[_PatrolTargetIndex];
        _Agent.SetDestination(_PatrolTarget.position);
        OnTarget = false;
    }

    public void Chase()
    {
        _Agent.SetDestination(_TargetObject.transform.position);
    }

    public void ClearNav()
    {
        _Agent.ResetPath();
    }

    public void MoveToTarget()
    {
        _Agent.SetDestination(_PatrolTarget.position);
    }

    public bool IsTargetObjectVisable()
    {
        RaycastHit hit;
        Vector3 rayDirection = _TargetObject.transform.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) < _FieldOfView)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, _ViewDistance))
            {
                return hit.collider.gameObject == _TargetObject;
            }

        }
        return false;
    }

    public bool InAttackRange()
    {
        return Vector3.Distance(transform.position, _TargetObject.transform.position) < AttackRange;
    }

    
    public void Fire()
    {
        _Gun.Shoot();
    }

    public bool Fled() { 
        return Vector3.Distance(_TargetObject.transform.position, transform.position) > _FleeRange;

    }

    public void Reload()
    {
        _Anim.SetInteger("ammo", _ammoCap);
    }
}

