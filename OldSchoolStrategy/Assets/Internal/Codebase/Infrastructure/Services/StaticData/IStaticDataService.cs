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
using RimuruDev.Internal.Codebase.Runtime.Common.Curtain.Configs;

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IDisposable
    {
        public void Initialize();
        public CurtainConfig ForCurtain();
    }
}