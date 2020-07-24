window.BlazorRegisterAnimationOrTransitionEnd = (dotnetHelper, element, animationOrTransitionNameFilter, isAnimation) => {
    var shouldCallback = function (name) {
        if (animationOrTransitionNameFilter) {
            if (name === animationOrTransitionNameFilter) {
                return true;
            }
            return false;
        }
        return true;
    }

    var createOnEndedHandler = function (propname, invokeMethodName) {
        var onEnded = function (args) {
            var name = args[propname]
            if (shouldCallback(name)) {
                dotnetHelper.invokeMethodAsync(invokeMethodName, name);
            }
        }
        return onEnded;        
    };   
   
    if (isAnimation) {
        var onEnded = createOnEndedHandler('animationName', 'AnimationHasEnded');
        // Code for Chrome, Safari and Opera
        element.addEventListener("webkitAnimationEnd", onEnded);
        // Standard syntax
        element.addEventListener("animationend", onEnded);
    }
    else {
        var onEnded = createOnEndedHandler('propertyName', 'TransitionHasEnded');
        element.addEventListener("transitionend", onEnded);
    }  
};