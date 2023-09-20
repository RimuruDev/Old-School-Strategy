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

using System;
using Zenject;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.States;

namespace RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine
{
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public sealed class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> states;
        private IExitableState currentState;

        [Inject]
        public GameStateMachine(BootstrapState bootstrapState, MainMenuState mainMenuState)
        {
            states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(MainMenuState)] = mainMenuState,
            };
        }

        // TODO: Initialize in a different way! This "IInitGameStateMachine" is a bad solution.
        public void Init()
        {
            states[typeof(BootstrapState)].Init(this);
            states[typeof(MainMenuState)].Init(this);
        }

        public void EnterState<TState>() where TState : IExitableState
        {
            currentState?.Exit();

            var state = states[typeof(TState)];

            (state as IStateNext)?.Enter();

            currentState = state;
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState
        {
            currentState?.Exit();

            var state = states[typeof(TState)];

            (state as IStateWithArgument<TArgs>)?.Enter(args);

            currentState = state;
        }
    }
}