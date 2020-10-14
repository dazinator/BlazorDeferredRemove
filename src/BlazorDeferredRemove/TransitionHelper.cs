using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorDeferredRemove
{
    public class TransitionHelper
    {
        private readonly Func<string, Task> _transitionEndedCallback;
        private readonly ElementReference _element;

        public TransitionHelper(ElementReference element, Func<string, Task> transitionEndedCallback)
        {
            _element = element;
            _transitionEndedCallback = transitionEndedCallback;
        }

        [JSInvokable]
        public async Task TransitionHasEnded(string name)
        {
            await _transitionEndedCallback(name);
        }

        [JSInvokable]
        public async Task AnimationHasEnded(string name)
        {
            await _transitionEndedCallback(name);
        }
    }
}
