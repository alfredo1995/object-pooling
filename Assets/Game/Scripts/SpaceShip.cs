using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("Define o ponto de onde o tiro vai sair")]
    [SerializeField] private Transform shotPoint;
    
    [Header("Define o cooldown entre os tiros em segundos")]
    [SerializeField] private float cooldown;

    private ObjectPooling shotsPooling;
    private bool canShotAgain = true;
    
    // Start is called before the first frame update
    private void Start()
    {
        shotsPooling = GetComponent<ObjectPooling>();
        canShotAgain = true;
    }

    // Update is called once per frame
    private void Update()
    {
        GetShotInput();
    }

    private void GetShotInput()
    {
        if (canShotAgain && Input.GetKey(KeyCode.Space))
        {
            ShotFire();
            Invoke(nameof(ResetShot), cooldown);
        }
    }

    private void ShotFire()
    {
        canShotAgain = false;
        GameObject shot = shotsPooling.GetPooledObject();
        if (shot is null)
        {
            Debug.LogWarning("Sem tiros disponíveis");
            return;
        }
        shot.transform.position = shotPoint.position;
        shot.SetActive(true);
    }

    private void ResetShot()
    {
        canShotAgain = true;
    }
}
