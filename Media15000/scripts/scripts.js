function playMusic(audio){
	document.getElementById(audio).play();
	document.getElementById(audio).loop = true;
}

function pauseMusic(audio){
	document.getElementById(audio).pause();
}

function changeRoom(id, room, extension){
	/*document.getElementById(id).style.backgroundImage = 'url(./'+room+extension;*/
	document.getElementById(id).src="../images/game images/"+room+extension;
}

function changePicture(elementId, folderName, fileName, fileExtension, textId, newText){
	document.getElementById(elementId).src=folderName+fileName+fileExtension;
	document.getElementById(textId).innerHTML=newText;
}

function readMouseMove(e){
	var result_x = document.getElementById('x_result');
	var result_y = document.getElementById('y_result');
	result_x.innerHTML = "x value:" + e.clientX;
	result_y.innerHTML = "y value:" + e.clientY;
}

function showVideo(){
	
}

/*var map1 = [
		{
			"item": "coin",
			"value": 50,
			"x": 0,
			"y": 0
		},
		{
			"item": "shoes",
			"x": 0,
			"y": 0
		}
	];

	for item in map1 {
		
	}*/