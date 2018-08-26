function linkTracker( e ) {
    var type = e.originalEvent.explicitOriginalTarget.id;

    var link;

    if (type == 'viewButton') {
	link = 'view.html?tracker=' + $( '#match' ).val();
    } else if (type == 'editButton') {
	link = 'edit.html?tracker=' + $( '#match' ).val();
    }

    //window.location.assign(link);
    window.location.href = link;

    e.preventDefault();
}

$('#matchForm').on('submit', linkTracker );
//$( '#viewButton' ).on( 'click', linkTracker );
//$( '#editButton' ).on( 'click', linkTracker );
