export function addWindowEventListener(dotNetObjRef) {
    
    function blurHeader() {
        dotNetObjRef.invokeMethodAsync('BlurHeader');
    };
    
    window.addEventListener('scroll', blurHeader);
}