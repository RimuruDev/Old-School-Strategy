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
using UnityEngine;

public static class ObjectExceptionExtension
{
    public static void CheckNullArgumentException(this object target)
    {
        try
        {
            if (target == null)
                throw new ArgumentNullException();
        }
        catch (ArgumentNullException ex)
        {
            Debug.LogError(ex.StackTrace);
        }
    }
}