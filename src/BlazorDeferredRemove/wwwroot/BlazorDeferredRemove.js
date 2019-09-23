window.BlazorRegisterAnimationEnd = (dotnetHelper, element) => {
    var onAnimationEnded = function (args) {
        var name = args.animationName;
        dotnetHelper.invokeMethodAsync('AnimationHasEnded', name);
    };
    // Code for Chrome, Safari and Opera
    element.addEventListener("webkitAnimationEnd", onAnimationEnded);
    // Standard syntax
    element.addEventListener("animationend", onAnimationEnded);
};

window.BlazorRegisterTransitionEnd = (dotnetHelper, element) => {
    var onTransitionEnded = function (args) {
        var name = args.propertyName;
        dotnetHelper.invokeMethodAsync('TransitionHasEnded', name);
    };
    element.addEventListener("transitionend", onTransitionEnded);
};