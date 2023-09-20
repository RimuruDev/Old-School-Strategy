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

namespace RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces
{
    public interface IGameStateMachine
    {
        public void EnterState<TState>() where TState : IExitableState;

        public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState;
    }
}