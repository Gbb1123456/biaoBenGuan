var FullScreen_WebGL = {

	UnityFullScreen : function()  {
			var isInFullScreen = (document.fullscreenElement && document.fullscreenElement !== null) ||
			(document.webkitFullscreenElement && document.webkitFullscreenElement !== null) ||
			(document.mozFullScreenElement && document.mozFullScreenElement !== null) ||
			(document.msFullscreenElement && document.msFullscreenElement !== null);
			
			var element = document.body.getElementsByClassName("webgl-content")[0];
			
			//兼容Unity2019
			var element1 = document.getElementById("#canvas");			
			element1.style.position="fixed";
			
			if (!isInFullScreen) {
				return (element.requestFullscreen ||
				element.webkitRequestFullscreen ||
				element.mozRequestFullScreen ||
				element.msRequestFullscreen).call(element);
			}
		},
		
		UnitySmallScreen : function()  {
			var isInFullScreen = (document.fullscreenElement && document.fullscreenElement !== null) ||
			(document.webkitFullscreenElement && document.webkitFullscreenElement !== null) ||
			(document.mozFullScreenElement && document.mozFullScreenElement !== null) ||
			(document.msFullscreenElement && document.msFullscreenElement !== null);
			
			var element = document.body.getElementsByClassName("webgl-content")[0];
			
			if (isInFullScreen) {
				if (document.exitFullscreen) {
					document.exitFullscreen();
				} else if (document.webkitExitFullscreen) {
					document.webkitExitFullscreen();
				} else if (document.mozCancelFullScreen) {
					document.mozCancelFullScreen();
				} else if (document.msExitFullscreen) {
					document.msExitFullscreen();
				}
			}
		}
};
mergeInto(LibraryManager.library, FullScreen_WebGL);