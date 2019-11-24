function onImageClick() {
    var name = this.id;
    //alert(name);
    if (name == "allskills") {
        window.location.href = name;
    } else if (name == "randomizer") {
        window.location.href = name;
    }
}
$('img').on('click', onImageClick);
