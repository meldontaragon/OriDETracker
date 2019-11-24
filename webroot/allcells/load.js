function linkViewer2( e ) {

    var link;

    link = 'view.html?tracker=' + $( '#match' ).val() + '&player=2';

    window.location.href = link;

    e.preventDefault();
}

function linkViewer1( e ) {

    var link;

    link = 'view.html?tracker=' + $( '#match' ).val() + '&player=1';

    window.location.href = link;

    e.preventDefault();
}

function linkEditor( e ) {

    var link;

    link = 'edit.html?tracker=' + $( '#match' ).val();

    window.location.href = link;

    e.preventDefault();
}


$( '#viewButton1' ).on( 'click', linkViewer1 );
$( '#viewButton2' ).on( 'click', linkViewer2 );
$( '#editButton' ).on( 'click', linkEditor );
