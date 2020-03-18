## Blazor Deferred Remove

Wait for CSS Transitions or Animations to complete, before removing your Blazor UI.
Allows you to achieve fadeout effects etc in your blazor applications.

[![Build Status](https://dev.azure.com/darrelltunnell/Public%20Projects/_apis/build/status/dazinator.BlazorDeferredRemove?branchName=master)](https://dev.azure.com/darrelltunnell/Public%20Projects/_build/latest?definitionId=8&branchName=master)

## Usage

1. Add the `BlazorDeferredRemove` nuget package to your blazor project: https://www.nuget.org/packages/BlazorDeferredRemove/

2. Include the following js script on your index.html page:

```html
    <script src="_content/BlazorDeferredRemove/BlazorDeferredRemove.js"></script>
```

3. In your blazor page / component you can now wrap your content in a `RemoveOnCssTransitionEnd` or `RemoveOnCssAnimationEnd` component, to have some UI that will be removed from the render tree only after a CSS property transition or CSS animation has completed respectively.

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

## Explaination

In the sample above, the content is wrapped in a `RemoveOnCssTransitionEnd` component. This component will stop rendering (omit from the RenderTree) its child content, once it detects that a CSS transition has been completed on the specified CSS property - in this case, thats the `visibility` propery:

```
<RemoveOnCssTransitionEnd CssPropertyName="visibility">
    <div class="@GetClassName()">
        <p>Hey there!</p>
        <button @onclick="()=>Done()"></button>
    </div>
</RemoveOnCssTransitionEnd>
```

Notice that the content is a `div` whose CSS class is provided by the `GetClassName()` method. This returns the `fadeout` css class once the button is clicked.

``` 

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

This means when you click the button, the CSS class `fadeout` is appended to the `div` - simple.
We then have some CSS for the `fadeout` class, that applies a CSS transition on the elements `visibility` property (and opacity but that doesn't matter for this sample):

```
.fadeout {
    visibility: hidden;
    opacity: 0;
    transition: visibility 0s 2s, opacity 2s linear;
}

```

This means that when you click the button, the css class is appended, which starts the CSS transition. The transition adjusts the visibility property to `hidden` after 2 seconds, and adjusts the `opacity` to 0 over the course of 2 seconds, linearly. This achieves our fadeout effect.

Once that CSS transition has completed, our `RemoveOnCssTransitionEnd` component is notified from the browser (interop) that the CSS transition has completed for a `visibility` property within it's child DOM content. It being a blazor component, it then stops including its child content in the render tree - which then removes the child content from the DOM.

Ofcourse, if you have common transition or animation effects - there is nothing to stop you from creating your own reusable components that leverage `RemoveOnCssTransitionEnd` and `RemoveOnCssAnimationEnd` components internally to achieve your goals.
