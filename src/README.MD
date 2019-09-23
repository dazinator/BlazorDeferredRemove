## Blazor Deferred Remove

Wait for CSS Transitions or Animations to occur, before removing blazor render fragments fro the UI.

Allows you to have fadeout effects etc in your blazor application.


## Usage

1. Add the nuget package to your blazor project.

2. Add the following js to your page - these methods are required for interop:

```

<script type="text/javascript">
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
    </script>

```

3. In your blazor page / component you can now use the `RemoveOnCssTransitionEnd` or `RemoveOnCssAnimationEnd` components, to have some UI that will be removed from the render tree only after a CSS property transition or CSS animation has completed respectively.

## Example

The following example renders some UI that will be removed once a CSS property transition has completed for `visibility` property:

```

@page "/"

<RemoveOnCssTransitionEnd CssPropertyName="visibility">
    <div class="@GetClassName()">
        <p>Hey there!</p>
        <button @onclick="()=>Done()"></button>
    </div>
</RemoveOnCssTransitionEnd>

@code{

    [Parameter]
    public bool StartTransition { get; set; } = false;

    public void Done()
    {
        StartTransition = true;
    }

    public string GetClassName()
    {
        return StartTransition ? "fadeout" : "";
    }
}

```

And here is the CSS required for this example:

```
.fadeout {
    visibility: hidden;
    opacity: 0;
    transition: visibility 0s 2s, opacity 2s linear;
}

```