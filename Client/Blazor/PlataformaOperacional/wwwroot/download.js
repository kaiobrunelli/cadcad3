


window.downloadFile = function (fileName, byteArray) {
		
	const fileNameNovo = decodeURIComponent(fileName.replace(/\+/g, ' '));
	
	const blob = new Blob([byteArray]);
	const url = URL.createObjectURL(blob);
	const link = document.createElement("a");
	link.href = url;
	link.download = fileNameNovo;
	document.body.appendChild(link);
	link.click();
	document.body.removeChild(link);
	URL.revokeObjectURL(url);
};


//window.downloadFile = function (fileName, byteArray) {
//	const blob = new Blob([byteArray]);
//	const url = URL.createObjectURL(blob);
//	const link = document.createElement("a");
//	link.href = url;
//	link.download = fileName;
//	document.body.appendChild(link);
//	link.click();
//	document.body.removeChild(link);
//	URL.revokeObjectURL(url);
//};