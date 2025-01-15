using UnityEngine;


public partial class StateMachine
{
    // EnemyWalkingState�� StateMachine Ŭ���� �������� ���� ������ Ŭ����
    public class EnemyAttackState : IState
    {
        private StateMachine stateMachine;

        public EnemyAttackState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered Attack State");
        }

        public void Update(GameObject obj)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                stateMachine.ChangeState(new EnemyWalkingState(stateMachine));
            }
        }

        public void Exit()
        {
            Debug.Log("Exit Attack State");
        }

    }
}
