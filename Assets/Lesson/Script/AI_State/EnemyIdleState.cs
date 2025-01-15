using UnityEngine;

public partial class StateMachine
{
    // EnemyIdleState�� StateMachine Ŭ���� �������� ���� ������ Ŭ����
    public class EnemyIdleState : IState
    {
        private StateMachine stateMachine;

        public EnemyIdleState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered Idle State");
        }

        public void Update(GameObject obj)
        {
            // ���� �Ÿ� �ȿ� Player�� ���� ���� ����
            if(Input.GetKeyDown(KeyCode.W))
            {
                stateMachine.ChangeState(new EnemyWalkingState(stateMachine));
            }
        }

        public void Exit()
        {
            Debug.Log("Exit Idle State");
        }
        
    }
}



