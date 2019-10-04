function createBoard() {
	var myTableDiv = document.getElementById("board");
	var table = document.createElement('TABLE');
	table.border = '1';
	var tableBody = document.createElement('TBODY');
	table.appendChild(tableBody);

	var colorArray = ["Orange", "Red", "Green", "Blue"];	//makes code smaller
	var arrayOfNums= [];

	for(var z=0; z<44; z++){	//makes an array with numbers 0-43
		arrayOfNums.push(z);
	}

	for (var i = 0; i < 7; i++) {
		var tr = document.createElement('TR');
		tableBody.appendChild(tr);

		for (var j = 0; j < 7; j++) {
			var td = document.createElement('TD');
			td.width = '75';
			td.height = '50';

			if((i=="1" && (j =="1" || j =="5")) || (i=="5" && (j=="1" || j=="5"))){		//4 corners
				//code to make sure specific spots of the board are blank
			}
			else if(i=="3" && j=="3"){							//middle section
				td.appendChild(document.createTextNode("?"))					
				td.style.fontSize='250%';
			}
			else{		//each spaces
				var arrayIndex = Math.floor(Math.random()*arrayOfNums.length);	//uses the % of the numbers to make sure that at max, 11 colors will be displayed at one board.
				var color = arrayOfNums[arrayIndex] % 4;

				arrayOfNums[arrayIndex]=arrayOfNums[arrayOfNums.length-1];	//replaces used value with last value then deletes it to avoid repeats
				arrayOfNums.pop();

				td.appendChild(document.createTextNode(colorArray[color]));	//0 = black, 1 = red, 2=green, 3=blue
				td.style.color=colorArray[color];

				td.appendChild(document.createTextNode('\n' + "Cell " + i + "," + j));
			}

			tr.appendChild(td);
		}
	}
	myTableDiv.appendChild(table);
}

function generateHazards(amtEach,elementId){	//0 = orange, 1 = red, 2 = green, 3 = blue
	var arrayOfNums= [];
	var orange = "Orange hazards go on: ";
	var red = "Red hazards go on: ";
	var green = "Green hazards go on: ";
	var blue = "Blue hazards go on: ";
	var textString = "";
	var finalString = "";
	var row;
	var col;

	//8,12,24,36,40
	for(var z=0; z<49; z++){	//makes an array with numbers 0-43
		if(z==8 || z==12 || z==24 || z==36 || z==40){
			continue;
		}
		else{
			arrayOfNums.push(z);
		}
	}

	for(var i=0; i<4; i++){//each color
		for(var j=0; j<amtEach; j++){	//amt hazards each
			var arrayIndex = Math.floor(Math.random()*arrayOfNums.length);	//generate random tile
			var arrayIndexValue = arrayOfNums[arrayIndex];
			row = Math.floor(arrayIndexValue/7);
			col = arrayIndexValue%7;
			textString = "";
			textString = "(" + row + "," + col + ")";
			if(i==0){
				orange += textString;
			}
			else if(i==1){
				red += textString;
			}
			else if(i==2){
				green += textString;
			}
			else if(i==3){	
				blue += textString;
			}

			arrayOfNums[arrayIndex]=arrayOfNums[arrayOfNums.length-1];	//replaces used value with last value then deletes it to avoid repeats
			arrayOfNums.pop();
		}
	}
	finalString += "<li>" + orange + "</li>" + "<li>" + red + "</li>" + "<li>" + green + "</li>" + "<li>" + blue + "</li>";
	document.getElementById(elementId).innerHTML = finalString;
	//document.getElementById(elementId).innerHTML = orange + " " + red + " " + green + " " + blue;
}

function display(hideId,showId){
	document.getElementById(hideId).style.display = "none";
	document.getElementById(showId).style.display = "block";
}

function rollDice(faces, elementId){
	var land = Math.floor(Math.random()*faces)+1;
	document.getElementById(elementId).innerHTML = "The D" + faces + " landed on: " + land;
}