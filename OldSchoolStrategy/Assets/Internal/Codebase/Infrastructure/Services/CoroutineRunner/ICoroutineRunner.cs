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

using UnityEngine;
using System.Collections;

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);

        public void StopAllCoroutines();

        public void StopCoroutine(Coroutine coroutine);
    }
}