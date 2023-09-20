// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;

namespace RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces
{
    public interface IInitGameStateMachine
    {
        public void Init(GameStateMachine stateMachine);
    }
}