function playMusic(audio){
	document.getElementById(audio).play();
	document.getElementById(audio).loop = true;
}

function pauseMusic(audio){
	document.getElementById(audio).pause();
}

function changeRoom(id, room, extension){
	/*document.getElementById(id).style.backgroundImage = 'url(./'+room+extension;*/
	document.getElementById(id).src="images/game images/"+room+extension;
}

function changePicture(id, name, extension, textId){
	document.getElementById(id).src="images/hw2 images/"+name+extension;
	document.getElementById(textId).innerHTML="These images are 800px by 800px.";
}

function changePicture2(id, name, extension, textId){
	document.getElementById(id).src="images/hw2 images/"+name+extension;
	document.getElementById(textId).innerHTML="This is the blurred image";
}

function readMouseMove(e){
	var result_x = document.getElementById('x_result');
	var result_y = document.getElementById('y_result');
	result_x.innerHTML = "x value:" + e.clientX;
	result_y.innerHTML = "y value:" + e.clientY;
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