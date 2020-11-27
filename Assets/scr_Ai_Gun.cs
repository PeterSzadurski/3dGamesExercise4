using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Ai_Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject _Gun;
    private ParticleSystem _MuzzleFlash;
    private GameObject _MuzzleLight;
    private GameObject _Bullet;
    [SerializeField]
    private GameObject _ShootPoint;
    [SerializeField]
    private AudioClip _Sfx;
    private AudioSource _Audio;
    private bool _ShotFiring = false;

    void Start()
    {
        _Gun = transform.Find("Hips").gameObject.transform.Find("Spine").gameObject.transform
            .Find("Spine1").gameObject.transform
            .Find("Spine2").gameObject.transform.Find("RightShoulder")
            .gameObject.transform.Find("RightArm").gameObject.transform.Find("RightForeArm").gameObject.transform.Find
            ("RightHand").gameObject.transform.Find("PistolHand").gameObject;
        GameObject muzzleFlashObj = _Gun.transform.Find("MuzzleFlash").gameObject;
        _MuzzleFlash = muzzleFlashObj.GetComponent<ParticleSystem>();
        _MuzzleLight = muzzleFlashObj.transform.Find("Light").gameObject;
        _ShootPoint = _Gun.transform.Find("ShotOut").gameObject;
        _Bullet = _ShootPoint.transform.Find("Bullet").gameObject;
        _Audio = _ShootPoint.GetComponent<AudioSource>();

    }

    private IEnumerator FireShot()
    {
        _ShotFiring = true;
        _MuzzleLight.SetActive(true);
        _MuzzleFlash.Play(true);
        _Audio.PlayOneShot(_Sfx);
        GameObject bullet = Instantiate(_Bullet, _ShootPoint.transform);
        bullet.SetActive(true);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000);
        yield return new WaitForSeconds(0.1f);
        _ShotFiring = false;
        _MuzzleLight.SetActive(false);
        _MuzzleFlash.Stop(true);

        yield return null;
    }

    public bool Shoot()
    {
        if (!_ShotFiring)
        {
            StartCoroutine(FireShot());
        }
        return !_ShotFiring;
    }
}
