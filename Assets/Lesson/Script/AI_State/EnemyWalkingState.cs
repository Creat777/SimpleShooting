using UnityEngine;

public partial class StateMachine
{
    // EnemyWalkingState�� StateMachine Ŭ���� �������� ���� ������ Ŭ����
    public class EnemyWalkingState : IState
    {
        private StateMachine stateMachine;

        public EnemyWalkingState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered Walking State");
        }

        public void Update(GameObject obj)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                stateMachine.ChangeState(new EnemyAttackState(stateMachine));
            }
            else if(Input.GetKeyUp(KeyCode.W))
            {
                stateMachine.ChangeState(new EnemyIdleState(stateMachine));
            }
        }

        public void Exit()
        {
            Debug.Log("Exit Walking State");
        }

    }
}
