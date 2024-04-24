using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoveState : State
{
    [SerializeField] private float _speed;

    private int _move;

    public Transform targetPlayer;
    public bool isFlipped = true;

    private void Update()
    {
        Move();
        LookAtPlayer();
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
    }

    private void Reflect(int move)
    {
        transform.localScale = new Vector3(move, 1, 1);
    }

    public void LookAtPlayer()
    {
        if (transform.position.x > targetPlayer.position.x)
        {
            //Reflect(-_move);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x < targetPlayer.position.x)
        {
            //Reflect(_move);
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
