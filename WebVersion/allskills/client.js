var matchId,
    intervalHandle;

function onCheckboxChange() {
    // id is of format `<t1|t2>-<ID of corresponding image>`
    var id = this.id,
	imgId = id.substr( 6 ),
	state = this.checked,
	data = {
	    match: matchId,
	    state: {
	    }
	};

    data.state[imgId] = state;
    $( '#' + imgId ).toggleClass( 'show' );
    $( '#' + imgId + '-g' ).toggleClass( 'show' );

    $.post( 'server.php', data, function ( res ) {
	$( '#sync-message' )
	    .text( 'Data sent successfully.' )
	    .fadeIn( 50 )
	    .delay( 3000 )
	    .fadeOut( 1000 );
    } );
}

function updateState( state ) {
    var ids = Object.keys( state );

    // We want to find everything that's currently shown EXCEPT
    // those images which are shown *by default* since we're going
    // to check these results agains the provided state and hide
    // any that don't match the server.
    $( '.show' ).not( '[id$="-g"]' ).each( function ( _, $el ) {
	if ( ids.indexOf( $el.id ) < 0 ) {
	    $( this ).removeClass( 'show' );
	    $( '#' + $el.id + '-g' ).addClass( 'show' );
	    $( '#check-' + $el.id ).prop( 'checked', false );
	}
    } );

    // Light up everything sent by the server, even if we just turned it off
    ids.forEach( function ( id ) {
	$( '#' + id ).addClass( 'show' );
	$( '#' + id + '-g' ).removeClass( 'show' );
	$( '#check-' + id ).prop( 'checked', true );
    } );
}

function linkMatch( e ) {
    if ( intervalHandle ) {
	clearInterval( intervalHandle );
	intervalHandle = undefined;
	$( 'form input[type="submit"]' ).val( 'Link' );
	return false;
    }

    matchId = $( '#match' ).val();
    
    $.get( 'server.php', { match: matchId }, function ( res ) {
	if ( Array.isArray( res ) && !res.length ) {
	    $( '#sync-message' )
		.text( 'Match not found. New match created.' )
		.css( 'color', 'black' )
		.fadeIn( 50 );

	    return;
	}
	updateState( res );
	intervalHandle = setInterval( function () {
	    $.get( 'server.php', { match: matchId }, function ( res ) {
		updateState( res );
	    } );
	}, 250 );

	$( 'form input[type="submit"]' ).val( 'End link' );
	$( '#sync-message' )
	    .text( 'Link established successfully. Now updating every 250ms.' )
	    .fadeIn( 50 )
	    .delay( 3000 )
	    .fadeOut( 1000 );
    } );

    e.preventDefault();
}

$( 'body' ).on( 'change', 'input[type="checkbox"]', onCheckboxChange );
$( '#matchForm' ).on( 'submit', linkMatch );
