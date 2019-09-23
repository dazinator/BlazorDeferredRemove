using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace BlazorDeferredRemove
{
    public static class TransitionCompleteHelper
    {        

        public static void RegisterForTransitionEnded(this ElementReference elementRef, IJSRuntime jsRuntime, Action<string> transitionEndedCallback)
        {

            //var tcs = new TaskCompletionSource<bool>();
            //cancellationToken.Register(() => tcs.TrySetCanceled());

            var helper = new TransitionHelper(elementRef, transitionEndedCallback);          

#pragma warning disable CS4014 // We don't need the result back so this is fire and forget.
            var dotnetRef = DotNetObjectReference.Create(helper);
            jsRuntime.InvokeAsync<object>("BlazorRegisterTransitionEnd", dotnetRef, elementRef);
#pragma warning restore CS4014

            //return tcs.Task;
        }

    }
}
