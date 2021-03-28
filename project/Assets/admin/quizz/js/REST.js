function HTTPGet(u, d, callback) {
	return $.getJSON(u, d, callback);
}
function HTTPPost(u, d, callback) {
	return $.post(u,d,callback(data,status,xhr),'json');
}
function HTTPPut(u, d, callback) {
	return $.ajax({
		url: u,
		type: 'PUT',
		contentType: 'application/json',
		data: d,
		success: callback(data)
	});
}
function HTTPDelete(u, d, callback) {
	return $.ajax({
		url: u,
		type: 'DELETE',
		dataType: 'json',
		data: d,
		success: callback(data,status,xhr)
	});
}