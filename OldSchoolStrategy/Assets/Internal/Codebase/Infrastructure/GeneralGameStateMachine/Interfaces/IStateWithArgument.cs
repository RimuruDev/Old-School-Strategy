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

using System.Diagnostics.CodeAnalysis;

namespace RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces
{
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public interface IStateWithArgument<T>
    {
        public void Enter<TArgs>(TArgs args);
    }
}