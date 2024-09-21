using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackByPlayer : MonoBehaviour
{

    private Animator anime;
    private MovementControlsPlayer movementControlsPlayer;
    [SerializeField] private float attackCoolDown;
    public float coolDownTimer = float.PositiveInfinity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && coolDownTimer > attackCoolDown && movementControlsPlayer.canAttack())
        {
            Attack();
        }
        coolDownTimer += Time.deltaTime;
    }

    private void Awake()
    {
        anime = GetComponent<Animator>();
        movementControlsPlayer = GetComponent<MovementControlsPlayer>();
    }

    public void Attack()
    {
        anime.SetTrigger("Attack");
        coolDownTimer = 0;
    }


}
