using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorDeferredRemove
{
    public class AnimationHelper
    {
        private readonly Func<string, Task> _animationEndedCallback;
        private readonly ElementReference _element;

        public AnimationHelper(ElementReference element, Func<string, Task> animationEndedCallback)
        {
            _element = element;
            _animationEndedCallback = animationEndedCallback;
        }

        [JSInvokable]
        public async Task AnimationHasEnded(string name)
        {
            await  _animationEndedCallback(name);
        }
    }
}
