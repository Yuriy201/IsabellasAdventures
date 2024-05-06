using UnityEngine;
using Zenject;

public class EnemyStateMachine : MonoBehaviour
{
   private IEnemyStates _enemyStates;
   private EnemyStats _enemyStats;
   private GameObject _player;
   private float _patrolRadius;
   
   [Inject]
   private void Inject(GameObject player, EnemyStats enemyStats)
   {
      _player = player;
      _enemyStats = enemyStats;
   }
   
   private void Awake()
   {
      _enemyStates = GetComponent<IEnemyStates>();
      _patrolRadius = _enemyStats._patrolRadius;
   }

   private void FixedUpdate()
   {
      if (Vector3.Distance(transform.position, _player.transform.position) < _patrolRadius)
      {
         _enemyStates.Attack(_player);
      }
      else
      {
         _enemyStates.Walk();
      }
   }

   private void Update()
   {
      _enemyStates.Die();
   }
}
