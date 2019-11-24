function linkViewer( e ) {

    var link;

    link = 'view.html?tracker=' + $( '#match' ).val();

    window.location.href = link;

    e.preventDefault();
}

function linkEditor( e ) {

    var link;

    link = 'edit.html?tracker=' + $( '#match' ).val();

    window.location.href = link;

    e.preventDefault();
}


$( '#viewButton' ).on( 'click', linkViewer );
$( '#editButton' ).on( 'click', linkEditor );
