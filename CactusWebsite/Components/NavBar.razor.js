export function getScrollYPosition() {
    return window.scrollY;
}

export function addWindowEventListener(dotNetObjRef) {
    
    function blurHeader() {
        dotNetObjRef.invokeMethodAsync('BlurHeader');
    };
    
    window.addEventListener('scroll', blurHeader);
}