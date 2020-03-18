using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BlazorDeferredRemove
{
    public class AnimationHelper
    {
        private readonly Action<string> _animationEndedCallback;
        private readonly ElementReference _element;

        public AnimationHelper(ElementReference element, Action<string> animationEndedCallback)
        {
            _element = element;
            _animationEndedCallback = animationEndedCallback;
        }

        [JSInvokable]
        public void AnimationHasEnded(string name)
        {
            _animationEndedCallback(name);
        }
    }
}
