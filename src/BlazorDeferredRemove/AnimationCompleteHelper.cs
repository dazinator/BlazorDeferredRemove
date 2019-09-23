using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace BlazorDeferredRemove
{
    public static class AnimationCompleteHelper
    {
        public static void RegisterForAnimationEnded(this ElementReference elementRef, IJSRuntime jsRuntime, Action<string> animationEndedCallback)
        {
            //var tcs = new TaskCompletionSource<bool>();
            //cancellationToken.Register(() => tcs.TrySetCanceled());
            var helper = new TransitionHelper(elementRef, animationEndedCallback);

#pragma warning disable CS4014 // We don't need the result back so this is fire and forget.
            var dotnetRef = DotNetObjectReference.Create(helper);
            jsRuntime.InvokeAsync<object>("BlazorRegisterAnimationEnd", dotnetRef, elementRef);
#pragma warning restore CS4014
        }

    }
}
