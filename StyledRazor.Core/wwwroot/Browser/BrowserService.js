export function WindowDimension() {
  return {
    width: window.innerWidth,
    height: window.innerHeight
  };
}

export function DimensionFrom(component) {
  return {
    width: component.offsetWidth,
    height: component.offsetHeight
  };
}

window.browserResize = () => {
  DotNet.invokeMethodAsync("StyledRazor.Core", 'OnBrowserResize').then(data => data);
}

window.addEventListener("resize", browserResize);