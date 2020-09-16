function swalSuccess(result) {
    Swal.fire("შესრულდა", "", "success")
}

function swalError(xhr, textStatus, errorThrown) {
    let errors = [];
    for(key in xhr.responseJSON.errors)
        errors.push(xhr.responseJSON.errors[key]);
    
    Swal.fire("შეცდომა", errors.join("<br/>"), "error");
}

function getBase64(file) {
    return new Promise((resolve, reject) => {
        var reader = new FileReader();
        reader.onload = function() {
            resolve(btoa(reader.result));
        };
        reader.onerror = function() {
            Swal.fire("", "failed converting file to base64", "error")
        };
        
        reader.readAsBinaryString(file);
    });
}


function makeRequest(type, url, data, success, error, args) {
    Swal.enableLoading();
    return $.ajax({
        url: url,
        type: type,
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: success,
        error: error,
    }, args);
};
