setInterval(function () {

    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
        }
    };
    xhttp.open("GET", "http://localhost:50240/api/movie/", true);
    xhttp.send();


}, 20);