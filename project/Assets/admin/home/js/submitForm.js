function login(form) {
    console.log(1, form["email"].value);
    HTTPPost('/login/verify', { email=form["email"].value, password=form["pass"].value }, result);
    return false;   
}
function result(res) {
    console.log(res);
}