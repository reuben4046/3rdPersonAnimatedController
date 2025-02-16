using UnityEngine;

public class Bow : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private GameObject arrow;

    Vector3 targetPoint;
    public Transform attackPoint;

    public AnimationCurve shootForceCurve;
    public float maxShootForce;
    float shootForce;

    float chargeAmmount = 0;


    public void ChargeBow()
    {
        chargeAmmount += Time.deltaTime;
        chargeAmmount = Mathf.Clamp01(chargeAmmount);
        Debug.Log(chargeAmmount);
    }

    public void CancelShot()
    {
        chargeAmmount = 0;
    }

    public void FireArrow()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
            Debug.Log("hitFound");
        } else 
        {
            targetPoint = ray.GetPoint(75);
            Debug.Log("hitNotFound");
        }
        Vector3 direction = targetPoint - attackPoint.position;

        GameObject currentArrow = Instantiate(this.arrow, attackPoint.position, Quaternion.identity);
        Debug.Log("arrowInstantiated");
        currentArrow.transform.LookAt(targetPoint);

        EvaluateShootForce();

        currentArrow.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        chargeAmmount = 0;
    }    
    
    void EvaluateShootForce()
    {
        shootForce = shootForceCurve.Evaluate(chargeAmmount) * maxShootForce;
    }
}

