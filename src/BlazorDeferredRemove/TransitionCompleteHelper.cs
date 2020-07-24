using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorDeferredRemove
{
    public static class TransitionCompleteHelper
    {       
        public static void RegisterForTransitionOrAnimationEnded(this ElementReference elementRef, IJSRuntime jsRuntime, Func<string, Task> onEndedCallback, string transitionOrAnimationName, bool isAnimation)
        {
            var helper = new TransitionHelper(elementRef, onEndedCallback);          

#pragma warning disable CS4014 // We don't need the result back so this is fire and forget.
            var dotnetRef = DotNetObjectReference.Create(helper);
            jsRuntime.InvokeAsync<object>("BlazorRegisterAnimationOrTransitionEnd", dotnetRef, elementRef, transitionOrAnimationName, isAnimation);
#pragma warning restore CS4014
        }

        public static void RegisterForTransitionEnded(this ElementReference elementRef, IJSRuntime jsRuntime, Func<string, Task> onEndedCallback, string transitionName)
        {
            RegisterForTransitionOrAnimationEnded(elementRef, jsRuntime, onEndedCallback, transitionName, false);
        }

        public static void RegisterForAnimationEnded(this ElementReference elementRef, IJSRuntime jsRuntime, Func<string, Task> onEndedCallback, string animationName)
        {
            RegisterForTransitionOrAnimationEnded(elementRef, jsRuntime, onEndedCallback, animationName, true);
        }
    }
}
