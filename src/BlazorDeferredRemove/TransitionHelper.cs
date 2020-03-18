using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BlazorDeferredRemove
{
    public class TransitionHelper
    {
        private readonly Action<string> _transitionEndedCallback;
        private readonly ElementReference _element;

        public TransitionHelper(ElementReference element, Action<string> transitionEndedCallback)
        {
            _element = element;
            _transitionEndedCallback = transitionEndedCallback;
        }

        [JSInvokable]
        public void TransitionHasEnded(string name)
        {
            _transitionEndedCallback(name);
        }
    }
}
